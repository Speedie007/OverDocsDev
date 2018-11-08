using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Database;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.BusinessApplicationLayer.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBusinessLayer<T> where T : class
    {
        CompletedTransactionResponses CTR { get; set; }
    }
}
