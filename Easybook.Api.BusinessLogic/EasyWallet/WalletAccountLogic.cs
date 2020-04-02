using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easybook.Api.DataAccessLayer.UnitOfWork;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Reflection;
using static Easybook.Api.BusinessLogic.EasyWallet.Utility.SecurityLogic;
using Easybook.Api.Core.CrossCutting.Utility;

namespace Easybook.Api.BusinessLogic.EasyWallet
{
    public class WalletAccountLogic
    {


        public string BuildCheckSumTotal1(Wallet_Account CurrenctWalletAcc)
        {
            string checkSumTotal1 = SecurityLogic.GetSha1Hash(CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrenctWalletAcc.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Total_Balance) + "|" + EwalletConstant.WebserverKey);
            //var logWallet = new LogWallet();
            //logWallet.Log(MethodBase.GetCurrentMethod(), CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate + "|" + CurrenctWalletAcc.UpdateDate + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Total_Balance) + "|" + EwalletConstant.WebserverKey, null, "BuildCheckSumTotal1");

            return checkSumTotal1;
        }

        public string BuildCheckSumTotal2(Wallet_Account CurrenctWalletAcc)
        {
            if (string.IsNullOrEmpty(Globals.StampServerKey))
            {
                try
                {
                        localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                        var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                        Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);                  
                }
                catch (Exception ex)
                {
                    Globals.StampServerKey = "5tg8ENcfBwP2z8pWsI5lL8Hab0Tr9VZ5";
                }
            }

