using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class ReturnDocumentsBussinessLogic : IReturnDocuments
    {

        private readonly IUserThatDownloadedFileRepository _UserThatDownloadedFileRepository;
        private readonly IFileRepository _FileRepsoitory;

        public ReturnDocumentsBussinessLogic()
        {
            _UserThatDownloadedFileRepository = new UserThatDownloadedFileRepository();
            _FileRepsoitory = new FileRepository();
        }

        public CompletedTransactionResponses CTR { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IList<FileModel> GetAllFilesThatAreDownloaded()
        {
            IList<FileModel> AllFilesThatUserHasDownloaded = _UserThatDownloadedFileRepository.GetAll(
              //Model Must Include the following items in the Graph
              a => a.File,
              a => a.File.FileOwner,
              a => a.File.PersonThatLastUpdatedFile,
              a => a.File.FileShareStatues,
              a => a.File.FileViewStatuses
               //end
               ).Select(b => b.File).ToList();
            return AllFilesThatUserHasDownloaded;
        }

        public IList<FileModel> GetAllFilesThatAreDownloadedByUser(int UserID)
        {
            IList<FileModel> AllFilesThatUserHasDownloaded = _UserThatDownloadedFileRepository.GetList(
                 a => a.UserIDThatDownloadedFIle == UserID,
               //Model Must Include the following items in the Graph
               a => a.File,
               a => a.File.FileOwner,
               a => a.File.PersonThatLastUpdatedFile,
               a => a.File.FileShareStatues,
               a => a.File.FileViewStatuses
                //end
                ).Select(b => b.File).ToList();
            return AllFilesThatUserHasDownloaded;
        }

        public FileUploadResponses SaveUpdatedUserFiles(UploadingUpdatedUserFileModel UUUFM)
        {
            FileUploadResponses FUR = null;
            //Checks to verify that teh file is vaild before processing the uploaded file.
            if (UUUFM.file != null && UUUFM.file.ContentLength > 0)
            {

                //Get current File version.
                FileModel FM = _FileRepsoitory.GetSingle(a => a.FileID == UUUFM.FileID,
                    a => a.FileBlob,
                    a => a.UserThatDownloadedFile);

                //Add the current version to the achive.
                FileArchiveModel FAM = new FileArchiveModel()
                {
                    FileArchiveBlob = new FileArchiveBlobModel()
                    {
                        FileImage = FM.FileBlob.FileImage,
                        EntityState = DomainModels.EntityState.Added
                    },
                    FileID = FM.FileID,
                    ContentType = FM.ContentType,
                    Name = FM.Name,
                    Size = FM.Size,
                    Version = FM.CurrentVersionNumber,
                    DateCreated = FM.Created,
                    DateArchived = DateTime.Now,
                     Extension = FM.Extension,
                    UserIDOfLastUploaded = FM.UserIDOfLastUploaded,

                    EntityState = DomainModels.EntityState.Added
                };
                FUR = Common.Helper.Files.UploadHelper.SaveArchivedFiles(FAM);

                //If the file was successfully added to the archive then update the current file with the current file deatils that was uploaded.
                if (FUR.WasSuccessfull)
                {
                    byte[] uploadedFile = new byte[UUUFM.file.InputStream.Length];
                    UUUFM.file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                    //update the current file deatils with tat of the new upadted file that was submitted blyu the user.
                    FM.CurrentVersionNumber = FM.CurrentVersionNumber + 1;
                    FM.Size = UUUFM.file.ContentLength;
                    FM.Extension = Path.GetExtension(UUUFM.file.FileName).Replace(".", "");
                    FM.Name = Path.GetFileNameWithoutExtension(UUUFM.file.FileName);
                    FM.ContentType = UUUFM.file.ContentType;
                    FM.FileLookupStatusID = (int)Common.Enum.DbLookupTables.EnumFileViewStatuses.Available;
                    FM.UserIDOfLastUploaded = UUUFM.IDOfCurrentUserUpdatingTheFile;
                    FM.FileBlob.FileImage = uploadedFile;

                    FM.EntityState = DomainModels.EntityState.Modified;
                    FM.FileBlob.EntityState = DomainModels.EntityState.Modified;
                    FM.UserThatDownloadedFile.EntityState = DomainModels.EntityState.Deleted;

                    FUR = Common.Helper.Files.UploadHelper.UpdateUploadedFiles(FM);

                }
                else
                {
                    //uploaded file not valid return error message.
                    FUR.Message = "Error with file that that was uploaded, unable to archive the current file and update the current with the file that was unloaded!! ";
                    FUR.FileName = UUUFM.file.FileName;
                    FUR.WasSuccessfull = false;

                }
            }
            else
            {
                //uploaded file not valid return error message.
                FUR.Message = "Error with file that that was uploaded, ither its empty or not unloaded correctly, please try again!! ";
                FUR.FileName = UUUFM.file.FileName;
                FUR.WasSuccessfull = false;
            }
            return FUR;
        }
    }
}
