using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.DomainModels.ViewModels.Files
{
    public class ProcessDownloadingOfSelectedFileModel
    {
        public int FileID { get; set; }
        public int UserIDOfPersonThatDownloadedTheFile { get; set; }
    }
}
