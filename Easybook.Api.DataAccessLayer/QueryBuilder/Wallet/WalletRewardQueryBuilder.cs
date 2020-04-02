using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletRewardQueryBuilder : QueryBuilder<Wallet_Account_Reward>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletRewardQueryBuilder"/> class.
        /// </summary>
        public WalletRewardQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Country_Code">The identifier.</param>
        /// <returns></returns>
        public WalletRewardQueryBuilder GetTranNearestDateByAccID(string AccID)
        {
            Query = Query.Where(p => p.ID == AccID).OrderByDescending(p => p.Createdate).Take(1);
            return this;
        }

        public WalletRewardQueryBuilder GetRewardByAccID(string AccID)
        {
            Query = Query.Where(p => p.ID == AccID);
            return this;
        }

        public WalletRewardQueryBuilder GetRewardWithAmount()
        {
            Query = Query.Where(p => p.Reward_Amount > 0);
            return this;
        }

        public WalletRewardQueryBuilder GetAllRewardAcc()
        {
            Query = Query.Where(p => !string.IsNullOrEmpty(p.ID) );
            return this;
        }

        public WalletRewardQueryBuilder GetRewardByWalletID(string WalletID)
        {
            Query = Query.Where(p => p.Wallet_ID == WalletID);
            return this;
        }

        public WalletRewardQueryBuilder HaveIds(List<string> Ids)
        {
            Query = Query.Where(r => Ids.Contains(r.ID));
            return this;
        }

        public WalletRewardQueryBuilder GotBalance()
        {
            Query = Query.Where(r => r.Reward_Amount > decimal.Zero);
            return this;
        }

        public int GetCountRewardTransaction(System.DateTime pDateStart)
        {
            return Query.Where(p=>p.Createdate >= pDateStart).Count();
        }

        public WalletRewardQueryBuilder GetRewardWithAccsAmount(List<string> Accounts)
        {
            Query = Query.Where(p => p.Reward_Amount > 0 && Accounts.Contains(p.ID));
            return this;
        }
    }
}
