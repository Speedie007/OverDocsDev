using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces.ProjectSections;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;
using WebDocs.DomainModels.ViewModels.Files;

namespace WebDocs.BusinessApplicationLayer.Query
{
    public class ReturnDocumentsBussinessLogic 
    {

        
        private readonly IFileRepository _FileRepsoitory;

        public ReturnDocumentsBussinessLogic()
        {
            
            _FileRepsoitory = new FileRepository();
        }

        public CompletedTransactionResponses CTR { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public IList<FileModel> GetAllFilesThatAreDownloaded()
        //{
           
        //}

        //public IList<FileModel> GetAllFilesThatAreDownloadedByUser(int UserID)
        //{
            
        //}

        
    }
}
