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
    public class PrivatelySharedDocumentsBusinessLogic : IPrivatelySharedDocuments
    {

        private readonly IPrivateFilesSharedWithUsers _PrivateFilesSharedWithUserRepository;


        public PrivatelySharedDocumentsBusinessLogic()
        {
            this._PrivateFilesSharedWithUserRepository = new PrivateFilesSharedWithUserRepository();
        }
        public IList<FileModel> GetAllPersonalFilesSharedWithUser(int UserID)
        {
            var allFilesSharedwithUser = _PrivateFilesSharedWithUserRepository.GetList(a => a.FileSharedWithUserID == UserID,
                a => a.File,
                a => a.File.PersonThatLastUpdatedFile,
                a => a.File.FileOwner,
                a => a.File.FileShareStatues,
                a => a.File.FileViewStatuses);
            return (from a in allFilesSharedwithUser
                    select a.File).ToList<FileModel>();
        }
    }
}
