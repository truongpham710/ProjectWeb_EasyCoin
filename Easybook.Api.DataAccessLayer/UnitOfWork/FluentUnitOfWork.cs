using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Easybook.Api.DataAccessLayer.UnitOfWork
{
    /// <summary>
    /// 
    /// </summary>
    public class FluentUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        private DbContext Context { get; }

        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        /// <value>
        /// The transaction.
        /// </value>
        private DbContextTransaction Transaction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentUnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FluentUnitOfWork(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns></returns>
        public FluentUnitOfWork BeginTransaction()
        {
            Transaction = Context.Database.BeginTransaction();
            return this;
        }

        /// <summary>
        /// Begins the transaction with IsolationLevel.
        /// </summary>
        /// <returns></returns>
        public FluentUnitOfWork BeginTransaction(System.Data.IsolationLevel IsoLevel)
        {
            Transaction = Context.Database.BeginTransaction(IsoLevel);
            return this;
        }

        /// <summary>
        /// Does the insert.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public FluentUnitOfWork DoInsert<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Set<TEntity>().Add(entity);
            return this;
        }

        public FluentUnitOfWork DoInsertMany<TEntity>(List<TEntity> entities) where TEntity : class
        {
            Context.Set<TEntity>().AddRange(entities);
            return this;
        }

        /// <summary>
        /// Does the insert.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="inserted">The inserted.</param>
        /// <returns></returns>
        public FluentUnitOfWork DoInsert<TEntity>(TEntity entity, out TEntity inserted) where TEntity : class
        {
            inserted = Context.Set<TEntity>().Add(entity);
            return this;
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public FluentUnitOfWork DoUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Entry(entity).State = EntityState.Modified;
            return this;
        }

        /// <summary>
        /// Does the update.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public FluentUnitOfWork DoUpdateMany<TEntity>(List<TEntity> entities) where TEntity : class
        {    
            foreach(var entity in entities)
            {   
                Context.Entry(entity).State = EntityState.Modified;               
            }
            Context.SaveChanges();
            return this;
        }

        /// <summary>
        /// Does the delete.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public FluentUnitOfWork DoDelete<TEntity>(TEntity entity) where TEntity : class
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Set<TEntity>().Remove(entity);
            Context.SaveChanges();
            return this;
        }

        /// <summary>
        /// Saves the and continue.
        /// </summary>
        /// <returns></returns>
        public FluentUnitOfWork SaveAndContinue()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty, (current1, validationErrors) => validationErrors.ValidationErrors.Aggregate(current1, (current, validationError) => current + (Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}")));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            return this;
        }

        /// <summary>
        /// Ends the transaction.
        /// </summary>
        /// <returns></returns>
        public bool EndTransaction()
        {
            try
            {
                Context.SaveChanges();
                Transaction.Commit();
            }
            catch (DbEntityValidationException dbEx)
            {
                Transaction.Rollback();
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty, (current1, validationErrors) => validationErrors.ValidationErrors.Aggregate(current1, (current, validationError) => current + (Environment.NewLine + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}")));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            return true;
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        public void RollBack()
        {
            Transaction.Rollback();
            Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Transaction?.Dispose();
            Context?.Dispose();
        }
    }
}
