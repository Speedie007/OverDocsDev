using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDocs.BusinessApplicationLayer.Query;
using PagedList;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.ViewModels.Files;
using WebDocs.DomainModels.TransactionResponse;
using System.Threading.Tasks;

namespace WebDocs.Web.Controllers
{

    public class FilesController : Controller
    {
        WebDocsBussinessLogic WDBL = new WebDocsBussinessLogic();
        // GET: Files
        [Authorize]
        [HttpGet]
        public ActionResult DisplayPublicDocs(string sortOrder, string currentFilter, string searchString, int? page)
        {
            WebDocsBussinessLogic WDBL = new WebDocsBussinessLogic();

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FullName_desc" : "";
            ViewBag.NameOfFileOwnerSortParm = sortOrder == "NameOfFileOwner" ? "NameOfFileOwner_desc" : "NameOfFileOwner";
            ViewBag.NameOfPersonLastUpdatedBySortParm = sortOrder == "NameOfPersonLastUpdatedBy" ? "NameOfPersonLastUpdatedBy_desc" : "NameOfPersonLastUpdatedBy";
            ViewBag.DateSortParm = sortOrder == "DateCreated" ? "DateCreated_desc" : "DateCreated";
            ViewBag.FileIDSortParm = sortOrder == "FileID" ? "FileID_desc" : "FileID";
            ViewBag.CurrentFileStatusSortParm = sortOrder == "CurrentFileStatus" ? "CurrentFileStatus_desc" : "CurrentFileStatus";
            ViewBag.CurrentFileShareStatusSortParm = sortOrder == "CurrentFileShareStatus" ? "CurrentFileShareStatus_desc" : "CurrentFileShareStatus";
            ViewBag.CurrentVersionNumberSortParm = sortOrder == "Version" ? "Version_desc" : "Version";
            ViewBag.FileSizeSortParm = sortOrder == "FileSize" ? "FileSize_desc" : "FileSize";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IList<FileModel> Rtn = Common.Helper.Sorting.FilesFieldSorting.SortFields(WDBL.GetAllPublicFiles(), sortOrder);

            //ViewBag.CurrentFilter = "";
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(Rtn.ToPagedList<FileModel>(pageNumber, pageSize));
        }

        [HttpPost]
        public PartialViewResult GetFileHistory(int FileID)
        {
            return PartialView("_FileHistoryPartialView", WDBL.GetFileArchivesByFileID(FileID));
        }

