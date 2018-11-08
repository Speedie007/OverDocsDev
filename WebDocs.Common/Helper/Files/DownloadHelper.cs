using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.Common.Helper.Files
{
    public static class DownloadHelper
    {

        public static async Task<FileModel> DownloadFile(ProcessDownloadingOfSelectedFileModel PDOSFM)
        {
            FileRepository FR = new FileRepository();
            FileModel FM = FR.GetSingle(
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
                FM.FileLookupStatusID = (int)Common.Enum.DbLookupTables.EnumFileViewStatuses.Locked;
                FM.EntityState = DomainModels.EntityState.Modified;

                CompletedTransactionResponses CTR = await FR.AsyncUpdate(FM);
                if (CTR.WasSuccessfull)
                {
                    return FM;// File(FM.FileBlob.FileImage, FM.ContentType, FM.FullFileName);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return FM;
            }
        }
    }
}
