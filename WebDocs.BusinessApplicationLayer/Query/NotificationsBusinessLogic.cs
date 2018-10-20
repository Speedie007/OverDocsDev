using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class NotificationsBusinessLogic : INotifications
    {
        private readonly INotificationRepository _NotificationsRepsoitory;

        public NotificationsBusinessLogic()
        {
            _NotificationsRepsoitory = new NotificationRepsitory();
        }
        public IList<NotificationModel> GetCurrentUserNotifications(int UserID)
        {
            IList<NotificationModel> Rtn = _NotificationsRepsoitory.GetList(
                a => a.UserIDOfNotificationRecipient == UserID, //where
                a => a.File,//Include
                a => a.RecipientUsers,//Include
                a => a.SendingUsers,//Include
                a => a.NotificationTypes//Include
                );


            return Rtn;
        }
    }
}
