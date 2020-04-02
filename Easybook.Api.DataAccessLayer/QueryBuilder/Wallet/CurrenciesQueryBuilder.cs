using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrenciesQueryBuilder : QueryBuilder<Currency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrenciesQueryBuilder"/> class.
        /// </summary>
        public CurrenciesQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="CurrencyCode">The identifier.</param>
        /// <returns></returns>
        public CurrenciesQueryBuilder HasCurrencyCode(string CurrencyCode)
        {
            Query = Query.Where(p => p.Currency_Code == CurrencyCode);
            return this;
        }

        /// <summary>
        /// Determines whether [has Currency name] [the specified Currency name].
        /// </summary>
        /// <param name="CurrencyName">Name of the user.</param>
        /// <returns></returns>
        public CurrenciesQueryBuilder HasCurrencyName(string CurrencyName)
        {
            Query = Query.Where(p => p.Currency_Name == CurrencyName);
            return this;
        }
    }
}
