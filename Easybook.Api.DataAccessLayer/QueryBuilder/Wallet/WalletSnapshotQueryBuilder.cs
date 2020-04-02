using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{

    public class WalletSnapshotQueryBuilder : QueryBuilder<Wallet_Snapshot>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrenciesQueryBuilder"/> class.
        /// </summary>
        public WalletSnapshotQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="pAccountID">The pAccountID.</param>
        /// <returns></returns>
        public WalletSnapshotQueryBuilder HasAccID(string pAccountID)
        {
            Query = Query.Where(p => p.Account_ID == pAccountID);
            return this;
        }
        public WalletSnapshotQueryBuilder GetAllSnapshot()
        {           
            return this;
        }
        public WalletSnapshotQueryBuilder HasLatestAccID(string pAccountID)
        {
            Query = Query.Where(p => p.Account_ID == pAccountID).OrderByDescending(p=>p.CreateDateSnapshot).Take(1);
            return this;
        }

        public WalletSnapshotQueryBuilder GetSnapshotByCheckSum(string pCheckSum)
        {
            Query = Query.Where(p => p.Snapshot == pCheckSum).OrderBy(p => p.Account_ID);
            return this;
        }
        public WalletSnapshotQueryBuilder GetLatestDateSnapshot()
        {
            Query = Query.OrderByDescending(p => p.CreateDateSnapshot).Take(1);
            return this;
        }
        public WalletSnapshotQueryBuilder HasSnapshotChecksum(string snapshotChecksum)
        {
            Query = Query.Where(p => p.Snapshot == snapshotChecksum);
            return this;
        }

        public WalletSnapshotQueryBuilder HasSnapshotChecksumWithMaxDate(string snapshotChecksum)
        {
            DateTime maxDate = Query.Max(s => s.CreateDateSnapshot);
            Query = Query.Where(p => p.Snapshot == snapshotChecksum && p.CreateDateSnapshot.Date == maxDate.Date);
            return this;
        }

        public WalletSnapshotQueryBuilder GetAllSnapshotWithNearestDate()
        {
            var Date = DateTime.Now.AddDays(-3);
            Query = Query.Where(p => p.CreateDateSnapshot > Date).OrderBy(p => p.Account_ID);
            return this;
        }

        public DateTime GetMaxDate()
        {
            return Query.Max(s => s.CreateDateSnapshot);
        }

        public Wallet_Snapshot GetMaxDateSnapshot()
        {
            //return Query.SingleOrDefault(s => s.CreateDateSnapshot == Query.Max(ss => ss.CreateDateSnapshot));
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 15);
            return Query.Where(p => !string.IsNullOrEmpty(p.Snapshot) && p.CreateDateSnapshot < fromDate).OrderByDescending(s => s.CreateDateSnapshot).FirstOrDefault();            
        }

        public Wallet_Snapshot GetMaxDateSnapshotByAccID(string AccID)
        {

            //return Query.SingleOrDefault(s => s.CreateDateSnapshot == Query.Max(ss => ss.CreateDateSnapshot));
            return Query.Where(p => p.Account_ID == AccID).OrderByDescending(s => s.CreateDateSnapshot).FirstOrDefault();
        }

        public WalletSnapshotQueryBuilder HasAccIDs(List<string> accounts)
        {
            Query = Query.Where(p => accounts.Contains(p.Account_ID));
            return this;
        }

        public List<Tuple<Wallet_Snapshot, Wallet_Account>> GetSnapshotByCheckSumWithAccountInfo(string pCheckSum)
        {
            var lstSnapshotWithAccount = ((WalletEntities)Context).Wallet_Snapshot.Join(((WalletEntities)Context).Wallet_Account,
                s => s.Account_ID, a => a.ID, (s, a) => new { Ss = s, Account = a })
                        .Where(sa => (sa.Ss.Snapshot == pCheckSum)).ToList();
            return lstSnapshotWithAccount.Select(sa => new Tuple<Wallet_Snapshot, Wallet_Account>(sa.Ss, sa.Account)).ToList();
        }
    }

 
}
