using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;

namespace WebDocs.DomainModels.ViewModels
{
    public class NotificationsViewModel
    {
        public IList<NotificationModel> UserNotifications { get; set; }
        public int CurrentTabIndex { get; set; }
    }
}
