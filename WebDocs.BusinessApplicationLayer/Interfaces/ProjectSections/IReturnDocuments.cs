using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections
{
    public interface IReturnDocuments : IBusinessLayer<UserThatDownloadedFileModel>
    {
        IList<FileModel> GetAllFilesThatAreDownloaded();
        IList<FileModel> GetAllFilesThatAreDownloadedByUser(int UserID);
        FileUploadResponses SaveUpdatedUserFiles(UploadingUpdatedUserFileModel UUUFM);
    }
}
