using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Interfaces.Responses;

namespace WebDocs.DomainModels.TransactionResponse
{
    public class CompletedTransactionResponses : ITransactionResponses
    {
        public TransactionType TransActionType { get; set; }
        public string Message { get; set; }
        public bool WasSuccessfull { get; set; }
    }

    public enum TransactionType
    {
        Insert,
        Update,
        Delete
    }

}
