using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;

namespace WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections
{
    public interface INotifications : IBusinessLayer<NotificationModel>
    {
        IList<NotificationModel> GetCurrentUserNotifications(int UserID);
    }
}
