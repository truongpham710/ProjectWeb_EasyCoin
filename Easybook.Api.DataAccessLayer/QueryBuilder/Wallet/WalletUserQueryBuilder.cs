using System.Linq;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System.Data.Entity;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletUserQueryBuilder : QueryBuilder<Wallet_User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletUserQueryBuilder"/> class.
        /// </summary>
        public WalletUserQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="userid">The identifier.</param>
        /// <returns></returns>
        public bool HasWalletId(string v_walletID)
        {
            return Query.Any(user => user.Wallet_ID == v_walletID);            
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="userid">The identifier.</param>
        /// <returns></returns>
        public bool HasUserId(string v_userID)
        {           
            return Query.Any(user => user.User_ID == v_userID);
        }
        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="userid">The identifier.</param>
        /// <returns></returns>
        public WalletUserQueryBuilder GetWalletIDByUserID(string UserID)
        {
            Query = Query.Where(cart => cart.User_ID.Equals(UserID));
            return this;
        }
        public WalletUserQueryBuilder GetAllUserWallet()
        {
            Query = Query.Where(cart => string.IsNullOrEmpty(cart.BlockChainID));
            return this;
        }
    }
}
