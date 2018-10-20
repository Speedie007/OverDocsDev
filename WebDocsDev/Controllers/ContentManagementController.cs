using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDocs.BusinessApplicationLayer.Query;
using WebDocs.Common.Enum.DbLookupTables;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.Web.Controllers
{
    public class ContentManagementController : Controller
    {


        [HttpPost] // Fetches the userfile from the database that the user uloaded and passes it through the controller
        public JsonResult UserFileUpload(int _FileShareStatusID)
        {

            List<FileModel> AllFilesToBeSaved = new List<FileModel>();
            List<FileUploadResponses> UploadResponse;
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        byte[] uploadedFile = new byte[fileContent.InputStream.Length];
                        fileContent.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                        FileModel NewFileUpload = new FileModel()
                        {
                            FileBlob = new FileBlobModel()
                            {
                                EntityState = DomainModels.EntityState.Added,
                                FileImage = uploadedFile
                            },
                            ContentType = fileContent.ContentType,
                            CurrentVersionNumber = 1,
                            Created = DateTime.Now,

                            Name = Path.GetFileNameWithoutExtension(fileContent.FileName),
                            Size = fileContent.ContentLength,
                            UserIDOfFileOwner = User.Identity.GetUserId<int>(),
                            UserIDOfLastUploaded = User.Identity.GetUserId<int>(),
                            FileLookupStatusID = (int)EnumFileViewStatuses.Available,
                            FileShareStatusID = _FileShareStatusID,
                            Extension = Path.GetExtension(fileContent.FileName).Replace(".", ""),
                            EntityState = DomainModels.EntityState.Added
                        };
                        AllFilesToBeSaved.Add(NewFileUpload);
                    }
                }
                UploadResponse = Common.Helper.Files.UploadHelper.SaveUploadedUserFiles(AllFilesToBeSaved);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(
                    new List<FileUploadResponses>() { new FileUploadResponses() {
                            FileName ="None",
                            Message ="Failed To Upload any files : Error - " + ex.Message,
                            WasSuccessfull = false
                    } }), JsonRequestBehavior.AllowGet);
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(UploadResponse), JsonRequestBehavior.AllowGet);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(UploadResponse); ;// Json("File uploaded successfully");
        }
    }
}