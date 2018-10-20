using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.DomainModels.Interfaces.FileLinks
{
    public interface IFileLinkDecisionModel
    {
        int FileID { get; set; }
        int FileOwnerID { get; set; }
        int FileSharedStautusID { get; set; }
        int FileStatusID { get; set; }
    }
}