            string checkSumTotal2 = SecurityLogic.GetSha1Hash(CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrenctWalletAcc.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Total_Balance) + "|" + Globals.StampServerKey);
            //var logWallet = new LogWallet();
            //logWallet.Log(MethodBase.GetCurrentMethod(), CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate + "|" + CurrenctWalletAcc.UpdateDate + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Total_Balance) + "|" + Globals.StampServerKey, null, "BuildCheckSumTotal2");

            return checkSumTotal2;
        }

        public string BuildCheckSumAvailable1(Wallet_Account CurrenctWalletAcc)
        {
            string checkSumAvailable1 = SecurityLogic.GetSha1Hash(CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrenctWalletAcc.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Available_Balance) + "|" + EwalletConstant.WebserverKey);
            //var logWallet = new LogWallet();
            //logWallet.Log(MethodBase.GetCurrentMethod(), CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate + "|" + CurrenctWalletAcc.UpdateDate + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Available_Balance) + "|" + EwalletConstant.WebserverKey, null, "BuildCheckSumAvailable1");

            return checkSumAvailable1;
        }

        public string BuildCheckSumAvailable2(Wallet_Account CurrenctWalletAcc)
        {
            try
            {
                if (Globals.StampServerKey != "Invalid Key")
                {
                    //var logWallet = new LogWallet();
                    //logWallet.Log(MethodBase.GetCurrentMethod(), CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate + "|" + CurrenctWalletAcc.UpdateDate + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Available_Balance) + "|" + Globals.StampServerKey, null, "BuildCheckSumAvailable2");
                    if (string.IsNullOrEmpty(Globals.StampServerKey))
                    {                       
                        localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                        var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                        Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);
                    }

                    return SecurityLogic.GetSha1Hash(CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrenctWalletAcc.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Available_Balance) + "|" + Globals.StampServerKey);

                }
                else
                {
                    //var logWallet = new LogWallet();
                    //logWallet.Log(MethodBase.GetCurrentMethod(), CurrenctWalletAcc.ID + "|" + CurrenctWalletAcc.Wallet_ID + "|" + CurrenctWalletAcc.Currency_Code + "|" + CurrenctWalletAcc.User_ID + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.CreateUser) ? "" : CurrenctWalletAcc.CreateUser.Trim()) + "|" + CurrenctWalletAcc.CreateDate + "|" + CurrenctWalletAcc.UpdateDate + "|" + (string.IsNullOrEmpty(CurrenctWalletAcc.UpdateUser) ? "" : CurrenctWalletAcc.UpdateUser.Trim()) + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrenctWalletAcc.Available_Balance) + "|" + Globals.StampServerKey, null, "BuildCheckSumAvailable2");

                    return "";
                }
                
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), CurrenctWalletAcc.Wallet_ID, ex, ""));
                return "";
            }
        }

        public string GenerateNewWalletAccount(string pUserID, string pUserCreate)
        {
            var lstWalletAccount = new List<Wallet_Account>();
            var lstWalletRule = new List<Wallet_Rule>();
            //var lstInterestSnapshot = new List<Transaction_Interest_Snapshot>();
            var lstRewards = new List<Wallet_Account_Reward>();
            var New_WalletUser = new Wallet_User();

            New_WalletUser.User_ID = pUserID;
            New_WalletUser.Wallet_ID = SecurityLogic.GenerateKey(30);
            var curDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
            WalletTransactionUow WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities());
            try
            {
                WalletTransactionUnitOfWork.BeginTransaction();
                WalletTransactionUnitOfWork.DoInsert(New_WalletUser).SaveAndContinue();

                using (var CurrenciesQueryBuilder = new CurrenciesQueryBuilder(new WalletEntities()))
                {
                    foreach (var CurrencyCode in CurrenciesQueryBuilder.ToList())
                    {
                        #region New Wallet Acc
                        Random random = new Random();
                        var New_walletAcc = new Wallet_Account();
                        New_walletAcc.ID = SecurityLogic.GenerateKey(30);
                        New_walletAcc.Wallet_ID = New_WalletUser.Wallet_ID;
                        New_walletAcc.User_ID = pUserID;
                        New_walletAcc.Available_Balance = ConvertUtility.RoundToTwoDecimalPlaces(0);
                        New_walletAcc.Total_Balance = ConvertUtility.RoundToTwoDecimalPlaces(0);
                        New_walletAcc.Currency_Code = CurrencyCode.Currency_Code;
                        New_walletAcc.CreateDate = curDate;
                        New_walletAcc.CreateUser = pUserCreate;
                        New_walletAcc.UpdateDate = curDate;
                        New_walletAcc.UpdateUser = pUserCreate;
                        New_walletAcc.ChecksumAvailable1 = BuildCheckSumAvailable1(New_walletAcc);

                        var strCheckSum2 = BuildCheckSumAvailable2(New_walletAcc);
                        if (strCheckSum2 != "")
                        {
                            New_walletAcc.ChecksumAvailable2 = strCheckSum2;
                        }
                        else
                        {
                            var logWallet = new LogWallet();
                            Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), New_WalletUser.Wallet_ID, null, "Can not build checksum2"));
                            return "Failed";
                        }

                        New_walletAcc.ChecksumTotal1 = BuildCheckSumTotal1(New_walletAcc);
                        New_walletAcc.ChecksumTotal2 = BuildCheckSumTotal2(New_walletAcc);
                        lstWalletAccount.Add(New_walletAcc);
                        #endregion

                        #region New Wallet Rule
                        var New_WalletRule = new Wallet_Rule();
                        New_WalletRule.ID = SecurityLogic.GenerateKey(30);
                        New_WalletRule.Account_ID = New_walletAcc.ID;

                        switch (CurrencyCode.Currency_Code)
                        {
                            case "VND":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_VND;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_VND;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_VND;
                                break;
                            case "USD":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_USD;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_USD;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_USD;
                                break;
                            case "SGD":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_SGN;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_SGN;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_SGN;
                                break;
                            case "MYR":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_MYR;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_MYR;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_MYR;
                                break;
                            case "THB":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_THB;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_THB;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_THB;
                                break;
                            case "CNY":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_CNY;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_CNY;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_CNY;
                                break;
                            case "KHR":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_KHR;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_KHR;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_KHR;
                                break;
                            case "MMK":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_MMK;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_MMK;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_MMK;
                                break;
                            case "Rp":
                                New_WalletRule.Maximum_Topup_Amount = EwalletConstant.EWallet_LimitTopupAmount_Rp;
                                New_WalletRule.Maximum_Withdraw_Amount = EwalletConstant.EWallet_LimitWithdrawAmount_Rp;
                                New_WalletRule.Minimum_Topup_Amount = EwalletConstant.EWallet_LimitTopupMinimumAmount_Rp;
                                break;
                        }
                        New_WalletRule.CreateDate = curDate;
                        New_WalletRule.UpdateDate = curDate;
                        New_WalletRule.Currency_Code = CurrencyCode.Currency_Code;
                        lstWalletRule.Add(New_WalletRule);

                        #endregion

                        //#region New Interest Snapshot
                        //var TransactionInterest = new WalletInterestLogic();
                        //var Tran_interest = new Transaction_Interest_Snapshot();
                        //Tran_interest.ID = Guid.NewGuid().ToString();
                        //Tran_interest.Account_ID = New_walletAcc.ID;
                        //Tran_interest.Tran_ID = Guid.NewGuid().ToString();
                        //Tran_interest.Interest_Amount = 0;
                        //Tran_interest.Total_Amount = 0;
                        //Tran_interest.Createdate = DateTime.Now;
                        //Tran_interest.Remark = "";
                        //var TransactionLogic = new WalletTransactionLogic(true);
                        //Tran_interest.CheckSumInterest = TransactionInterest.BuildCheckSum_SnapshotInterest(Tran_interest);
                        //lstInterestSnapshot.Add(Tran_interest);
                        //#endregion

                        #region New Reward Account

                        var Transaction_Reward = new Wallet_Account_Reward();
                        Transaction_Reward.ID = New_walletAcc.ID;
                        Transaction_Reward.Wallet_ID = New_walletAcc.Wallet_ID;
                        Transaction_Reward.Reward_Amount = 0;
                        Transaction_Reward.Createdate = curDate;
                        Transaction_Reward.Updatedate = curDate;
                        Transaction_Reward.Remark = "";
                        var TransactionRewardLogic = new WalletRewardLogic();
                        Transaction_Reward.CheckSumReward = TransactionRewardLogic.BuildCheckSum_Reward(Transaction_Reward);
                        lstRewards.Add(Transaction_Reward);
                        #endregion
                    }
                }
               
                WalletTransactionUnitOfWork.DoInsertMany(lstWalletAccount);
                WalletTransactionUnitOfWork.DoInsertMany(lstWalletRule);
                //WalletTransactionUnitOfWork.DoInsertMany(lstInterestSnapshot);
                WalletTransactionUnitOfWork.DoInsertMany(lstRewards);
                WalletTransactionUnitOfWork.EndTransaction();
               
                return New_WalletUser.Wallet_ID;
            }
            catch (Exception ex)
            {
                WalletTransactionUnitOfWork.RollBack();
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), New_WalletUser.Wallet_ID, ex, ""));
                return "Failed";
            }
        }

        public List<Wallet_Account> GetWalletAccountByWalletID(string pWalletID)
        {
            var WalletAcc = new List<Wallet_Account>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    WalletAcc = new WalletAccountQueryBuilder(new WalletEntities()).GetWalletByWalletID(pWalletID).ToList();
                }
                return WalletAcc;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pWalletID, ex, ""));
                return WalletAcc;
            }
        }

        public List<Wallet_Account> GetWalletAccountByUserID(string pUserID)
        {
            var WalletAcc = new List<Wallet_Account>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    WalletAcc = new WalletAccountQueryBuilder(new WalletEntities()).GetWalletByUserID(pUserID).ToList();
                }
                return WalletAcc;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, ""));
                return WalletAcc;
            }
        }

        public Wallet_Account GetWalletAccountByAccID(string pAccID)
        {
            var WalletAcc = new Wallet_Account();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    WalletAcc = new WalletAccountQueryBuilder(new WalletEntities()).GetWalletByID(pAccID).FirstOrDefault();
                }
                return WalletAcc;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pAccID, ex, ""));
                return WalletAcc;
            }
        }

        public Wallet_Rule GetWalletRuleByAccID(string pAccID)
        {
            var WalletRule = new Wallet_Rule();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    WalletRule = new WalletRuleQueryBuilder(new WalletEntities()).GetWalletByID(pAccID).FirstOrDefault();
                }
                return WalletRule;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pAccID, ex, ""));
                return WalletRule;
            }
        }

        public Wallet_Account GetWalletAccByWallAccIDnCurrencyCode(string WalletID, string CurrencyCode)
        {
            var WalletAcc = new Wallet_Account();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    WalletAcc = new WalletAccountQueryBuilder(new WalletEntities()).GetAccIdByWalletIDnCurrencyCode(WalletID, CurrencyCode).FirstOrDefault();
                }
                return WalletAcc;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), WalletID + "|" + CurrencyCode, ex, ""));
                return WalletAcc;
            }
        }

        public Wallet_Account GetWalletAccByUserIDnCurrencyCode(string UserID, string CurrencyCode)
        {
            var WalletAcc = new Wallet_Account();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    WalletAcc = new WalletAccountQueryBuilder(new WalletEntities()).GetAccIdByUserIDnCurrencyCode(UserID, CurrencyCode).FirstOrDefault();
                }
                return WalletAcc;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), UserID + "|" + CurrencyCode, ex, ""));
                return WalletAcc;
            }
        }

        public Transaction GetWalletTranByTranID(string TranID)
        {
            var Tran = new Transaction();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTranByTranID(TranID).FirstOrDefault();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), TranID, ex, ""));
                return Tran;
            }
        }

        public List<Transaction> GetTransBQ()
        {
            var Tran = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTransBQ().ToList();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return Tran;
            }
        }

        public List<Transaction> GetTransWithdraw()
        {
            var Tran = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTransWithdraw().ToList();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return Tran;
            }
        }

        public List<Transaction> GetTransWithdrawWithVerified()
        {
            var Tran = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTransWithdrawWithVerified().ToList();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return Tran;
            }
        }

        public List<Transaction> GetTransWithdrawWithCancel()
        {
            var Tran = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTransWithdrawWithCancel().ToList();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return Tran;
            }
        }

        public List<Transaction> GetTranWithdrawByUserIDNoVerify(string pUserID, string pCurrencyCode)
        {
            var Tran = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTranWithdrawByUserIDNoVerify(pUserID, pCurrencyCode).ToList();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return Tran;
            }
        }
        public List<Transaction> GetTranTownBusnWithdrawByUserIDNoVerify(string pUserID, string pCurrencyCode)
        {
            var Tran = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTranTownBusnWithdrawByUserIDNoVerify(pUserID, pCurrencyCode).ToList();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return Tran;
            }
        }
        public List<Transaction> GetTransTopupWithoutVerified()
        {
            var Tran = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Tran = new TransactionQueryBuilder(new WalletEntities()).GetTransTopup().ToList();
                }
                return Tran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return Tran;
            }
        }

        public List<Wallet_Account> GetWalletAccsByWalletIdCurrencies(List<string> walletCurrencies)
        {
            var WalletAccs = new List<Wallet_Account>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    WalletAccs = new WalletAccountQueryBuilder(new WalletEntities()).HasWalletCurrencies(walletCurrencies).ToList();
                }
                return WalletAccs;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), walletCurrencies.Aggregate((a, b) => $"{a},{b}"), ex, ""));
                return WalletAccs;
            }
        }
        public bool IsExistWalletID(string pWalletID)
        {
            bool result = false;
            try
            {

                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    result = new WalletUserQueryBuilder(new WalletEntities()).HasWalletId(pWalletID);
                }
                return result;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pWalletID, ex, ""));
                return result;
            }
        }

        public bool IsExistUserID(string pUserID)
        {
            bool result = false;
            try
            {

                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    result = new WalletUserQueryBuilder(new WalletEntities()).HasUserId(pUserID);
                }
                return result;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, ""));
                return result;
            }
        }
        public bool UpdateAllCheckSumWalletAccount(string FromDate, string ToDate)
        {
            var lstWalletAccount = new List<Wallet_Account>();
            var userLogic = new WalletUserLogic(true);
            var logWallet = new LogWallet();
            try
            {
                WalletTransactionUow WalletTransactionUnitOfWork = null;
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    var startDate = new DateTime(int.Parse(FromDate.Split('-')[0]), int.Parse(FromDate.Split('-')[1]), int.Parse(FromDate.Split('-')[2]));
                    var toDate = new DateTime(int.Parse(ToDate.Split('-')[0]), int.Parse(ToDate.Split('-')[1]), int.Parse(ToDate.Split('-')[2]));
                    var diffDate = toDate.Subtract(startDate).TotalDays;
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Start UpdateAllCheckSumWalletAccount", null, "");
                    for (int i = 0; i <= diffDate; i++)
                    {
                        var date = startDate.AddDays(i);
                        lstWalletAccount = WalletTransactionUnitOfWork.GetAllWalletAccountByDate(date);
                     
                        foreach (Wallet_Account walletAcc in lstWalletAccount)
                        {                        
                            walletAcc.ChecksumAvailable1 = BuildCheckSumAvailable1(walletAcc);
                            walletAcc.ChecksumAvailable2 = BuildCheckSumAvailable2(walletAcc);
                            walletAcc.ChecksumTotal1 = BuildCheckSumTotal1(walletAcc);
                            walletAcc.ChecksumTotal2 = BuildCheckSumTotal2(walletAcc);
                        }
                        WalletTransactionUnitOfWork.DoUpdateMany(lstWalletAccount);
                        logWallet.Log(MethodBase.GetCurrentMethod(), date, null, date.ToString("yyyy-MM-dd") + ": " + lstWalletAccount.Count);
                    }
                    logWallet.Log(MethodBase.GetCurrentMethod(), "End UpdateAllCheckSumWalletAccount", null, "");
                }
                return true;
            }
            catch (Exception ex)
            {  
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return false;
            }
        }
        public bool UpdateAllCheckSumWalletAccountWithEmptyCS(string FromDate, string ToDate)
        {
            var lstWalletAccount = new List<Wallet_Account>();
            var userLogic = new WalletUserLogic(true);
            var logWallet = new LogWallet();
            try
            {
                WalletTransactionUow WalletTransactionUnitOfWork = null;
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    var startDate = new DateTime(int.Parse(FromDate.Split('-')[0]), int.Parse(FromDate.Split('-')[1]), int.Parse(FromDate.Split('-')[2]));
                    var toDate = new DateTime(int.Parse(ToDate.Split('-')[0]), int.Parse(ToDate.Split('-')[1]), int.Parse(ToDate.Split('-')[2]));
                    var diffDate = toDate.Subtract(startDate).TotalDays;
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Start UpdateAllCheckSumWalletAccountWithEmptyCS", null, "");

                    for (int i = 0; i <= diffDate; i++)
                    {
                        var date = startDate.AddDays(i);
                        lstWalletAccount = WalletTransactionUnitOfWork.GetAllWalletAccountByDateWithoutCS(date);

                        foreach (Wallet_Account walletAcc in lstWalletAccount)
                        {
                            walletAcc.ChecksumAvailable1 = BuildCheckSumAvailable1(walletAcc);
                            walletAcc.ChecksumAvailable2 = BuildCheckSumAvailable2(walletAcc);
                            walletAcc.ChecksumTotal1 = BuildCheckSumTotal1(walletAcc);
                            walletAcc.ChecksumTotal2 = BuildCheckSumTotal2(walletAcc);
                        }
                        WalletTransactionUnitOfWork.DoUpdateMany(lstWalletAccount);
                        logWallet.Log(MethodBase.GetCurrentMethod(), date, null, date.ToString("yyyy-MM-dd") + ": " + lstWalletAccount.Count);
                    }
                    logWallet.Log(MethodBase.GetCurrentMethod(), "End UpdateAllCheckSumWalletAccountWithEmptyCS", null, "");
                }
                return true;
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return false;
            }
        }

        public bool UpdateAllCheckSumWalletAccountByUserID( string pUserID)
        {
            var lstWalletAccount = new List<Wallet_Account>();
            var userLogic = new WalletUserLogic(true);
            try
            {
                var logWallet = new LogWallet();
                WalletTransactionUow WalletTransactionUnitOfWork = null;
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {

                    lstWalletAccount = WalletTransactionUnitOfWork.GetWalletAccountByUserID(pUserID);
                    var flg = false;
                    foreach (Wallet_Account walletAcc in lstWalletAccount)
                    {
                      
                        //if (walletAcc.ChecksumAvailable2 == walletAcc.ChecksumAvailable1)
                        //{   
                            walletAcc.ChecksumAvailable1 = BuildCheckSumAvailable1(walletAcc);
                            walletAcc.ChecksumAvailable2 = BuildCheckSumAvailable2(walletAcc);
                            walletAcc.ChecksumTotal1 = BuildCheckSumTotal1(walletAcc);
                            walletAcc.ChecksumTotal2 = BuildCheckSumTotal2(walletAcc);
                            flg = true;
                        //}                        
                    }
                    if (flg)
                    {
                        WalletTransactionUnitOfWork.DoUpdateMany(lstWalletAccount);                 
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                return false;
            }
        }
    }
}
