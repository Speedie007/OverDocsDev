using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;
using PagedList;

namespace WebDocs.DomainModels.ViewModels.Notifications
{
    public class NotificationsViewModel
    {
        //public IPagedList<NotificationModel> NewUserNotifications { get; set; }
        //public IPagedList<NotificationModel> ArchivedUserNotifications { get; set; }
        public List<NotificationModel> NewUserNotifications { get; set; }
        public List<NotificationModel> ArchivedUserNotifications { get; set; }
        public int CurrentTabIndex { get; set; }
    }
}
