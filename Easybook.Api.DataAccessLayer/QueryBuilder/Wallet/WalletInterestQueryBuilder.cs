using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletInterestQueryBuilder : QueryBuilder<Wallet_Interest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletInterestQueryBuilder"/> class.
        /// </summary>
        public WalletInterestQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Country_Code">The identifier.</param>
        /// <returns></returns>
        public WalletInterestQueryBuilder HasCountry_Code(string Country_Code)
        {
            Query = Query.Where(p => p.Country_Code == Country_Code);
            return this;
        }
        public WalletInterestQueryBuilder GetAllWalletInterest()
        {          
            return this;
        }

    }
}
