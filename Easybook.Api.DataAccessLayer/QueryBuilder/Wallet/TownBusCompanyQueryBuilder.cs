using System.Linq;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System.Data.Entity;
using Easybook.Api.Core.Model.EasyWallet.Models.TownBus;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class TownBusCompanyQueryBuilder : QueryBuilder<TownBusCompany>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TownBusCompanyQueryBuilder"/> class.
        /// </summary>
        public TownBusCompanyQueryBuilder(DbContext dbContext) : base(dbContext) { }

        public TownBusCompanyQueryBuilder GetCompanyByTownBusCompanyId(int townBusCompanyId)
        {
            Query = Query.Where(company => company.TownBus_Company_ID == townBusCompanyId);
            return this;            
        }

    }
}
