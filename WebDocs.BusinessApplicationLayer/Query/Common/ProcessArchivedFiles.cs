using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.BusinessApplicationLayer.AbstractClassess;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.BusinessApplicationLayer.Query.Common
{
    public  class ProcessArchivedFiles : AbstractFileProcessing<FileArchiveModel, IFileArchiveRepository>
    {
        public override List<FileUploadResponses> SaveFile(List<FileArchiveModel> CurrentFiles, IFileArchiveRepository Repo)
        {
            List<FileUploadResponses> Rtn = new List<FileUploadResponses>();
            foreach (FileArchiveModel FileToSave in CurrentFiles)
            {
                try
                {
                    Repo.Add(FileToSave);
                    Rtn.Add(new FileUploadResponses()
                    {
                        FileName = FileToSave.FullFileName,
                        Message = "Successfully Saved",
                        WasSuccessfull = true
                    });
                }
                catch (Exception ex)
                {
                    Rtn.Add(new FileUploadResponses()
                    {
                        FileName = FileToSave.FullFileName,
                        Message = "Fialed To Upload - Error : " + ex.Message,
                        WasSuccessfull = false
                    });
                }
            }
            return Rtn;
        }
    }
}
