using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections
{
    public interface IPublicDocuments : IBusinessLayer<FileModel>
    {
        IList<FileModel> GetAllPublicFiles();
        Task<FileModel> DownloadSelectedFile(ProcessDownloadingOfSelectedFileModel PDOSFM);
    }
}
