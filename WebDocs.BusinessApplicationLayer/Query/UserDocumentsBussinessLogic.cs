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
    public class UserDocumentsBussinessLogic : IUserDocuments
    {

        private readonly IFileRepository _FileRepsoitory;

        public UserDocumentsBussinessLogic()
        {
            _FileRepsoitory = new FileRepository();
        }


        public List<FileUploadResponses> SaveUploadedUserFiles(List<FileModel> CurrentFiles)
        {

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

        public IList<FileModel> GetSelectedUserFiles(int UserID)
        {
            IList<FileModel> SelectedUserFiles = _FileRepsoitory.GetList(
                a => a.UserIDOfFileOwner == UserID,
                a => a.FileOwner,
                a => a.PersonThatLastUpdatedFile,
                a => a.FileViewStatuses,
                a => a.FileShareStatues);

            return SelectedUserFiles;
        }
    }
}
