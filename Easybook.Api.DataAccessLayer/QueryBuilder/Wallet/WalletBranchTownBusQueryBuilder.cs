using Easybook.Api.Core.Model.EasyWallet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    public class WalletBranchTownBusQueryBuilder : QueryBuilder<Branch>
    {
        public WalletBranchTownBusQueryBuilder(DbContext dbContext) : base(dbContext) { }
        /// <summary>
        /// HasBranchId
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public WalletBranchTownBusQueryBuilder HasBranchId(int branchID)
        {
            Query = Query.Where(cart => cart.Branch_ID == branchID);
            return this;
        }
        
    }
}
