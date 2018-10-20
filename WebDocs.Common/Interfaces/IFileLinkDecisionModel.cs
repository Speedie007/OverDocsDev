using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.Common.Interfaces
{
    public interface IFileLinkDecisionModel<T> where T : class 
    {
        int FileID { get; set; }
        int FileOwnerID { get; set; }
        int FileSharedStautusID { get; set; }
        int FileStatusID { get; set; }
    }
}
