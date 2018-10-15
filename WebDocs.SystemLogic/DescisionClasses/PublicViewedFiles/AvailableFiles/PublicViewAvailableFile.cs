using WebDocs.Common.Enum.DBLookupEnum;
using WebDocs.Common.Enum.SystemLogicEnum;
using OverDocsModels.DecisionModels.PublicDocuments.IsAvaiable;
using OverDocsModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebDocs.SystemLogic.AbstractClasses;

namespace SystemLogic.DescisionClasses.PublicViewedFiles.AvailableFiles
{
    public class PublicViewAvailableFile : AbstractDecision
    {

        /// <summary>
        /// Constructor accepts tthe id of the currently login in user.
        /// </summary>
        /// <param name="UserID">ID Of the Current Person taht is logged on</param>
        public PublicViewAvailableFile(string UserID, IFileLinkDecisionModel Model)
        {
            this.ID_OfUserCurrentlyLoggedIn = UserID;
            this.Model = Model;
            this.IntialiseDecisionVariables();
            this.DetermineCorrectButton();

        }

        /// <summary>
        /// returns the Type of link to display
        /// </summary>
        public override ControlTypes FinalDecision
        {
            get { return _FinalDecision; }
        }

        /// <summary>
        /// Initialises the Decision Variables required to determine the correct link to display
        /// </summary>
        protected override void IntialiseDecisionVariables()
        {
            this.Determine_IS_FILE_PUBLIC_OR_PRIVATE();
            this.Determine_IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON();
            this.Determine_IS_THE_CURRENT_FILE_SHARED_WITH_USER_CURRENTLY_LOGGED_IN();
        }

        /// <summary>
        /// Inherited from the common interface. Used to execute logic sequence to determine the correct button to display.
        /// </summary>
        protected override void DetermineCorrectButton()
        {

            if (IS_THE_CURRENT_USER_THATlOGGED_IN_THE_SAME_AS_THE_PERSON_THAT_DOWNLOADED_THE_FILE_LAST)
            {
                this._FinalDecision = ControlTypes.Download;
            }
            else
            {
                if (IS_FILE_PUBLIC)
                {
                    this._FinalDecision = ControlTypes.Download;
                }
                else
                {
                    if (IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON)
                    {
                        this._FinalDecision = ControlTypes.Download;
                    }
                    else
                    {
                        if (IS_THE_CURRENT_FILE_SHARED_WITH_USER_CURRENTLY_LOGGED_IN)
                        {
                            this._FinalDecision = ControlTypes.Download;
                        }
                        else
                        {
                            this._FinalDecision = ControlTypes.RequestPermissionNotifications;
                        }
                    }
                }
            }
        }

        protected override void Determine_IS_FILE_PUBLIC_OR_PRIVATE()
        {
            switch (Model.FileSharedStautusID)
            {
                case (int)FileSharedStatus.Public:
                    this.IS_FILE_PUBLIC = true;
                    break;
                case (int)FileSharedStatus.Private:
                    this.IS_FILE_PRIVATE = true;
                    break;
                default:
                    break;
            }

        }

        protected override void Determine_IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON()
        {
            if (this.ID_OfUserCurrentlyLoggedIn == this.Model.FileOwnerID)
            {
                this.IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON = true;
            }
        }

        protected override void Determine_IS_THE_CURRENT_FILE_SHARED_WITH_USER_CURRENTLY_LOGGED_IN()
        {
            if (((PublicDocsAvailableDataModel)Model).FileID_FilesSharedWithUser.Length > 0)
            {
                string[] AllFilesSharedWithUser = ((PublicDocsAvailableDataModel)Model).FileID_FilesSharedWithUser.Split('?');
                foreach (string SharedFileID in AllFilesSharedWithUser)
                {
                    if (SharedFileID.Equals(SharedFileID))
                    {
                        this.IS_THE_CURRENT_FILE_SHARED_WITH_USER_CURRENTLY_LOGGED_IN = true;
                    }
                }
            }

        }

        protected override void Determine_IS_THE_CURRENT_USER_LOGGED_IN_THE_SAME_PERSON_THAT_DOWNLOADED_THE_FILE()
        {

        }

        protected override void Determine_IS_THE_CURRENT_USER_THAT_IS_CURRENTLY_LOGGED_IN_THE_SAME_AS_THE_PERSON_THAT_DOWNLOADED_THE_FILE_LAST()
        {
            if (((PublicDocsAvailableDataModel)Model).IDOfUserThatLastDownLoadedTheSelectedFile.Equals(this.ID_OfUserCurrentlyLoggedIn))
            {
                IS_THE_CURRENT_USER_THATlOGGED_IN_THE_SAME_AS_THE_PERSON_THAT_DOWNLOADED_THE_FILE_LAST = true;
            }

        }
    }
}
