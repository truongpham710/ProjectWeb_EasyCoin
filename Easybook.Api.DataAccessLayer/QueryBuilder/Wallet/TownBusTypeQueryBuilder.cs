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
    public class TownBusTypeQueryBuilder : QueryBuilder<Core.Model.EasyWallet.Models.TownBus.TownBusType>
    {
      
        public TownBusTypeQueryBuilder(DbContext dbContext) : base(dbContext) { }      

        /// <summary>
        /// Determines whether the specified cart has unique identifier.
        /// </summary>
        /// <param name="WalletID">The cart unique identifier.</param>
        /// <returns></returns>
        public TownBusTypeQueryBuilder GetBusIdByCompanyIdnBusNo(int companyId,string busNo)
        {
            Query = Query.Where(bus => bus.Company_ID==companyId && bus.Bus_No==busNo).Take(1);
            return this;
        }     

      
    }
}
