using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDocs.BusinessApplicationLayer.Query;
using WebDocs.DomainModels.ViewModels;

namespace WebDocs.Web.Controllers
{
    public class NotificationsController : Controller
    {
        // GET: Notificationst
        [HttpGet]
        public ActionResult DisplayUserNotifications(int TabIndex = 0)
        {

            NotificationsBusinessLogic NBL = new NotificationsBusinessLogic();

            return View(new NotificationsViewModel()
            {
                CurrentTabIndex = TabIndex,
                UserNotifications = NBL.GetCurrentUserNotifications(User.Identity.GetUserId<int>())
            });

        }
    }
}