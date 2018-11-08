using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;

namespace WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections
{
    public interface IPrivatelySharedDocuments : IBusinessLayer<PrivateFilesSharedWithUserModel>
    {
        IList<FileModel> GetAllPersonalFilesSharedWithUser(int UserID);
    }
}
