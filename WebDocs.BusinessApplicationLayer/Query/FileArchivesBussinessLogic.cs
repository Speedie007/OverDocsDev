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
    public class FileArchivesBussinessLogic : IFileArchives
    {
        public CompletedTransactionResponses CTR { get; set; }

       

        //FileArchiveRepository

        public FileArchivesBussinessLogic()
        {
            //_FileArchiveRepsoitory = new FileArchiveRepository();
        }

        //public IList<FileArchiveModel> GetFileArchivesByFileID(int FIleID)
        //{
        //    IList<FileArchiveModel> Rtn = _FileArchiveRepsoitory.GetList(a => a.FileID == FIleID,
        //        a=>a.UserThatUploadedTheFile);

        //    return Rtn;
        //}
    }
}
