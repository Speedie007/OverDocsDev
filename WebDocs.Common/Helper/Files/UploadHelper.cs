using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DataAccessLayer.Interfaces.Entities;
using WebDocs.DataAccessLayer.Repositories;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.Common.Helper.Files
{
    public static class UploadHelper
    {
        public static List<FileUploadResponses> SaveUploadedUserFiles(List<FileModel> CurrentFiles)
        {

            IFileRepository _FileRepsoitory = new FileRepository();
            List<FileUploadResponses> Rtn = new List<FileUploadResponses>();
            foreach (FileModel FileToSave in CurrentFiles)
            {
                try
                {
                    _FileRepsoitory.Add(FileToSave);
                    Rtn.Add(new FileUploadResponses()
                    {
                        FileName = FileToSave.FullName,
                        Message = "Successfully Uploaded",
                        WasSuccessfull = true
                    });
                }
                catch (Exception ex)
                {
                    Rtn.Add(new FileUploadResponses()
                    {
                        FileName = FileToSave.FullName,
                        Message = "Fialed To Upload - Error : " + ex.Message,
                        WasSuccessfull = true
                    });
                }
            }
            return Rtn;

        }
    }
}
