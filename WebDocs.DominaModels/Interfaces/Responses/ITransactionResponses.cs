using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocs.DomainModels.Interfaces.Responses
{
    public interface ITransactionResponses
    {
        string Message { get; set; }
        Boolean WasSuccessfull { get; set; }
    }
}
