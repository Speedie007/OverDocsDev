using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WebDocs.DataAccessLayer.Interfaces;
using System.Data.Entity.Infrastructure;
using WebDocs.DomainModels.Interfaces.Entities;
using WebDocs.DomainModels.TransactionResponse;
using System.Data.Entity.Validation;

namespace WebDocs.DataAccessLayer
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class, IEntity
    {
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new WebDocsEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new WebDocsEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new WebDocsEntities())
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }

        public virtual CompletedTransactionResponses Add(params T[] items)
        {
            CompletedTransactionResponses CurrentResponse = Update(items);
            CurrentResponse.TransActionType = TransactionType.Insert;
            return CurrentResponse;
        }

        public virtual CompletedTransactionResponses Remove(params T[] items)
        {
            CompletedTransactionResponses CurrentResponse = Update(items);
            CurrentResponse.TransActionType = TransactionType.Delete;
            return CurrentResponse;
        }

        public virtual CompletedTransactionResponses Update(params T[] items)
        {
            CompletedTransactionResponses CurrentResponse = new CompletedTransactionResponses()
            {
                Message = "",
                TransActionType = TransactionType.Update,
                 WasSuccessfull = false
            };
            using (var context = new WebDocsEntities())
            {

                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DbSet<T> dbSet = context.Set<T>();
                        foreach (T item in items)
                        {
                            dbSet.Add(item);
                            foreach (DbEntityEntry<IEntity> entry in context.ChangeTracker.Entries<IEntity>())
                            {
                                IEntity entity = entry.Entity;
                                entry.State = GetEntityState(entity.EntityState);
                            }
                        }
                        context.SaveChanges();

                        transaction.Commit();
                        CurrentResponse.WasSuccessfull = true;
                        CurrentResponse.Message = "Transaction Successfully Completed.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        CurrentResponse.Message = "Error occurred. - " + ex.Message;
                    }
                }
            }
            return CurrentResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityState"></param>
        /// <returns></returns>
        protected static System.Data.Entity.EntityState GetEntityState(WebDocs.DomainModels.EntityState entityState)
        {
            switch (entityState)
            {
                case DomainModels.EntityState.Unchanged:
                    return System.Data.Entity.EntityState.Unchanged;
                case DomainModels.EntityState.Added:
                    return System.Data.Entity.EntityState.Added;
                case DomainModels.EntityState.Modified:
                    return System.Data.Entity.EntityState.Modified;
                case DomainModels.EntityState.Deleted:
                    return System.Data.Entity.EntityState.Deleted;
                default:
                    return System.Data.Entity.EntityState.Detached;
            }
        }
    }
}
