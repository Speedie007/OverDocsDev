

using OverDocsModels.Interfaces;
using System;
using WebDocs.Common.Enum.SystemLogicEnum;
using WebDocs.SystemLogic.Interfaces;


namespace WebDocs.SystemLogic.AbstractClasses
{
    public abstract class AbstractDecision : IDecsions
    {
        protected ControlTypes _FinalDecision = ControlTypes.Download;
        public abstract ControlTypes FinalDecision { get; }
        protected string ID_OfUserCurrentlyLoggedIn { get; set; }

        /// <summary>
        /// Data required to determine the correct link to display.
        /// </summary>
        protected IFileLinkDecisionModel Model;

        protected Boolean IS_FILE_PUBLIC = false;
        protected Boolean IS_FILE_PRIVATE = false;
        protected Boolean IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON = false;
        protected Boolean IS_THE_CURRENT_FILE_SHARED_WITH_USER_CURRENTLY_LOGGED_IN = false;
        protected Boolean IS_THE_CURRENT_USER_LOGGED_IN_THE_SAME_PERSON_THAT_DOWNLOADED_THE_FILE = false;
        protected Boolean IS_THE_CURRENT_USER_THATlOGGED_IN_THE_SAME_AS_THE_PERSON_THAT_DOWNLOADED_THE_FILE_LAST = false;

        protected abstract void DetermineCorrectButton();
        protected abstract void IntialiseDecisionVariables();

        /// <summary>
        /// Variable used to etermines weather the Shared states are public or private
        /// </summary>
        protected abstract void Determine_IS_FILE_PUBLIC_OR_PRIVATE();
        /// <summary>
        /// Variable used to determines weather the File Owner and the current user logged in are the same person.
        /// </summary>
        protected abstract void Determine_IS_FILE_OWNER_AND_USER_LOGGED_IN_THE_SAME_PERSON();
        /// <summary>
        /// 
        /// </summary>
        protected abstract void Determine_IS_THE_CURRENT_FILE_SHARED_WITH_USER_CURRENTLY_LOGGED_IN();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void Determine_IS_THE_CURRENT_USER_LOGGED_IN_THE_SAME_PERSON_THAT_DOWNLOADED_THE_FILE();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void Determine_IS_THE_CURRENT_USER_THAT_IS_CURRENTLY_LOGGED_IN_THE_SAME_AS_THE_PERSON_THAT_DOWNLOADED_THE_FILE_LAST();
    }
}
