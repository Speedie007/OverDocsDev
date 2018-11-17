using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class PrivatelySharedDocumentsBusinessLogic 
    {

        private readonly IPrivateFilesSharedWithUsersRepository _PrivateFilesSharedWithUserRepository;


        public PrivatelySharedDocumentsBusinessLogic()
        {
            this._PrivateFilesSharedWithUserRepository = new PrivateFilesSharedWithUserRepository();
        }

        public CompletedTransactionResponses CTR { get; set; }

        //public IList<FileModel> GetAllPersonalFilesSharedWithUser(int UserID)
        //{
            
        //}

        //public CompletedTransactionResponses UnlinkPrivatelySharedDocument(int UserIDPersonSharedWith, int FileID)
        //{

            
        //}
    }
}
