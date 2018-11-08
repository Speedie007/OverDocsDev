using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Json;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.Common.Enum.DbLookupTables;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class UserDocumentsBussinessLogic : IUserDocuments
    {

        private readonly IFileRepository _FileRepsoitory;

        public CompletedTransactionResponses CTR { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UserDocumentsBussinessLogic()
        {
            _FileRepsoitory = new FileRepository();
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UNUFM"></param>
        /// <returns></returns>
        public FileUploadResponses SaveUploadedUserFiles(UploadingNewUserFileModel UNUFM)//List<FileModel> CurrentFiles)
        {

            if (UNUFM.file != null && UNUFM.file.ContentLength > 0)
            {
                byte[] uploadedFile = new byte[UNUFM.file.InputStream.Length];
                UNUFM.file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                FileModel NewFileUpload = new FileModel()
                {
                    FileBlob = new FileBlobModel()
                    {
                        EntityState = DomainModels.EntityState.Added,
                        FileImage = uploadedFile
                    },
                    ContentType = UNUFM.file.ContentType,
                    CurrentVersionNumber = 1,
                    Created = DateTime.Now,

                    Name = Path.GetFileNameWithoutExtension(UNUFM.file.FileName),
                    Size = UNUFM.file.ContentLength,
                    UserIDOfFileOwner = UNUFM.IDOfCurrentUser,
                    UserIDOfLastUploaded = UNUFM.IDOfCurrentUser,
                    FileLookupStatusID = (int)EnumFileViewStatuses.Available,
                    FileShareStatusID = UNUFM.FileShareStatusID,
                    Extension = Path.GetExtension(UNUFM.file.FileName).Replace(".", ""),
                    EntityState = DomainModels.EntityState.Added
                };

                return WebDocs.Common.Helper.Files.UploadHelper.SaveUploadedFile(NewFileUpload);
            }
            else
            {
                                     
                return new FileUploadResponses()
                {
                    FileName = UNUFM.file.FileName,
                    Message = "Failed To Upload - Error :  file is Null or Not uploaded correctly.",
                    WasSuccessfull = false
                };
            }




        }

        public IList<FileModel> GetSelectedUserFiles(int UserID)
        {
            IList<FileModel> SelectedUserFiles = _FileRepsoitory.GetList(
                a => a.UserIDOfFileOwner == UserID,
                a => a.FileOwner,
                a => a.PersonThatLastUpdatedFile,
                a => a.FileViewStatuses,
                a => a.FileShareStatues,
                a => a.UserThatDownloadedFile);

            return SelectedUserFiles;
        }
    }
}
