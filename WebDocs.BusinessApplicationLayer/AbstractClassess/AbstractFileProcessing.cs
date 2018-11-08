using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.Interfaces;
using WebDocs.BusinessApplicationLayer.Interfaces.CommonSections;
using WebDocs.DataAccessLayer.Interfaces;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.BusinessApplicationLayer.AbstractClassess
{
    public abstract  class AbstractFileProcessing<Model,Repo> 
    {
        public AbstractFileProcessing()
        {
                        
        }

        public abstract List<FileUploadResponses> SaveFile(List<Model> CurrentFile, Repo Repo);
       


    }
}
