using System;
using System.Collections.Generic;
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
    public class PublicDocumentsBusinessLogic : IPublicDocuments
    {

        private readonly IFileRepository _FileRepsoitory;

        public PublicDocumentsBusinessLogic()
        {
            _FileRepsoitory = new FileRepository();
        }

        public CompletedTransactionResponses CTR { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IList<FileModel> GetAllPublicFiles()
        {
            IList<FileModel> GetAllPublicFiles = _FileRepsoitory.GetAll(
                //Model Must Include the following items in the Graph
                a => a.FileOwner,
                a => a.PersonThatLastUpdatedFile,
                a => a.FileViewStatuses,
                a => a.FileShareStatues,
                a => a.PrivateFilesSharedWithUsers,
                a => a.UserThatDownloadedFile.PersonTahtDownloadedTheFile
                 //end
                 ).Where(a => a.FileShareStatusID != (int)WebDocs.Common.Enum.DbLookupTables.EnumFileShareStatues.Hidden).ToList();
            return GetAllPublicFiles;
        }

        public async Task<FileModel> DownloadSelectedFile(ProcessDownloadingOfSelectedFileModel PDOSFM)
        {
            //Incomplete must updtea the CompletedTransactionResponses to include the data return.

            FileModel FM = _FileRepsoitory.GetSingle(
                                        //Where    
                                        a => a.FileID == PDOSFM.FileID,
                                             //Include the following in the object graph.
                                             a => a.FileBlob,
                                              a => a.UserThatDownloadedFile);
            /* Lock the file that is being downloaded and link the person that downloadedd the file to it.
             * this is done by inserting a record into the UserThatDownloadedFile table.
             * This links the user to the file that was downloaded and set the file status from avaiable to Locked in the Files table.
             * */
            if (FM.UserThatDownloadedFile is null)
            {
                FM.UserThatDownloadedFile = new UserThatDownloadedFileModel()
                {
                    FileID = PDOSFM.FileID,
                    UserIDThatDownloadedFIle = PDOSFM.UserIDOfPersonThatDownloadedTheFile,
                    DateDownloaded = DateTime.Now,
                    HasFileBeenReturned = false,
                    EntityState = DomainModels.EntityState.Added
                };
                FM.FileLookupStatusID = (int)WebDocs.Common.Enum.DbLookupTables.EnumFileViewStatuses.Locked;
                FM.EntityState = DomainModels.EntityState.Modified;

                CompletedTransactionResponses CTR = await _FileRepsoitory.AsyncUpdate(FM);
                if (CTR.WasSuccessfull)
                {
                    return FM;
                }
                else
                {
                    return FM;
                    // return View("Error", new HandleErrorInfo(new Exception(), "ContentManagement", "DownLoadSelectedFile"));
                }
            }
            else
            {
                return FM;
            }
        }
    }
}
