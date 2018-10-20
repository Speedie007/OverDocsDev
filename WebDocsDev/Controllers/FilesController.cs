using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebDocs.BusinessApplicationLayer.Query;
using PagedList;
using WebDocs.DomainModels.Database;

namespace WebDocs.Web.Controllers
{

    public class FilesController : Controller
    {


        // GET: Files
        [Authorize]
        public ActionResult DisplayPublicDocs(string sortOrder, string currentFilter, string searchString, int? page)
        {
            PublicDocumentsBusinessLogic PDBL = new PublicDocumentsBusinessLogic();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.FileIDSortParm = sortOrder == "FileID" ? "FileID_desc" : "FileID";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FullName_desc" : "";
            ViewBag.DateSortParm = sortOrder == "DateCreated" ? "DateCreated_desc" : "DateCreated";
            ViewBag.NameOfFileOwnerSortParm = sortOrder == "NameOfFileOwner" ? "NameOfFileOwner_desc" : "NameOfFileOwner";
            ViewBag.CurrentFileStatusSortParm = sortOrder == "CurrentFileStatus" ? "CurrentFileStatus_desc" : "CurrentFileStatus";
            ViewBag.CurrentFileShareStatusSortParm = sortOrder == "CurrentFileShareStatus" ? "CurrentFileShareStatus_desc" : "CurrentFileShareStatus";
            ViewBag.CurrentVersionNumberSortParm = sortOrder == "Version" ? "Version_desc" : "Version";
            ViewBag.FileSizeSortParm = sortOrder == "Size" ? "Size_desc" : "Size";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IList<FileModel> Rtn = PDBL.GetAllPublicFiles();


            switch (sortOrder)
            {
                case "FullFileName_desc":
                    Rtn = Rtn.OrderByDescending(s => s.FullName).ToList();
                    break;

                case "DateCreated":
                    Rtn = Rtn.OrderBy(s => s.Created).ToList();
                    break;
                case "DateCreated_desc":
                    Rtn = Rtn.OrderByDescending(s => s.Created).ToList();
                    break;

                case "NameOfFileOwner":
                    Rtn = Rtn.OrderBy(s => s.FileOwner.UserFullName).ToList();
                    break;
                case "NameOfFileOwner_desc":
                    Rtn = Rtn.OrderByDescending(s => s.FileOwner.UserFullName).ToList();
                    break;

                case "FileID":
                    Rtn = Rtn.OrderBy(s => s.FileID).ToList();
                    break;
                case "FileID_desc":
                    Rtn = Rtn.OrderByDescending(s => s.FileID).ToList();
                    break;


                case "FileSize":
                    Rtn = Rtn.OrderBy(s => s.Size).ToList();
                    break;
                case "FileSize_desc":
                    Rtn = Rtn.OrderByDescending(s => s.Size).ToList();
                    break;

                //case "CurrentFileStatus":
                //    AllUserFiles = AllUserFiles.OrderBy(s => s.CurrentFileStatus).ToList();
                //    break;
                //case "CurrentFileStatus_desc":
                //    AllUserFiles = AllUserFiles.OrderByDescending(s => s.CurrentFileStatus).ToList();
                //    break;

                //case "CurrentFileShareStatus":
                //    AllUserFiles = AllUserFiles.OrderBy(s => s.CurrentFileShareStatus).ToList();
                //    break;
                //case "CurrentFileShareStatus_desc":
                //    AllUserFiles = AllUserFiles.OrderByDescending(s => s.CurrentFileShareStatus).ToList();
                //    break;

                case "Version":
                    Rtn = Rtn.OrderBy(s => s.CurrentVersionNumber).ToList();
                    break;
                case "Version_desc":
                    Rtn = Rtn.OrderByDescending(s => s.CurrentVersionNumber).ToList();
                    break;
                default:  // Name ascending 
                    Rtn = Rtn.OrderBy(s => s.FullName).ToList();
                    break;
            };

            //ViewBag.CurrentFilter = "";
            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(Rtn.ToPagedList<FileModel>(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult DisplayUserDocs(string sortOrder, string currentFilter, string searchString, int? page)
        {
            UserDocumentsBussinessLogic UDBL = new UserDocumentsBussinessLogic();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            IList<FileModel> Rtn = UDBL.GetSelectedUserFiles(User.Identity.GetUserId<int>());


            //ViewBag.CurrentFilter = "";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Rtn.ToPagedList<FileModel>(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult DisplayPrivateFilesSharedWithUser()
        {
            PrivatelySharedDocumentsBusinessLogic PSDBL = new PrivatelySharedDocumentsBusinessLogic();

            return View(PSDBL.GetAllPersonalFilesSharedWithUser(User.Identity.GetUserId<int>()));
        }
        // public ActionResult Display
    }
}