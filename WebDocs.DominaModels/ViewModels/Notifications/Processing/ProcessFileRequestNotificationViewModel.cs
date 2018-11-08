using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.DomainModels.ViewModels.Notifications.Processing
{
    public class ProcessFileRequestNotificationViewModel
    {
        public int FileID { get; set; }
        public int IDOFPersonLoggedOn { get; set; }
        public int IDOfTheFileOwner { get; set; }
    }
}
