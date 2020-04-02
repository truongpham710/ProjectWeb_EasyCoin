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
    public class WalletTownBusCoordinateQueryBuilder : QueryBuilder<Core.Model.EasyWallet.Models.TownBus.TownBusCoordinate>
    {
      
        public WalletTownBusCoordinateQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// GetTownBusCoordinateByCoordinateID
        /// </summary>
        /// <param name="Coordinate_ID"></param>
        /// <returns></returns>
        public WalletTownBusCoordinateQueryBuilder GetTownBusCoordinateByCoordinateID(int Coordinate_ID)
        {
            Query = Query.Where(station => station.TownBus_Coordinate_ID == Coordinate_ID);
            return this;
        }


    }
}
