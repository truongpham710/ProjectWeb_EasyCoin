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
    public class WalletTownbusQueryBuilder : QueryBuilder<TownBusTrip>
    {
      
        public WalletTownbusQueryBuilder(DbContext dbContext) : base(dbContext) { }      

        /// <summary>
        /// Determines whether the specified cart has unique identifier.
        /// </summary>
        /// <param name="WalletID">The cart unique identifier.</param>
        /// <returns></returns>
        public WalletTownbusQueryBuilder GetTownbusInfoByTripID(int TripID)
        {
            Query = Query.Where(cart => cart.TownBus_Trip_ID == TripID);
            return this;
        }
        public WalletTownbusQueryBuilder GetTownbusInfoByCarIDnDeparttime(int carID,int companyID)
        {
            Query = Query.Where(cart => cart.Bus_ID == carID && cart.Company_ID==companyID);
            return this;
        }

      
    }
}
