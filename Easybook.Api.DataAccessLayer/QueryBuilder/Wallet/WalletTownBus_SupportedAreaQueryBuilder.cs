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
    public class WalletTownBus_SupportedAreaQueryBuilder : QueryBuilder<Core.Model.EasyWallet.Models.TownBus.TownBus_SupportedArea>
    {

        public WalletTownBus_SupportedAreaQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// GetTownBusCoordinateByCoordinateID
        /// </summary>
        /// <param name="Coordinate_ID"></param>
        /// <returns></returns>
        public WalletTownBus_SupportedAreaQueryBuilder GetTownBus_SupportedAreaByStatus()
        {
            Query = Query.Where(p => p.Status != "d").OrderByDescending(s => s.Create_Date);
            return this;
        }


    }
}
