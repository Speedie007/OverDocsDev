using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.Common.DescisionClasses.PublicViewedFiles.AvailableFiles;
using WebDocs.Common.DescisionClasses.PublicViewedFiles.LockedFiles;
using WebDocs.Common.DescisionClasses.UserViewedFiles;
using WebDocs.Common.Interfaces;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.DecisionModels.PublicDocuments.IsAvailable;
using WebDocs.DomainModels.DecisionModels.PublicDocuments.IsLocked;
using WebDocs.DomainModels.DecisionModels.UserDocuments;

namespace WebDocs.Common.Helper.LinkDecision
{
    public static class LinkDecision
    {
        public static IDecsions GetPublicDocsFileLinkDecsion(FileModel CurrentFile, int CurrentUserID)
        {

            IDecsions Decision;
            int? x;
            if (!(CurrentFile.UserThatDownloadedFile is null))
            {
                x = CurrentFile.UserThatDownloadedFile.UserIDThatDownloadedFIle;
            }
            else
            {
                x = null;
            }



            switch (CurrentFile.FileLookupStatusID)
            {
                case (int)Common.Enum.DbLookupTables.EnumFileViewStatuses.Available:

                    Decision = new DecisionForPublicViewAvailableFile(CurrentUserID, new PublicDocsAvailableDataModel()
                    {
                        FileID = CurrentFile.FileID,
                        FileOwnerID = CurrentFile.UserIDOfFileOwner,
                        FileStatusID = CurrentFile.FileLookupStatusID,
                        FileSharedStautusID = CurrentFile.FileShareStatusID,
                        ListOfFilesSharedWithUser = CurrentFile.PrivateFilesSharedWithUsers,
                        IDOfUserThatLastDownLoadedTheSelectedFile = x ?? 0
                    });
                    break;
                case (int)Common.Enum.DbLookupTables.EnumFileViewStatuses.Locked:
                    Decision = new PublicViewLockedFile(CurrentUserID, new PublicDocsLockedDataModel()
                    {
                        FileID = CurrentFile.FileID,
                        FileOwnerID = CurrentFile.UserIDOfFileOwner,
                        FileStatusID = CurrentFile.FileLookupStatusID,
                        FileSharedStautusID = CurrentFile.FileShareStatusID,
                        ListOfFilesSharedWithUser = CurrentFile.PrivateFilesSharedWithUsers,
                        UserIDOfthePersonThatDownloadedTheFile = x ?? 0
                    });
                    break;
                default:
                    Decision = null;
                    break;
            }

            return Decision;
        }
        public static IDecsions GetUserDocsFileLinkDecsion(FileModel CurrentFile, int CurrentUserID)
        {

            IDecsions Decision;
            int? x;
            if (!(CurrentFile.UserThatDownloadedFile is null))
            {
                x = CurrentFile.UserThatDownloadedFile.UserIDThatDownloadedFIle;
            }
            else
            {
                x = null;
            }




            Decision = new DecisionForUserViewFile(CurrentUserID, new UserDocsDataModel()
            {
                FileID = CurrentFile.FileID,
                FileOwnerID = CurrentFile.UserIDOfFileOwner,
                FileStatusID = CurrentFile.FileLookupStatusID,
                FileSharedStautusID = CurrentFile.FileShareStatusID,
                UserIDOfthePersonThatDownloadedTheFile = x ?? 0
            });


            return Decision;
        }
    }
}
