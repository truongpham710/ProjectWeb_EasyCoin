using System.Linq;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletAccountQueryBuilder : QueryBuilder<Wallet_Account>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalletEntities"/> class.
        /// </summary>
        public WalletAccountQueryBuilder(DbContext dbContext) : base(dbContext) { }      

        /// <summary>
        /// Determines whether the specified cart has unique identifier.
        /// </summary>
        /// <param name="WalletID">The cart unique identifier.</param>
        /// <returns></returns>
        public WalletAccountQueryBuilder GetWalletByID(string AccountID)
        {
            Query = Query.Where(cart => cart.ID.Equals(AccountID));
            return this;
        }

        public WalletAccountQueryBuilder GetWalletByIDs(List<string> accs)
        {
            Query = Query.Where(cart => accs.Contains(cart.ID));
            return this;
        }

        public WalletAccountQueryBuilder HasWalletCurrencies(List<string> walletCurrencies)
        {
            Query = Query.Where(c => walletCurrencies.Contains(c.Wallet_ID + "_" + c.Currency_Code));
            return this;
        }

        public WalletAccountQueryBuilder GetWalletByWalletID(string WalletID)
        {
            Query = Query.Where(cart => cart.Wallet_ID.Equals(WalletID));
            return this;
        }

        public WalletAccountQueryBuilder GetWalletByUserID(string UserID)
        {
            Query = Query.Where(cart => cart.User_ID.Equals(UserID));
            return this;
        }

        public WalletAccountQueryBuilder GetWalletByUserIDs(List<string> UserIDs)
        {
            Query = Query.Where(cart => UserIDs.Contains(cart.User_ID));
            return this;
        }

        public WalletAccountQueryBuilder GetWalletByUserIDsOrAccounts(List<string> UserIDs, List<string> Accounts)
        {
            Query = Query.Where(acc => UserIDs.Contains(acc.User_ID + "_" + acc.Currency_Code) || Accounts.Contains(acc.ID));
            return this;
        }

        public WalletAccountQueryBuilder GetAccIdByWalletIDnCurrencyCode(string WalletID, string CurrencyCode)
        {
            Query = Query.Where(cart => cart.Wallet_ID.Equals(WalletID) && cart.Currency_Code.ToLower().Equals(CurrencyCode.Trim().ToLower()));
            return this;
        }

        public WalletAccountQueryBuilder GetAccIdByUserIDnCurrencyCode(string UserID, string CurrencyCode)
        {
            Query = Query.Where(cart => cart.User_ID.Equals(UserID) && cart.Currency_Code.ToLower().Equals(CurrencyCode.Trim().ToLower()));
            return this;
        }      
        public WalletAccountQueryBuilder GetAllWalletAccountByDate(DateTime pDateTime)
        {
            Query = Query.Where(cart => cart.CreateDate.Year == pDateTime.Year && cart.CreateDate.Month == pDateTime.Month && cart.CreateDate.Day== pDateTime.Day);
            return this;
        }
        public WalletAccountQueryBuilder GetAllWalletAccountByDateWithoutCS(DateTime pDateTime)
        {
            Query = Query.Where(cart => cart.CreateDate.Year == pDateTime.Year && cart.CreateDate.Month == pDateTime.Month && cart.CreateDate.Day == pDateTime.Day && cart.ChecksumAvailable1 == "") ;
            return this;
        }
        public WalletAccountQueryBuilder GetAllWalletAccount()
        {
            Query = Query.Where(cart => !cart.ChecksumAvailable1.Equals(""));
            return this;
        }

        public WalletAccountQueryBuilder GetCurrencyByUserID(string pUserID)
        {
            Query = Query.Where(cart => cart.User_ID == pUserID && cart.Available_Balance > 0).OrderBy(cart => cart.Currency_Code);
            return this;
        }

    }
}
