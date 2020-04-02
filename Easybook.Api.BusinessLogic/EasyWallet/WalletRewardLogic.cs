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
    public class WalletRewardLogic
    {
        public WalletRewardLogic(bool genStampKey = false)
        {
            try
            {
                if (genStampKey)
                {
                    localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                    var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                    Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);
                }
            }
            catch (Exception ex)
            {
                Globals.StampServerKey = "5tg8ENcfBwP2z8pWsI5lL8Hab0Tr9VZ5";
            }
        }

        public List<Wallet_Account_Reward> GetRewardByWalletID(string pWalletID)
        {
            var logWallet = new LogWallet();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                  return new WalletRewardQueryBuilder(new WalletEntities()).GetRewardByWalletID(pWalletID).ToList();
                }               
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), pWalletID, ex, "");
                return null;
            }

        }

        public Wallet_Account_Reward GetRewardByAccID(string pAccID)
        {
            var logWallet = new LogWallet();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    return new WalletRewardQueryBuilder(new WalletEntities()).GetRewardByAccID(pAccID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), pAccID, ex, "");
                return null;
            }

        
        }

        public List<Wallet_Account_Reward> GetRewardsByAccIds(List<string> Ids)
        {
            var logWallet = new LogWallet();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    return new WalletRewardQueryBuilder(new WalletEntities()).GotBalance().HaveIds(Ids).ToList();
                }
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "Error in GetRewardsByAccIds of TallyWithdrawBalance");
                return null;
            }
        }


        public Wallet_Account_Reward BuildWalletAccReward(string accID, string walletID, decimal rewardAmt)
        {
            #region New Reward Account

            var Transaction_Reward = new Wallet_Account_Reward();
            Transaction_Reward.ID = accID;
            Transaction_Reward.Wallet_ID = walletID;
            Transaction_Reward.Reward_Amount = rewardAmt;
            Transaction_Reward.Createdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
            Transaction_Reward.Updatedate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
            Transaction_Reward.Remark = "";            
            Transaction_Reward.CheckSumReward = BuildCheckSum_Reward(Transaction_Reward);
            return Transaction_Reward;
            #endregion
        }
      
        public bool CheckConditionRewardByDate(DateTime pStartDate)
        {
            try
            {
                var countReward = 0;
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    countReward = new TransactionQueryBuilder(new WalletEntities()).CountRewardTransaction(pStartDate);
                }
                if (countReward > 5000)
                { return false; }
                else
                { return true; }
              
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return false;
            }
        }
        public bool CheckConditionRewardByDatenUserID(DateTime pStartDate, string pUserID)
        {
            try
            {
                var countReward = 0;
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    countReward = new TransactionQueryBuilder(new WalletEntities()).CountRewardTransactionByDatenUserID(pStartDate, pUserID);
                }
                if (countReward > EwalletConstant.LimitTopupExtraPerUser)
                { return false; }
                else
                { return true; }

            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return false;
            }
        }


        public decimal Calculate_Reward_Amount(decimal AmtTopup, string currencyCode)
        {
            decimal rewardAmount = 0;          
            if (currencyCode == "MYR" || currencyCode == "SGD")
            {
                if (AmtTopup >= 100 && AmtTopup < 200)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                else if (AmtTopup >= 200 && AmtTopup < 500)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 500 && AmtTopup < 1000)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 1000)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            else if (currencyCode == "USD")
            {
                if (AmtTopup >= 25 && AmtTopup < 50)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                else if (AmtTopup >= 50 && AmtTopup < 100)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 100 && AmtTopup < 200)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 200)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            else if (currencyCode == "THB")
            {
                if (AmtTopup >= 1000 && AmtTopup < 2000)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                else if (AmtTopup >= 2000 && AmtTopup < 5000)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 5000 && AmtTopup < 10000)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 10000)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            else if (currencyCode.ToLower() == "rp")
            {
                if (AmtTopup >= 350000 && AmtTopup < 700000)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                else if (AmtTopup >= 700000 && AmtTopup < 1750000)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 1750000 && AmtTopup < 3500000)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 3500000)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            else if (currencyCode == "VND")
            {
                if (AmtTopup >= 550000 && AmtTopup < 1100000)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                if (AmtTopup >= 1100000 && AmtTopup < 2750000)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 2750000 && AmtTopup < 5500000)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 5500000)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            else if (currencyCode == "MMK")
            {
                if (AmtTopup >= 40000 && AmtTopup < 80000)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                else if (AmtTopup >= 80000 && AmtTopup < 200000)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 200000 && AmtTopup < 400000)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 400000)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            else if (currencyCode == "CNY")
            {
                if (AmtTopup >= 200 && AmtTopup < 400)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                else if (AmtTopup >= 400 && AmtTopup < 1000)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 1000 && AmtTopup < 2000)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 2000)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            else if (currencyCode == "LAK")
            {
                if (AmtTopup >= 20000 && AmtTopup < 400000)
                {
                    rewardAmount = AmtTopup * 10 / 100;
                }
                else if (AmtTopup >= 400000 && AmtTopup < 1000000)
                {
                    rewardAmount = AmtTopup * 15 / 100;
                }
                else if (AmtTopup >= 1000000 && AmtTopup < 2000000)
                {
                    rewardAmount = AmtTopup * 20 / 100;
                }
                else if (AmtTopup >= 2000000)
                {
                    rewardAmount = AmtTopup * 30 / 100;
                }
            }
            return rewardAmount;
           
        }
       
        public bool UpdateRewardByAccIDForTopup(WalletTransactionUow eWalletTransactionUnitOfWork, string AccID, decimal rewardamount, string remarks)
        {
            var logWallet = new LogWallet();
            try
            {              
                var RewardAcc = eWalletTransactionUnitOfWork.GetRewardByAccID(AccID);
                if (RewardAcc == null)
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Could not find Reward Account by AccID: " + AccID, null, "");
                    return false;
                }
                RewardAcc.Reward_Amount = RewardAcc.Reward_Amount + rewardamount;
                RewardAcc.Updatedate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                RewardAcc.CheckSumReward = BuildCheckSum_Reward(RewardAcc);
                RewardAcc.Remark = remarks;
                eWalletTransactionUnitOfWork.DoUpdate(RewardAcc).SaveAndContinue();
                return true;
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), AccID, ex, "");
                return false;
            }
        }

        public decimal UpdateRewardByAccIDForPayment(WalletTransactionUow eWalletTransactionUnitOfWork, Wallet_Account_Reward RewardAcc, decimal CalReward, string remarks)
        {
            decimal realReward = 0;
            try
            { 
                if (CalReward >= RewardAcc.Reward_Amount)
                {
                    realReward = RewardAcc.Reward_Amount;
                    RewardAcc.Reward_Amount = 0;
                }
                else
                {
                    realReward = CalReward;
                    RewardAcc.Reward_Amount = RewardAcc.Reward_Amount - CalReward;
                }
                
                RewardAcc.Updatedate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                RewardAcc.CheckSumReward = BuildCheckSum_Reward(RewardAcc);
                RewardAcc.Remark = remarks;
                eWalletTransactionUnitOfWork.DoUpdate(RewardAcc).SaveAndContinue();
              
                return realReward;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), RewardAcc.ID, ex, "");
                return -1;
            }
        }

        public decimal UpdateRewardByAccIDForPaymentWithoutTownbus(WalletTransactionUow eWalletTransactionUnitOfWork, Wallet_Account_Reward RewardAcc, decimal CalReward, string remarks)
        {
            decimal realReward=0;
            try
            {
                if (CalReward >= RewardAcc.Reward_Amount)
                {
                    realReward = RewardAcc.Reward_Amount;
                    RewardAcc.Reward_Amount = 0;                    
                }
                else if (CalReward < RewardAcc.Reward_Amount)
                {
                    realReward = CalReward;
                    RewardAcc.Reward_Amount = RewardAcc.Reward_Amount - CalReward;
                }

                RewardAcc.Updatedate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                RewardAcc.CheckSumReward = BuildCheckSum_Reward(RewardAcc);
                RewardAcc.Remark = remarks;
                eWalletTransactionUnitOfWork.DoUpdate(RewardAcc).SaveAndContinue();

                return realReward;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), RewardAcc.ID, ex, "");
                return -1;
            }
        }


        public string BuildCheckSum_Reward(Wallet_Account_Reward InterestTran)
        {
            string checkSum = SecurityLogic.GetSha1Hash(InterestTran.ID + "|" + InterestTran.Wallet_ID + "|" + InterestTran.Createdate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + InterestTran.Updatedate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + InterestTran.Reward_Amount + "|" + EwalletConstant.WebserverKey);
            return checkSum;
        }

    }
}
