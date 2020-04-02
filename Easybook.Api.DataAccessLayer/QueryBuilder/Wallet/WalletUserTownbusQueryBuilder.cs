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
    public class WalletUserTownbusQueryBuilder : QueryBuilder<Core.Model.EasyWallet.Models.TownBus.User>
    {
      
        public WalletUserTownbusQueryBuilder(DbContext dbContext) : base(dbContext) { }      

        /// <summary>
        /// Determines whether the specified cart has unique identifier.
        /// </summary>
        /// <param name="WalletID">The cart unique identifier.</param>
        /// <returns></returns>
        public WalletUserTownbusQueryBuilder GetUserByLoginIdnPassword(string login,string password)
        {
            Query = Query.Where(user => user.Login_ID.Equals(login)&& user.Password.Equals(password));
            return this;
        }     

      
    }
}
