using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionInterestSnapshotQueryBuilder : QueryBuilder<Transaction_Interest_Snapshot>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionInterestSnapshotQueryBuilder"/> class.
        /// </summary>
        public TransactionInterestSnapshotQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Country_Code">The identifier.</param>
        /// <returns></returns>
        public TransactionInterestSnapshotQueryBuilder GetTranNearestDateByAccID(string AccID)
        {
            Query = Query.Where(p => p.Account_ID == AccID).OrderByDescending(p => p.Createdate).Take(1);
            return this;
        }

    }
}
