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
using Easybook.Api.Core.Model.EasyWallet.DataTransferObject;
using Easybook.Api.Core.Model.EasyWallet.Models.TownBus;

namespace Easybook.Api.BusinessLogic.EasyWallet
{
    public class WalletTransactionLogic
    {
        public WalletTransactionLogic(bool genStampKey = false)
        {
            if (genStampKey)
            {
                localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);
            }
        }
        public bool InsertTempTransaction(Transaction record)
        {
            var logWallet = new LogWallet();
            try
            {                
                using (var eWalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    if (record.Source_Amount < 0)
                    {                       
                        logWallet.Log(MethodBase.GetCurrentMethod(), record.Tran_ID, null, "Amount is not allowed smaller than zero");
                        return false;
                    }                        
                    eWalletTransactionUnitOfWork.BeginTransaction().DoInsert(record).EndTransaction();
                    return true;
                }
            }
            catch (Exception ex)
            {              
                logWallet.Log(MethodBase.GetCurrentMethod(), record.Tran_ID, ex, "");
                return false;
            }
        }

        public string GenerateNewSubTransaction_TopUp(WalletTransactionUow walletTranUOW, Transaction p_Tran, string AccID, ref SubTransaction SubTran, string lastCheckSum1)
        {
            try
            {
                if (walletTranUOW == null)
                {
                    return "";
                }
                SubTran = new SubTransaction();
                SubTran.Tran_ID = p_Tran.Tran_ID;
                SubTran.Account_ID = AccID;
                SubTran.User_ID = p_Tran.User_ID;
                SubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(p_Tran.Source_Amount); 
                SubTran.Currency_Code = p_Tran.Source_Currency;
                SubTran.Direction = "IN";
                SubTran.Remarks = p_Tran.Remarks??"";
                SubTran.Verified = false;
                SubTran.CreateUser = p_Tran.CreateUser??"";
                SubTran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second, 0);
                SubTran.Sub_ID = SecurityLogic.GenerateKey(30);                    
                SubTran.Checksum1 = BuildCheckSum1(lastCheckSum1, SubTran, true);
                var strCheckSum2 = BuildCheckSum2(SubTran.Checksum1, SubTran, true);
                if (strCheckSum2 != "")
                {
                    SubTran.Checksum2 = strCheckSum2;
                }
                else
                {
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, null, "Can not build checksum2");
                    return "";
                }      
                walletTranUOW.DoInsert(SubTran).SaveAndContinue();
                return SubTran.Sub_ID;                
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, ex, "");
                return "";
            }
        }

        public string GenerateNewSubTransaction_Payment(WalletTransactionUow eWalletTransactionUnitOfWork, Transaction p_Tran, string AccID, ref SubTransaction SubTran, string lastCheckSum1)
        {
            try
            {
                if (eWalletTransactionUnitOfWork == null)
                {
                    return "";
                }
                SubTran.Tran_ID = p_Tran.Tran_ID;
                SubTran.Account_ID = AccID;
                SubTran.User_ID = p_Tran.User_ID;
                SubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(p_Tran.Source_Amount);  
                SubTran.Currency_Code = p_Tran.Source_Currency;
                SubTran.Direction = "OUT";
                SubTran.Remarks = p_Tran.Remarks??"";
                SubTran.Verified = true;
                SubTran.CreateUser = p_Tran.CreateUser??"";
                SubTran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                SubTran.Sub_ID = SecurityLogic.GenerateKey(30);
                SubTran.Checksum1 = BuildCheckSum1(lastCheckSum1, SubTran, true);               
                var strCheckSum2 = BuildCheckSum2(SubTran.Checksum1, SubTran, true);
                if (strCheckSum2 != "")
                {
                    SubTran.Checksum2 = strCheckSum2;
                }
                else
                {
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, null, "Can not build checksum2");
                    return "";
                }

                eWalletTransactionUnitOfWork.DoInsert(SubTran).SaveAndContinue();
                return SubTran.Sub_ID;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, ex, "");                
                return "";
            }
        }

        public string GenerateNewSubTransaction_ConvertCurrency(WalletTransactionUow eWalletTransactionUnitOfWork,
            Transaction p_Tran, string SourceAccID, string DestAccID, 
            ref SubTransaction SourceSubTran, ref SubTransaction DestSubTran, string lastCheckSum1)
        {
            try
            {
                if (eWalletTransactionUnitOfWork == null)
                {
                    return "";
                }
                SourceSubTran.Tran_ID = p_Tran.Tran_ID;
                SourceSubTran.Account_ID = SourceAccID;
                SourceSubTran.User_ID = p_Tran.User_ID;
                SourceSubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(p_Tran.Source_Amount); 
                SourceSubTran.Currency_Code = p_Tran.Source_Currency;
                SourceSubTran.Direction = "OUT";
                SourceSubTran.Remarks = p_Tran.Remarks??"";
                SourceSubTran.Verified = true;
                SourceSubTran.CreateUser = p_Tran.CreateUser??"";
                SourceSubTran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                SourceSubTran.Sub_ID = SecurityLogic.GenerateKey(30);
                SourceSubTran.Checksum1 = BuildCheckSum1(lastCheckSum1, SourceSubTran, true);
                var strCheckSum2 = BuildCheckSum2(SourceSubTran.Checksum1, SourceSubTran, true);
                if (strCheckSum2 != "")
                {
                    SourceSubTran.Checksum2 = strCheckSum2;
                }
                else
                {
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, null, "Can not build source checksum2");
                    return "";
                }
                eWalletTransactionUnitOfWork.DoInsert(SourceSubTran).SaveAndContinue();

                DestSubTran.Tran_ID = p_Tran.Tran_ID;
                DestSubTran.Account_ID = DestAccID;
                DestSubTran.Remarks = p_Tran.Remarks??"";
                DestSubTran.User_ID = p_Tran.User_ID;
                DestSubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(p_Tran.Destination_Amount); 
                DestSubTran.Currency_Code = p_Tran.Destination_Currency;
                DestSubTran.Direction = "IN";
                DestSubTran.Verified = true;
                DestSubTran.CreateUser = p_Tran.CreateUser??"";
                DestSubTran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                DestSubTran.Sub_ID = SecurityLogic.GenerateKey(30);
                DestSubTran.Checksum1 = BuildCheckSum1(SourceSubTran.Checksum1, DestSubTran);             
                strCheckSum2 = BuildCheckSum2(DestSubTran.Checksum1, DestSubTran);
                if (strCheckSum2 != "")
                {
                    DestSubTran.Checksum2 = strCheckSum2;
                }
                else
                {
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, null, "Can not build dest checksum2");
                    return "";
                }

                eWalletTransactionUnitOfWork.DoInsert(DestSubTran).SaveAndContinue();
                return SourceSubTran.Sub_ID + "|" + DestSubTran.Sub_ID;
           
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, ex, "");
                return "";
            }
        }


        public string GenerateNewSubTransaction_Withdraw(WalletTransactionUow eWalletTransactionUnitOfWork, Transaction p_Tran, string AccID, ref SubTransaction SubTran, string lastCheckSum1)
        {
            try
            {
                if (eWalletTransactionUnitOfWork == null)
                {
                    return "";
                }
                SubTran.Tran_ID = p_Tran.Tran_ID;
                SubTran.Account_ID = AccID;
                SubTran.User_ID = p_Tran.User_ID;
                SubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(p_Tran.Source_Amount);
                SubTran.Currency_Code = p_Tran.Source_Currency;
                SubTran.Direction = "OUT";
                SubTran.Remarks = p_Tran.Remarks ?? "";
                SubTran.Verified = true;
                SubTran.CreateUser = p_Tran.CreateUser ?? "";
                SubTran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                SubTran.Sub_ID = SecurityLogic.GenerateKey(30);
                SubTran.Checksum1 = BuildCheckSum1(lastCheckSum1, SubTran, true);
                var strCheckSum2 = BuildCheckSum2(SubTran.Checksum1, SubTran, true);
                if (strCheckSum2 != "")
                {
                    SubTran.Checksum2 = strCheckSum2;
                }
                else
                {
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, null, "Can not build checksum2");
                    return "";
                }

                eWalletTransactionUnitOfWork.DoInsert(SubTran);
                return SubTran.Sub_ID;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, ex, "");
                return "";
            }
        }

        public string GenerateNewSubTransaction_Interest(WalletTransactionUow eWalletTransactionUnitOfWork, Transaction p_Tran, string AccID, ref SubTransaction SubTran, string lastCheckSum1)
        {
            try
            {
                if (eWalletTransactionUnitOfWork == null)
                {
                    return "";
                }
                SubTran.Tran_ID = p_Tran.Tran_ID;
                SubTran.Account_ID = AccID;
                SubTran.User_ID = p_Tran.User_ID;
                SubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(p_Tran.Source_Amount);
                SubTran.Currency_Code = p_Tran.Source_Currency;
                SubTran.Direction = "IN";
                SubTran.Remarks = p_Tran.Remarks ?? "";
                SubTran.Verified = true;
                SubTran.CreateUser = p_Tran.CreateUser ?? "";
                SubTran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                SubTran.Sub_ID = SecurityLogic.GenerateKey(30);
                SubTran.Checksum1 = !string.IsNullOrEmpty(lastCheckSum1) ? BuildCheckSum1(lastCheckSum1, SubTran, true) : string.Empty;
                var strCheckSum2 = !string.IsNullOrEmpty(lastCheckSum1) ? BuildCheckSum2(SubTran.Checksum1, SubTran, true) : "interest-processing";
                if (strCheckSum2 != "")
                {
                    SubTran.Checksum2 = strCheckSum2;
                }
                else
                {
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, null, "Can not build checksum2");
                    return "";
                }

                eWalletTransactionUnitOfWork.DoInsert(SubTran);
                return SubTran.Sub_ID;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), p_Tran.Tran_ID, ex, "");
                return "";
            }
        }      

        public string BuildCheckSum1(string p_LastCheckSum1, SubTransaction CurrentSubTran, bool isAddLog = false)
        {            
            string checkSum1 = SecurityLogic.GetSha1Hash(CurrentSubTran.Account_ID + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrentSubTran.Amount) + "|" + CurrentSubTran.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrentSubTran.CreateUser + "|" + CurrentSubTran.Currency_Code + "|" + CurrentSubTran.Direction + "|" + CurrentSubTran.Sub_ID + "|" + CurrentSubTran.Tran_ID + "|" + CurrentSubTran.User_ID + "|" + CurrentSubTran.Verified + "|" + p_LastCheckSum1 + "|" + EwalletConstant.WebserverKey);
            if (isAddLog)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), CurrentSubTran.Account_ID + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrentSubTran.Amount) + "|" + CurrentSubTran.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrentSubTran.CreateUser + "|" + CurrentSubTran.Currency_Code + "|" + CurrentSubTran.Direction + "|" + CurrentSubTran.Sub_ID + "|" + CurrentSubTran.Tran_ID + "|" + CurrentSubTran.User_ID + "|" + CurrentSubTran.Verified + "|" + p_LastCheckSum1 + "|" + EwalletConstant.WebserverKey, null, "BuildCheckSum1");
            }
            return checkSum1;
        }

        public string BuildCheckSum2(string p_CurrentCheckSum1, SubTransaction CurrentSubTran, bool isAddLog = false)
        {
            try
            {
                var StampServerKey = "";
                if (Globals.StampServerKey != "Invalid Key")
                {
                    var logWallet = new LogWallet();
                    if (string.IsNullOrEmpty(Globals.StampServerKey))
                    {
                        logWallet.Log(MethodBase.GetCurrentMethod(), "StampServerKey is Empty", null, "BuildCheckSum2");
                        localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                        var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                        Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);                        
                    }
                    StampServerKey = Globals.StampServerKey;
                    if (isAddLog)
                    logWallet.Log(MethodBase.GetCurrentMethod(), CurrentSubTran.Account_ID + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrentSubTran.Amount) + "|" + CurrentSubTran.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrentSubTran.CreateUser + "|" + CurrentSubTran.Currency_Code + "|" + CurrentSubTran.Direction + "|" + CurrentSubTran.Sub_ID + "|" + CurrentSubTran.Tran_ID + "|" + CurrentSubTran.User_ID + "|" + CurrentSubTran.Verified + "|" + p_CurrentCheckSum1 + "|" + StampServerKey, null, "BuildCheckSum2");
                    return SecurityLogic.GetSha1Hash(CurrentSubTran.Account_ID + "|" + ConvertUtility.RoundToTwoDecimalPlaces(CurrentSubTran.Amount) + "|" + CurrentSubTran.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "|" + CurrentSubTran.CreateUser + "|" + CurrentSubTran.Currency_Code + "|" + CurrentSubTran.Direction + "|" + CurrentSubTran.Sub_ID + "|" + CurrentSubTran.Tran_ID + "|" + CurrentSubTran.User_ID + "|" + CurrentSubTran.Verified + "|" + p_CurrentCheckSum1 + "|" + StampServerKey);
                }
                else
                    return "";
            }         
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), CurrentSubTran.Account_ID, ex, ""));
                return "";
            }
        }       

        public bool UpdateBalanceByAccountID(Wallet_Account WalletAcc, WalletTransactionUow walletTranUOW, string AccID, decimal Amount, string direction, string TypeOfTran, bool isSave = true)
        {
            var logWallet = new LogWallet();
            var WalletAccLogic = new WalletAccountLogic();
            try
            {                
                WalletAcc.UpdateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                switch (direction)
                {
                    case "IN":
                        if (TypeOfTran == "TopupWithoutVerify")
                        {
                            WalletAcc.Total_Balance = WalletAcc.Total_Balance + Amount;
                        }
                        else if (TypeOfTran == "TopupWithVerify")
                        {
                            WalletAcc.Available_Balance = WalletAcc.Available_Balance + Amount;
                        }
                        else
                        {
                            WalletAcc.Available_Balance = WalletAcc.Available_Balance + Amount;
                            WalletAcc.Total_Balance = WalletAcc.Total_Balance + Amount;
                        }
                        break;
                    case "OUT":
                        WalletAcc.Available_Balance = WalletAcc.Available_Balance - Amount;
                        WalletAcc.Total_Balance = WalletAcc.Total_Balance - Amount;
                        break;
                }
                //if (WalletAcc.Available_Balance < 0)
                //{
                //    logWallet = new LogWallet();
                //    Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), AccID, null, "Available balance could not be negative."));
                //    return false;
                //}

                WalletAcc.ChecksumAvailable1 = WalletAccLogic.BuildCheckSumAvailable1(WalletAcc);               
                var strCheckSum2 = WalletAccLogic.BuildCheckSumAvailable2(WalletAcc);
                if (strCheckSum2 != "")
                {
                    WalletAcc.ChecksumAvailable2 = strCheckSum2;
                }
                else
                {
                    logWallet = new LogWallet();
                    Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), AccID, null, "Can not build checksum2"));
                    return false;
                }

                WalletAcc.ChecksumTotal1 = WalletAccLogic.BuildCheckSumTotal1(WalletAcc);
                WalletAcc.ChecksumTotal2 = WalletAccLogic.BuildCheckSumTotal2(WalletAcc);


                walletTranUOW.DoUpdate(WalletAcc);
                if (isSave)
                    walletTranUOW.SaveAndContinue();
                return true;
            }             
            catch (Exception ex)
            {                
                logWallet.Log(MethodBase.GetCurrentMethod(), AccID, ex, "");
                return false;
            }
        }

        public bool UpdateStatusTranAndSubTran(WalletTransactionUow walletTranUOW, string TranID, SubTransaction pSubTran, string TypeOfTran)
        {
            var logWallet = new LogWallet();
            Transaction Tran = new Transaction();
            SubTransaction SubTran = pSubTran;
            try
            {
               
                if (TypeOfTran == EwalletConstant.TOPUP || TypeOfTran == EwalletConstant.COMMISSION)
                {
                    SubTran.Verified = true;
                    walletTranUOW.DoUpdate(SubTran).SaveAndContinue();
                }
                using (var TransactionBuilder = new TransactionQueryBuilder(new WalletEntities()))
                {
                    Tran = TransactionBuilder.GetTranByTranID(TranID).FirstOrDefault();
                    Tran.Status = "true";
                }
                
                walletTranUOW.DoUpdate(Tran).SaveAndContinue();
                return true;
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), SubTran.Sub_ID+ "|" + TranID, ex, "");
                return false;
            }
        }

        public bool CancelWithdraw(string tranId, string username)
        {
            try
            {
                var tran = new TransactionQueryBuilder(new WalletEntities()).GetTranByTranID(tranId).FirstOrDefault();
                if(tran!= null && tran.Description == EwalletConstant.WITHDRAW.ToString())
                {
                    tran.Status = "canceled";
                    tran.UpdateDate = DateTime.Now;
                    tran.UpdateUser = username;
                    new WalletTransactionUow(new WalletEntities()).BeginTransaction().DoUpdate(tran).EndTransaction();
                    return true;
                }
            }
            catch (Exception ex)
            {
                new LogWallet().Log(MethodBase.GetCurrentMethod(), tranId, ex, "");
            }
            return false;
        }

        public bool PendWithdraw(string tranId, string username)
        {
            try
            {
                var tran = new TransactionQueryBuilder(new WalletEntities()).GetTranByTranID(tranId).FirstOrDefault();
                if (tran != null && tran.Description == EwalletConstant.WITHDRAW.ToString())
                {
                    tran.Status = "pending";
                    tran.UpdateDate = DateTime.Now;
                    tran.UpdateUser = username;
                    new WalletTransactionUow(new WalletEntities()).BeginTransaction().DoUpdate(tran).EndTransaction();
                    return true;
                }
            }
            catch (Exception ex)
            {
                new LogWallet().Log(MethodBase.GetCurrentMethod(), tranId, ex, "");
            }
            return false;
        }

        public bool UpdateTransactionRefund(string tranId)
        {
            try
            {
                var tran = new TransactionQueryBuilder(new WalletEntities()).GetTranByTranID(tranId).FirstOrDefault();
                if (tran != null && tran.Description == EwalletConstant.PAYMENT.ToString())
                {
                    tran.Status = "refunded";
                    new WalletTransactionUow(new WalletEntities()).BeginTransaction().DoUpdate(tran).EndTransaction();
                    return true;
                }
            }
            catch (Exception ex)
            {
                new LogWallet().Log(MethodBase.GetCurrentMethod(), tranId, ex, "");
            }
            return false;
        }

        public bool UpdateStatusBQForTran(WalletTransactionUow walletTranUOW, string TranID)
        {
            var logWallet = new LogWallet();
            Transaction Tran = new Transaction();
          
            try
            {              
                using (var TransactionBuilder = new TransactionQueryBuilder(new WalletEntities()))
                {
                    Tran = TransactionBuilder.GetTranByTranID(TranID).FirstOrDefault();
                    Tran.Scheduler_Check = "YES";
                }

                walletTranUOW.DoUpdate(Tran).SaveAndContinue();
                return true;
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + TranID, ex, "");
                return false;
            }
        }

        public bool UpdateIsProcessing(Wallet_Account wallAcc, WalletTransactionUow walletTranUOW, string AccID, bool status)
        {
            //var wallAcc = new Wallet_Account();
            var logWallet = new LogWallet();
            try
            {
                //wallAcc = walletTranUOW.GetWalletByID(AccID);
                wallAcc.IsProcessing = status;
                walletTranUOW.DoUpdate(wallAcc).SaveAndContinue();        
                return true;
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), AccID + "|" + status.ToString(), ex, "");
                return false;
            }
        }


        /// <summary>
        /// VerifyTopUpTransaction 
        /// 1.	Check Total Amount  = Available Amount + Topup Amount
        /// 2.	System check CheckSum1 C51 from table Subtransaction is same with CheckSum1 build from Last transaction query from current row whether or not.
        /// 3.	Check CheckSumAvailable1 and CheckSumAvailable2 with value query from current row

        /// </summary>
        /// <param name="subTranID"></param>
        /// <param name="AccID"></param>
        /// <returns></returns>
        public bool VerifyTopUpTransaction(Wallet_Account WalletAcc, WalletTransactionUow WalletTransactionUow, SubTransaction subTran, string lastCheckSum)
        {
            var logWallet = new LogWallet();
            try
            {
                SubTransaction CurSubTran = subTran;
                if (CurSubTran != null || WalletAcc != null)
                {
                    if (WalletAcc.Total_Balance == CurSubTran.Amount + WalletAcc.Available_Balance)
                    {
                        CurSubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(CurSubTran.Amount);
                        var curCheckSum = BuildCheckSum1(lastCheckSum, CurSubTran);
                        if (curCheckSum == CurSubTran.Checksum1)
                        {
                            return true;
                        }
                        else
                        {
                            logWallet.Log(MethodBase.GetCurrentMethod(), CurSubTran.Account_ID, null, "Check CheckSum at SubTransaction Failed");
                            return false;
                        }
                    }
                    else
                    {
                        logWallet.Log(MethodBase.GetCurrentMethod(), CurSubTran.Account_ID, null, "Check Balance Total Amount Failed");
                        return false;
                    }                    
                }
                else
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), subTran, null, "Current Tran or WalletAcc is null");
                    return false;
                }                
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), subTran, ex, "");
                return false;
            }
        }

        public bool CheckSumWalletAccount(Wallet_Account WalletAcc)
        {
            var logWallet = new LogWallet();
            try
            {
                var WalletAccLogic = new WalletAccountLogic();
                if (string.IsNullOrEmpty(WalletAcc.ChecksumAvailable1))
                {
                    var result = WalletAccLogic.UpdateAllCheckSumWalletAccountByUserID(WalletAcc.User_ID);
                    if (WalletAcc.Currency_Code != "LAK")
                    {
                        logWallet.Log(MethodBase.GetCurrentMethod(), result, null, "Update Checksum for Customer re-engagement: " + WalletAcc.User_ID);
                    }
                    
                    return result;
                }
                else
                {
                 
                    string checkSumTotal1 = WalletAccLogic.BuildCheckSumTotal1(WalletAcc);
                    string checkSumTotal2 = WalletAccLogic.BuildCheckSumTotal2(WalletAcc);
                    string checkSumAvailable1 = WalletAccLogic.BuildCheckSumAvailable1(WalletAcc);
                    string checkSumAvailable2 = WalletAccLogic.BuildCheckSumAvailable2(WalletAcc);

                    if (WalletAcc.ChecksumAvailable1 == checkSumAvailable1 && WalletAcc.ChecksumAvailable2 == checkSumAvailable2 && WalletAcc.ChecksumTotal1 == checkSumTotal1 && WalletAcc.ChecksumTotal2 == checkSumTotal2)
                    {
                        return true;
                    }
                    else
                    {
                        logWallet.Log(MethodBase.GetCurrentMethod(), WalletAcc.ID, null, "Check CheckCheckSumWalletAccount Failed");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), WalletAcc.User_ID, ex, "");
                return false;
            }
        }

        public List<HistorySubTransaction> GetSubTransByUserId(string pUserID, int pPageNumber, int pRow, DateTime pDateFrom, DateTime pDateTo, ref int pRecordTotal, int pTransactionType, string pCurrencyCode)
        {
            var walletAPIlogic = new WalletApiLogic();
            var SubTrans = new List<SubTransaction>();
            var wallAcc = new Wallet_Account();
            var result = new List<HistorySubTransaction>();
            var lstTranWithdraw = new List<Transaction>();
            var subSubTrans = new List<SubTransaction>();
            var lstDailyAmount = new Dictionary<DateTime, decimal>();
            var walletEntities = new WalletEntities();
            var lstAllTrans = new List<Transaction>();
            var lstAllSubTrans = new List<SubTransaction>();

            try
            {
                if (pCurrencyCode == "")
                {
                    wallAcc = new WalletAccountQueryBuilder(walletEntities).GetCurrencyByUserID(pUserID).FirstOrDefault();
                    pCurrencyCode = wallAcc.Currency_Code;
                }

                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    lstAllTrans = new TransactionQueryBuilder(walletEntities).GetAllTransByUserID(pUserID).ToList();
                    lstAllSubTrans = new SubTransactionQueryBuilder(walletEntities).GetAllSubTranByUserID(pUserID).ToList();
                }
               
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    //All: 1, Payment: 2, Topup: 3, Withdraw: 4, Cashbonus: 5, Reward: 6, Townbus: 7, Commission: 8     
                    if (pTransactionType == 3 || pTransactionType == 5 || pTransactionType == 6 || pTransactionType == 8)
                    {
                        SubTrans = lstAllSubTrans.Where(p => p.Currency_Code == pCurrencyCode && p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo && p.Direction.Equals("IN")).OrderByDescending(x => x.CreateDate).ToList();                       
                    }
                    else if (pTransactionType == 2 || pTransactionType == 4 || pTransactionType == 7)
                    {                
                        SubTrans = lstAllSubTrans.Where(p => p.Currency_Code == pCurrencyCode && p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo && p.Direction.Equals("OUT")).OrderByDescending(x => x.CreateDate).ToList();
                        if (pTransactionType == 4)
                            lstTranWithdraw = lstAllTrans.Where(p => p.Description == "WITHDRAW" && p.Source_Currency == pCurrencyCode && (p.Status == "NULL" || p.Status == "pending") && p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo).ToList();
                    }
                    else if (pTransactionType == 1)
                    { 
                        SubTrans = lstAllSubTrans.Where(p => p.Currency_Code == pCurrencyCode && p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo).OrderByDescending(x => x.CreateDate).ToList();
                        lstTranWithdraw = lstAllTrans.Where(p => p.Description == "WITHDRAW" && p.Source_Currency == pCurrencyCode && p.User_ID == pUserID && (p.Status == "NULL" || p.Status == "pending") && p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo).ToList();
                    }
                }

                if (pTransactionType == 5 || pTransactionType == 1)
                {
                    subSubTrans = walletAPIlogic.SearchingWalletAccountWithInterestByUserIDnDateRange(pUserID, pDateFrom, pDateTo, pCurrencyCode, ref lstDailyAmount, lstAllSubTrans, lstAllTrans);                    
                }

                List<string> lstTran = new List<string>();
                    SubTrans.ForEach(p => {
                        lstTran.Add(p.Tran_ID);
                    });
                //var Trans = GetTransbyTransID(lstTran);
                var Trans = lstAllTrans.Where(p => lstTran.Contains(p.Tran_ID)).ToList();
                List<Company> companies = new CompanyQueryBuilder(new TownBusEntities()).ToList();
                foreach (var SubTran in SubTrans)
                    {
                        var psubTran = new HistorySubTransaction();
                        psubTran.Account_ID = SubTran.Account_ID;
                        psubTran.Source_Amount = SubTran.Amount;
                        psubTran.Source_Currency = SubTran.Currency_Code;
                        psubTran.TransactionType = Trans.Find(p => p.Tran_ID == SubTran.Tran_ID).Description;
                        if (psubTran.TransactionType == EwalletConstant.TOPUP || psubTran.TransactionType == EwalletConstant.PAYMENT || psubTran.TransactionType == EwalletConstant.COMMISSION)
                        {
                            psubTran.Remarks = SubTran.Remarks;
                        }
                        else if (psubTran.TransactionType == EwalletConstant.REWARD)
                        {
                            psubTran.Remarks = Trans.Find(p => p.Tran_ID == SubTran.Tran_ID).Remarks;
                        }
                        else if (psubTran.TransactionType == EwalletConstant.CASHBONUS)
                        {
                            psubTran.Remarks = Trans.Find(p => p.Tran_ID == SubTran.Tran_ID).Remarks;
                        }
                        else
                        {
                            psubTran.Remarks = "";
                        }
                        if (SubTran.CreateUser.Contains("TownBus"))
                        {
                            var value = SubTran.Remarks.Split('|');
                            var isAutoCheckOut = value.Count() > 14 ? bool.Parse(value[14].Trim()):false;
                            var coordinateOutId = (value.Count() > 4) ? int.Parse(value[4]) : 0;
                            var townbusCompany =  (value.Count() > 12) ? int.Parse(value[12]) : 0;
                            var coordinationOut = new WalletTownbusLogic().GetTownBusCoordinateByCoordinateID(coordinateOutId);
                            var company = companies.Where(x => x.Company_ID == townbusCompany).FirstOrDefault();
                            psubTran.TransactionType = "TOWNBUS";
                            psubTran.BusCompanyName = company!=null && string.IsNullOrEmpty(company.Company_Name) ? "": company.Company_Name;

                            psubTran.Remarks = "";
                            psubTran.IsAutoCheckOut = isAutoCheckOut;
                            psubTran.ChargeType = value.Count() > 3 ? value[3].Trim() : "";
                            if (value.Count() > 3 && value[3].Trim().Contains(EwalletConstant.FLATRATE))
                            {
                                psubTran.FromStationName = coordinationOut != null && !isAutoCheckOut ? coordinationOut.TownBus_Coordinate_Name : "";
                            }
                            else
                            {
                                 psubTran.TotalDistance = (value.Count() > 13) ? decimal.Parse(value[13]) : 0;
                                    var TranInId = (value.Length > 0) ? value[0] : "";
                                var TranIn = new TransactionQueryBuilder(walletEntities).GetTranByTranID(TranInId).FirstOrDefault();
                                var coordinateInId = TranIn != null && (TranIn.Remarks.Split('|').Length > 4) ? int.Parse(TranIn.Remarks.Split('|')[4]) : 0;
                                var coordinationIn = new WalletTownbusLogic().GetTownBusCoordinateByCoordinateID(coordinateInId);
                                psubTran.FromStationName = coordinationIn != null ? coordinationIn.TownBus_Coordinate_Name : "";
                                psubTran.ToStationName = coordinationOut != null && !isAutoCheckOut ? coordinationOut.TownBus_Coordinate_Name : "";
                            }
                        }
                        psubTran.PaymentGateway = Trans.Find(p => p.Tran_ID == SubTran.Tran_ID).PaymentGateway;
                        psubTran.Source = Trans.Find(p => p.Tran_ID == SubTran.Tran_ID).Source;
                        psubTran.Merchant_ref = Trans.Find(p => p.Tran_ID == SubTran.Tran_ID).Merchant_ref;
                        psubTran.CreateDate = SubTran.CreateDate;
                        psubTran.Verified = SubTran.Verified;
                        psubTran.Tran_ID = SubTran.Tran_ID;
                        psubTran.Sub_ID = SubTran.Sub_ID;
                        result.Add(psubTran);                      
                    }

                    foreach (var tran in lstTranWithdraw)
                    {                       
                        var psubTran = new HistorySubTransaction();
                        psubTran.Source_Amount = tran.Source_Amount;
                        psubTran.Source_Currency = tran.Source_Currency;
                        psubTran.TransactionType = tran.Description;
                        psubTran.Remarks = "PENDING APPROVAL";
                        psubTran.CreateDate = tran.CreateDate;
                        psubTran.Tran_ID = tran.Tran_ID;
                        result.Add(psubTran);                                             
                    }
                    var i = 0;
                    foreach (var sub in subSubTrans)
                    {                    
                        var psubTran = new HistorySubTransaction();
                        psubTran.Source_Amount = sub.Amount;
                        psubTran.Source_Currency = sub.Currency_Code;
                        psubTran.TransactionType = "CASHBONUS";
                        psubTran.Remarks = sub.Remarks;
                        psubTran.Merchant_ref = sub.CreateUser;
                        psubTran.CreateUser = sub.CreateUser;
                        psubTran.CreateDate = sub.CreateDate;
                        psubTran.Tran_ID = sub.Tran_ID;
                        psubTran.Balance = lstDailyAmount.AsEnumerable().Where(p=>p.Key.DayOfYear == sub.CreateDate.DayOfYear).First().Value;
                        result.Add(psubTran);
                        i++;
                    }


                    if (pTransactionType == 3)
                    {
                        result = result.FindAll(p => p.TransactionType == EwalletConstant.TOPUP);
                    }
                    else if (pTransactionType == 2 )
                    {
                        result = result.FindAll(p => p.TransactionType == EwalletConstant.PAYMENT);
                    }
                    else if (pTransactionType == 4)
                    {
                        result = result.FindAll(p => p.TransactionType == EwalletConstant.WITHDRAW);
                    }
                    else if (pTransactionType == 5)
                    {
                        result = result.FindAll(p => p.TransactionType == EwalletConstant.CASHBONUS);
                    }
                    else if (pTransactionType == 6)
                    {
                        result = result.FindAll(p => p.TransactionType == EwalletConstant.REWARD);
                    }
                    else if (pTransactionType == 7)
                    {
                        result = result.FindAll(p => p.TransactionType == EwalletConstant.TOWNBUS);
                    }
                    else if (pTransactionType == 8)
                    {
                        result = result.FindAll(p => p.TransactionType == EwalletConstant.COMMISSION);
                    }
                    pRecordTotal = result.Count;
                    result.ForEach(p => p.Balance = Convert.ToDecimal(string.Format("{0:0.0000}", p.Balance)));
                    result = result.OrderByDescending(p => p.CreateDate).ToList();
                    result = result.Skip(pRow * (pPageNumber - 1)).Take(pRow).ToList();                    
                    return result;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, ex, ""));
                return result;
            }
        }


        public List<Transaction> GetTransbyTransID(List<string> TransID)
        {
            var Trans = new List<Transaction>();
            try
            {               
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    Trans = new TransactionQueryBuilder(new WalletEntities()).GetTransByTranIDs(TransID).ToList();
                }
                return Trans;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), TransID.ToString(), ex, ""));
                return Trans;
            }
        }
        public List<Transaction> GetTrans(string cartGuid = "", string tranId = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            var Trans = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    TransactionQueryBuilder query = null;
                    if (!string.IsNullOrEmpty(cartGuid))
                        query = new TransactionQueryBuilder(new WalletEntities()).HasRemark(cartGuid);
                    if(!string.IsNullOrEmpty(tranId))
                        query = new TransactionQueryBuilder(new WalletEntities()).GetTranByTranID(tranId);
                    if (fromDate != null && toDate != null)
                        query = query.FromDate(fromDate).ToDate(toDate);
                    Trans = query.ToList();
                }
                return Trans;
            }
            catch (Exception ex)
            {
                return Trans;
            }
        }
        public SubTransaction GetSubTranByTran_Amt(string tranID, decimal Amt)
        {
            var SubTran = new SubTransaction();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    SubTran = new SubTransactionQueryBuilder(new WalletEntities()).GetSubTranByTran_Amt(tranID, Amt).FirstOrDefault();
                }
                return SubTran;
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), tranID, ex, ""));
                return SubTran;
            }
        }       

        public bool VerifyTransactionsPreWithdraw(string subtransactionId, string subTransactionChecksum, DateTime? snapshotDate, out List<SubTransaction> subTrans, out string message)
        {
            List<string> messages = new List<string>();
            subTrans = new List<SubTransaction>();
            try
            {
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, new System.Transactions.TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                    subTrans = new SubTransactionQueryBuilder(new WalletEntities()).GetSubTransactionsAfterSnapshotWithLastTrans(subtransactionId, snapshotDate).ToList().OrderBy(s => s.Sequent_ID).ToList();

                //Verify Finance noted transaction and transaction itself checksum2
                var lastSubTran = subTrans.SingleOrDefault(st => st.Sub_ID == subtransactionId);
                lastSubTran.Amount = ConvertUtility.RoundToTwoDecimalPlaces(lastSubTran.Amount);
                if (!(subTransactionChecksum == lastSubTran.Checksum2 && lastSubTran.Checksum2 == BuildCheckSum2(lastSubTran.Checksum1, lastSubTran)))
                {
                    lastSubTran.Verified = false;//!workaround for current code (2019-04-03) due to this field changed after insert, just reset to false to check original checksum
                    if (!(subTransactionChecksum == lastSubTran.Checksum2 && lastSubTran.Checksum2 == BuildCheckSum2(lastSubTran.Checksum1, lastSubTran)))//!workaround for current code (2019-04-03) due to this field changed after insert, just reset to false to check original checksum
                        messages.Add(subtransactionId);
                }

                //Verify transactions after snapshot checksum1, checksum2
                SubTransaction prevSub = null;
                foreach (var s in lastSubTran.CreateDate > snapshotDate ? subTrans : subTrans.Where(st=>st.Sub_ID != subtransactionId).ToList())
                {
                    s.Amount = ConvertUtility.RoundToTwoDecimalPlaces(s.Amount);
                    //Verify Checksum1
                    if (prevSub != null)
                    {
                        if (s.Checksum1 != BuildCheckSum1(prevSub.Checksum1, s))
                        {
                            s.Verified = false;//!workaround for current code (2019-04-03) due to this field changed after insert, just reset to false to check original checksum
                            if (s.Checksum1 != BuildCheckSum1(prevSub.Checksum1, s))//!workaround for current code (2019-04-03) due to this field changed after insert, just reset to false to check original checksum
                                messages.Add(s.Sub_ID);
                        }
                    }
                    prevSub = s;

                    //Verify Checksum2
                    if (s.Checksum2 != BuildCheckSum2(s.Checksum1, s))
                    {
                        s.Verified = false;//!workaround for current code (2019-04-03) due to this field changed after insert, just reset to false to check original checksum
                        if (s.Checksum2 != BuildCheckSum2(s.Checksum1, s))//!workaround for current code (2019-04-03) due to this field changed after insert, just reset to false to check original checksum
                            messages.Add(s.Sub_ID);
                    }
                }

                //Rare case: If lastSubTransaction before snapshot time, Remove it from subtrans list
                subTrans = subTrans.Where(st => st.CreateDate >= (snapshotDate?? new DateTime(1900,1,1))).ToList();
                message = messages.Count > 0 ? string.Join(",", messages) : string.Empty;
                if (message.Length > 5000)
                    message = "many warnings when VerifyTransactionsPreWithdraw";
            }

            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                message = ex.ToString();
                return false;
            }
            return string.IsNullOrEmpty(message);
        }
        
        public bool GetWithdraw(List<SubTransaction> subtrans, List<Wallet_Snapshot> snapshot, string subtransactionId, out List<TransactionTransferObject> transferWithdraws, out string message)
        {
            List<string> messages = new List<string>();
            transferWithdraws = new List<TransactionTransferObject>();
            try
            {
                //Get withdraws
                var tranWithdraws = new WalletAccountLogic().GetTransWithdraw();
                if (tranWithdraws == null)
                    messages.Add("Failed to query Transaction Wallet for withdraw. Please check log file");
                if(tranWithdraws != null && tranWithdraws.Count>0)
                {
                    //Get Current Account Balance
                    var walletAccounts = new WalletAccountLogic().GetWalletAccsByWalletIdCurrencies(tranWithdraws.Select(w => $"{w.Wallet_ID}_{w.Source_Currency}").Distinct().ToList());

                    //Tally snapshot + Topup/Payment = CurrentBalance
                    //Tally withdraw + pending withdraw <= CurrentBalance
                    string error = TallyWithdrawBalance(tranWithdraws, subtrans, snapshot, walletAccounts, out transferWithdraws);
                    if (!string.IsNullOrEmpty(error))
                        messages.Add(error);

                    

                    
                }
                message = messages.Count > 0 ? messages.Aggregate((a, b) => a + ";" + b) : string.Empty;
            }

            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                Task.Factory.StartNew(() => logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, ""));
                message = ex.ToString();
                return false;
            }
            return string.IsNullOrEmpty(message);
        }

        public string TallyWithdrawBalance(List<Transaction> withdraws, List<SubTransaction> subtrans, List<Wallet_Snapshot> snapshot, List<Wallet_Account> walletAccs, out List<TransactionTransferObject> transferWithdraws)
        {
            List<string> messages = new List<string>();
            transferWithdraws = new List<TransactionTransferObject>();
            //Get Current Rewards data
            var walletRewards = new WalletRewardLogic().GetRewardsByAccIds(walletAccs.Select(a=>a.ID).Distinct().ToList());
            //Get Users
            var users = new WalletUserLogic().GetAspNetUsersByIds(withdraws.Select(w => w.User_ID).ToList());
            if(walletRewards == null || users == null || walletAccs == null)
                messages.Add($"walletRewards count {(walletRewards?.Count().ToString()?? "null")}, users count {(users?.Count().ToString()?? "null")}, walletAccs count {(walletAccs?.Count().ToString()?? "null")}");
            
            foreach (var tran in withdraws)
            {
                string error = string.Empty, warning = string.Empty;
                try
                {
                    decimal snapshotAmount = decimal.Zero, snapshotRewardAmount = decimal.Zero, subtransAmount = decimal.Zero, pendingWithdrawsAmount = decimal.Zero;
                    var walletAcc = walletAccs.FirstOrDefault(w => w.Wallet_ID == tran.Wallet_ID && w.Currency_Code == tran.Source_Currency);
                    var reward = walletRewards.FirstOrDefault(r => r.ID == walletAcc.ID);
                    if ((snapshot?.Count?? 0) > 0)
                    {
                        snapshotAmount = snapshot.FirstOrDefault(s => s.Account_ID == walletAcc.ID)?.Balance ?? decimal.Zero;
                        snapshotRewardAmount = snapshot.FirstOrDefault(s => s.Account_ID == walletAcc.ID)?.Reward_Amount ?? decimal.Zero;
                    }
                    //Calculate total amount IN,OUT after snapshot
                    subtrans.Where(st => st.Account_ID == walletAcc.ID).ToList().ForEach(st => subtransAmount += (st.Direction == "IN" ? st.Amount.Value : (-1 * st.Amount.Value)));

                    //Include pending withdraw
                    withdraws.Where(stw => stw.Wallet_ID == tran.Wallet_ID && stw.Source_Currency == tran.Source_Currency && stw.CreateDate < tran.CreateDate).ToList().ForEach(stw => pendingWithdrawsAmount += stw.Source_Amount.Value);

                    //Tally snapshot and transactions after snapshot with Balance; pending withdraws with Balance
                    if (snapshotAmount + snapshotRewardAmount + subtransAmount != walletAcc.Available_Balance + (reward?.Reward_Amount?? decimal.Zero))
                        error = $"Balance not tallied!";
                    else if (tran.Source_Amount + pendingWithdrawsAmount > walletAcc.Available_Balance)
                        error = $"{(pendingWithdrawsAmount > decimal.Zero? " + some pending Withdraw(s) " : "" )} Amount is higher than Balance!";

                    //Validate Fraud (Number of topup) (Check when customer topups first)
                    if (subtrans.Where(st => st.Account_ID == walletAcc.ID && st.Direction == "IN" && st.CreateDate > (tran.CreateDate.Value.AddDays(-5))).Count() > 5)
                        warning = "warning: topup > 5 times in last 5 days";
                    if(users.FirstOrDefault(u => u.Id == tran.User_ID) == null)
                        warning += $"User data missing.";
                    else if((users.FirstOrDefault(u=>u.Id == tran.User_ID).Status?? 0) == 0)
                        warning += $"User blocked/not active.";

                    if(!string.IsNullOrEmpty(error))
                        messages.Add(error.Contains("Balance not tallied") ? $"WalletAccount { walletAcc.ID} {error}": $"Withdaws {tran.Tran_ID} {error}");
                }catch(Exception ex) {
                    messages.Add($"Withdraw {tran.Tran_ID}: Fatal error! {ex.ToString()}");
                }

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
                tranObjectTransfer.Data_Check = !string.IsNullOrEmpty(tran.Status) && tran.Status != "NULL" ? tran.Status : (error + warning);
                transferWithdraws.Add(tranObjectTransfer);

            }

            return messages.Count>0 ? messages.Aggregate((a, b) => a + "," + b) : string.Empty;
        }

        public bool UpdateCheckSumSubTransaction(string pDateTime, string plastCheckSum1)
        {
            var lstSubTrans = new List<SubTransaction>();
            var userLogic = new WalletUserLogic(true);
            try
            {
                DateTime dFrom = new DateTime(int.Parse(pDateTime.Split('-')[0]), int.Parse(pDateTime.Split('-')[1]), int.Parse(pDateTime.Split('-')[2]), 0, 0, 1);
                var logWallet = new LogWallet();
                WalletTransactionUow WalletTransactionUnitOfWork = null;
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(new WalletEntities()))
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Start Update UpdateCheckSumSubTransaction", null, "");
                    lstSubTrans = WalletTransactionUnitOfWork.GetAllSubTran(dFrom);
                    var lastCheckSum1 = "";
                    for (var i = 0; i < lstSubTrans.Count; i++)
                    {
                        if (i == 0)
                        {
                            lastCheckSum1 = plastCheckSum1;
                        }
                        else
                        {
                            lastCheckSum1 = lstSubTrans[i - 1].Checksum1;
                        }
                        lstSubTrans[i].Checksum1 = BuildCheckSum1(lastCheckSum1, lstSubTrans[i], true);
                        lstSubTrans[i].Checksum2 = BuildCheckSum2(lstSubTrans[i].Checksum1, lstSubTrans[i], true);
                    }
                    WalletTransactionUnitOfWork.DoUpdateMany(lstSubTrans);
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Finish Update UpdateCheckSumSubTransaction", null, "");
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

        public List<Transaction> GetTrans(string currencyCode, string cartGuid = "", string tranId = "", string createUser = "", string userID = "")
        {
            var Trans = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    TransactionQueryBuilder query = null;
                    if (!string.IsNullOrEmpty(cartGuid))
                        query = new TransactionQueryBuilder(new WalletEntities()).GetTransByRemark(cartGuid);
                    if (!string.IsNullOrEmpty(tranId))
                        query = new TransactionQueryBuilder(new WalletEntities()).GetTransByTranID(tranId);
                    if (!string.IsNullOrEmpty(createUser))
                        query = new TransactionQueryBuilder(new WalletEntities()).GetTransByCreateUser(currencyCode, createUser);
                    if (!string.IsNullOrEmpty(userID))
                        query = new TransactionQueryBuilder(new WalletEntities()).GetTransByUserID(currencyCode, userID);
                    Trans = query.ToList();
                }
                return Trans;
            }
            catch (Exception ex)
            {
                return Trans;
            }
        }
    }
}