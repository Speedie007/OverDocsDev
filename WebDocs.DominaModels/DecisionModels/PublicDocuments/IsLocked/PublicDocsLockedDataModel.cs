
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.Interfaces.FileLinks;

namespace WebDocs.DomainModels.DecisionModels.PublicDocuments.IsLocked
{
    /// <summary>
    /// 
    /// </summary>
    public class PublicDocsLockedDataModel : IFileLinkDecisionModel
    {
        public int FileID { get; set; }
        public int FileOwnerID { get; set; }
        public int FileSharedStautusID { get; set; }
        public int FileStatusID { get; set; }
        public ICollection<PrivateFilesSharedWithUser> ListOfFilesSharedWithUser { get; set; }
        public int UserIDOfthePersonThatDownloadedTheFile { get; set; }
        
    }
}
