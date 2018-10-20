using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Interfaces.Responses;

namespace WebDocs.DomainModels.TransactionResponse
{
    public class FileUploadResponses : ITransactionResponses
    {
        public string FileName { get; set; }
        public string Message { get; set; }
        public bool WasSuccessfull { get; set; }
    }
}
