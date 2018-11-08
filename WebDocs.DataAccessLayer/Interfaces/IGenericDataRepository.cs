using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebDocs.DomainModels.Interfaces.Entities;
using WebDocs.DomainModels.TransactionResponse;

namespace WebDocs.DataAccessLayer.Interfaces
{
    public interface IGenericDataRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        Task<IList<T>> AsyncGetAll(params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        Task<CompletedTransactionResponses> AsyncAdd(params T[] items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        CompletedTransactionResponses Add(params T[] items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<CompletedTransactionResponses> AsyncUpdate(params T[] items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        CompletedTransactionResponses Update(params T[] items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        CompletedTransactionResponses Remove(params T[] items);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<CompletedTransactionResponses> AsyncRemove(params T[] items);
    }
}
