using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System;

namespace Easybook.Api.DataAccessLayer.QueryBuilder.Wallet
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionQueryBuilder : QueryBuilder<Transaction>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionQueryBuilder"/> class.
        /// </summary>
        public TransactionQueryBuilder(DbContext dbContext) : base(dbContext) { }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTranByTranID(string TranID)
        {
            Query = Query.Where(p => p.Tran_ID == TranID);
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTransBQ()
        {
            Query = Query.Where(p => p.Scheduler_Check == "YES" && p.Status == "NULL").OrderByDescending(p => p.CreateDate).Take(4);
            return this;
        }

        public int CountRewardTransaction(DateTime pDateTime)
        {
            return Query.Where(p => p.Description == "REWARD" && p.Status.ToLower() == "true" && p.CreateDate >= pDateTime).Count();
        }

        public int CountRewardTransactionByDatenUserID(DateTime pDateTime, string pUserID)
        {
            return Query.Where(p => p.Description == "REWARD" && p.User_ID == pUserID && p.Status.ToLower() == "true" && p.CreateDate >= pDateTime).Count();
        }
        

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTransByTranIDs(List<string> lstTranIDs)
        {
            Query = Query.Where(p => lstTranIDs.Contains(p.Tran_ID));
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTransWithdraw()
        {
            Query = Query.Where(p => p.Description == "WITHDRAW" && (string.IsNullOrEmpty(p.Status) || p.Status == "NULL" || p.Status == "pending"));
            return this;
        }
         

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTransWithdrawWithVerified()
        {
            Query = Query.Where(p => p.Description == "WITHDRAW" && p.Status.ToLower() == "true");
            return this;
        }

        public TransactionQueryBuilder GetTransWithdrawWithCancel()
        {
            Query = Query.Where(p => p.Description == "WITHDRAW" && p.Status.ToLower() == "canceled");
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTranWithdrawByUserIDNoVerify(string pUserID, string pCurrencyCode)
        {
            Query = Query.Where(p => p.Description == "WITHDRAW" && p.User_ID == pUserID && p.Source_Currency == pCurrencyCode && (string.IsNullOrEmpty(p.Status) || p.Status == "NULL" || p.Status == "pending"));
            return this;
        }

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTranTownBusnWithdrawByUserIDNoVerify(string pUserID, string pCurrencyCode)
        {
            Query = Query.Where(p =>  p.Description == "WITHDRAW" && p.User_ID == pUserID && p.Source_Currency == pCurrencyCode && (string.IsNullOrEmpty(p.Status) || p.Status == "NULL" || p.Status == "pending"));
            return this;
        }       

        /// <summary>
        /// Determines whether the specified identifier has identifier.
        /// </summary>
        /// <param name="Transaction ID">The identifier.</param>
        /// <returns></returns>
        public TransactionQueryBuilder GetTransTopup()
        {
            Query = Query.Where(p => p.Description == "TOPUP" && (string.IsNullOrEmpty(p.Status) || p.Status == "NULL") && p.Scheduler_Check != "YES");
            return this;
        }
       

        public TransactionQueryBuilder HasRemark(string remark)
        {
            Query = Query.Where(p => p.Remarks == remark);
            return this;
        }

        public TransactionQueryBuilder FromDate(DateTime? fromDate)
        {
            Query = Query.Where(p => p.CreateDate >= fromDate);
            return this;
        }

        public TransactionQueryBuilder ToDate(DateTime? toDate)
        {
            Query = Query.Where(p => p.CreateDate < toDate);
            return this;
        }
       
        public decimal GetTotalTopupByUserIDnCurrency(string pUserID, string pCurrencyCode)
        {
            return Query.Where(p => p.Description == "TOPUP" && p.User_ID == pUserID && p.Source_Currency == pCurrencyCode && p.Status == "true").Sum(p=>p.Source_Amount)??0;
        }

        public int GetNumberofTimeTopupByUserID(string pUserID)
        {
            return Query.Where(p => p.Description == "TOPUP" && p.User_ID == pUserID && p.Status == "true").Count();
        }

        public TransactionQueryBuilder GetTransByCreateUser(string currencyCode, string createUser)
        {
            //var compareDate = DateTime.Now.AddMonths(-6);
            //Query = Query.Where(p => p.CreateUser.ToLower().Contains(createUser.ToLower().Trim())).Where(p => p.CreateDate > compareDate).OrderByDescending(s => s.CreateDate);
            Query = Query.Where(p => p.CreateUser.ToLower().Contains(createUser.ToLower().Trim()) && p.Source_Currency == currencyCode).OrderBy(s => s.CreateDate);
            return this;
        }
        public TransactionQueryBuilder GetTransByRemark(string remark)
        {
            //var compareDate = DateTime.Now.AddMonths(-6);
            //Query = Query.Where(p => p.Remarks.ToLower().Contains(remark.ToLower().Trim())).Where(p => p.CreateDate > compareDate).OrderByDescending(s => s.CreateDate);
            Query = Query.Where(p => p.Remarks.ToLower().Contains(remark.ToLower().Trim())).OrderBy(s => s.CreateDate);
            return this;
        }
        public TransactionQueryBuilder GetTransByTranID(string tranID)
        {
            //var compareDate = DateTime.Now.AddMonths(-6);
            //Query = Query.Where(p => p.Tran_ID == tranID).Where(p => p.CreateDate > compareDate).OrderByDescending(s => s.CreateDate);
            Query = Query.Where(p => p.Tran_ID == tranID).OrderBy(s => s.CreateDate);
            return this;
        }
        public TransactionQueryBuilder GetTransByUserID(string currencyCode, string userID)
        {
            //var compareDate = DateTime.Now.AddMonths(-6);
            //Query = Query.Where(p => p.User_ID.Trim() == userID).Where(p => p.CreateDate > compareDate).OrderByDescending(s => s.CreateDate);
            Query = Query.Where(p => p.User_ID.Trim() == userID && p.Source_Currency == currencyCode).OrderBy(s => s.CreateDate);
            return this;
        }
        public TransactionQueryBuilder GetTransBySource(string source)
        {
            Query = Query.Where(p => p.Source.ToLower().Contains(source.ToLower().Trim()));
            return this;
        }
        public TransactionQueryBuilder GetAllTransByUserID(string userID)
        {
            Query = Query.Where(p => p.User_ID.Trim() == userID);
            return this;
        }
      
        public TransactionQueryBuilder GetPendingTransByUserIDnStatus(string status ,string userID)
        {
            Query = Query.Where(p => p.Status.ToLower().Contains(status.ToLower().Trim()) && p.User_ID.ToLower().Contains(userID.ToLower().Trim())).OrderByDescending(s=>s.CreateDate);
            return this;
        }
        public TransactionQueryBuilder GetPendingTransByUserIDnStatus(string status, string userID, string SourceType)
        {
            Query = Query.Where(p => p.Status.ToLower().Equals(status.ToLower()) && p.User_ID.Equals(userID) && p.Source.Equals(SourceType));
            return this;
        }
        public bool IsExistPendingTransByUserIDnStatus(string status, string userID, string SourceType)
        {
            return  Query.Where(p => p.Status.ToLower().Equals(status.ToLower()) && p.User_ID.Equals(userID) && p.Source.Equals(SourceType)).Any();
            
        }
        public bool HasFreeRides(int FreeRidesNumber, string userID , string TripID)
        {
            return Query.Where(p => p.User_ID.ToLower().Contains(userID.ToLower().Trim()) && p.Status.Contains("settled") && p.Source_Amount == 0.00M && p.Source == "TownBus" && p.Remarks.Substring(0,5).Contains(TripID)).Count() < FreeRidesNumber;
        
        }     
      
        public int? CountWithdrawTimeByUserIDnDateTimenCurrency(string pUserID, DateTime fromDate, DateTime toDate)
        {
            return Query.Where(p => p.Description.Contains("WITHDRAW") && p.User_ID == pUserID && (string.IsNullOrEmpty(p.Status) || p.Status == "NULL") && p.CreateDate <= fromDate && p.CreateDate >= toDate).Count();
        }
        public TransactionQueryBuilder GetHistoryDebitByUserIDnCurrency(string pUserID, string pCurrency)
        {
            Query = Query.Where(p => (p.Description.Contains("TOPUP") ||  p.Description.Contains("CASHBONUS") || p.Description.Contains("COMMISSION")) && p.User_ID == pUserID && p.Status.Contains("true") && p.Source_Currency.Contains(pCurrency)).OrderByDescending(s => s.CreateDate);
            return this;
        }
        public TransactionQueryBuilder GetHistoryCreditByUserIDnCurrency(string pUserID, string pCurrency)
        {
            Query = Query.Where(p => (p.Description.Contains("WITHDRAW") || p.Description.Contains("PAYMENT") ) && p.User_ID == pUserID && p.Status.Contains("true") && p.Source_Currency.Contains(pCurrency)).OrderByDescending(s => s.CreateDate);
            return this;
        }
        public List<Tuple<Transaction,Wallet_Account>> GetAllTopupWithdrawWithAccountInfo(DateTime? fromDate)
        {
            var lstTransactionWithSnapshot = ((WalletEntities)Context).Transactions.Join(((WalletEntities)Context).Wallet_Account,
                t => new { UserId = t.User_ID, Currency = t.Source_Currency }, a => new { UserId = a.User_ID, Currency = a.Currency_Code }, (t, a) => new { Transaction = t, Account = a })
                        .Where(ta => (ta.Transaction.Description == "WITHDRAW" || ta.Transaction.Description == "TOPUP" || ta.Transaction.Description == "COMMISSION") && ta.Transaction.CreateDate >= fromDate.Value).ToList();
            return lstTransactionWithSnapshot.Select(ta => new Tuple<Transaction, Wallet_Account>(ta.Transaction, ta.Account)).ToList();
        }
        public List<Transaction> GetListTranWithdrawBeforeSnapshotDate(DateTime? fromDate)
        {
            var lstTransactionWithdraw = ((WalletEntities)Context).Transactions.Join(((WalletEntities)Context).SubTransactions,
                 t => new { TranID = t.Tran_ID }, a => new { TranID = a.Tran_ID }, (t, a) => new { Transaction = t, SubTransaction = a })
                         .Where(ta => ta.Transaction.Description == "WITHDRAW" && ta.Transaction.Status != "canceled" && ta.Transaction.CreateDate < fromDate.Value && ta.SubTransaction.CreateDate >= fromDate.Value).ToList();
            return lstTransactionWithdraw.Select(ta => ta.Transaction).ToList();
        }
    }
}
