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
    public class NotificationsBusinessLogic : INotifications
    {
        private readonly INotificationRepository _NotificationsRepsoitory;
        private readonly IPrivateFilesSharedWithUsersRepository _PrivateFilesSharedWithUserRepository;
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
            _NotificationsRepsoitory = new NotificationRepsitory();
            _PrivateFilesSharedWithUserRepository = new PrivateFilesSharedWithUserRepository();
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

        public CompletedTransactionResponses RemoveNotification(RemoveNotificationViewModel RNVM)
        {
            WebDocs.DomainModels.Database.NotificationModel NM = new NotificationModel()
            {
                NotificationID = RNVM.NotificationID,
                EntityState = DomainModels.EntityState.Deleted
            };
            CTR = _NotificationsRepsoitory.Remove(NM);

            if (CTR.WasSuccessfull)
            {
                CTR.Message = CTR.Message + " - Notification Removed.";
            }
            return CTR;
        }

        public async Task<CompletedTransactionResponses> ProcessFileRequest(ProcessFileRequestNotificationViewModel PFRN)
        {
            /*Has the request already been sent
            if it has simply send the response indcating that the request has already been sent 
            if not then insert the requestin the data base and send message to the recipient.*/

            NotificationModel NM = _NotificationsRepsoitory.GetSingle(
                a => a.FileID == PFRN.FileID &&
                a.UserIDOfNotificationSender == PFRN.IDOFPersonLoggedOn &&
                a.UserIDOfNotificationRecipient == PFRN.IDOfTheFileOwner);

            if (NM is null)
            {
                NotificationModel newRequestNotification = new NotificationModel()
                {
                    FileID = PFRN.FileID,
                    UserIDOfNotificationRecipient = PFRN.IDOfTheFileOwner,
                    UserIDOfNotificationSender = PFRN.IDOFPersonLoggedOn,
                    DateCreated = DateTime.Now,
                    NotificationTypeID = (int)EnumNotificationTypes.Share_Request,
                    UserHasAcknowledgement = false,
                    EntityState = DomainModels.EntityState.Added
                };

                CTR = await _NotificationsRepsoitory.AsyncAdd(newRequestNotification);

                if (CTR.WasSuccessfull)
                {
                    //Send Message to Recipient - Must still implement..
                    CTR.Message = CTR.Message + " - Notification Sent.";
                }
            }
            else
            {
                /*Implement section where it sends a notification to recipient with new notification.
                 * 
                 * */
                CTR.Message = CTR.Message + " Recipient already been notified, how ever another request has been sent!";
                CTR.WasSuccessfull = true;
            }
            return CTR;
        }

        public async Task<CompletedTransactionResponses> ProcessAcceptedFileRequest(AcceptFileRequestNotifictionViewModel AFRN)
        {
            //Get Current Notification that was accepted.
            NotificationModel NM = _NotificationsRepsoitory.GetSingle(
               a => a.NotificationID == AFRN.NotificationID && a.UserHasAcknowledgement == false);

            //if Notification Item is returned - update the acknownlagement field and link the file with the person that requested it .
            if (!(NM is null))
            {
                NM.UserHasAcknowledgement = true;
                NM.EntityState = DomainModels.EntityState.Modified;

                CTR = await _NotificationsRepsoitory.AsyncUpdate(NM);
                if (CTR.WasSuccessfull)
                {
                    //Link the file to the user so that he can acces the private files.
                    PrivateFilesSharedWithUserModel PFSWUM = new PrivateFilesSharedWithUserModel()
                    {
                        FileID = NM.FileID,
                        UserIDPersonSharedWith = NM.UserIDOfNotificationSender,
                        DateShared = DateTime.Now,
                        EntityState = DomainModels.EntityState.Added
                    };
                    CTR = await _PrivateFilesSharedWithUserRepository.AsyncAdd(PFSWUM);
                    CTR.Message = CTR.Message + ": File Successfully shared with the requested user.";
                }
                else
                {
                    //Undo the changes that where sent to the database.
                    NM.UserHasAcknowledgement = false;
                    NM.EntityState = DomainModels.EntityState.Modified;
                    await _NotificationsRepsoitory.AsyncUpdate(NM);
                    CTR.Message = CTR.Message + ": No Files where share request failed to process.";
                }
            }
            else
            {
                CTR.WasSuccessfull = false;
                CTR.Message = CTR.Message + ": No Files where share request failed to process.";
            }

            return CTR;
        }
    }
}
