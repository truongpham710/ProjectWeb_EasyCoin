using System.Linq;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System.Data.Entity;
using Easybook.Api.Core.Model.EasyWallet.Models.TownBus;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class CompanyQueryBuilder : QueryBuilder<Company>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TownBusCompanyQueryBuilder"/> class.
        /// </summary>
        public CompanyQueryBuilder(DbContext dbContext) : base(dbContext) { }

        public CompanyQueryBuilder GetCompanyByCompanyId(int companyId)
        {
            Query = Query.Where(company => company.Company_ID == companyId);
            return this;            
        }

    }
}
