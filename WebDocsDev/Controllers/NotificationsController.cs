using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebDocs.BusinessApplicationLayer.Query;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels;
using WebDocs.DomainModels.ViewModels.Notifications;
using WebDocs.DomainModels.ViewModels.Notifications.Processing;
using PagedList;
using WebDocs.DomainModels.Database;


namespace WebDocs.Web.Controllers
{
    public class NotificationsController : Controller
    {
        // GET: Notificationst
        [HttpGet]
        [Authorize]
        public ActionResult DisplayUserNotifications(string sortOrder, string currentFilter, string searchString, int? page, int TabIndex = 0)
        {
            NotificationsBusinessLogic NBL = new NotificationsBusinessLogic();

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


            IList<NotificationModel> Rtn = NBL.GetCurrentUserNotifications(User.Identity.GetUserId<int>());

            //ViewBag.CurrentFilter = "";



            //var ThereIsGoing = Rtn.Where(a => a.UserHasAcknowledgement == false).ToPagedList<NotificationModel>(pageNumber, pageSize);
            //var ccc = Rtn.Where(a => a.UserHasAcknowledgement == true).ToPagedList<NotificationModel>(pageNumber, pageSize);

            return View(new NotificationsViewModel()
            {
                CurrentTabIndex = TabIndex,
                NewUserNotifications = Rtn.Where(a => a.UserHasAcknowledgement == false).ToList<NotificationModel>(),
                ArchivedUserNotifications = Rtn.Where(a => a.UserHasAcknowledgement == true).ToList<NotificationModel>()
            });
        }


        [HttpPost]
        [Authorize]
        public ActionResult DisplayUserNotifications(int _TabIndex = 0)
        {
            return RedirectToAction("DisplayUserNotifications", new { TabIndex = _TabIndex });
        }


        [HttpPost]
        public async Task<ActionResult> ProcessRequestNotification(ProcessFileRequestNotificationViewModel PFRN)
        {
            NotificationsBusinessLogic NBL = new NotificationsBusinessLogic();
            CompletedTransactionResponses rtnSuccessMessage = await NBL.ProcessFileRequest(PFRN);
            return Json(rtnSuccessMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AcceptFileRequestNotification(AcceptFileRequestNotifictionViewModel AFRN)
        {
            NotificationsBusinessLogic NBL = new NotificationsBusinessLogic();
            //await NBL.ProcessAcceptedFileRequest(AFRN);
            return Json(await NBL.ProcessAcceptedFileRequest(AFRN), JsonRequestBehavior.AllowGet);
            // return RedirectToAction("DisplayUserNotifications", new { TabIndex = AFRN.PageIndex });
        }

        public ActionResult DeleteNotification(int NotifictionID, int _TabIndex)
        {
            NotificationsBusinessLogic NBL = new NotificationsBusinessLogic();

            NBL.RemoveNotification(new RemoveNotificationViewModel()
            {
                NotificationID = NotifictionID
            });

            return RedirectToAction("DisplayUserNotifications", new { TabIndex = _TabIndex });

        }
    }
}