using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebDocs.DomainModels.ViewModels.Files
{
    public class UploadingUpdatedUserFileModel
    {
        public HttpPostedFileBase file { get; set; }
        public int FileID { get; set; }
        public int IDOfCurrentUserUpdatingTheFile { get; set; }
    }
}
