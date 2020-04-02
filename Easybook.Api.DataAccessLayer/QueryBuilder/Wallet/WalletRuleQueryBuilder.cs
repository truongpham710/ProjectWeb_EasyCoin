using System.Linq;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System.Data.Entity;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletRuleQueryBuilder : QueryBuilder<Wallet_Rule>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletEntities"/> class.
        /// </summary>
        public WalletRuleQueryBuilder(DbContext dbContext) : base(dbContext) { }      

        /// <summary>
        /// Determines whether the specified cart has unique identifier.
        /// </summary>
        /// <param name="WalletID">The cart unique identifier.</param>
        /// <returns></returns>
        public WalletRuleQueryBuilder GetWalletByID(string AccountID)
        {
            Query = Query.Where(cart => cart.Account_ID.Equals(AccountID));
            return this;
        }      
    }
}
