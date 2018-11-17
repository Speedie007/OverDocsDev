using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Notifications.Processing;

namespace WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections
{
    public interface INotifications : IBusinessLayer<NotificationModel>
    {
        IList<NotificationModel> GetCurrentUserNotifications(int UserID);
        CompletedTransactionResponses RemoveNotification(RemoveNotificationViewModel RNVM);
        Task<CompletedTransactionResponses> ProcessFileRequest(ProcessFileRequestNotificationViewModel PFRN);
        Task<CompletedTransactionResponses> ProcessAcceptedFileRequest(AcceptFileRequestNotifictionViewModel AFRN);
    }
}
