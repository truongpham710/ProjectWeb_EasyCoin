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
    public class UserBankAccountQueryBuilder : QueryBuilder<User_Bank_Account>
    {
    

        /// <summary>
        /// Initializes a new instance of the <see cref="WalletEntities"/> class.
        /// </summary>
        public UserBankAccountQueryBuilder(DbContext dbContext) : base(dbContext) { }      
       
        public UserBankAccountQueryBuilder GetInfoUserBankAccount(string accountID, string Currency )
        {
            Query = Query.Where(s => s.User_ID.Equals(accountID) &&  s.BankCurrency.Equals(Currency));
            return this;
        }

        public UserBankAccountQueryBuilder HasID(string ID)
        {
            Query = Query.Where(s => s.ID.Equals(ID));
            return this;
        }

        public UserBankAccountQueryBuilder GetInfoUserBankAccountByUserID(string UserID)
        {
            Query = Query.Where(s => s.User_ID.Equals(UserID));
            return this;
        }
        public UserBankAccountQueryBuilder GetBankAccwithoutPending()
        {
            Query = Query.Where(s => !s.Verify.Equals("Pending"));
            return this;
        }

        public UserBankAccountQueryBuilder GetUserBankAccountByIds(List<string> AccountIds)
        {
            Query = Query.Where(cart => AccountIds.Contains(cart.ID));
            return this;
        }

        public UserBankAccountQueryBuilder GetAllUserBankAccount()
        {
            Query = Query.OrderByDescending(s => s.Create_date);
            return this;
        }
        public UserBankAccountQueryBuilder UserBankAccount_ByID(string id)
        {
            Query = Query.Where(s => s.ID == id);
            return this;
        }
            

    }
}
