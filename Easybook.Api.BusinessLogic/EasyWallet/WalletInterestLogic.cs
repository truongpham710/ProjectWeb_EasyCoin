using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet;
using Easybook.Api.BusinessLogic.ApiLogic.Logic;
using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using Easybook.Api.Core.CrossCutting.Utility;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using Easybook.Api.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using static Easybook.Api.BusinessLogic.EasyWallet.Utility.SecurityLogic;

namespace Easybook.Api.BusinessLogic.EasyWallet
{
    public class WalletInterestLogic
    {
        public WalletInterestLogic(bool genStampKey = false)
        {
            if (genStampKey)
            {
                localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);
            }
        }

        public string GenerateNewTransaction_Interest_Snapshot(WalletEntities newWalletEntities, string Tran_ID, decimal Interest_Amount, string AccID, decimal totalamount, string remarks)
        {
            try
            {
                var Tran_interest = new Transaction_Interest_Snapshot();
                Tran_interest.ID = Guid.NewGuid().ToString();
                Tran_interest.Account_ID = AccID;
                Tran_interest.Tran_ID = Tran_ID;
                Tran_interest.Interest_Amount = ConvertUtility.RoundToTwoDecimalPlaces(Interest_Amount);
                Tran_interest.Total_Amount = ConvertUtility.RoundToTwoDecimalPlaces(totalamount);
                Tran_interest.Createdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                Tran_interest.Remark = remarks;
                Tran_interest.CheckSumInterest = BuildCheckSum_SnapshotInterest(Tran_interest);
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(newWalletEntities))
                {
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(Tran_interest).EndTransaction();
                }

                return Tran_interest.ID;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), Tran_ID, ex, "");
                return "";
            }
        }

        public string BuildCheckSum_SnapshotInterest(Transaction_Interest_Snapshot InterestTran)
        {
            string checkSum = SecurityLogic.GetSha1Hash(InterestTran.ID + "|" + InterestTran.Account_ID + "|" + InterestTran.Createdate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + InterestTran.Total_Amount + "|" + InterestTran.Interest_Amount + "|" + EwalletConstant.WebserverKey);
            return checkSum;
        }

        public Transaction BuildTransactionInterest(decimal pTotalInterest, string currencyCode, string pUserID, string pAcc_ID, string pSource, string pWalletID, string pInterestrate)
        {
            var tran = new Transaction();
            tran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
            tran.CreateUser = "";
            tran.Description = "CASHBONUS";
            tran.Merchant_ref = "";
            tran.PaymentGateway = "";
            tran.Remarks = "Inserted live Cash Bonus.";
            tran.Source = pSource;
            tran.Source_Amount = pTotalInterest;
            tran.Source_Currency = currencyCode;
            tran.Status = "NULL";
            tran.User_ID = pUserID;
            tran.Wallet_ID = pWalletID;
            tran.Tran_ID = ("EW" + Guid.NewGuid().ToString().Replace("-", "")).Substring(0, 20);
            return tran;
        }


        public Transaction_Interest_Snapshot BuildNewTransaction_Interest_Snapshot(WalletEntities newWalletEntities, string Tran_ID, decimal Interest_Amount, string AccID, decimal totalamount, string remarks)
        {
            try
            {
                var Tran_interest = new Transaction_Interest_Snapshot();
                Tran_interest.ID = Guid.NewGuid().ToString();
                Tran_interest.Account_ID = AccID;
                Tran_interest.Tran_ID = Tran_ID;
                Tran_interest.Interest_Amount = ConvertUtility.RoundToTwoDecimalPlaces(Interest_Amount);
                Tran_interest.Total_Amount = ConvertUtility.RoundToTwoDecimalPlaces(totalamount);
                Tran_interest.Createdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                Tran_interest.Remark = remarks;
                Tran_interest.CheckSumInterest = BuildCheckSum_SnapshotInterest(Tran_interest);
                return Tran_interest;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), Tran_ID, ex, "");
                return null;
            }
        }
        public bool InsertTransactionRewards(Wallet_Account_Reward record)
        {
            try
            {                
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(record).EndTransaction();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), record.ID, ex, "");
                return false;
            }
        }
      

        public string BuildCheckSum_Reward(Wallet_Account_Reward InterestTran)
        {
            string checkSum = SecurityLogic.GetSha1Hash(InterestTran.ID + "|" + InterestTran.Wallet_ID + "|" + InterestTran.Createdate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + InterestTran.Updatedate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + InterestTran.Reward_Amount + "|" + EwalletConstant.WebserverKey);
            return checkSum;
        }

    }
}
