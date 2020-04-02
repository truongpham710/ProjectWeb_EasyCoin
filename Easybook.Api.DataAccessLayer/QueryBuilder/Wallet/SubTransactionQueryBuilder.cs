using System.Data.Entity;
using System.Linq;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System;
using System.Collections.Generic;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class SubTransactionQueryBuilder : QueryBuilder<SubTransaction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionQueryBuilder"/> class.
        /// </summary>
        public SubTransactionQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="SubTransaction">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetSubTransactionBySubID(string SubTranID)
        {
            Query = Query.Where(p => p.Sub_ID == SubTranID);
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetSubTransactionByTranID(string TranID)
        {
            Query = Query.Where(p => p.Tran_ID == TranID);
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction">The identifier.</param>
        /// <returns></returns>
        public bool IsExistTranIDinSubTransaction(string TranID)
        {
            return Query.Any(p => p.Tran_ID == TranID); 
        }          

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetSubTransByUserIDnMonth(string UserID, string CurrencyCode, System.DateTime DateSnapshot, System.DateTime lastDayOfMonth)
        {
            //Query = Query.Where(p => p.User_ID == UserID && p.Currency_Code == CurrencyCode && p.CreateDate > DateSnapshot && p.CreateDate <= lastDayOfMonth && (p.Remarks != "REWARD") && !(p.Remarks.Contains("Deducted from REWARD")) && (p.Remarks != "REBATE FOR FLIGHT") && (p.Remarks != "Referral Reward"));
            Query = Query.Where(p => p.User_ID == UserID && p.Currency_Code == CurrencyCode && p.CreateDate > DateSnapshot && p.CreateDate <= lastDayOfMonth && !EwalletConstant.Rewardstring.Contains(p.Remarks));
            return this; 
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetSubTranForFirstTopupByCurrency(string UserID, string CurrencyCode)
        {
            Query = Query.Where(p => p.User_ID == UserID && p.Currency_Code == CurrencyCode).OrderBy(p => p.Sequent_ID);
            return this;
        }

        public SubTransactionQueryBuilder GetAllSubTranByUserID(string UserID)
        {
            Query = Query.Where(p => p.User_ID.Trim() == UserID);
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>

        public SubTransactionQueryBuilder GetSubTransByMonth(System.DateTime pDateTime)
        {
            
            //Query = Query.Where(p =>p.CreateDate >= pDateTime && (p.Remarks != "REWARD") && !(p.Remarks.Contains("Deducted from REWARD")) && (p.Remarks != "REBATE FOR FLIGHT") && (p.Remarks != "Referral Reward"));
            Query = Query.Where(p => p.CreateDate >= pDateTime && !EwalletConstant.Rewardstring.Contains(p.Remarks.ToLower()));
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<SubTransaction> GetSubTransByUserIDnCurrencyCode(string pUserID, string CurrencyCode)
        {
            //IEnumerable<SubTransaction> lstSubTrans = Query.AsEnumerable().Where(p => p.User_ID == pUserID && (p.CreateDate.DayOfYear - pDateFrom.DayOfYear) >= 0 && (p.CreateDate.DayOfYear - pDateTo.DayOfYear) <= 0 && p.Currency_Code == CurrencyCode && (p.Remarks != "REWARD") && !(p.Remarks.Contains("Deducted from REWARD")) && (p.Remarks != "REBATE FOR FLIGHT") && (p.Remarks != "Referral Reward"));
            //IEnumerable<SubTransaction> lstSubTrans = Query.AsEnumerable().Where(p => p.User_ID == pUserID && p.Currency_Code == CurrencyCode && (p.Remarks != "REWARD") && !(p.Remarks.Contains("Deducted from REWARD")) && (p.Remarks != "REBATE FOR FLIGHT") && (p.Remarks != "Referral Reward"));
            IEnumerable<SubTransaction> lstSubTrans = Query.AsEnumerable().Where(p => p.User_ID == pUserID && p.Currency_Code == CurrencyCode && !EwalletConstant.Rewardstring.Contains(p.Remarks.ToLower()));
            return lstSubTrans;
        }

        public IEnumerable<SubTransaction> GetSumDailyByUserIDnCurrencynDate(string pUserID, string CurrencyCode, System.DateTime pDateTime)
        {
            //IEnumerable<SubTransaction> lstSubTrans = Query.AsEnumerable().Where(p => p.User_ID == pUserID && (p.CreateDate.DayOfYear - pDateFrom.DayOfYear) >= 0 && (p.CreateDate.DayOfYear - pDateTo.DayOfYear) <= 0 && p.Currency_Code == CurrencyCode && (p.Remarks != "REWARD") && !(p.Remarks.Contains("Deducted from REWARD")) && (p.Remarks != "REBATE FOR FLIGHT") && (p.Remarks != "Referral Reward"));
            //IEnumerable<SubTransaction> lstSubTrans = Query.AsEnumerable().Where(p => p.User_ID == pUserID && p.CreateDate < pDateTime && p.Currency_Code == CurrencyCode && (p.Remarks != "REWARD") && !(p.Remarks.Contains("Deducted from REWARD")) && (p.Remarks != "REBATE FOR FLIGHT") && (p.Remarks != "Referral Reward"));
            IEnumerable<SubTransaction> lstSubTrans = Query.AsEnumerable().Where(p => p.User_ID == pUserID && p.Currency_Code == CurrencyCode && !EwalletConstant.Rewardstring.Contains(p.Remarks.ToLower()));
            return lstSubTrans;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetAllSubTran(DateTime pDateTime)
        {
            Query = Query.Where(p => p.CreateDate > pDateTime).OrderBy(p=>p.Sequent_ID);
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetAllSubTran()
        {   
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetSubTranByTran_Amt(string pTranID, decimal Amt)
        {
            Query = Query.Where(p => p.Tran_ID == pTranID && p.Amount == Amt ) ;
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public SubTransactionQueryBuilder GetSubTransByUserID(string UserID, System.DateTime pDateFrom, System.DateTime pDateTo, string pTransactionType, string pCurrencyCode)
        {          
            if (string.IsNullOrEmpty(pTransactionType))
            {
                Query = Query.Where(p => p.User_ID == UserID && p.Currency_Code == pCurrencyCode && p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo).OrderByDescending(x => x.CreateDate);
            }
            else
            {
                Query = Query.Where(p => p.User_ID == UserID && p.Currency_Code == pCurrencyCode && p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo && p.Direction.Equals(pTransactionType)).OrderByDescending(x => x.CreateDate);
            }
            return this;
        }
        
        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="UserID">The identifier.</param>
        /// <returns></returns>
        public string GetLastCheckSum1()
        {
            var chkSum1 = "";
            //if (Query.Count() == 0)
            //    return chkSum1;
            chkSum1 = Query.OrderByDescending(p => p.Sequent_ID).First().Checksum1;
            return chkSum1;
        }
     

        public SubTransactionQueryBuilder GetSubTransactionsAfterSnapshotWithLastTrans(string lastTransactionId, DateTime? snapshotDate)
        {
            if(snapshotDate != null)
                Query = Query.Where(p => p.CreateDate >= snapshotDate.Value || p.Sub_ID == lastTransactionId);
            return this;
        }

        public SubTransactionQueryBuilder FromDate(DateTime pDateTime)
        {
            Query = Query.Where(p => p.CreateDate >= pDateTime);
            return this;
        }

        public SubTransactionQueryBuilder ExcludeReward(List<string> rewards)
        {
            Query = Query.Where(s => !rewards.Any(x => s.Remarks.ToLower().Contains(x)));
            return this;
        }

        public SubTransactionQueryBuilder GetListUserIDWithInsertedCashBonusInCurrentMonth()
        {
            DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Query = Query.Where(p => p.CreateDate >= fromDate && p.Remarks.Contains("Inserted live Cash Bonus"));
            return this;
        }
    }
}
