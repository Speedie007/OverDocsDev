using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections
{
    public interface IPrivatelySharedDocuments : IBusinessLayer<PrivateFilesSharedWithUserModel>
    {
        IList<FileModel> GetAllPersonalFilesSharedWithUser(int UserID);
        CompletedTransactionResponses UnlinkPrivatelySharedDocument(int UserIDPersonSharedWith, int FileID);
        
    }
}
