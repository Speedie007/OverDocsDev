using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverDocsModels.Interfaces
{
    public interface IFileLinkDecisionModel
    {
        int FileID { get; set; }
        string FileOwnerID { get; set; }
        int FileSharedStautusID { get; set; }
        int FileStatusID { get; set; }
    }
}
