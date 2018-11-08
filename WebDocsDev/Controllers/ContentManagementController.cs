using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebDocs.BusinessApplicationLayer.Query;
using WebDocs.Common.Enum.DbLookupTables;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.Web.Controllers
{
    public class ContentManagementController : Controller
    {


        [HttpPost] // Fetches the userfile from the database that the user uloaded and passes it through the controller
        public JsonResult UserFileUpload(int _FileShareStatusID)
        {

            //List<FileModel> AllFilesToBeSaved = new List<FileModel>();
            //List<FileUploadResponses> UploadResponse;
            //try
            //{
            //    foreach (string file in Request.Files)
            //    {
            //        HttpPostedFileBase fileContent = Request.Files[file];
            //        if (fileContent != null && fileContent.ContentLength > 0)
            //        {
            //            byte[] uploadedFile = new byte[fileContent.InputStream.Length];
            //            fileContent.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            //            FileModel NewFileUpload = new FileModel()
            //            {
            //                FileBlob = new FileBlobModel()
            //                {
            //                    EntityState = DomainModels.EntityState.Added,
            //                    FileImage = uploadedFile
            //                },
            //                ContentType = fileContent.ContentType,
            //                CurrentVersionNumber = 1,
            //                Created = DateTime.Now,

            //                Name = Path.GetFileNameWithoutExtension(fileContent.FileName),
            //                Size = fileContent.ContentLength,
            //                UserIDOfFileOwner = User.Identity.GetUserId<int>(),
            //                UserIDOfLastUploaded = User.Identity.GetUserId<int>(),
            //                FileLookupStatusID = (int)EnumFileViewStatuses.Available,
            //                FileShareStatusID = _FileShareStatusID,
            //                Extension = Path.GetExtension(fileContent.FileName).Replace(".", ""),
            //                EntityState = DomainModels.EntityState.Added
            //            };
            //            AllFilesToBeSaved.Add(NewFileUpload);
            //        }
            //    }
            //    UploadResponse = Common.Helper.Files.UploadHelper.SaveUploadedFiles(AllFilesToBeSaved);
            //}
            //catch (Exception ex)
            //{
            //    Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(
            //        new List<FileUploadResponses>() { new FileUploadResponses() {
            //                FileName ="None",
            //                Message ="Failed To Upload any files : Error - " + ex.Message,
            //                WasSuccessfull = false
            //        } }), JsonRequestBehavior.AllowGet);
            //}
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject("{Message = Not Implemented!!}"), JsonRequestBehavior.AllowGet);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(UploadResponse); ;// Json("File uploaded successfully");
        }


        [HttpPost]
        public async Task<ActionResult> DownLoadSelectedFile(ProcessDownloadingOfSelectedFileModel PDOSFM)//int FileID, int UserIDOfPersonThatDownloadedTheFile)
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
                    return File(FM.FileBlob.FileImage, FM.ContentType, FM.FullFileName);
                }
                else
                {
                    return View("Error", new HandleErrorInfo(new Exception(), "ContentManagement", "DownLoadSelectedFile"));
                }
            }
            else
            {
                return File(FM.FileBlob.FileImage, FM.ContentType, FM.FullFileName);
            }
        }
    }
}