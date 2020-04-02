using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Easybook.Api.DataAccessLayer.QueryBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class QueryBuilder<TEntity> : IDisposable where TEntity : class, new()
    {
        protected DbContext Context { get; set; }

        /// <summary>
        /// The query
        /// </summary>
        protected IQueryable<TEntity> Query;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{TEntity}"/> class.
        /// </summary>
        protected QueryBuilder(DbContext context)
        {
            Context = context;
            Query = Context.Set<TEntity>();
        }

        ///// <summary>
        ///// Performs an implicit conversion from <see cref="QueryBuilder{TEntity}"/> to <see cref="List{TEntity}"/>.
        ///// </summary>
        ///// <param name="queryBuilder">The query builder.</param>
        ///// <returns>
        ///// The result of the conversion.
        ///// </returns>
        //public static implicit operator List<TEntity>(QueryBuilder<TEntity> queryBuilder)
        //{
        //    return Query.ToList();
        //}

        ///// <summary>
        ///// Performs an implicit conversion from <see cref="QueryBuilder{TEntity}"/> to <see cref="TEntity"/>.
        ///// </summary>
        ///// <param name="queryBuilder">The query builder.</param>
        ///// <returns>
        ///// The result of the conversion.
        ///// </returns>
        //public static implicit operator TEntity(QueryBuilder<TEntity> queryBuilder)
        //{
        //    return Query.FirstOrDefault();
        //}

        /// <summary>
        /// To the list.
        /// </summary>
        /// <returns></returns>
        public List<TEntity> ToList()
        {
            return Query.ToList();
        }

        public IEnumerable<TEntity> AsEnumerable()
        {
            return Query.AsEnumerable();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return Query;
        }

        public QueryBuilder<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            foreach (var includeExpression in includeExpressions)
            {
                Query = Query.Include(includeExpression);
            }

            return this;
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return Query.Count();
        }

        /// <summary>
        /// Counts the specified count expression.
        /// </summary>
        /// <param name="countExpression">The count expression.</param>
        /// <returns></returns>
        public int Count(Func<TEntity, bool> countExpression)
        {
            return Query.Count(countExpression);
        }

        /// <summary>
        /// Firsts the or default.
        /// </summary>
        /// <returns></returns>
        public TEntity FirstOrDefault()
        {
            return Query.FirstOrDefault();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Context.Dispose();
        }

        /// <summary>
        /// Selects required column as anonymous variable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        public IQueryable<T> SelectAsAnonymous<T>(Expression<Func<TEntity, T>> selector)
        {
            return Query.Select(selector);
        }

        public T Cast<T>(object currentObject, T type)
        {
            return (T) currentObject;
        }
    }

    public class Integer
    {
        public int Value { get; set; }
    }
}
