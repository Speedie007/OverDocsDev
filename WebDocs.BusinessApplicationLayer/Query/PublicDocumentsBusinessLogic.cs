using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class PublicDocumentsBusinessLogic: IPublicDocuments
    {

        private readonly IFileRepository _FileRepsoitory;

        public PublicDocumentsBusinessLogic()
        {
            _FileRepsoitory = new FileRepository();
        }

        public IList<FileModel> GetAllPublicFiles()
        {
            IList<FileModel> GetAllPublicFiles = _FileRepsoitory.GetAll(
                //Model Must Include the following items in the Graph
                a => a.FileOwner,
                a => a.PersonThatLastUpdatedFile,
                a => a.FileViewStatuses,
                a => a.FileShareStatues
                 //end
                 );
            return GetAllPublicFiles;
        }
    }
}
