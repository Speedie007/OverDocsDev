using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.Common.AbstractClasses;
using WebDocs.Common.Enum.SystemLogicEnum;
using WebDocs.Common.Interfaces;
using WebDocs.DomainModels.DecisionModels.PublicDocuments.IsLocked;
using WebDocs.DomainModels.DecisionModels.UserDocuments;
using WebDocs.DomainModels.Interfaces.FileLinks;

namespace WebDocs.Common.DescisionClasses.UserViewedFiles
{
    public class DecisionForUserViewFile : AbstractDecision
    {
        public override ControlTypes FinalDecision
        {
            get { return _FinalDecision; }
        }

        public DecisionForUserViewFile(int UserID, IFileLinkDecisionModel Model)
        {
            this.ID_OfUserCurrentlyLoggedIn = UserID;
            this.Model = Model;
            this.IntialiseDecisionVariables();
            this.DetermineCorrectButton();

        }

        protected override void DetermineCorrectButton()
        {
            if (Model.FileStatusID == (int)Common.Enum.DbLookupTables.EnumFileViewStatuses.Locked)
            {
                if (IS_THE_CURRENT_USER_LOGGED_IN_THE_SAME_PERSON_THAT_DOWNLOADED_THE_FILE)
                {
                    this._FinalDecision = ControlTypes.Download;
                }
                else
                {
                    this._FinalDecision = ControlTypes.UploadFileNotification;
                }
            }
            else
            {
                this._FinalDecision = ControlTypes.Download;
            }
        }

        protected override void Determine_IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON()
        {
            if (this.ID_OfUserCurrentlyLoggedIn == this.Model.FileOwnerID)
            {
                this.IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON = true;
            }
        }

        protected override void Determine_IS_FILE_PUBLIC_OR_PRIVATE()
        {
            throw new NotImplementedException();
        }

        protected override void Determine_IS_THE_CURRENT_FILE_SHARED_WITH_USER_CURRENTLY_LOGGED_IN()
        {
            throw new NotImplementedException();
        }

        protected override void Determine_IS_THE_CURRENT_USER_LOGGED_IN_THE_SAME_PERSON_THAT_DOWNLOADED_THE_FILE()
        {
            if (((UserDocsDataModel)Model).UserIDOfthePersonThatDownloadedTheFile == this.ID_OfUserCurrentlyLoggedIn)
            {
                this.IS_THE_CURRENT_USER_LOGGED_IN_THE_SAME_PERSON_THAT_DOWNLOADED_THE_FILE = true;
            }
        }

        protected override void Determine_IS_THE_CURRENT_USER_THAT_IS_CURRENTLY_LOGGED_IN_THE_SAME_AS_THE_PERSON_THAT_DOWNLOADED_THE_FILE_LAST()
        {
            throw new NotImplementedException();
        }

        protected override void IntialiseDecisionVariables()
        {
            //this.Determine_IS_FILE_PUBLIC_OR_PRIVATE();
            //this.Determine_IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON();
            this.Determine_IS_THE_CURRENT_USER_LOGGED_IN_THE_SAME_PERSON_THAT_DOWNLOADED_THE_FILE();
        }
    }
}
