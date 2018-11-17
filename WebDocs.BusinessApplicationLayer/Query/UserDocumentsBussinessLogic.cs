using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Json;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.Common.Enum.DbLookupTables;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class UserDocumentsBussinessLogic 
    {

        private readonly IFileRepository _FileRepsoitory;

        public CompletedTransactionResponses CTR { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UserDocumentsBussinessLogic()
        {
            _FileRepsoitory = new FileRepository();
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UNUFM"></param>
        /// <returns></returns>
        

        //public IList<FileModel> GetSelectedUserFiles(int UserID)
        //{
           
        //}
    }
}
