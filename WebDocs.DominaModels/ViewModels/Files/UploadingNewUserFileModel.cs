using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebDocs.DomainModels.ViewModels.Files
{
    public class UploadingNewUserFileModel
    {
        public HttpPostedFileBase file { get; set; }
        public int FileShareStatusID { get; set; }
        public int IDOfCurrentUser {get;set;}
    }
}
