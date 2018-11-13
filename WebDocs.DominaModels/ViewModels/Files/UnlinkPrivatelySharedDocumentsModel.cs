using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.DomainModels.ViewModels.Files
{
    public class UnlinkPrivatelySharedDocumentsModel
    {
        public int FileSharedWithUserID { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? page { get; set; }
    }
}
