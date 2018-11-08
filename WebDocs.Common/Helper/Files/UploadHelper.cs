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
        public static FileUploadResponses SaveUploadedFile(FileModel CurrentFile)
        {

            IFileRepository _FileRepsoitory = new FileRepository();
            FileUploadResponses Rtn;

            try
            {
               CompletedTransactionResponses CTR =  _FileRepsoitory.Add(CurrentFile);
                if (CTR.WasSuccessfull)
                {
                    Rtn = new FileUploadResponses()
                    {
                        FileName = CurrentFile.FullFileName,
                        Message = "Successfully Saved",
                        WasSuccessfull = true
                    };
                }
                else
                {
                    Rtn = new FileUploadResponses()
                    {
                        FileName = CurrentFile.FullFileName,
                        Message = "Failed To Upload - Error : " + CTR.Message,
                        WasSuccessfull = false
                    };
                }
               
            }
            catch (Exception ex)
            {
                Rtn = new FileUploadResponses()
                {
                    FileName = CurrentFile.FullFileName,
                    Message = "Failed To Upload - Error : " + ex.Message,
                    WasSuccessfull = false
                };
            }
            return Rtn;
        }

        public static FileUploadResponses SaveArchivedFiles(FileArchiveModel CurrentFile)
        {

            IFileArchiveRepository _FileArchiveRepository = new FileArchiveRepository();
            FileUploadResponses Rtn;

            try
            {
                CompletedTransactionResponses CTR = _FileArchiveRepository.Add(CurrentFile);
                if (CTR.WasSuccessfull)
                {
                    Rtn = new FileUploadResponses()
                    {
                        FileName = CurrentFile.FullFileName,
                        Message = "Successfully Saved",
                        WasSuccessfull = true
                    };
                }
                else
                {
                    Rtn = new FileUploadResponses()
                    {
                        FileName = CurrentFile.FullFileName,
                        Message = "Failed To Upload - Error : " + CTR.Message,
                        WasSuccessfull = false
                    };
                }

            }
            catch (Exception ex)
            {
                Rtn = new FileUploadResponses()
                {
                    FileName = CurrentFile.FullFileName,
                    Message = "Failed To Upload - Error : " + ex.Message,
                    WasSuccessfull = false
                };
            }
            return Rtn;

        }

        public static FileUploadResponses UpdateUploadedFiles(FileModel CurrentFile)
        {

            IFileRepository _FileRepsoitory = new FileRepository();
            FileUploadResponses Rtn;

            try
            {
                CompletedTransactionResponses CTR = _FileRepsoitory.Update(CurrentFile);
                if (CTR.WasSuccessfull)
                {
                    Rtn = new FileUploadResponses()
                    {
                        FileName = CurrentFile.FullFileName,
                        Message = "Successfully Saved",
                        WasSuccessfull = true
                    };
                }
                else
                {
                    Rtn = new FileUploadResponses()
                    {
                        FileName = CurrentFile.FullFileName,
                        Message = "Failed To Upload - Error : " + CTR.Message,
                        WasSuccessfull = false
                    };
                }

            }
            catch (Exception ex)
            {
                Rtn = new FileUploadResponses()
                {
                    FileName = CurrentFile.FullFileName,
                    Message = "Failed To Upload - Error : " + ex.Message,
                    WasSuccessfull = false
                };
            }
            return Rtn;

        }
    }
}
