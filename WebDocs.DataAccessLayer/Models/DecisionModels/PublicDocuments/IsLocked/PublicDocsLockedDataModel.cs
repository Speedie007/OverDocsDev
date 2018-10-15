using OverDocsModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverDocsModels.DecisionModels.PublicDocuments.IsLocked
{
    /// <summary>
    /// 
    /// </summary>
    public class PublicDocsLockedDataModel : IFileLinkDecisionModel
    {
        public int FileID { get; set; }
        public string FileOwnerID { get; set; }
        public int FileSharedStautusID { get; set; }
        public int FileStatusID { get; set; }
        public string FileID_FilesSharedWithUser { get; set; }
        public string UserIDOfthePersonThatDownloadedTheFile { get; set; }
    }
}
