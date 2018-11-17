using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.Common.Enum.DbLookupTables;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels;
using WebDocs.DomainModels.ViewModels.Notifications.Processing;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class NotificationsBusinessLogic 
    {
       
       

        private readonly IEmailCacheRepository _EmailCacheRepsoitory;


        private CompletedTransactionResponses _CTR;

        public CompletedTransactionResponses CTR
        {
            get
            {
                if (_CTR is null)
                {
                    _CTR = new CompletedTransactionResponses()
                    {
                        Message = "",
                        TransActionType = TransactionType.NoneExecuted,
                        WasSuccessfull = false
                    };
                    return _CTR;
                }
                else
                {
                    return _CTR;
                }
            }
            set
            {
                _CTR = value;
            }
        }


        public NotificationsBusinessLogic()
        {
            
            
            _EmailCacheRepsoitory = new EmailCacheRepository();
        }

        //public IList<NotificationModel> GetCurrentUserNotifications(int UserID)
        //{
            
        //}

        //public CompletedTransactionResponses RemoveNotification(RemoveNotificationViewModel RNVM)
        //{
           
        //}

        //public async Task<CompletedTransactionResponses> ProcessFileRequest(ProcessFileRequestNotificationViewModel PFRN)
        //{
           
        //}

        //public async Task<CompletedTransactionResponses> ProcessAcceptedFileRequest(AcceptFileRequestNotifictionViewModel AFRN)
        //{
           
        //}
    }
}
