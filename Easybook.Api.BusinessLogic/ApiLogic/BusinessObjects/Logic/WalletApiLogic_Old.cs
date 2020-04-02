using System;
using Easybook.Api.BusinessLogic.EasyWallet;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using Easybook.Api.DataAccessLayer.UnitOfWork;
using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Easybook.Api.Core.Model.EasyWallet.DataTransferObject;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Threading.Tasks;
using System.Reflection;
using static Easybook.Api.BusinessLogic.EasyWallet.Utility.SecurityLogic;
using Easybook.Api.Core.CrossCutting.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Easybook.Api.BusinessLogic.ApiLogic.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class WalletApiLogic 
    {

        /// <summary>
        /// Insert New Wallet Account
        /// </summary>
        /// <returns></returns>
        public WalletUserResponse InsertNewWalletAccount(string pUserID, string pCreateUser)
        {
            var result = new WalletUserResponse();
            var walletAccLogic = new WalletAccountLogic();
            var walletUserLogic = new WalletUserLogic();
            try
            {
                if (!walletUserLogic.IsExistUserIDInWalletUser(pUserID))
                {
                    result.WalletID = walletAccLogic.GenerateNewWalletAccount(pUserID, pCreateUser);
                    if (result.WalletID == "Failed")
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to insert new wallet. Please check log file.";
                        return result;
                    }
                }
                else
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;  
                    result.Status = 0;
                    result.Message = "UserID existed!";
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to insert new wallet account";            
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New User
        /// </summary>
        /// <returns></returns>
        public UserResponse InsertNewUser(UserRequest pUser)
        {
            var result = new UserResponse();
            var walletLogic = new WalletUserLogic();
            var user = new User();
            try
            {
                if (!walletLogic.IsExistEmail(pUser.Email))
                {
                    user.Address = pUser.Address;
                    user.City = pUser.City;
                    user.CreateDate = pUser.CreateDate;
                    user.CreateUser = pUser.CreateUser;
                    user.DOB = pUser.DOB;
                    user.Email = pUser.Email;
                    user.EmailConfirmed = pUser.EmailConfirmed;
                    user.FirstName = pUser.FirstName;
                    user.GUID = Guid.NewGuid().ToString();
                    user.LastName = pUser.LastName;
                    user.Nationality = pUser.CountryID;
                    user.NRIC = pUser.NRIC;
                    user.Passport = pUser.Passport;
                    user.PasswordHash = pUser.PasswordHash;
                    user.PhoneNumber = pUser.PhoneNumber;
                    user.PhoneNumberConfirmed = pUser.PhoneNumberConfirmed;
                    user.Postal = pUser.Postal;
                    user.State = pUser.State;
                    user.UpdateDate = pUser.UpdateDate;
                    user.UpdateUser = pUser.UpdateUser;                   
                    user.User_Name = pUser.User_Name;
                    result.UserID = walletLogic.InsertUserRecord(user);
                    if (string.IsNullOrEmpty(result.UserID))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to insert new User Account. Please check log file!";
                        return result;
                    }
                }
                else
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Email existed!";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to insert new User Account";
                var logWallet = new LogWallet();
               logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + pUser.User_ID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public WalletUserResponse InsertNewUserForEBW(UserRequest pUser)
        {
            var result = new WalletUserResponse();
            var walletLogic = new WalletUserLogic(true);
            var user = new User();
            try
            {
                if (!walletLogic.IsExistUserIDInWalletUser(pUser.User_ID))
                {                   
                    user.User_ID = pUser.User_ID;
                    user.UpdateUser = pUser.UpdateUser;
                    user.CreateUser= pUser.CreateUser;
                    user.DOB = DateTime.Now;
                    user.CreateDate = DateTime.Now; 
                    user.UpdateDate = DateTime.Now;
                    if (!walletLogic.InsertUserRecordForEBW(user))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to insert new User Account. Please check log file!";
                        return result;
                    }

                    result = InsertNewWalletAccount(pUser.User_ID, user.CreateUser);

                    if (string.IsNullOrEmpty(result.WalletID))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to insert new Wallet Account. Please check log file!";
                        return result;
                    }
                    
                }
                else
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "UserID existed!";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to insert new User Account";
                result.Status = 0;
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + pUser.User_ID, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New Temp Transaction 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertTempTransaction(TransactionRequest pTran)
        {
            var logWallet = new LogWallet();
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic();
            var walletSnapshotLogic = new WalletSnapshotLogic();
            var tran = new Transaction();
            try
            {
                //Fix for Checksum, only run for first time
                //walletSnapshotLogic.UpdateCheckSumSnapshot("etSL5kyM2RkHREGkGXduzlHQyU4=");
                //walletSnapshotLogic.UpdateCheckSumSnapshot("f0PgDbAA/zNQeownsoRO4i5oSiM=");
                //walletSnapshotLogic.UpdateCheckSumSnapshot("+8JRKX7IG9s+8qcHGC000tp8jWA=");
                //walletLogic.UpdateCheckSumSubTransaction();

                result = CheckBalanceWalletAccount(pTran.AccID, pTran);
                if (result.Status == 0)
                {
                   return result;
                }
               
                tran.CreateDate = pTran.CreateDate;
                tran.CreateUser = pTran.CreateUser??"";
                tran.Description = pTran.Description;
                tran.Destination_Amount = Convert.ToDecimal(string.Format("{0:0.00}", pTran.Destination_Amount ?? 0));
                tran.Destination_Currency = pTran.Destination_Currency;
                tran.Merchant_ref = pTran.Merchant_ref;
                tran.PaymentGateway = pTran.PaymentGateWay;
                tran.Remarks = pTran.Remarks;
                tran.Source = pTran.Source;
                tran.Source_Amount = Convert.ToDecimal(string.Format("{0:0.00}", pTran.Source_Amount ?? 0));
                tran.Source_Currency = pTran.Source_Currency;
                tran.Status = "NULL";
                tran.User_ID = pTran.User_ID;
                tran.Wallet_ID = pTran.Wallet_ID;
                tran.Tran_ID = pTran.Tran_ID;
                if (walletLogic.InsertTempTransaction(tran))
                {                  
                    result.TranID = tran.Tran_ID;
                }
                else
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to create new temp transaction. Please check log file!";
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to create new temp transaction. Please check log file!";
                logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "WalletID: " + pTran.Wallet_ID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            //logWallet.Log(MethodBase.GetCurrentMethod(), pTran, null , "", 1, 1);
            return result;
        }


        public TranResponse CheckBalanceWalletAccount(string pAccID, TransactionRequest pTran)
        {
            var result = new TranResponse();          
            var walletLogic = new WalletAccountLogic();
            var walletTranLogic = new WalletTransactionLogic(true);
            var walletRewardLogic = new WalletRewardLogic();
            try
            {
                
                if (string.IsNullOrEmpty(pAccID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Message;
                    return result;
                }

                var WalletAcc = walletLogic.GetWalletAccountByAccID(pAccID);
                if (!walletLogic.IsExistWalletID(pTran.Wallet_ID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Message;
                    return result;
                }
                if (!walletTranLogic.CheckSumWalletAccount(WalletAcc))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_CHECKSUMACC_FAIL.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_CHECKSUMACC_FAIL.Message;
                    return result;
                }
                if (pTran.Description.ToUpper() == EwalletConstant.TOPUP)
                {
                    var WalletRule = walletLogic.GetWalletRuleByAccID(pAccID);
                    if (pTran.Source_Amount < WalletRule.Minimum_Topup_Amount)
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_MINIMUM_AMOUNT_TOPUP.Code;
                        result.Status = 0;
                        result.Message = "The minimum topup is " + ConvertUtility.RoundToZeroDecimalPlaces(WalletRule.Minimum_Topup_Amount ?? 0) + " " + pTran.Source_Currency + ". " + ApiReturnCodeConstant.Wallet.ERR_WALLET_MINIMUM_AMOUNT_TOPUP.Message;
                        return result;
                    }
                }
                else if (pTran.Description.ToUpper() == EwalletConstant.PAYMENT)
                {
                    var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(pTran.User_ID, pTran.Source_Currency);
                    var totalPendingAmt = 0.0M;

                    foreach (var pendingAmtWithdraw in TransWithdraw)
                    {
                        totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                    }
                    var walletReward = walletRewardLogic.GetRewardByAccID(pTran.AccID);
                    decimal rewardAmount = 0;
                    if (walletReward != null)
                    {
                        rewardAmount = walletReward.Reward_Amount;
                    }

                    if (pTran.Source_Amount > WalletAcc.Available_Balance + rewardAmount - totalPendingAmt )
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Code;
                        result.Status = 0;
                        result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Message;
                        return result;
                    }
                }
                else if (pTran.Description.ToUpper() == EwalletConstant.CONVERT)
                {                   
                                   
                    if (pTran.Source_Amount > WalletAcc.Available_Balance)
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_DIFF_BALANCE.Code;
                        result.Status = 0;
                        result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_DIFF_BALANCE.Message;
                        return result;
                    }
                }
                else if (pTran.Description.ToUpper() == EwalletConstant.WITHDRAW)
                {
                  
                    var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(pTran.User_ID, pTran.Source_Currency);
                    var totalPendingAmt = 0.0M;
                    foreach (var pendingAmtWithdraw in TransWithdraw)
                    {
                        totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                    }
                    if (pTran.Source_Amount > WalletAcc.Available_Balance - totalPendingAmt)
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_DIFF_BALANCE.Code;
                        result.Status = 0;
                        result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_DIFF_BALANCE.Message;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {               
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to Check Balance Wallet Account";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "AccID: " + pAccID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New TopUp 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertTopupTransaction(string pTranID)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            var walletAccLogic = new WalletAccountLogic();
            var logWallet = new LogWallet();
            SubTransaction subTran = new SubTransaction();
            Transaction TempTran = new Transaction();
            Wallet_Account wallAcc = new Wallet_Account();
            WalletTransactionUow WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities());
            string subID, lastCheckSum1, accID = "";
      
            try
            {
                TempTran = walletAccLogic.GetWalletTranByTranID(pTranID);
                if (TempTran == null)
                {
                    WalletTransactionUnitOfWork.RollBack();
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to retrieve transaction. Please check log file!";
                    return result;
                }
                accID = walletAccLogic.GetWalletAccByWallAccIDnCurrencyCode(TempTran.Wallet_ID, TempTran.Source_Currency).ID;
                if (string.IsNullOrEmpty(accID))
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Could not get Account ID by TranID: " + pTranID + "when insert topup subTransaction. Please check log file!";
                    return result;
                }
                
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                  
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);

                    if (!walletLogic.CheckSumWalletAccount(wallAcc))
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_CHECKSUMACC_FAIL.Code;
                        result.Status = 0;
                        result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_CHECKSUMACC_FAIL.Message;
                        return result;
                    }

                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, true))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing true with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }               

                    using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                    {
                        lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                    }

                    subID = walletLogic.GenerateNewSubTransaction_TopUp(WalletTransactionUnitOfWork, TempTran, accID, ref subTran, lastCheckSum1);
                    if (!string.IsNullOrEmpty(subID))
                    {
                        if (!walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, subTran.Amount ?? 0, subTran.Direction, "TopupWithoutVerify"))
                        {
                            WalletTransactionUnitOfWork.RollBack();
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Updated failed Total Balance Account. Please check log file!";
                            return result;
                        }
                    }
                    else
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to create new topup SubTransaction. Please check log file!";
                        return result;
                    }          

                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    if (walletLogic.VerifyTopUpTransaction(wallAcc, WalletTransactionUnitOfWork, subTran, lastCheckSum1))
                    {
                        if (walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, subTran.Amount ?? 0, subTran.Direction, "TopupWithVerify"))
                        {
                            if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, pTranID, subTran, "TOPUP"))
                            {
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Failed to Update status Tran and SubTran!";
                                WalletTransactionUnitOfWork.RollBack();
                                return result;
                            }
                        }
                        else
                        {
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Failed to update Available Balance";
                            WalletTransactionUnitOfWork.RollBack();
                            return result;
                        }
                    }
                    else
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to verify topup Transaction!";
                        WalletTransactionUnitOfWork.RollBack();
                        return result;
                    }
                     
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, false))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing false with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }
                    result.SubID = subID;
                    result.TranID = TempTran.Tran_ID;
                    WalletTransactionUnitOfWork.EndTransaction();                
                }

                var resultReward = InsertRewardTransaction(TempTran.Source_Amount ?? 0, accID);
                if (string.IsNullOrEmpty(resultReward.TranID))
                {
                    var bodyEmail = "Failed to insert Reward Transaction with ACCID: " + accID + " and Amt:" + TempTran.Source_Amount;
                    EmailUtil.SendEmail("[Exception]-[Wallet Reward]", bodyEmail,  "truong.pham@easybook.com");
                    logWallet.Log(MethodBase.GetCurrentMethod(), bodyEmail, null, "");                    
                }
            }            
            catch (Exception ex)
            {                
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to create new topup transaction. Please check log file!";
                result.Status = 0;
                WalletTransactionUnitOfWork.RollBack();
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + pTranID + "|AccountID: " + accID, ex, "");
                return result;
            }
            ResetStampServerKey();
            logWallet.Log(MethodBase.GetCurrentMethod(), TempTran, null, "", 1, 1);
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New Payment 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertPaymentTransaction(string pTranID, string accID)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            var walletRewardLogic = new WalletRewardLogic();
            var tran = new Transaction();
            var logWallet = new LogWallet();
            SubTransaction subTran = new SubTransaction();
            Wallet_Account wallAcc = new Wallet_Account();
            var RewardAcc = new Wallet_Account_Reward();
            string subID = "", lastCheckSum1 = "";
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            var walletEntity = new WalletEntities();
            try
            {
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(walletEntity))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    RewardAcc = WalletTransactionUnitOfWork.GetRewardByAccID(accID);

                    if (!walletLogic.CheckSumWalletAccount(wallAcc))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to check checksum for wallet account. Please check log file!";
                        return result;
                    }

                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, true))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing true with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }

                    if (RewardAcc == null)
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Reward Account could not find with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }

                    using (var TransactionQueryBuilder = new TransactionQueryBuilder(new WalletEntities()))
                    {
                        tran = TransactionQueryBuilder.GetTranByTranID(pTranID).FirstOrDefault();
                    }
                   
                    if (tran == null)
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to create find transaction. Please check log file!";
                        return result;
                    }


                    if (RewardAcc.Reward_Amount == 0) // Create 1 subtransaction: Main Balance
                    {
                        using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                        {
                            lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                        }                    

                        subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                        if (!string.IsNullOrEmpty(subID))
                        {
                            if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, pTranID, subTran, ""))
                            {
                                WalletTransactionUnitOfWork.RollBack();
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                return result;
                            }

                            if (!walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, tran.Source_Amount??0, subTran.Direction, "OUT"))
                            {
                                WalletTransactionUnitOfWork.RollBack();
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Updated failed Balance Account. Please check log file!";
                                return result;
                            }
                        }
                        else
                        {
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Failed to create new Payment SubTransaction. Please check log file!";
                            WalletTransactionUnitOfWork.RollBack();
                            return result;
                        }
                    }
                    else if (RewardAcc.Reward_Amount >= tran.Source_Amount) // Create 1 subtransaction: Reward
                    {
                        using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                        {
                            lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                        }
                        tran.Remarks = tran.Remarks + " | Deducted from REWARD";

                        subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                        if (!string.IsNullOrEmpty(subID))
                        {
                            var amtAvailable = walletRewardLogic.UpdateRewardByAccIDForPayment(WalletTransactionUnitOfWork, RewardAcc, tran.Source_Amount ?? 0, "");
                            if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, pTranID, subTran, ""))
                            {
                                WalletTransactionUnitOfWork.RollBack();
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                return result;
                            }
                        }
                        else
                        {
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Failed to create new Payment SubTransaction. Please check log file!";
                            WalletTransactionUnitOfWork.RollBack();
                            return result;
                        }
                    }
                    else   // Create 2 subtransaction: Reward and Main Balance
                    {
                        using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                        {
                            lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                        }
                      
                        var totalPayment = tran.Source_Amount;
                        var rewardAmt = RewardAcc.Reward_Amount;
                        var amtAvailable = walletRewardLogic.UpdateRewardByAccIDForPayment(WalletTransactionUnitOfWork, RewardAcc, totalPayment ?? 0, "");
                        if (amtAvailable > 0)
                        {
                            tran.Source_Amount = rewardAmt;
                            tran.Remarks = tran.Remarks + " | Deducted from REWARD";
                            subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                            if (!string.IsNullOrEmpty(subID))
                            {
                                if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, pTranID, subTran, ""))
                                {
                                    WalletTransactionUnitOfWork.RollBack();
                                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                    result.Status = 0;
                                    result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                    return result;
                                }
                                lastCheckSum1 = subTran.Checksum1;
                                tran.Source_Amount = amtAvailable;
                                tran.Remarks = tran.Remarks.Replace(" | Deducted from REWARD", " | Deducted from WALLET");
                                subTran = new SubTransaction();
                                subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                                if (!string.IsNullOrEmpty(subID))
                                {
                                    if (!walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, amtAvailable, subTran.Direction, "OUT"))
                                    {
                                        WalletTransactionUnitOfWork.RollBack();
                                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                        result.Status = 0;
                                        result.Message = "Updated failed Balance Account. Please check log file!";
                                        return result;
                                    }
                                }
                            }
                            else
                            {
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Failed to create new Payment SubTransaction. Please check log file!";
                                WalletTransactionUnitOfWork.RollBack();
                                return result;
                            }
                        }
                    }                 

                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, false))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing false with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }
                    result.SubID = subID;
                    result.TranID = tran.Tran_ID;
                    WalletTransactionUnitOfWork.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                WalletTransactionUnitOfWork.RollBack();
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to create new payment transaction. Please check log file!";
                result.Status = 0;
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + pTranID + "|AccountID: " + accID, ex, "");
                return result;                
            }
            ResetStampServerKey();
            logWallet.Log(MethodBase.GetCurrentMethod(), tran, null, "", 1, 1);
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New Payment 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertInterestTransaction(WalletEntities newWalletEntities, string pTranID, string accID)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            var tran = new Transaction();
            SubTransaction subTran = new SubTransaction();
            Wallet_Account wallAcc = new Wallet_Account();
            string subID = "", lastCheckSum1 = "";
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            try
            {
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(newWalletEntities))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);

                    if (!walletLogic.CheckSumWalletAccount(wallAcc))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to check checksum for wallet account. Please check log file!";
                        return result;
                    }

                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, true))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing true with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }

                    using (var TransactionQueryBuilder = new TransactionQueryBuilder(new WalletEntities()))
                    {
                        tran = TransactionQueryBuilder.GetTranByTranID(pTranID).FirstOrDefault();
                    }

                    if (tran == null)
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to create find transaction. Please check log file!";
                        return result;
                    }

                    using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                    {
                        lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                    }

                    subID = walletLogic.GenerateNewSubTransaction_Interest(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                    if (!string.IsNullOrEmpty(subID))
                    {
                        if (walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, subTran.Amount ?? 0, subTran.Direction, "IN"))
                        {
                            if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, pTranID, subTran, ""))
                            {
                                WalletTransactionUnitOfWork.RollBack();
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                return result;
                            }
                        }
                        else
                        {
                            WalletTransactionUnitOfWork.RollBack();
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Updated failed Balance Account. Please check log file!";
                            return result;
                        }
                    }
                    else
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to create new Payment SubTransaction. Please check log file!";
                        WalletTransactionUnitOfWork.RollBack();
                        return result;
                    }

                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, false))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing false with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }
                    result.SubID = subID;
                    result.TranID = tran.Tran_ID;
                    WalletTransactionUnitOfWork.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                WalletTransactionUnitOfWork.RollBack();
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to create new interest transaction. Please check log file!";
                result.Status = 0;
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + pTranID + "|AccountID: " + accID, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New Convert Currency 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertConvertCurrency(string TranID, string SourceAccID, string DestAccID)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            var tran = new Transaction();
            SubTransaction subTran = new SubTransaction();
            Wallet_Account SourceWallAcc = new Wallet_Account();
            Wallet_Account DestWallAcc = new Wallet_Account();
            string subID = "", lastCheckSum1 = "";
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            try
            {
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    SourceWallAcc = WalletTransactionUnitOfWork.GetWalletByID(SourceAccID);
                    DestWallAcc = WalletTransactionUnitOfWork.GetWalletByID(DestAccID);

                    if (!walletLogic.UpdateIsProcessing(SourceWallAcc, WalletTransactionUnitOfWork, SourceAccID, true) || !walletLogic.UpdateIsProcessing(DestWallAcc, WalletTransactionUnitOfWork, DestAccID, true))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing true with ACCID: " + SourceAccID + ". Please check log file!";
                        return result;
                    }
                    using (var TransactionQueryBuilder = new TransactionQueryBuilder(new WalletEntities()))
                    {
                        tran = TransactionQueryBuilder.GetTranByTranID(TranID).FirstOrDefault();
                    }

                    if (tran == null)
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to find transaction. Please check log file!";
                        WalletTransactionUnitOfWork.RollBack();
                        return result;
                    }

                    using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                    {
                        lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                    }
                   
                        SubTransaction SourceSubTran = new SubTransaction();
                        SubTransaction DestSubTran = new SubTransaction();

                        subID = walletLogic.GenerateNewSubTransaction_ConvertCurrency(WalletTransactionUnitOfWork, tran, SourceAccID, DestAccID, ref SourceSubTran, ref DestSubTran, lastCheckSum1);
                        if (!string.IsNullOrEmpty(subID))
                        {
                            if (walletLogic.UpdateBalanceByAccountID(SourceWallAcc, WalletTransactionUnitOfWork, SourceAccID, SourceSubTran.Amount ?? 0, SourceSubTran.Direction, "OUT") && walletLogic.UpdateBalanceByAccountID(DestWallAcc, WalletTransactionUnitOfWork, DestAccID, DestSubTran.Amount ?? 0, DestSubTran.Direction, "OUT"))
                            {
                                if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, TranID, subTran, ""))
                                {
                                    WalletTransactionUnitOfWork.RollBack();
                                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                    result.Status = 0;
                                    result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                    return result;
                                }
                            }
                            else
                            {
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Updated failed Balance. Please check log file!";
                                WalletTransactionUnitOfWork.RollBack();
                                return result;
                            }
                        }
                        else
                        {
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Failed to create new Convert SubTransaction. Please check log file!";
                            WalletTransactionUnitOfWork.RollBack();
                            return result;
                        }

                    SourceWallAcc = WalletTransactionUnitOfWork.GetWalletByID(SourceAccID);
                    DestWallAcc = WalletTransactionUnitOfWork.GetWalletByID(DestAccID);

                    if (!walletLogic.UpdateIsProcessing(SourceWallAcc, WalletTransactionUnitOfWork, SourceAccID, false) || !walletLogic.UpdateIsProcessing(DestWallAcc, WalletTransactionUnitOfWork, DestAccID, false))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing false with ACCID: " + SourceAccID + ". Please check log file!";
                        return result;
                    }

                    result.SubID = subID;
                    result.TranID = tran.Tran_ID;
                    WalletTransactionUnitOfWork.EndTransaction();
                }                   
                
            }
            catch (Exception ex)
            {
                WalletTransactionUnitOfWork.RollBack();
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to create new convert transaction. Please check log file!";
                result.Status = 0;
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + TranID + "|Source AccountID: " + SourceAccID + "|Desc AccountID: " + DestAccID, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New Payment 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertWithdrawTransaction(string pTranID, string accID)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            var tran = new Transaction();
            var logWallet = new LogWallet();
            SubTransaction subTran = new SubTransaction();
            Wallet_Account wallAcc = new Wallet_Account();
            string subID = "", lastCheckSum1 = "";
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            try
            {

                // Please remove this code after run few day on live - 2019 March 18.
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    //UpdateChecksumWalletAccountByUserID(wallAcc.User_ID);
                }

                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
              
                    if (!walletLogic.CheckSumWalletAccount(wallAcc))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to check checksum for wallet account. Please check log file!";
                        return result;
                    }

                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, true))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing true with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }

                    using (var TransactionQueryBuilder = new TransactionQueryBuilder(new WalletEntities()))
                    {
                        tran = TransactionQueryBuilder.GetTranByTranID(pTranID).FirstOrDefault();
                    }

                    if (tran == null)
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to create find transaction. Please check log file!";
                        return result;
                    }

                    using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                    {
                        lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                    }

                    subID = walletLogic.GenerateNewSubTransaction_Withdraw(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                    if (!string.IsNullOrEmpty(subID))
                    {
                        if (walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, subTran.Amount ?? 0, subTran.Direction, "OUT"))
                        {
                            if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, pTranID, subTran, ""))
                            {
                                WalletTransactionUnitOfWork.RollBack();
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                return result;
                            }
                        }
                        else
                        {
                            WalletTransactionUnitOfWork.RollBack();
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Updated failed Balance Account. Please check log file!";
                            return result;
                        }
                    }
                    else
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to create new WithDraw SubTransaction. Please check log file!";
                        WalletTransactionUnitOfWork.RollBack();
                        return result;
                    }

                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    if (!walletLogic.UpdateIsProcessing(wallAcc, WalletTransactionUnitOfWork, accID, false))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing false with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }
                    result.SubID = subID;
                    result.TranID = tran.Tran_ID;
                    WalletTransactionUnitOfWork.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                WalletTransactionUnitOfWork.RollBack();
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to create new withdraw transaction. Please check log file!";
                result.Status = 0;
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + pTranID + "|AccountID: " + accID, ex, "");
                return result;
            }
            ResetStampServerKey();
            logWallet.Log(MethodBase.GetCurrentMethod(), tran, null, "", 1, 1);
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New Payment 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertRewardTransaction(decimal amtTopup,string accID, string remark = "", bool isAPI = false)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            var walletRewardLogic = new WalletRewardLogic();
            var logWallet = new LogWallet();
            Wallet_Account wallAcc = new Wallet_Account();
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            var walletEntity = new WalletEntities();
            
            decimal RewardAmount = 0;         
            try
            {
                if (!isAPI)
                {
                    if (!walletRewardLogic.CheckConditionReward(new DateTime(2019, 04, 05)))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "There is not any reward program for this moment.";
                        result.TranID = "Empty";
                        return result;
                    }
                }
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(walletEntity))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);

                    if (wallAcc == null)
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to create find wallet account. Please check log file!";
                        return result;
                    }

                    RewardAmount = isAPI ? amtTopup : walletRewardLogic.Calculate_Reward_Amount(amtTopup, wallAcc.Currency_Code);
                    if (RewardAmount == 0)
                    {
                        result.Code = 0;
                        result.Status = 0;
                        result.Message = "";
                        result.TranID = "Empty";
                        return result;
                    }

                    #region Insert TempTransaction

                    var tran = new Transaction();
                    tran.CreateDate = DateTime.Now;
                    tran.CreateUser =  "";
                    tran.Description = "REWARD";
                    tran.Merchant_ref = "";
                    tran.PaymentGateway = "";
                    tran.Remarks = remark;
                    tran.Source = "";
                    tran.Source_Amount = Convert.ToDecimal(string.Format("{0:0.00}", RewardAmount));
                    tran.Source_Currency = wallAcc.Currency_Code;
                    tran.Status = "true";
                    tran.User_ID = wallAcc.User_ID;
                    tran.Wallet_ID = wallAcc.Wallet_ID;
                    tran.Tran_ID = ("EW" + Guid.NewGuid().ToString().Replace("-", "")).Substring(0, 30);
                    WalletTransactionUnitOfWork.DoInsert(tran).SaveAndContinue();                              
                    #endregion

                    #region Insert SubTransaction

                    var lastCheckSum1 = new SubTransactionQueryBuilder(walletEntity).GetLastCheckSum1();
                    var SubTran = new SubTransaction();
                    SubTran.Tran_ID = tran.Tran_ID;
                    SubTran.Account_ID = wallAcc.ID;
                    SubTran.User_ID = wallAcc.User_ID;
                    SubTran.Amount = Convert.ToDecimal(string.Format("{0:0.00}", RewardAmount));
                    SubTran.Currency_Code = wallAcc.Currency_Code;
                    SubTran.Direction = "IN";
                    SubTran.Remarks = "REWARD";
                    SubTran.Verified = true;
                    SubTran.CreateUser ="";
                    SubTran.CreateDate = DateTime.Now;
                    SubTran.Sub_ID = SecurityLogic.GenerateKey(30);
                    SubTran.Checksum1 = walletLogic.BuildCheckSum1(lastCheckSum1, SubTran, true);
                    var strCheckSum2 = walletLogic.BuildCheckSum2(SubTran.Checksum1, SubTran, true);
                    if (strCheckSum2 != "")
                    {
                        SubTran.Checksum2 = strCheckSum2;
                    }
                    else
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        logWallet.Log(MethodBase.GetCurrentMethod(), tran.Tran_ID, null, "Can not build checksum2");
                        return result;
                    }
                    WalletTransactionUnitOfWork.DoInsert(SubTran).SaveAndContinue();
                    #endregion
                    
                    var flg = walletRewardLogic.UpdateRewardByAccIDForTopup(WalletTransactionUnitOfWork, accID, RewardAmount, "");
                    if (!flg)
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Updated failed UpdateRewardByAccIDForTopup. Please check log file!";
                        return result;
                    }
                    
                    result.TranID = tran.Tran_ID;
                    WalletTransactionUnitOfWork.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                WalletTransactionUnitOfWork.RollBack();
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to create new Reward transaction. Please check log file!";
                result.Status = 0;
                logWallet.Log(MethodBase.GetCurrentMethod(), "|AccountID: " + accID, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public WalletAccountResponse GetWalletAccountByWalletID(string pWalletID)
        {
            var result = new WalletAccountResponse();
            var walletLogic = new WalletAccountLogic();
            try
            {
                if (!walletLogic.IsExistWalletID(pWalletID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = "WalletID is not existed!";
                    return result;
                }
                var lstWalletAcc = walletLogic.GetWalletAccountByWalletID(pWalletID);
                result.lstWalletAccount = new System.Collections.Generic.List<WalletTransferObject>();
                // Automapper will coming soon.
                foreach (var WalletAcc in lstWalletAcc)
                {
                    var newwallACC = new WalletTransferObject();
                    newwallACC.Available_Balance = WalletAcc.Available_Balance;
                    newwallACC.Currency_Code = WalletAcc.Currency_Code;
                    newwallACC.ID = WalletAcc.ID;
                    newwallACC.Total_Balance = WalletAcc.Total_Balance;
                    newwallACC.User_ID = WalletAcc.User_ID;
                    newwallACC.Wallet_ID = WalletAcc.Wallet_ID;
                    result.lstWalletAccount.Add(newwallACC);
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Wallet ID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pWalletID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public WalletAccountResponse GetWalletAccountByUserID(string pUserID)
        {
            var result = new WalletAccountResponse();
            var walletLogic = new WalletAccountLogic();
            var walletRewardLogic = new WalletRewardLogic();
            var lstWalletReward = new List<Wallet_Account_Reward>();
            result.lstWalletAccount = new List<WalletTransferObject>();
            var logWallet = new LogWallet();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    using (var CurrenciesQueryBuilder = new CurrenciesQueryBuilder(new WalletEntities()))
                    {
                        foreach (var CurrencyCode in CurrenciesQueryBuilder.ToList())
                        {
                            var newwallACC = new WalletTransferObject();
                            newwallACC.Available_Balance = 0;
                            newwallACC.Currency_Code = CurrencyCode.Currency_Code;
                            newwallACC.ID = string.Empty;
                            newwallACC.Total_Balance = 0;
                            newwallACC.User_ID = string.Empty;
                            newwallACC.Wallet_ID = string.Empty;
                            result.lstWalletAccount.Add(newwallACC);
                        }
                    }
                }
                else
                {
                    var lstWalletAcc = walletLogic.GetWalletAccountByUserID(pUserID);
                    if (lstWalletAcc != null)
                    {
                        if (lstWalletAcc.Count > 0)
                        { lstWalletReward = walletRewardLogic.GetRewardByWalletID(lstWalletAcc[0].Wallet_ID); }
                    }
                    
                    // Automapper will coming soon.
                    foreach (var WalletAcc in lstWalletAcc)
                    {
                        var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(pUserID, WalletAcc.Currency_Code);
                        var totalPendingAmt = 0.0M;
                        decimal rewardAmount = 0;
                        
                        foreach (var pendingAmtWithdraw in TransWithdraw)
                        {
                            totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                        }
                        if (lstWalletReward != null)
                        {
                            if (lstWalletReward.Count > 0)
                            {
                                rewardAmount = lstWalletReward.Find(p => p.ID == WalletAcc.ID).Reward_Amount;
                            }
                        }
                        
                        var newwallACC = new WalletTransferObject();
                        newwallACC.Available_Balance = WalletAcc.Available_Balance - totalPendingAmt + rewardAmount;
                        newwallACC.Currency_Code = WalletAcc.Currency_Code;
                        newwallACC.ID = WalletAcc.ID;
                        newwallACC.Total_Balance = WalletAcc.Total_Balance;
                        newwallACC.User_ID = WalletAcc.User_ID;
                        newwallACC.Wallet_ID = WalletAcc.Wallet_ID;
                        result.lstWalletAccount.Add(newwallACC);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Wallet ID";
               
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public WalletAccountResponse GetWalletAccountByUserIDWithoutReward(string pUserID)
        {
            var result = new WalletAccountResponse();
            var walletLogic = new WalletAccountLogic();
       
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.lstWalletAccount = new List<WalletTransferObject>();
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = "WalletID is not existed or is new UserID!";
                    return result;
                }

                var lstWalletAcc = walletLogic.GetWalletAccountByUserID(pUserID);
                result.lstWalletAccount = new List<WalletTransferObject>();
            
                // Automapper will coming soon.
                foreach (var WalletAcc in lstWalletAcc)
                {
                    var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(pUserID, WalletAcc.Currency_Code);
                    var totalPendingAmt = 0.0M;
                 
                    foreach (var pendingAmtWithdraw in TransWithdraw)
                    {
                        totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                    }                  

                    var newwallACC = new WalletTransferObject();
                    newwallACC.Available_Balance = WalletAcc.Available_Balance - totalPendingAmt;
                    newwallACC.Currency_Code = WalletAcc.Currency_Code;
                    newwallACC.ID = WalletAcc.ID;
                    newwallACC.Total_Balance = WalletAcc.Total_Balance;
                    newwallACC.User_ID = WalletAcc.User_ID;
                    newwallACC.Wallet_ID = WalletAcc.Wallet_ID;
                    result.lstWalletAccount.Add(newwallACC);
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Wallet ID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public WalletAccountResponse GetWalletAccountByUserIDForMobile(string pUserID)
        {
            var result = new WalletAccountResponse();
            var walletLogic = new WalletAccountLogic();
            result.lstWalletAccount = new List<WalletTransferObject>();
            var walletRewardLogic = new WalletRewardLogic();
            var lstWalletReward = new List<Wallet_Account_Reward>();

            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    using (var CurrenciesQueryBuilder = new CurrenciesQueryBuilder(new WalletEntities()))
                    {
                        foreach (var CurrencyCode in CurrenciesQueryBuilder.ToList())
                        {                          
                            var newwallACC = new WalletTransferObject();
                            newwallACC.Available_Balance = 0;
                            newwallACC.Currency_Code = CurrencyCode.Currency_Code;
                            newwallACC.ID = string.Empty;
                            newwallACC.Total_Balance = 0;
                            newwallACC.User_ID = string.Empty;
                            newwallACC.Wallet_ID = string.Empty;
                            result.lstWalletAccount.Add(newwallACC);
                        }
                    }
                }
                else
                {
                    var lstWalletAcc = walletLogic.GetWalletAccountByUserID(pUserID);
                    
                    result.lstWalletAccount = new List<WalletTransferObject>();
                    if (lstWalletAcc.Count > 0)
                    {
                        lstWalletReward = walletRewardLogic.GetRewardByWalletID(lstWalletAcc[0].Wallet_ID);
                    }

                    // Automapper will coming soon.
                    foreach (var WalletAcc in lstWalletAcc)
                    {
                        var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(pUserID, WalletAcc.Currency_Code);
                        var totalPendingAmt = 0.0M;
                        foreach (var pendingAmtWithdraw in TransWithdraw)
                        {
                            totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                        }
                        var rewardAmount = lstWalletReward.Find(p => p.ID == WalletAcc.ID).Reward_Amount;

                        var newwallACC = new WalletTransferObject();
                        newwallACC.Available_Balance = WalletAcc.Available_Balance - totalPendingAmt + rewardAmount;
                        newwallACC.Currency_Code = WalletAcc.Currency_Code;
                        newwallACC.ID = WalletAcc.ID;
                        newwallACC.Total_Balance = WalletAcc.Total_Balance;
                        newwallACC.User_ID = WalletAcc.User_ID;
                        newwallACC.Wallet_ID = WalletAcc.Wallet_ID;
                        result.lstWalletAccount.Add(newwallACC);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Wallet ID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public TransactionResponse GetTranWalletByTranID(string pTranID)
        {
            var result = new TransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {   
                var tranResponse = walletAccLogic.GetWalletTranByTranID(pTranID);
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet. Please check log file";                   
                }
                result.Wallet_ID= tranResponse.Wallet_ID;
                result.TranID = tranResponse.Tran_ID;
                result.User_ID = tranResponse.User_ID;
                result.TranStatus = tranResponse.Status;
                result.Description = tranResponse.Description;
                result.Source_Currency = tranResponse.Source_Currency;
                result.Source_Amount = tranResponse.Source_Amount;
                result.Source = tranResponse.Source;
                result.Remarks = tranResponse.Remarks;
                result.PaymentGateWay = tranResponse.PaymentGateway;
                result.Merchant_ref = tranResponse.Merchant_ref;
                result.Destination_Currency = tranResponse.Destination_Currency;
                result.Destination_Amount = tranResponse.Destination_Amount;
                result.CreateUser = tranResponse.CreateUser;
                result.CreateDate = tranResponse.CreateDate;                    
                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Tran ID: " + pTranID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public ListTransactionResponse GetTransBQ()
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = walletAccLogic.GetTransBQ();
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet for BQ. Please check log file";
                }
                var lstTran = new List<TransactionTransferObject>();

                foreach (var tran in tranResponse)
                {
                    using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                    {
                        if (!SubTransactionQueryBuilder.IsExistTranIDinSubTransaction(tran.Tran_ID))
                        {
                            var tranObjectTransfer = new TransactionTransferObject();
                            tranObjectTransfer.Wallet_ID = tran.Wallet_ID;
                            tranObjectTransfer.TranID = tran.Tran_ID;
                            tranObjectTransfer.User_ID = tran.User_ID;
                            tranObjectTransfer.TranStatus = tran.Status;
                            tranObjectTransfer.Source_Currency = tran.Source_Currency;
                            tranObjectTransfer.Source_Amount = tran.Source_Amount;
                            tranObjectTransfer.Source = tran.Source;
                            tranObjectTransfer.Remarks = tran.Remarks;
                            tranObjectTransfer.Description = tran.Description;
                            tranObjectTransfer.PaymentGateWay = tran.PaymentGateway;
                            tranObjectTransfer.Merchant_ref = tran.Merchant_ref;
                            tranObjectTransfer.Destination_Currency = tran.Destination_Currency;
                            tranObjectTransfer.Destination_Amount = tran.Destination_Amount;
                            tranObjectTransfer.CreateUser = tran.CreateUser;
                            tranObjectTransfer.CreateDate = tran.CreateDate;
                            tranObjectTransfer.Scheduler_Check = tran.Scheduler_Check;
                            lstTran.Add(tranObjectTransfer);
                        }
                    }                   
                }
                result.lstTrans = lstTran;
                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet for BQ";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public ListTransactionResponse GetTransWithdraw()
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = walletAccLogic.GetTransWithdraw();
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet for withdraw. Please check log file";
                }
                var lstTran = new List<TransactionTransferObject>();

                foreach (var tran in tranResponse)
                {                    
                    var tranObjectTransfer = new TransactionTransferObject();
                    tranObjectTransfer.Wallet_ID = tran.Wallet_ID;
                    tranObjectTransfer.TranID = tran.Tran_ID;
                    tranObjectTransfer.User_ID = tran.User_ID;
                    tranObjectTransfer.TranStatus = tran.Status;
                    tranObjectTransfer.Source_Currency = tran.Source_Currency;
                    tranObjectTransfer.Source_Amount = tran.Source_Amount;
                    tranObjectTransfer.Source = tran.Source;
                    tranObjectTransfer.Remarks = tran.Remarks;
                    tranObjectTransfer.Description = tran.Description;
                    tranObjectTransfer.PaymentGateWay = tran.PaymentGateway;
                    tranObjectTransfer.Merchant_ref = tran.Merchant_ref;
                    tranObjectTransfer.Destination_Currency = tran.Destination_Currency;
                    tranObjectTransfer.Destination_Amount = tran.Destination_Amount;
                    tranObjectTransfer.CreateUser = tran.CreateUser;
                    tranObjectTransfer.CreateDate = tran.CreateDate;
                    tranObjectTransfer.Scheduler_Check = tran.Scheduler_Check;
                    tranObjectTransfer.Data_Check = !string.IsNullOrEmpty(tran.Status) && tran.Status != "NULL" ? tran.Status : string.Empty;
                    lstTran.Add(tranObjectTransfer);                    
                }
                result.lstTrans = lstTran;
                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet for withdraw";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }


        public ListTransactionResponse GetTransWithdrawWithVerified()
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = walletAccLogic.GetTransWithdrawWithVerified();
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet for withdraw. Please check log file";
                }
                var lstTran = new List<TransactionTransferObject>();

                foreach (var tran in tranResponse)
                {
                    var tranObjectTransfer = new TransactionTransferObject();
                    tranObjectTransfer.Wallet_ID = tran.Wallet_ID;
                    tranObjectTransfer.TranID = tran.Tran_ID;
                    tranObjectTransfer.User_ID = tran.User_ID;
                    tranObjectTransfer.TranStatus = tran.Status;
                    tranObjectTransfer.Source_Currency = tran.Source_Currency;
                    tranObjectTransfer.Source_Amount = tran.Source_Amount;
                    tranObjectTransfer.Source = tran.Source;
                    tranObjectTransfer.Remarks = tran.Remarks;
                    tranObjectTransfer.Description = tran.Description;
                    tranObjectTransfer.PaymentGateWay = tran.PaymentGateway;
                    tranObjectTransfer.Merchant_ref = tran.Merchant_ref;
                    tranObjectTransfer.Destination_Currency = tran.Destination_Currency;
                    tranObjectTransfer.Destination_Amount = tran.Destination_Amount;
                    tranObjectTransfer.CreateUser = tran.CreateUser;
                    tranObjectTransfer.CreateDate = tran.CreateDate;
                    tranObjectTransfer.Scheduler_Check = tran.Scheduler_Check;
                    lstTran.Add(tranObjectTransfer);
                }
                result.lstTrans = lstTran;
                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet for withdraw";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public ListTransactionResponse GetTransTopupWithoutVerified()
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = walletAccLogic.GetTransTopupWithoutVerified();
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet for withdraw. Please check log file";
                }
                var lstTran = new List<TransactionTransferObject>();

                foreach (var tran in tranResponse)
                {
                    var tranObjectTransfer = new TransactionTransferObject();
                    tranObjectTransfer.Wallet_ID = tran.Wallet_ID;
                    tranObjectTransfer.TranID = tran.Tran_ID;
                    tranObjectTransfer.User_ID = tran.User_ID;
                    tranObjectTransfer.TranStatus = tran.Status;
                    tranObjectTransfer.Source_Currency = tran.Source_Currency;
                    tranObjectTransfer.Source_Amount = tran.Source_Amount;
                    tranObjectTransfer.Source = tran.Source;
                    tranObjectTransfer.Description = tran.Description;
                    tranObjectTransfer.Remarks = tran.Remarks;
                    tranObjectTransfer.PaymentGateWay = tran.PaymentGateway;
                    tranObjectTransfer.Merchant_ref = tran.Merchant_ref;
                    tranObjectTransfer.Destination_Currency = tran.Destination_Currency;
                    tranObjectTransfer.Destination_Amount = tran.Destination_Amount;
                    tranObjectTransfer.CreateUser = tran.CreateUser;
                    tranObjectTransfer.CreateDate = tran.CreateDate;
                    tranObjectTransfer.Scheduler_Check = tran.Scheduler_Check;
                    lstTran.Add(tranObjectTransfer);
                }
                result.lstTrans = lstTran;
                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet for Topup without verified";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public WalletUserResponse GetWalletIDByUserID(string pUserID)
        {
            var result = new WalletUserResponse();
            var walletUserLogic = new WalletUserLogic();
            var walletLogic = new WalletAccountLogic();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = "WalletID is not existed!";
                    return result;
                }
                var WalletUser = walletUserLogic.GetWalletIDByUserID(pUserID);
                result.WalletID = WalletUser.Wallet_ID;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Wallet ID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public WalletUserAccountResponse GetWalletAccIDByCurrencyCodenUserID(string pUserID, string pCurrencyCode)
        {
            var result = new WalletUserAccountResponse();
            var walletLogic = new WalletAccountLogic();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = "WalletID is not existed!";
                    return result;
                }
                var WalletUser = walletLogic.GetWalletAccByUserIDnCurrencyCode(pUserID, pCurrencyCode);
                if (string.IsNullOrEmpty(WalletUser.ID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETACC_NOTEXIST.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLETACC_NOTEXIST.Message;
                    return result;
                }
                result.AccountID = WalletUser.ID;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Account ID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By Email for EBK
        /// </summary>
        /// <returns></returns>
        public WalletAccountResponse GetUserWalletByEmail_EBK(string pEmail, string pCreateUser)
        {
            var result = new WalletAccountResponse();
            var walletUserLogic = new WalletUserLogic();
            try
            {
                //pEmail = SimpleAesUtil.Decrypt(pEmail);
                var AspNetUser = walletUserLogic.GetAspNetUserByEmail(pEmail);
                if (string.IsNullOrEmpty(AspNetUser.Id))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_USERID_NOTEXIST.Code;
                    result.Status = 0;
                    result.Message = "User is not existed!";
                    return result;
                }

                var UserWallet = walletUserLogic.GetUserWalletByUserID(AspNetUser.Id);
                if (UserWallet == null)
                {
                    var user = walletUserLogic.ConvertAspNetUserToUser(AspNetUser);
                    var UserID = walletUserLogic.InsertUserRecord(user);

                    if (string.IsNullOrEmpty(UserID))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to insert new User Account. Please check log file!";
                        return result;
                    }

                    var NewWalletAcc = InsertNewWalletAccount(UserID, pCreateUser);

                    if (string.IsNullOrEmpty(NewWalletAcc.WalletID))
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to insert new Wallet Account. Please check log file!";
                        return result;
                    }
               
                    result = GetWalletAccountByWalletID(NewWalletAcc.WalletID);
                }
                else
                {
                    result = GetWalletAccountByUserID(UserWallet.User_ID);
                }
                
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Email";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Email ID: " + pEmail, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account by Transaction ID
        /// </summary>
        /// <returns></returns>
        public WalletAmountResponse GetAvailableAmtAccbyTranID(string pTranID, string pCurrencyCode)
        {
            var result = new WalletAmountResponse();
            var walletLogic = new WalletAccountLogic();
            var walletRewardLogic = new WalletRewardLogic();
            try
            {
                var tran = walletLogic.GetWalletTranByTranID(pTranID);
                if (string.IsNullOrEmpty(tran.Status))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_TRAN_NOTEXIST.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_TRAN_NOTEXIST.Message;
                    return result;
                }
                var WalletAcc = walletLogic.GetWalletAccByUserIDnCurrencyCode(tran.User_ID, pCurrencyCode);
                if (WalletAcc==null)
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Message;
                    return result;
                }

                var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(tran.User_ID, pCurrencyCode);
                var totalPendingAmt = 0.0M;
                foreach (var pendingAmtWithdraw in TransWithdraw)
                {
                    totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                }

                var walletReward = walletRewardLogic.GetRewardByAccID(WalletAcc.ID);
                decimal rewardAmount = 0;
                if (walletReward != null)
                {
                    rewardAmount = walletReward.Reward_Amount;
                }
                result.AvailableAmount = (WalletAcc.Available_Balance??0) - totalPendingAmt + rewardAmount;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Available Amount";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Tran ID: " + pTranID + "Currency Code: " + pCurrencyCode, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account by Transaction ID
        /// </summary>
        /// <returns></returns>
        public WalletAmountResponse GetAvailableAmtAccWithoutRewardbyTranID(string pTranID, string pCurrencyCode)
        {
            var result = new WalletAmountResponse();
            var walletLogic = new WalletAccountLogic();
            var walletRewardLogic = new WalletRewardLogic();
            try
            {
                var tran = walletLogic.GetWalletTranByTranID(pTranID);
                if (string.IsNullOrEmpty(tran.Status))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_TRAN_NOTEXIST.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_TRAN_NOTEXIST.Message;
                    return result;
                }
                var WalletAcc = walletLogic.GetWalletAccByUserIDnCurrencyCode(tran.User_ID, pCurrencyCode);
                if (WalletAcc == null)
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Message;
                    return result;
                }

                var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(tran.User_ID, pCurrencyCode);
                var totalPendingAmt = 0.0M;
                foreach (var pendingAmtWithdraw in TransWithdraw)
                {
                    totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                }              
                result.AvailableAmount = (WalletAcc.Available_Balance ?? 0) - totalPendingAmt;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Available Amount";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Tran ID: " + pTranID + "Currency Code: " + pCurrencyCode, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }
        private void ResetStampServerKey()
        {
            Globals.StampServerKey = "";
        }

        /// <summary>
        /// Get SubTrans by UserID
        /// </summary>
        /// <returns></returns>
        public ListSubTransactionResponse GetSubTransByUserId(string pUserID, int pPageNumber, int pRow, string pDateFrom, string pDateTo, int pTransactionType)
        {
            var result = new ListSubTransactionResponse();
            var tranLogic = new WalletTransactionLogic();
            var walletLogic = new WalletAccountLogic();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.Code = 1;
                    result.Status = 1;
                    result.Message = "Please topup to use this feature.";
                    return result;
                }

                DateTime dFrom = new DateTime(int.Parse(pDateFrom.Split('-')[0]), int.Parse(pDateFrom.Split('-')[1]), int.Parse(pDateFrom.Split('-')[2]), 0, 0, 1);
                DateTime dTo = new DateTime(int.Parse(pDateTo.Split('-')[0]), int.Parse(pDateTo.Split('-')[1]), int.Parse(pDateTo.Split('-')[2]), 23, 59, 59);

                if (dFrom.AddDays(+31).CompareTo(dTo) <= 0)
                {
                    dTo = dFrom.AddDays(+31);
                    //result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_OVERDATE_RANGE_.Code;
                    //result.Status = 0;
                    //result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_OVERDATE_RANGE_.Message;
                    //return result;
                }

                var RecordTotal = 0;
                var SubTrans = tranLogic.GetSubTransByUserId(pUserID, pPageNumber, pRow, dFrom, dTo, ref RecordTotal, pTransactionType);
                result.RecordTotal = RecordTotal;
                result.lstSubTrans = SubTrans;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query List SubTrans";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;

            return result;
        }


        /// <summary>
        /// Get SubTrans by UserID
        /// </summary>
        /// <returns></returns>
        public UpdateTranBQResponse UpdateStatusBQForTran(string pTranID)
        {
            var tranLogic = new WalletTransactionLogic();
            var response = new UpdateTranBQResponse();
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            try
            {
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    var flgResult = tranLogic.UpdateStatusBQForTran(WalletTransactionUnitOfWork, pTranID);
                    response.StatusUpdate = flgResult;
                }                      
            }
            catch (Exception ex)
            {
                response.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                response.Status = 0;
                response.Message = "Failed to UpdateStatusBQForTran";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Tran ID: " + pTranID, ex, "");
                return response;
            }
            response.Code = ApiReturnCodeConstant.SUCCESS.Code;
            response.Message = ApiReturnCodeConstant.SUCCESS.Message;
            response.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return response;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public SubTransactionReponse GetSubTranByTran_Amt(string pTranID, decimal pAmount)
        {
            var result = new SubTransactionReponse();
            var TranLogic = new WalletTransactionLogic();
            try
            {
                var SubTran = TranLogic.GetSubTranByTran_Amt(pTranID, pAmount);
                if (SubTran != null)
                { result.subTranID = SubTran.Sub_ID; }                
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Tran ID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "Tran ID: " + pTranID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New User
        /// </summary>
        /// <returns></returns>
        public UserCardResponse InsertNewUserCard(UserCardRequest pUserCard)
        {
            var result = new UserCardResponse();
            var walletLogic = new WalletUserLogic(true);
            var usercard = new User_Card();
            
            try
            {
                usercard.Bank_Name = pUserCard.Bank_Name;
                usercard.Card_ExpireDate = pUserCard.Card_ExpireDate;
                usercard.Card_HolderName = pUserCard.Card_HolderName;

                usercard.Card_Number = SimpleAesUtil.Encrypt(SimpleAesUtil.DecryptAES(pUserCard.Card_Number, EwalletConstant.keyAES) + Globals.StampServerKey);
                usercard.Card_Type = pUserCard.Card_Type;
                usercard.Create_date = DateTime.Now;
                usercard.Currency_Code = pUserCard.Currency_Code;
                usercard.ID = Guid.NewGuid().ToString();
                usercard.Update_date = DateTime.Now;
                usercard.User_ID = pUserCard.User_ID;
                   
                var flgResult = walletLogic.InsertUserCard(usercard);
                if (!flgResult)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to insert new User Card. Please check log file!";
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + pUserCard.User_ID, null, result.Message);
                    return result;
                }
                result.CardID = usercard.ID;
               
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to insert new User Card";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + pUserCard.User_ID, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public UserCardResponse DeleteUserCardByID(string pCardID, string pUserID)
        {
            var result = new UserCardResponse();
            var walletUserLogic = new WalletUserLogic();
            var walletLogic = new WalletAccountLogic();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = "UserID is not existed!";
                    return result;
                }
                var flg = walletUserLogic.DeleteUserCard(pCardID);
                result.CardID = pCardID;
                result.Status = flg ? 1:0;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query User Card";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID + "|Card ID: " + pCardID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            return result;
        }

        /// <summary>
        /// Get Wallet Account By WalletID
        /// </summary>
        /// <returns></returns>
        public ListUserCardResponse GetUserCardByUserIDnCurrency(string pUserID, string pCurrency)
        {
            var result = new ListUserCardResponse();
            var walletUserLogic = new WalletUserLogic(true);
            var walletLogic = new WalletAccountLogic();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLETID_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = "UserID is not existed!";
                    return result;
                }
                var UserCard = walletUserLogic.GetUserCardByUserIDnCurrency(pUserID, pCurrency);
                foreach (var card in UserCard)
                {
                    card.Card_Number = SimpleAesUtil.EncryptAES(SimpleAesUtil.Decrypt(card.Card_Number).Replace(Globals.StampServerKey,""), EwalletConstant.keyAES); 
                }
                result.lstUserCard = UserCard;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query User Card";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID + "|Currency: " + pCurrency, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public ListUserCardResponse GetUserCardByUserID(string pUserID)
        {
            var result = new ListUserCardResponse();
            var walletUserLogic = new WalletUserLogic(true);
            var walletLogic = new WalletAccountLogic();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.Code = 1;
                    result.Status = 1;
                    result.Message = "Please topup to use this feature.";
                    result.lstUserCard = new List<User_Card>();
                    return result;
                }
                var UserCard = walletUserLogic.GetUserCardByUserID(pUserID);
                foreach (var card in UserCard)
                {
                    card.Card_Number = SimpleAesUtil.EncryptAES(SimpleAesUtil.Decrypt(card.Card_Number).Replace(Globals.StampServerKey, ""), EwalletConstant.keyAES);
                }
                result.lstUserCard = UserCard;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query User Card";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID , ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public UserResponse UpdateChecksumWalletAccount()
        {
            var result = new UserResponse();         
            var walletLogic = new WalletAccountLogic();
            bool flg = false;
            try
            {             
                flg = walletLogic.UpdateAllCheckSumWalletAccount();               
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to UpdateChecksumWalletAccount";
                var logWallet = new LogWallet();
                return result;
            }
            
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = flg?1:0;
            return result;
        }

        public UserResponse UpdateChecksumWalletAccountByUserID(string pUserID)
        {
            var result = new UserResponse();
            var walletLogic = new WalletAccountLogic();
            bool flg = false;
            try
            {             
                flg = walletLogic.UpdateAllCheckSumWalletAccountByUserID(pUserID);
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to UpdateChecksumWalletAccount by UserID:" + pUserID;
                var logWallet = new LogWallet();
                return result;
            }

            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = flg ? 1 : 0;
            return result;
        }

        public SnapShotResponse InsertSnapshot(string pNow)
        {
            var result = new SnapShotResponse();
            var walletSnapshotLogic = new WalletSnapshotLogic();
            bool flg = false;
            try
            {
                flg = walletSnapshotLogic.InsertSnapshot(pNow);
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertSnapshot";
                var logWallet = new LogWallet();
                return result;
            }

            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = flg ? 1 : 0;
            return result;
        }

        public InterestResponse InsertInterest_ForAll(string pSource)
        {
            var result = new InterestResponse();
            var logWallet = new LogWallet();
            try
            {               
                var Alluserwallet = new WalletUserQueryBuilder(new WalletEntities()).GetAllUserWallet();
                foreach (var userwallet in Alluserwallet.ToList())
                {
                    result = InsertInterest_ByUserID(userwallet.User_ID, pSource);
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertInterest_ByDatenUserID";
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
        }

        public InterestResponse InsertInterest_RootDate(string pRootDate)
        {
            var result = new InterestResponse();
            var logWallet = new LogWallet();
            try
            {
                var Alluserwallet = new WalletUserQueryBuilder(new WalletEntities()).GetAllUserWallet();
                foreach (var userwallet in Alluserwallet.ToList())
                {
                    result = InsertRootDate_Interest(userwallet.User_ID, pRootDate);
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertInterest_ByDatenUserID";
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
        }

        //public InterestResponse InsertInterest_ByUserID(string pUserID, string pSource)
        //{
        //    var result = new InterestResponse();
        //    var walletTranLogic = new WalletTransactionLogic();
        //    var WalletInterestLogic = new WalletInterestLogic();
        //    var logWallet = new LogWallet();
        //    var lstTrans = new List<Transaction>();
        //    try
        //    {
        //        var newWalletEntities = new WalletEntities();
        //        var curDateTime = DateTime.Now;
        //        var startDateCount = curDateTime;
        //        var WalletInterestQueryBuilder = new WalletInterestQueryBuilder(newWalletEntities);
        //        var lstWalletInterest = WalletInterestQueryBuilder.ToList();
        //        var WalletAcc = GetWalletAccountByUserID(pUserID);
        //        //var WalletAcc = walletAPI.GetWalletAccountByUserID(pUserID);
        //        decimal totalAmountPreviousMonth = 0;
        //        if (WalletAcc.Status == 0)
        //        {
        //            logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, WalletAcc.Message);
        //            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
        //            result.Status = 0;
        //            result.Message = WalletAcc.Message;
        //            return result;
        //        }
        //        foreach (var acc in WalletAcc.lstWalletAccount.FindAll(p => p.Available_Balance >0))
        //        {
        //            try
        //            {
        //                var rate = lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_day;
        //                acc.InterestRate = Convert.ToDecimal(lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_year);
        //                var DateInterestSnapshot = new TransactionInterestSnapshotQueryBuilder(newWalletEntities).GetTranNearestDateByAccID(acc.ID);
        //                if (DateInterestSnapshot.Count() > 0)
        //                {
        //                    startDateCount = DateInterestSnapshot.FirstOrDefault().Createdate;
        //                    totalAmountPreviousMonth = DateInterestSnapshot.FirstOrDefault().Total_Amount;
        //                }
        //                else
        //                {
        //                    logWallet.Log(MethodBase.GetCurrentMethod(), acc.ID, null, "Could not get GetTranNearestDateByAccID");
        //                    continue;
        //                }
        //                double TotalamtInterest = 0;
        //                double amtInterest = 0;

        //                if (curDateTime.CompareTo(startDateCount) > 0)
        //                {
        //                    var lstSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetSubTransByUserIDnMonth(pUserID, acc.Currency_Code, startDateCount, curDateTime).ToList();
        //                    foreach (var subTran in lstSubTrans)
        //                    {
        //                        var dateTran = subTran.CreateDate;
        //                        int remaindate = Convert.ToInt32(Math.Round((curDateTime.Subtract(dateTran).TotalDays)));

        //                        if (subTran.Direction == "IN")
        //                        {
        //                            amtInterest = +((Convert.ToDouble(subTran.Amount) * remaindate * double.Parse(rate)) / 100);
        //                        }
        //                        else
        //                        {
        //                            amtInterest = -((Convert.ToDouble(subTran.Amount) * remaindate * double.Parse(rate)) / 100);
        //                        }
        //                        TotalamtInterest += amtInterest;
        //                    }

        //                    var totalDateInMonth = Convert.ToInt32((curDateTime.Subtract(startDateCount).TotalDays));
        //                    TotalamtInterest += ((Convert.ToDouble(totalAmountPreviousMonth) * totalDateInMonth * double.Parse(rate)) / 100);
        //                    acc.InterestEarned = Convert.ToDecimal(string.Format("{0:0.00}", TotalamtInterest));

        //                    var TempTran = walletTranLogic.BuildTransactionInterest(acc.InterestEarned ?? 0, acc.Currency_Code, pUserID, acc.ID, pSource, acc.Wallet_ID, rate);
        //                    if (walletTranLogic.InsertTempTransaction(TempTran))
        //                    {
        //                        var tranResponse = InsertInterestTransaction(newWalletEntities, TempTran.Tran_ID, acc.ID);
        //                        if (tranResponse.Status == 1)
        //                        {
        //                            var totalAmt = GetAvailableAmtAccWithoutRewardbyTranID(TempTran.Tran_ID, TempTran.Source_Currency).AvailableAmount;
        //                            var strIDInterest = WalletInterestLogic.GenerateNewTransaction_Interest_Snapshot(newWalletEntities, TempTran.Tran_ID, TempTran.Source_Amount??0, acc.ID, totalAmt, "");
        //                            if (string.IsNullOrEmpty(strIDInterest))
        //                            {
        //                                logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, "Inserted failed InsertInterest_ByDatenUserID: totalAmt: " + totalAmt + "AccID: " + acc.ID);
        //                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
        //                                result.Status = 0;
        //                                result.Message = "Inserted failed InsertInterest_ByDatenUserID. Please check log file!";
        //                            }
        //                            else
        //                            {
        //                                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
        //                                result.Status = ApiReturnCodeConstant.SUCCESS.Code;
        //                                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
        //                                result.InterestID = strIDInterest;
        //                                TempTran.Merchant_ref = strIDInterest;
        //                                lstTrans.Add(TempTran);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, "Inserted failed InsertInterest_ByDatenUserID: " + tranResponse.Message + "AccID: " + acc.ID);
        //                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
        //                            result.Status = 0;
        //                            result.Message = "Inserted failed InsertInterest_ByDatenUserID. Please check log file!";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    logWallet.Log(MethodBase.GetCurrentMethod(), acc.ID, null, "Could not get GetTranNearestDateByAccID: " + "Curdate: " + curDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "| StartDateCount: " + startDateCount.ToString("yyyy-MM-dd HH:mm:ss"));
        //                    continue;
        //                }
        //            }
        //            catch (Exception ex)
        //            {                       
        //                logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, acc.ID);
        //                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
        //                result.Status = 0;
        //                result.Message = "Inserted failed InsertInterest_ByDatenUserID. Please check log file!";
        //            }
        //        }
        //        result.lstTrans = lstTrans;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
        //        result.Status = 0;
        //        result.Message = "Failed to InsertInterest_ByDatenUserID";
        //        logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, "");
        //        return result;
        //    }
        //    return result;
        //}

        public InterestResponse InsertInterest_ByUserID(string pUserID, string pSource)
        {
            var result = new InterestResponse();
            var walletTranLogic = new WalletTransactionLogic();
            var WalletInterestLogic = new WalletInterestLogic();
            var logWallet = new LogWallet();
            var lstTrans = new List<Transaction>();
            try
            {
                var newWalletEntities = new WalletEntities();
                var curDateTime = DateTime.Now;
                var startDateCount = curDateTime;
                var WalletInterestQueryBuilder = new WalletInterestQueryBuilder(newWalletEntities);
                var lstWalletInterest = WalletInterestQueryBuilder.ToList();
                var WalletAcc = GetWalletAccountByUserID(pUserID);
                decimal totalAmountPreviousMonth = 0;
                if (WalletAcc.Status == 0)
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, WalletAcc.Message);
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = WalletAcc.Message;
                    return result;
                }
                foreach (var acc in WalletAcc.lstWalletAccount)
                {
                    try
                    {
                        newWalletEntities = new WalletEntities();
                        var rate = lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_day;
                        acc.InterestRate = Convert.ToDecimal(lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_year);
                        var DateInterestSnapshot = new WalletSnapshotQueryBuilder(newWalletEntities).HasLatestAccID(acc.ID);
                        if (DateInterestSnapshot.Count() > 0)
                        {
                            startDateCount = DateInterestSnapshot.FirstOrDefault().CreateDateSnapshot;
                            totalAmountPreviousMonth = DateInterestSnapshot.FirstOrDefault().Balance ?? 0;
                        }
                        else
                        {
                            startDateCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);                      
                        }
                        double TotalamtInterest = 0;
                        double amtInterest = 0;
                      
                        var lstSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetSubTransByUserIDnMonth(pUserID, acc.Currency_Code, startDateCount, curDateTime).ToList();
                        foreach (var subTran in lstSubTrans)
                        {
                            var dateTran = subTran.CreateDate;
                            int remaindate = Convert.ToInt32(Math.Round((curDateTime.Subtract(dateTran).TotalDays)));

                            if (remaindate == 0)
                            {
                                amtInterest = 0;
                            }
                            else if (subTran.Direction == "IN")
                            {
                                amtInterest = (Convert.ToDouble(subTran.Amount) * Math.Pow(1 + double.Parse(rate) / 100, remaindate));
                                amtInterest = +(amtInterest - Convert.ToDouble(subTran.Amount));
                            }
                            else
                            {
                                amtInterest = (Convert.ToDouble(subTran.Amount) * Math.Pow(1 + double.Parse(rate) / 100, remaindate));
                                amtInterest = -(amtInterest - Convert.ToDouble(subTran.Amount));
                            }
                            TotalamtInterest += amtInterest;
                        }

                        var totalDateInMonth = Convert.ToInt32((curDateTime.Subtract(startDateCount).TotalDays));
                        var interestPriciple = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(rate) / 100, totalDateInMonth));
                        interestPriciple = +(interestPriciple - Convert.ToDouble(totalAmountPreviousMonth));
                        TotalamtInterest += interestPriciple;
                        acc.InterestEarned = Convert.ToDecimal(string.Format("{0:0.00}", TotalamtInterest));
                        if (acc.InterestEarned > 0)
                        {
                            var TempTran = WalletInterestLogic.BuildTransactionInterest(acc.InterestEarned ?? 0, acc.Currency_Code, pUserID, acc.ID, pSource, acc.Wallet_ID, rate);
                            if (walletTranLogic.InsertTempTransaction(TempTran))
                            {
                                lstTrans.Add(TempTran);
                                var tranResponse = InsertInterestTransaction(newWalletEntities, TempTran.Tran_ID, acc.ID);

                                if (tranResponse.Status == 0)
                                {
                                    logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, "Inserted failed InsertInterestTransaction: " + TempTran.Tran_ID + "AccID: " + acc.ID);
                                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                    result.Status = 0;
                                    result.Message = "Inserted failed InsertInterestTransaction. Please check log file!";
                                }
                            }
                            else
                            {
                                logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, "Inserted failed InsertTempTransaction: " + TempTran.Tran_ID + "AccID: " + acc.ID);
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Inserted failed InsertTempTransaction. Please check log file!";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, acc.ID);
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Inserted failed InsertInterest_ByDatenUserID. Please check log file!";
                    }
                }
                result.lstTrans = lstTrans;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertInterest_ByDatenUserID";
                logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, "");
                return result;
            }
            return result;
        }

        public InterestResponse InsertRootDate_Interest(string pUserID, string pRootDate)
        {
            var result = new InterestResponse();
            var walletTranLogic = new WalletTransactionLogic();
            var WalletInterestLogic = new WalletInterestLogic();
            var WalletRewardLogic = new WalletRewardLogic();
            var logWallet = new LogWallet();
            var lstTrans = new List<Transaction>();
            var lstReward = new List<Wallet_Account_Reward>();
            var lstInterestTrans = new List<Transaction_Interest_Snapshot>();
            try
            {
                var newWalletEntities = new WalletEntities();
                var startDateCount = new System.DateTime(int.Parse(pRootDate.Split('-')[0]), int.Parse(pRootDate.Split('-')[1]), int.Parse(pRootDate.Split('-')[2]), 11, 59, 59);
                var WalletInterestQueryBuilder = new WalletInterestQueryBuilder(newWalletEntities);
                var lstWalletInterest = WalletInterestQueryBuilder.ToList();
                var WalletAcc = GetWalletAccountByUserIDWithoutReward(pUserID);

                if (WalletAcc.Status == 0)
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, WalletAcc.Message);
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = WalletAcc.Message;
                    return result;
                }
                foreach (var acc in WalletAcc.lstWalletAccount)
                {
                    try
                    {                   
                        //var rate = lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_day;
                        //acc.InterestRate = Convert.ToDecimal(lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_year);
                        //acc.InterestEarned = 0;
                        //var TempTran = WalletInterestLogic.BuildTransactionInterest(acc.InterestEarned ?? 0, acc.Currency_Code, pUserID, acc.ID, "", acc.Wallet_ID, rate);
                        
                        //var objInterest = WalletInterestLogic.BuildNewTransaction_Interest_Snapshot(newWalletEntities, TempTran.Tran_ID, TempTran.Source_Amount??0, acc.ID, acc.Available_Balance??0, "");
                        
                        //if (objInterest == null)
                        //{
                        //    logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, "Inserted failed InsertRootDate_Interest - AccID: " + acc.ID);
                        //    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        //    result.Status = 0;
                        //    result.Message = "Inserted failed InsertRootDate_Interest. Please check log file!";
                        //}
                        //else
                        //{
                        //    lstInterestTrans.Add(objInterest);
                        //    result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                        //    result.Status = ApiReturnCodeConstant.SUCCESS.Code;
                        //    result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                        //    result.InterestID = objInterest.ID;
                        //    TempTran.Merchant_ref = objInterest.ID;
                        //    lstTrans.Add(TempTran);
                        //}
                        var objReward = WalletRewardLogic.BuildWalletAccReward(acc.ID, acc.Wallet_ID, 0);
                        lstReward.Add(objReward);


                    }
                    catch (Exception ex)
                    {
                        logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, acc.ID);
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Inserted failed InsertRootDate_Interest. Please check log file!";
                    }
                }
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(newWalletEntities))
                {
                    //eWalletTransactionUnitOfWork.BeginTransaction().DoInsertMany(lstInterestTrans).EndTransaction();
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsertMany(lstReward).EndTransaction();
                }
                result.lstTrans = lstTrans;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertRootDate_Interest";
                logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, "");
                return result;
            }
            return result;
        }

        //public WalletAccountResponse GetWalletAccountWithInterestByUserID(string pUserID)
        //{
        //    var walletAPI = new WalletApiLogic();
        //    var logWallet = new LogWallet();
        //    var walletTranLogic = new WalletTransactionLogic();
        //    var lstWallet = new List<Wallet_Account>();
        //    var result = new WalletAccountResponse();
        //    try
        //    {
        //        var newWalletEntities = new WalletEntities();
        //        var curDateTime = DateTime.Now;
        //        var startDateCount = curDateTime;
        //        var WalletInterestQueryBuilder = new WalletInterestQueryBuilder(newWalletEntities);
        //        var lstWalletInterest = WalletInterestQueryBuilder.ToList();

        //        var WalletAcc = GetWalletAccountByUserID(pUserID);
        //        result.lstWalletAccount = new List<WalletTransferObject>();
        //        decimal totalAmountPreviousMonth = 0;
        //        if (WalletAcc.Status == 0)
        //        {
        //            logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, WalletAcc.Message);
        //            result.Status = 0;
        //            result.Code = WalletAcc.Code;
        //            result.Message = WalletAcc.Message;
        //            return result;
        //        }
        //        foreach (var acc in WalletAcc.lstWalletAccount)
        //        {
        //            var rate = lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_day;
        //            acc.InterestRate = Convert.ToDecimal(lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_year);
        //            var DateInterestSnapshot = new TransactionInterestSnapshotQueryBuilder(newWalletEntities).GetTranNearestDateByAccID(acc.ID);
        //            if (DateInterestSnapshot.Count() > 0)
        //            {
        //                startDateCount = DateInterestSnapshot.FirstOrDefault().Createdate;
        //                totalAmountPreviousMonth = DateInterestSnapshot.FirstOrDefault().Total_Amount;
        //            }
        //            else
        //            {
        //                logWallet.Log(MethodBase.GetCurrentMethod(), acc.ID, null, "Could not get GetTranNearestDateByAccID");
        //                acc.InterestEarned = 0;
        //                continue;
        //            }                   
        //            if (curDateTime.CompareTo(startDateCount) > 0)
        //            {
        //                var lstSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetSubTransByUserIDnMonth(pUserID, acc.Currency_Code, startDateCount, curDateTime).ToList();

        //                double TotalamtInterest = 0;
        //                double amtInterest = 0;

        //                foreach (var subTran in lstSubTrans)
        //                {
        //                    var dateTran = subTran.CreateDate;
        //                    int remaindate = Convert.ToInt32(Math.Round((curDateTime.Subtract(dateTran).TotalDays)));

        //                    if (subTran.Direction == "IN")
        //                    {
        //                        amtInterest = +((Convert.ToDouble(subTran.Amount) * remaindate * double.Parse(rate)) / 100);
        //                    }
        //                    else
        //                    {
        //                        amtInterest = -((Convert.ToDouble(subTran.Amount) * remaindate * double.Parse(rate)) / 100);
        //                    }
        //                    TotalamtInterest += amtInterest;
        //                }

        //                var totalDateInMonth = Convert.ToInt32((curDateTime.Subtract(startDateCount).TotalDays));
        //                TotalamtInterest += ((Convert.ToDouble(totalAmountPreviousMonth) * totalDateInMonth * double.Parse(rate)) / 100);
        //                acc.InterestEarned = Convert.ToDecimal(string.Format("{0:0.00}", TotalamtInterest));
        //            }
        //            else
        //            {   
        //                acc.InterestEarned = 0;
        //            }
        //        }
        //    result.lstWalletAccount = WalletAcc.lstWalletAccount;
        //    }

        //    catch (Exception ex)
        //    {
        //        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
        //        result.Status = 0;
        //        result.Message = "Failed to query Wallet ID";              
        //        logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pUserID, ex, "");
        //        return result;
        //    }
        //    result.Code = ApiReturnCodeConstant.SUCCESS.Code;
        //    result.Message = ApiReturnCodeConstant.SUCCESS.Message;
        //    result.Status = ApiReturnCodeConstant.SUCCESS.Code;
        //    return result;
        //}

        public WalletAccountResponse GetWalletAccountWithInterestByUserID(string pUserID)
        {
            var walletAPI = new WalletApiLogic();
            var logWallet = new LogWallet();
            var walletTranLogic = new WalletTransactionLogic();
            var lstWallet = new List<Wallet_Account>();
            var result = new WalletAccountResponse();
            try
            {
                var newWalletEntities = new WalletEntities();
                var curDateTime = DateTime.Now;
                var startDateCount = curDateTime;
                var WalletInterestQueryBuilder = new WalletInterestQueryBuilder(newWalletEntities);
                var lstWalletInterest = WalletInterestQueryBuilder.ToList();

                var WalletAcc = GetWalletAccountByUserID(pUserID);
                result.lstWalletAccount = new List<WalletTransferObject>();
                decimal totalAmountPreviousMonth = 0;
                if (WalletAcc.Status == 0)
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, WalletAcc.Message);
                    result.Status = 0;
                    result.Code = WalletAcc.Code;
                    result.Message = WalletAcc.Message;
                    return result;
                }
                foreach (var acc in WalletAcc.lstWalletAccount)
                {
                    var rate = lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_day;
                    acc.InterestRate = Convert.ToDecimal(lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_year);
                    var DateInterestSnapshot = new WalletSnapshotQueryBuilder(newWalletEntities).HasLatestAccID(acc.ID);
                    startDateCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);                    
                    if (DateInterestSnapshot != null)
                    {
                        if (DateInterestSnapshot.Count() > 0)
                        {
                            startDateCount = DateInterestSnapshot.FirstOrDefault().CreateDateSnapshot;
                            totalAmountPreviousMonth = DateInterestSnapshot.FirstOrDefault().Balance ?? 0;
                        }                       
                    }                  
                
                    
                    var lstSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetSubTransByUserIDnMonth(pUserID, acc.Currency_Code, startDateCount, curDateTime).ToList();

                    double TotalamtInterest = 0;
                    double amtInterest = 0;

                    foreach (var subTran in lstSubTrans)
                    {
                        var dateTran = subTran.CreateDate;
                        int remaindate = Convert.ToInt32(Math.Round((curDateTime.Subtract(dateTran).TotalDays)));

                        if (remaindate == 0)
                        {
                            amtInterest = 0;
                        }
                        else if (subTran.Direction == "IN")
                        {
                            amtInterest = (Convert.ToDouble(subTran.Amount) * Math.Pow(1 + double.Parse(rate) / 100, remaindate));
                            amtInterest = +(amtInterest - Convert.ToDouble(subTran.Amount));
                        }
                        else
                        {
                            amtInterest = (Convert.ToDouble(subTran.Amount) * Math.Pow(1 + double.Parse(rate) / 100, remaindate));
                            amtInterest = -(amtInterest - Convert.ToDouble(subTran.Amount));
                        }
                        
                        TotalamtInterest += amtInterest;
                    }
                 
                    var totalDateInMonth = Convert.ToInt32((curDateTime.Subtract(startDateCount).TotalDays));
                    var interestPriciple = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(rate) / 100, totalDateInMonth));
                    interestPriciple = +(interestPriciple - Convert.ToDouble(totalAmountPreviousMonth));
                    TotalamtInterest += interestPriciple;
                    acc.InterestEarned = Convert.ToDecimal(string.Format("{0:0.00}", TotalamtInterest));
                   
                }
                result.lstWalletAccount = WalletAcc.lstWalletAccount;
            }

            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Wallet ID";
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public ListTransactionResponse GetTransByCartGuidOrIdOrDate(string cartGuid = "", string transactionId = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = new WalletTransactionLogic(false).GetTrans(cartGuid, transactionId, fromDate, toDate);
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet for withdraw. Please check log file";
                }
                var lstTran = new List<TransactionTransferObject>();

                foreach (var tran in tranResponse)
                {
                    var tranObjectTransfer = new TransactionTransferObject();
                    tranObjectTransfer.Wallet_ID = tran.Wallet_ID;
                    tranObjectTransfer.TranID = tran.Tran_ID;
                    tranObjectTransfer.User_ID = tran.User_ID;
                    tranObjectTransfer.TranStatus = tran.Status;
                    tranObjectTransfer.Source_Currency = tran.Source_Currency;
                    tranObjectTransfer.Source_Amount = tran.Source_Amount;
                    tranObjectTransfer.Source = tran.Source;
                    tranObjectTransfer.Remarks = tran.Remarks;
                    tranObjectTransfer.Description = tran.Description;
                    tranObjectTransfer.PaymentGateWay = tran.PaymentGateway;
                    tranObjectTransfer.Merchant_ref = tran.Merchant_ref;
                    tranObjectTransfer.Destination_Currency = tran.Destination_Currency;
                    tranObjectTransfer.Destination_Amount = tran.Destination_Amount;
                    tranObjectTransfer.CreateUser = tran.CreateUser;
                    tranObjectTransfer.CreateDate = tran.CreateDate;
                    tranObjectTransfer.Scheduler_Check = tran.Scheduler_Check;
                    tranObjectTransfer.Data_Check = !string.IsNullOrEmpty(tran.Status) && tran.Status != "NULL" ? tran.Status : null;
                    lstTran.Add(tranObjectTransfer);
                }
                result.lstTrans = lstTran;
                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }



        public ListTransactionResponse GetTransWithdraw(string snapshotChecksum, string lastSubTransactionId, string lastSubTransactionChecksum)
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            var transferWithdraws = new List<TransactionTransferObject>();
            string message1 = string.Empty, message2 = string.Empty;
            bool isSuccess = false;
            try
            {
                List<SubTransaction> subtrans = null;
                List<Wallet_Snapshot> snapshot = null;

                //Verify Snapshot
                bool IsSnapshotVerified = new WalletSnapshotLogic().VerifySnapshot(snapshotChecksum, out snapshot);
                //Verify Transactions after snapshot
                bool IsTransactionVerified = new WalletTransactionLogic(true).VerifyTransactionsPreWithdraw(lastSubTransactionId, lastSubTransactionChecksum, snapshot?[0].CreateDateSnapshot ?? null, out subtrans, out message1);
                //Get Withdraw and tally Balance 
                //(include random check checksum1 for relevant subtransactions, can remove check if using prev tran instead prev User tran in checksum1)
                bool IsBalanceVerified = new WalletTransactionLogic().GetWithdraw(subtrans, snapshot, lastSubTransactionId, out transferWithdraws, out message2);

                result.Status = IsSnapshotVerified && IsTransactionVerified && IsBalanceVerified ? ApiReturnCodeConstant.SUCCESS.Code : 0;
                result.Code = !IsSnapshotVerified ? ApiReturnCodeConstant.ERR_VALIDATE_SNAPSHOT_FAILED.Code
                                : !IsTransactionVerified ? ApiReturnCodeConstant.ERR_VALIDATE_TRX_FAILED.Code
                                : !IsBalanceVerified ? ApiReturnCodeConstant.ERR_VALIDATE_WITHDRAW_FAILED.Code
                                : ApiReturnCodeConstant.SUCCESS.Code;
                result.lstTrans = transferWithdraws;
                result.Message = $"{(!IsSnapshotVerified ? $"{ApiReturnCodeConstant.ERR_VALIDATE_SNAPSHOT_FAILED.Message}." : string.Empty)}{(!IsTransactionVerified ? $"{ApiReturnCodeConstant.ERR_VALIDATE_TRX_FAILED.Message}.{message1}." : string.Empty)}{message2}";
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet for withdraw";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
            }
            return result;
        }

        public CancelWithdrawResponse CancelWithdraw(string tranId)
        {
            var result = new CancelWithdrawResponse();
            try
            {
                if (new WalletTransactionLogic().CancelWithdraw(tranId))
                    result.Code = result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet for withdraw";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
            }
            return result;
        }

        public PendWithdrawResponse PendWithdraw(string tranId)
        {
            var result = new PendWithdrawResponse();
            try
            {
                if (new WalletTransactionLogic().PendWithdraw(tranId))
                    result.Code = result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Transaction Wallet for withdraw";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
            }
            return result;
        }

        public RefundTransactionResponse RefundTransaction(string tranId, string refundAmount, string user)
        {
            var result = new RefundTransactionResponse() { Code = 0, Status = 0 };
            bool isSuccess = false;
            try
            {
                var tran = new TransactionQueryBuilder(new WalletEntities()).GetTranByTranID(tranId).FirstOrDefault();
                var subtran = new SubTransactionQueryBuilder(new WalletEntities()).GetSubTransactionByTranID(tranId).ToList();
                if (tran != null && tran.Description == EwalletConstant.PAYMENT.ToString())
                {

                    if (!string.IsNullOrEmpty(refundAmount) && Convert.ToDecimal(refundAmount) > tran.Source_Amount)
                        isSuccess = false;
                    else if (string.IsNullOrEmpty(refundAmount) || (!string.IsNullOrEmpty(refundAmount) && Convert.ToDecimal(refundAmount) <= tran.Source_Amount))
                    {
                        decimal originRfAmount = (!string.IsNullOrEmpty(refundAmount) && Convert.ToDecimal(refundAmount) < tran.Source_Amount) ? Convert.ToDecimal(refundAmount) : tran.Source_Amount.Value;
                        decimal rfAmount = originRfAmount;
                        //Refund/Insert Tran/Subtran for refund for main account
                        var mainSubTran = subtran.SingleOrDefault(s => s.Remarks == tran.Remarks);
                        if(mainSubTran != null)
                        {
                            decimal mainRfAmount = rfAmount > mainSubTran.Amount ? mainSubTran.Amount.Value : rfAmount;
                            Transaction refundTran = BuildTransactionRefund(tran, user, mainRfAmount);
                            if (new WalletTransactionLogic().InsertTempTransaction(refundTran))
                            {
                                var subtranRes = InsertTopupTransaction(refundTran.Tran_ID);
                                rfAmount -= subtranRes.Code == ApiReturnCodeConstant.SUCCESS.Code ? mainRfAmount : 0;
                            }
                        }
                        //Refund/Insert Tran/Subtran for refund for reward
                        if (rfAmount > 0)
                        {
                            var rewardSubTran = subtran.SingleOrDefault(s => s.Remarks.Split('|').Count() == 2 && s.Remarks.Contains(tran.Remarks) && s.Remarks.Contains("REWARD"));
                            decimal rewardRfAmount = rfAmount > rewardSubTran.Amount ? rewardSubTran.Amount.Value : rfAmount;
                            TranResponse rewardResponse = rewardResponse = InsertRewardTransaction(rewardRfAmount, rewardSubTran.Account_ID, "", true);
                            rfAmount -= rewardResponse.Code == ApiReturnCodeConstant.SUCCESS.Code ? rewardRfAmount : 0;
                        }
                        if(rfAmount < originRfAmount)
                        {
                            //Update: 
                            tran.Status = "refunded";
                            new WalletTransactionUow(new WalletEntities()).BeginTransaction().DoUpdate(tran).EndTransaction();
                            result.Code = result.Status = ApiReturnCodeConstant.SUCCESS.Code;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to refund transaction";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
            }
            return result;
        }
        public Transaction BuildTransactionRefund(Transaction tran, string user, decimal refundAmount)
        {
            var ntran = new Transaction();
            ntran.CreateDate = DateTime.Now;
            ntran.CreateUser = user;
            ntran.Description = EwalletConstant.TOPUP.ToString();
            ntran.PaymentGateway = "Backend Program";
            ntran.Remarks = $"Refund {tran.Remarks}";
            ntran.Source = "Backend Program";
            ntran.Source_Amount = refundAmount;
            ntran.Source_Currency = tran.Source_Currency;
            ntran.Destination_Amount = tran.Destination_Amount;
            ntran.Status = "NULL";
            ntran.User_ID = tran.User_ID;
            ntran.Wallet_ID = tran.Wallet_ID;
            ntran.Tran_ID = ("EW" + Guid.NewGuid().ToString().Replace("-", "")).Substring(0, 20);
            return ntran;
        }
        public bool UpdateCheckSumSnapshot(string pChecksum)
        {
            var logWallet = new LogWallet();
            try
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), "Start UpdateCheckSumSnapshot", null, "");
                var walletSnapshotLogic = new WalletSnapshotLogic();
                walletSnapshotLogic.UpdateCheckSumSnapshot("qmlLHu6tDVlxiiMSG8m28KA+IfU=");
                walletSnapshotLogic.UpdateCheckSumSnapshot("Xb8WmcQDMGk9Bmi5am6hTvPURy0=");
                logWallet.Log(MethodBase.GetCurrentMethod(), "End UpdateCheckSumSnapshot", null, "");
                return true;
            }
          
            catch (Exception ex)
            {              
              
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return false;
            }
        }
        public void UpdateCheckSumSubTransaction()
        {
            var walletLogic = new WalletTransactionLogic();
            walletLogic.UpdateCheckSumSubTransaction();
        }
        
    }
}