        [Authorize]
        public ActionResult DisplayUserDocs(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //UserDocumentsBussinessLogic UDBL = new UserDocumentsBussinessLogic();

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FullName_desc" : "";
            ViewBag.NameOfFileOwnerSortParm = sortOrder == "NameOfFileOwner" ? "NameOfFileOwner_desc" : "NameOfFileOwner";
            ViewBag.NameOfPersonLastUpdatedBySortParm = sortOrder == "NameOfPersonLastUpdatedBy" ? "NameOfPersonLastUpdatedBy_desc" : "NameOfPersonLastUpdatedBy";
            ViewBag.DateSortParm = sortOrder == "DateCreated" ? "DateCreated_desc" : "DateCreated";
            ViewBag.FileIDSortParm = sortOrder == "FileID" ? "FileID_desc" : "FileID";
            ViewBag.CurrentFileStatusSortParm = sortOrder == "CurrentFileStatus" ? "CurrentFileStatus_desc" : "CurrentFileStatus";
            ViewBag.CurrentFileShareStatusSortParm = sortOrder == "CurrentFileShareStatus" ? "CurrentFileShareStatus_desc" : "CurrentFileShareStatus";
            ViewBag.CurrentVersionNumberSortParm = sortOrder == "Version" ? "Version_desc" : "Version";
            ViewBag.FileSizeSortParm = sortOrder == "FileSize" ? "FileSize_desc" : "FileSize";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            IList<FileModel> Rtn = Common.Helper.Sorting.FilesFieldSorting.SortFields(WDBL.GetSelectedUserFiles(User.Identity.GetUserId<int>()), sortOrder);
            //ViewBag.CurrentFilter = "";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Rtn.ToPagedList<FileModel>(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult DisplayPrivateFilesSharedWithUser(string sortOrder, string currentFilter, string searchString, int? page)
        {
            

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FullName_desc" : "";
            ViewBag.NameOfFileOwnerSortParm = sortOrder == "NameOfFileOwner" ? "NameOfFileOwner_desc" : "NameOfFileOwner";
            ViewBag.NameOfPersonLastUpdatedBySortParm = sortOrder == "NameOfPersonLastUpdatedBy" ? "NameOfPersonLastUpdatedBy_desc" : "NameOfFileOwner";
            ViewBag.DateSortParm = sortOrder == "DateCreated" ? "DateCreated_desc" : "DateCreated";
            ViewBag.FileIDSortParm = sortOrder == "FileID" ? "FileID_desc" : "FileID";
            ViewBag.CurrentFileStatusSortParm = sortOrder == "CurrentFileStatus" ? "CurrentFileStatus_desc" : "CurrentFileStatus";
            ViewBag.CurrentFileShareStatusSortParm = sortOrder == "CurrentFileShareStatus" ? "CurrentFileShareStatus_desc" : "CurrentFileShareStatus";
            ViewBag.CurrentVersionNumberSortParm = sortOrder == "Version" ? "Version_desc" : "Version";
            ViewBag.FileSizeSortParm = sortOrder == "FileSize" ? "FileSize_desc" : "FileSize";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IList<FileModel> Rtn = Common.Helper.Sorting.FilesFieldSorting.SortFields(WDBL.GetAllPersonalFilesSharedWithUser(User.Identity.GetUserId<int>()), sortOrder);

            //ViewBag.CurrentFilter = "";
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(Rtn.ToPagedList<FileModel>(pageNumber, pageSize));
        }
        // public ActionResult Display

        [Authorize]
        public ActionResult DisplayUserUploadDocs()
        {

            // IList<FileModel> AllFilesDownloadeByThisUSer = RDBL.GetAllFilesThatAreDownloadedByUser(User.Identity.GetUserId<int>());
            IList<FileModel> Rtn = WDBL.GetAllFilesThatAreDownloadedByUser(User.Identity.GetUserId<int>());

            return View(Rtn);
        }

        [HttpPost]
        public ActionResult SaveNewUserFile(int FileShareStatusID)
        {

            int UserID = User.Identity.GetUserId<int>();

            List<FileUploadResponses> AllFileResponses = new List<FileUploadResponses>();

            foreach (string file in Request.Files)
            {
                //in the list of responses for each file indicates weather or not it has passed and the file name of each file that was uploaded.
                AllFileResponses.Add(WDBL.SaveUploadedUserFiles(new UploadingNewUserFileModel()
                {
                    FileShareStatusID = FileShareStatusID,
                    file = Request.Files[file],
                    IDOfCurrentUser = UserID
                }));
            }

            //in the list of responses for each file indicates weather or not it has passed and the file name of each file that was uploaded.
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(AllFileResponses), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DownLoadSelectedHistoricalFile(ProcessDownloadingOfSelectedFileModel PDOSFM)
        {
            FileModel FM = WDBL.DownloadHistoricalFile(PDOSFM);
            //FileModel FM = await Common.Helper.Files.DownloadHelper.DownloadFile(PDOSFM);
            if (!(FM is null))
            {
                return File(FM.FileBlob.FileImage, FM.ContentType, FM.FullFileName);
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception(), "Files", "DownLoadSelectedHistoricalFile"));
            }
        }

        public ActionResult SendUploadFileNotification(ProcessDownloadingOfSelectedFileModel PDOSFM)
        {
            CompletedTransactionResponses RTN =  WDBL.SendFileUploadNotification(PDOSFM);

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(RTN), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateCurrentFile(int FileIDToUpdate, int UserIDOfPersonThatUploadingTheFile)
        {
            int UserID = User.Identity.GetUserId<int>();

            List<FileUploadResponses> AllFileResponses = new List<FileUploadResponses>();

            foreach (string file in Request.Files)
            {
                //in the list of responses for each file indicates weather or not it has passed and the file name of each file that was uploaded.
                AllFileResponses.Add(WDBL.SaveUpdatedUserFiles(new UploadingUpdatedUserFileModel()
                {
                    FileID = FileIDToUpdate,
                    IDOfCurrentUserUpdatingTheFile = UserIDOfPersonThatUploadingTheFile,
                    file = Request.Files[file]
                }));
            }

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(AllFileResponses), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> DownLoadSelectedFile(ProcessDownloadingOfSelectedFileModel PDOSFM)//int FileID, int UserIDOfPersonThatDownloadedTheFile)
        {

            FileModel FM = await WDBL.DownloadSelectedFile(PDOSFM);
            //FileModel FM = await Common.Helper.Files.DownloadHelper.DownloadFile(PDOSFM);
            if (!(FM is null))
            {
                return File(FM.FileBlob.FileImage, FM.ContentType, FM.FullFileName);
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception(), "ContentManagement", "DownLoadSelectedFile"));
            }
        }

        public ActionResult UnlinkPrivteSharedFile(int UserIDPersonSharedWith, int FileID, string _sortOrder, string _currentFilter, int? _page)
        {
            CompletedTransactionResponses CTR = WDBL.UnlinkPrivatelySharedDocument(UserIDPersonSharedWith, FileID);
            if (CTR.WasSuccessfull)
            {
                return RedirectToAction("DisplayPrivateFilesSharedWithUser", new
                {
                    sortOrder = _sortOrder,
                    currentFilter = _currentFilter,
                    page = _page
                });
            }
            else
            {
                return View("Error", new HandleErrorInfo(new Exception(), "Files", "UnlinkPrivteSharedFile"));
            }

        }
    }
}