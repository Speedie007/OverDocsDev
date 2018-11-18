using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.Common.Enum.DbLookupTables;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;
using WebDocs.DomainModels.ViewModels.Notifications.Processing;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public partial class WebDocsBussinessLogic :
        IUserDocuments,
        IReturnDocuments,
        IPublicDocuments,
        INotifications,
        IEmailCaches,
        IFileArchives,
        IPrivatelySharedDocuments
    {

        private readonly IFileRepository _FileRepsoitory;
        private readonly IFileArchiveRepository _FileArchiveRepsoitory;
        private readonly INotificationRepository _NotificationsRepsoitory;
        private readonly IPrivateFilesSharedWithUsersRepository _PrivateFilesSharedWithUserRepository;
        private readonly IUserThatDownloadedFileRepository _UserThatDownloadedFileRepository;
        private readonly IEmailCacheRepository _EmailCacheRepsoitory;
        private readonly ISystemTransactionRepository _SystemTransactionRepository;
        private readonly IUsersRepository _UserRepository;


        private CompletedTransactionResponses _CTR;

        public WebDocsBussinessLogic()
        {
            _FileRepsoitory = new FileRepository();
            _FileArchiveRepsoitory = new FileArchiveRepository();
            _NotificationsRepsoitory = new NotificationRepsitory();
            _PrivateFilesSharedWithUserRepository = new PrivateFilesSharedWithUserRepository();
            _UserThatDownloadedFileRepository = new UserThatDownloadedFileRepository();
            _EmailCacheRepsoitory = new EmailCacheRepository();
            _SystemTransactionRepository = new SystemTransctionRepository();
            _UserRepository = new UserRepsoitory();
        }

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

        public async Task<FileModel> DownloadSelectedFile(ProcessDownloadingOfSelectedFileModel PDOSFM)
        {
            //Incomplete must updtea the CompletedTransactionResponses to include the data return.

            FileModel FM = _FileRepsoitory.GetSingle(
                                        //Where    
                                        a => a.FileID == PDOSFM.FileID,
                                             //Include the following in the object graph.
                                             a => a.FileBlob,
                                              a => a.UserThatDownloadedFile);
            /* Lock the file that is being downloaded and link the person that downloadedd the file to it.
             * this is done by inserting a record into the UserThatDownloadedFile table.
             * This links the user to the file that was downloaded and set the file status from avaiable to Locked in the Files table.
             * */
            if (FM.UserThatDownloadedFile is null)
            {
                FM.UserThatDownloadedFile = new UserThatDownloadedFileModel()
                {
                    FileID = PDOSFM.FileID,
                    UserIDThatDownloadedFIle = PDOSFM.UserIDOfPersonThatDownloadedTheFile,
                    DateDownloaded = DateTime.Now,
                    HasFileBeenReturned = false,
                    EntityState = DomainModels.EntityState.Added
                };
                FM.FileLookupStatusID = (int)WebDocs.Common.Enum.DbLookupTables.EnumFileViewStatuses.Locked;
                FM.EntityState = DomainModels.EntityState.Modified;

                CompletedTransactionResponses CTR = await _FileRepsoitory.AsyncUpdate(FM);
                if (CTR.WasSuccessfull)
                {
                    return FM;
                }
                else
                {
                    return FM;
                    // return View("Error", new HandleErrorInfo(new Exception(), "ContentManagement", "DownLoadSelectedFile"));
                }
            }
            else
            {
                return FM;
            }
        }

        public CompletedTransactionResponses SendFileUploadNotification(ProcessDownloadingOfSelectedFileModel PDOSFM)
        {



            FileModel FM = _FileRepsoitory.GetSingle(
                a => a.FileID == PDOSFM.FileID,//where
                a => a.FileOwner,
                a => a.UserThatDownloadedFile,
                a => a.UserThatDownloadedFile.PersonTahtDownloadedTheFile);//include

            UsersModel UserThatLoggedOn = _UserRepository.GetSingle(a => a.Id == PDOSFM.UserIDOfPersonThatDownloadedTheFile);

            NotificationModel NM = _NotificationsRepsoitory.GetSingle(
              a => a.FileID == PDOSFM.FileID &&
              a.UserHasAcknowledgement == false &&
              a.UserIDOfNotificationSender == PDOSFM.UserIDOfPersonThatDownloadedTheFile &&
              a.UserIDOfNotificationRecipient == FM.UserThatDownloadedFile.UserIDThatDownloadedFIle,//where
              a => a.RecipientUsers,//include
              a => a.SendingUsers,
              a => a.File);//include
            if (NM is null)
            {
                NotificationModel newUploadNotification = new NotificationModel()
                {
                    FileID = PDOSFM.FileID,
                    UserIDOfNotificationRecipient = FM.UserThatDownloadedFile.UserIDThatDownloadedFIle,
                    UserIDOfNotificationSender = PDOSFM.UserIDOfPersonThatDownloadedTheFile,
                    DateCreated = DateTime.Now,
                    NotificationTypeID = (int)EnumNotificationTypes.Request_File_Upload,
                    UserHasAcknowledgement = false,
                    EntityState = DomainModels.EntityState.Added
                };

                CTR = _NotificationsRepsoitory.Add(newUploadNotification);

                if (CTR.WasSuccessfull)
                {

                    WriteToSystemTransactionLog(new SystemTransactionLogsModel()
                    {
                        SystemTransactionTypeID = (int)Common.Enum.DbLookupTables.EnumSystemTransactionTypes.SendFileUploadNotification,
                        UserIDThatPerformedTansaction = PDOSFM.UserIDOfPersonThatDownloadedTheFile,
                        DateTansactionPerformed = DateTime.Now,
                        TransactionComments = UserThatLoggedOn.UserFullName + ": has sent a request to : " + FM.UserThatDownloadedFile.PersonTahtDownloadedTheFile.UserFullName + " to upload to file.",
                        FileID = PDOSFM.FileID
                    });


                    //add email that will be sent tot the recipient

                    _EmailCacheRepsoitory.AsyncAdd(new EmailCacheModel()
                    {
                        IDOfPersonSender = PDOSFM.UserIDOfPersonThatDownloadedTheFile,
                        IDOfRecipient = FM.UserThatDownloadedFile.PersonTahtDownloadedTheFile.Id,
                        EmailMessage = "A request to upload the following File# " + FM.FileID + " to be uploaded as the following user requires to gain access to the file.<br/>The File that is required name:<b>" + FM.FullFileName + "</b><br/>Regards Web Docs Team.",
                        EmailSubject = "Upload Notification for - (File# " + FM.FileID + ")",
                        HasBeenSent = false,
                        EntityState = DomainModels.EntityState.Added,
                        RetryAttempt = 0
                    });
                }
            }
            else // already exists
            {
                WriteToSystemTransactionLog(new SystemTransactionLogsModel()
                {
                    SystemTransactionTypeID = (int)Common.Enum.DbLookupTables.EnumSystemTransactionTypes.SendFileUploadNotification,
                    UserIDThatPerformedTansaction = PDOSFM.UserIDOfPersonThatDownloadedTheFile,
                    DateTansactionPerformed = DateTime.Now,
                    TransactionComments = UserThatLoggedOn.UserFullName + ": has previously sent a request to : " + FM.UserThatDownloadedFile.PersonTahtDownloadedTheFile.UserFullName + " to upload to file, has sent another upload request.",
                    FileID = PDOSFM.FileID
                });


                //add email that will be sent tot the recipient

                _EmailCacheRepsoitory.AsyncAdd(new EmailCacheModel()
                {
                    IDOfPersonSender = PDOSFM.UserIDOfPersonThatDownloadedTheFile,
                    IDOfRecipient = FM.UserThatDownloadedFile.PersonTahtDownloadedTheFile.Id,
                    EmailMessage = "A follow up request to kindly upload the following File# " + FM.FileID + " to be uploaded as the following user requires to gain access to the file.<br/>The File that is required name:<b>" + FM.FullFileName + "</b><br/>Regards Web Docs Team.",
                    EmailSubject = "Follow up request to Upload Notification for - (File# " + FM.FileID + ")",
                    HasBeenSent = false,
                    EntityState = DomainModels.EntityState.Added,
                    RetryAttempt = 0
                });
            }

            CompletedTransactionResponses RTN = new CompletedTransactionResponses()
            {
                Message = "Notification has been sent to : " + FM.UserThatDownloadedFile.PersonTahtDownloadedTheFile.UserFullName,
                TransActionType = TransactionType.NoneExecuted,
                WasSuccessfull = true
            };


            return RTN;
        }

        public FileModel DownloadHistoricalFile(ProcessDownloadingOfSelectedFileModel PDOSFM)
        {
            //Incomplete must updtea the CompletedTransactionResponses to include the data return.

            FileModel FM = _FileRepsoitory.GetSingle(
                                        //Where    
                                        a => a.FileID == PDOSFM.FileID,
                                             //Include the following in the object graph.
                                             a => a.FileBlob,
                                              a => a.UserThatDownloadedFile);

            return FM;



        }

        public IList<FileModel> GetAllFilesThatAreDownloaded()
        {
            IList<FileModel> AllFilesThatUserHasDownloaded = _UserThatDownloadedFileRepository.GetAll(
             //Model Must Include the following items in the Graph
             a => a.File,
             a => a.File.FileOwner,
             a => a.File.PersonThatLastUpdatedFile,
             a => a.File.FileShareStatues,
             a => a.File.FileViewStatuses
              //end
              ).Select(b => b.File).ToList();
            return AllFilesThatUserHasDownloaded;
        }

        public IList<FileModel> GetAllFilesThatAreDownloadedByUser(int UserID)
        {
            IList<FileModel> AllFilesThatUserHasDownloaded = _UserThatDownloadedFileRepository.GetList(
                 a => a.UserIDThatDownloadedFIle == UserID,
               //Model Must Include the following items in the Graph
               a => a.File,
               a => a.File.FileOwner,
               a => a.File.PersonThatLastUpdatedFile,
               a => a.File.FileShareStatues,
               a => a.File.FileViewStatuses
                //end
                ).Select(b => b.File).ToList();
            return AllFilesThatUserHasDownloaded;
        }

        public IList<FileModel> GetAllPersonalFilesSharedWithUser(int UserID)
        {
            var allFilesSharedwithUser = _PrivateFilesSharedWithUserRepository.GetList(a => a.UserIDPersonSharedWith == UserID,
                a => a.File,
                a => a.File.PersonThatLastUpdatedFile,
                a => a.File.FileOwner,
                a => a.File.FileShareStatues,
                a => a.File.FileViewStatuses);
            return (from a in allFilesSharedwithUser
                    select a.File).ToList<FileModel>();
        }

        public IList<FileModel> GetAllPublicFiles()
        {
            IList<FileModel> GetAllPublicFiles = _FileRepsoitory.GetAll(
                //Model Must Include the following items in the Graph
                a => a.FileOwner,
                a => a.PersonThatLastUpdatedFile,
                a => a.FileViewStatuses,
                a => a.FileShareStatues,
                a => a.PrivateFilesSharedWithUsers,
                a => a.UserThatDownloadedFile.PersonTahtDownloadedTheFile
                 //end
                 ).Where(a => a.FileShareStatusID != (int)WebDocs.Common.Enum.DbLookupTables.EnumFileShareStatues.Hidden).ToList();
            return GetAllPublicFiles;
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

        public IList<FileArchiveModel> GetFileArchivesByFileID(int FIleID)
        {
            IList<FileArchiveModel> Rtn = _FileArchiveRepsoitory.GetList(a => a.FileID == FIleID,
                a => a.UserThatUploadedTheFile);

            return Rtn;
        }

        public IList<FileModel> GetSelectedUserFiles(int UserID)
        {
            IList<FileModel> SelectedUserFiles = _FileRepsoitory.GetList(
               a => a.UserIDOfFileOwner == UserID,
               a => a.FileOwner,
               a => a.PersonThatLastUpdatedFile,
               a => a.FileViewStatuses,
               a => a.FileShareStatues,
               a => a.UserThatDownloadedFile);

            return SelectedUserFiles;
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

        public async Task<CompletedTransactionResponses> ProcessFileRequest(ProcessFileRequestNotificationViewModel PFRN)
        {
            /*Has the request already been sent
            if it has simply send the response indcating that the request has already been sent 
            if not then insert the requestin the data base and send message to the recipient.*/

            NotificationModel NM = _NotificationsRepsoitory.GetSingle(
                a => a.FileID == PFRN.FileID &&
                a.UserHasAcknowledgement == false &&
                a.UserIDOfNotificationSender == PFRN.IDOFPersonLoggedOn &&
                a.UserIDOfNotificationRecipient == PFRN.IDOfTheFileOwner,//where
                a => a.RecipientUsers,//include
                a => a.SendingUsers,
                a => a.File);//include

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

                    NotificationModel NME = _NotificationsRepsoitory.GetSingle(
                        a => a.NotificationID == newRequestNotification.NotificationID,//where
                        a => a.RecipientUsers,//include
                        a => a.SendingUsers,
                        a => a.File);//include

                    WriteToSystemTransactionLog(new SystemTransactionLogsModel()
                    {
                        SystemTransactionTypeID = (int)Common.Enum.DbLookupTables.EnumSystemTransactionTypes.SendFileShareRequestNotification,
                        UserIDThatPerformedTansaction = NME.UserIDOfNotificationSender,
                        DateTansactionPerformed = DateTime.Now,
                        TransactionComments = "An Notification to share this document was sent by " + NME.SendingUsers.UserFullName,
                        FileID = NME.FileID
                    });
                    WriteToSystemTransactionLog(new SystemTransactionLogsModel()
                    {
                        SystemTransactionTypeID = (int)Common.Enum.DbLookupTables.EnumSystemTransactionTypes.SendFileShareRequestNotification,
                        UserIDThatPerformedTansaction = NME.UserIDOfNotificationSender,
                        DateTansactionPerformed = DateTime.Now,
                        TransactionComments = "An Notification requesting that this file be shared was sent to: " + NME.RecipientUsers.UserFullName,
                        FileID = NME.FileID
                    });
                    //add email that will be sent tot the recipient

                    _EmailCacheRepsoitory.Add(new EmailCacheModel()
                    {
                        IDOfPersonSender = NME.UserIDOfNotificationSender,
                        IDOfRecipient = NME.UserIDOfNotificationRecipient,
                        EmailMessage = "A request for the File# " + NME.FileID + " to be share your private file has been sent File Name:<br/><b>" + NME.File.FullFileName + "</b><br/>Regards Web Docs Team.",
                        EmailSubject = "Request to share your private file - (File# " + NME.FileID + ")",
                        HasBeenSent = false,
                        EntityState = DomainModels.EntityState.Added,
                        RetryAttempt = 0
                    });

                    //add email that will be sent to the sender 
                    _EmailCacheRepsoitory.Add(new EmailCacheModel()
                    {
                        IDOfPersonSender = NME.UserIDOfNotificationRecipient,
                        IDOfRecipient = NME.UserIDOfNotificationSender,
                        EmailMessage = "A Notification  has been sent to " + NME.RecipientUsers.UserFullName + " to in form him/her that you requested that the file be shared. Name of the file that was requested is:<br/><b>" + NME.File.FullFileName + "</b><br/>Regards Web Docs Team.",
                        EmailSubject = "Request to share your private file - (File# " + NME.FileID + ")",
                        HasBeenSent = false,
                        EntityState = DomainModels.EntityState.Added,
                        RetryAttempt = 0
                    });

                }

            }
            else
            {
                CTR.Message = CTR.Message + " Recipient already been notified, how ever another request has been sent!";
                CTR.WasSuccessfull = true;

                WriteToSystemTransactionLog(new SystemTransactionLogsModel()
                {
                    SystemTransactionTypeID = (int)Common.Enum.DbLookupTables.EnumSystemTransactionTypes.SendFileShareRequestNotification,
                    UserIDThatPerformedTansaction = NM.UserIDOfNotificationSender,
                    DateTansactionPerformed = DateTime.Now,
                    TransactionComments = "An Notification to share this document was resent by " + NM.SendingUsers.UserFullName,
                    FileID = NM.FileID
                });
                WriteToSystemTransactionLog(new SystemTransactionLogsModel()
                {
                    SystemTransactionTypeID = (int)Common.Enum.DbLookupTables.EnumSystemTransactionTypes.SendFileShareRequestNotification,
                    UserIDThatPerformedTansaction = NM.UserIDOfNotificationSender,
                    DateTansactionPerformed = DateTime.Now,
                    TransactionComments = "An Notification requesting that this file be shared was resent to: " + NM.RecipientUsers.UserFullName,
                    FileID = NM.FileID
                });
                _EmailCacheRepsoitory.Add(new EmailCacheModel()
                {
                    IDOfPersonSender = NM.UserIDOfNotificationSender,
                    IDOfRecipient = NM.UserIDOfNotificationRecipient,
                    EmailMessage = "A request for the File# " + NM.FileID + " to be share your private file has already been sent from previously for File:<br/><b>" + NM.File.FullFileName + "</b><br/>This is just a friendly remainder sent from the same user for file.<br/>Regards Web Docs Team.",
                    EmailSubject = "Request to share your private file - (File# " + NM.FileID + ")",
                    HasBeenSent = false,
                    EntityState = DomainModels.EntityState.Added,
                    RetryAttempt = 0
                });
                //add email that will be sent to the sender 
                _EmailCacheRepsoitory.Add(new EmailCacheModel()
                {
                    IDOfPersonSender = NM.UserIDOfNotificationRecipient,
                    IDOfRecipient = NM.UserIDOfNotificationSender,
                    EmailMessage = "A Notification  has been resent to " + NM.RecipientUsers.UserFullName + " to in form him/her that you requested that the file be shared. Name of the file that was requested is:<br/><b>" + NM.File.FullFileName + "</b><br/>Regards Web Docs Team.",
                    EmailSubject = "Resend of Request to share your private file - (File# " + NM.FileID + ")",
                    HasBeenSent = false,
                    EntityState = DomainModels.EntityState.Added,
                    RetryAttempt = 0
                });


            }
            return CTR;
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

        public FileUploadResponses SaveUploadedUserFiles(UploadingNewUserFileModel UNUFM)//List<FileModel> CurrentFiles)
        {

            if (UNUFM.file != null && UNUFM.file.ContentLength > 0)
            {
                byte[] uploadedFile = new byte[UNUFM.file.InputStream.Length];
                UNUFM.file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                FileModel NewFileUpload = new FileModel()
                {
                    FileBlob = new FileBlobModel()
                    {
                        EntityState = DomainModels.EntityState.Added,
                        FileImage = uploadedFile
                    },
                    ContentType = UNUFM.file.ContentType,
                    CurrentVersionNumber = 1,
                    Created = DateTime.Now,

                    Name = Path.GetFileNameWithoutExtension(UNUFM.file.FileName),
                    Size = UNUFM.file.ContentLength,
                    UserIDOfFileOwner = UNUFM.IDOfCurrentUser,
                    UserIDOfLastUploaded = UNUFM.IDOfCurrentUser,
                    FileLookupStatusID = (int)EnumFileViewStatuses.Available,
                    FileShareStatusID = UNUFM.FileShareStatusID,
                    Extension = Path.GetExtension(UNUFM.file.FileName).Replace(".", ""),
                    EntityState = DomainModels.EntityState.Added
                };

                return WebDocs.Common.Helper.Files.UploadHelper.SaveUploadedFile(NewFileUpload);
            }
            else
            {

                return new FileUploadResponses()
                {
                    FileName = UNUFM.file.FileName,
                    Message = "Failed To Upload - Error :  file is Null or Not uploaded correctly.",
                    WasSuccessfull = false
                };
            }




        }

        public FileUploadResponses SaveUpdatedUserFiles(UploadingUpdatedUserFileModel UUUFM)
        {
            FileUploadResponses FUR = null;
            //Checks to verify that teh file is vaild before processing the uploaded file.
            if (UUUFM.file != null && UUUFM.file.ContentLength > 0)
            {

                //Get current File version.
                FileModel FM = _FileRepsoitory.GetSingle(a => a.FileID == UUUFM.FileID,
                    a => a.FileBlob,
                    a => a.UserThatDownloadedFile);

                //Add the current version to the achive.
                FileArchiveModel FAM = new FileArchiveModel()
                {
                    FileArchiveBlob = new FileArchiveBlobModel()
                    {
                        FileImage = FM.FileBlob.FileImage,
                        EntityState = DomainModels.EntityState.Added
                    },
                    FileID = FM.FileID,
                    ContentType = FM.ContentType,
                    Name = FM.Name,
                    Size = FM.Size,
                    Version = FM.CurrentVersionNumber,
                    DateCreated = FM.Created,
                    DateArchived = DateTime.Now,
                    Extension = FM.Extension,
                    UserIDOfLastUploaded = FM.UserIDOfLastUploaded,

                    EntityState = DomainModels.EntityState.Added
                };
                FUR = Common.Helper.Files.UploadHelper.SaveArchivedFiles(FAM);

                //If the file was successfully added to the archive then update the current file with the current file deatils that was uploaded.
                if (FUR.WasSuccessfull)
                {
                    IList<NotificationModel> ListOfNM = _NotificationsRepsoitory.GetList(a => a.FileID == FM.FileID &&
                                                                a.NotificationTypeID == (int)Common.Enum.DbLookupTables.EnumNotificationTypes.Request_File_Upload);
                    //Clear/update any Upload notifiaction to be acknowlaged.
                    foreach (NotificationModel NMToUpdate in ListOfNM)
                    {
                        NMToUpdate.UserHasAcknowledgement = true;
                        NMToUpdate.EntityState = DomainModels.EntityState.Modified;
                        _NotificationsRepsoitory.Update(NMToUpdate);
                    }

                    byte[] uploadedFile = new byte[UUUFM.file.InputStream.Length];
                    UUUFM.file.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                    //update the current file deatils with tat of the new upadted file that was submitted blyu the user.
                    FM.CurrentVersionNumber = FM.CurrentVersionNumber + 1;
                    FM.Size = UUUFM.file.ContentLength;
                    FM.Extension = Path.GetExtension(UUUFM.file.FileName).Replace(".", "");
                    FM.Name = Path.GetFileNameWithoutExtension(UUUFM.file.FileName);
                    FM.ContentType = UUUFM.file.ContentType;
                    FM.FileLookupStatusID = (int)Common.Enum.DbLookupTables.EnumFileViewStatuses.Available;
                    FM.UserIDOfLastUploaded = UUUFM.IDOfCurrentUserUpdatingTheFile;
                    FM.FileBlob.FileImage = uploadedFile;

                    FM.EntityState = DomainModels.EntityState.Modified;
                    FM.FileBlob.EntityState = DomainModels.EntityState.Modified;
                    FM.UserThatDownloadedFile.EntityState = DomainModels.EntityState.Deleted;

                    FUR = Common.Helper.Files.UploadHelper.UpdateUploadedFiles(FM);



                }
                else
                {
                    //uploaded file not valid return error message.
                    FUR.Message = "Error with file that that was uploaded, unable to archive the current file and update the current with the file that was unloaded!! ";
                    FUR.FileName = UUUFM.file.FileName;
                    FUR.WasSuccessfull = false;

                }
            }
            else
            {
                //uploaded file not valid return error message.
                FUR.Message = "Error with file that that was uploaded, ither its empty or not unloaded correctly, please try again!! ";
                FUR.FileName = UUUFM.file.FileName;
                FUR.WasSuccessfull = false;
            }
            return FUR;
        }

        public CompletedTransactionResponses UnlinkPrivatelySharedDocument(int UserIDPersonSharedWith, int FileID)
        {
            PrivateFilesSharedWithUserModel PFSWUM = _PrivateFilesSharedWithUserRepository.GetSingle(a => a.FileID == FileID && a.UserIDPersonSharedWith == UserIDPersonSharedWith);

            PFSWUM.EntityState = DomainModels.EntityState.Deleted;

            CTR = _PrivateFilesSharedWithUserRepository.Remove(PFSWUM);
            if (CTR.WasSuccessfull)
            {
                CTR.Message = CTR + " -  Successfully unlinked this file that was shared.";
            }
            return CTR;

        }

        protected void WriteToSystemTransactionLog(SystemTransactionLogsModel STLM)
        {
            STLM.EntityState = DomainModels.EntityState.Added;
            _SystemTransactionRepository.Add(STLM);
        }
    }
}
