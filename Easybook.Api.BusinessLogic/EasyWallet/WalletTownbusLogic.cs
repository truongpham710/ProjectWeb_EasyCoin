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
using Easybook.Api.Core.Model.EasyWallet.Models.TownBus;

namespace Easybook.Api.BusinessLogic.EasyWallet
{
    public class WalletTownbusLogic
    {
        public WalletTownbusLogic(bool genStampKey = false)
        {
            if (genStampKey)
            {
                localhost.EWallet_StampService stampService = new localhost.EWallet_StampService();
                var EncryptTokenEBW = SimpleAesUtil.Encrypt(EwalletConstant.TokenEBW);
                Globals.StampServerKey = stampService.Generate_Stamp_Key(EncryptTokenEBW);
            }
        }

        public TownBusTrip GetMaxChargeByTripID(TownBusEntities newWalletEntities, int TripID)
        {
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                     return new WalletTownbusQueryBuilder(newWalletEntities).GetTownbusInfoByTripID(TripID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return null;
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
            tran.Remarks = pInterestrate;
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
        public TokenTownBusResponse GetAccessTokenTownBus(TokenRequestTownBus requestParams)
        {
            TokenTownBusResponse tokenTownBus = new TokenTownBusResponse();
            var townBusEntity = new TownBusEntities();
            bool isExistToken = false;
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            try
            {
                
                var strCredencial = SimpleAesUtil.DecryptAES(requestParams.Credential, EwalletConstant.keyAES).Split(';');
                var isExists = new WalletUserTownbusQueryBuilder(new TownBusEntities()).GetUserByLoginIdnPassword(strCredencial.First(), strCredencial.Last()).FirstOrDefault();
                if (isExists!=null)
                {
                    var branch = new WalletBranchTownBusQueryBuilder(new TownBusEntities()).HasBranchId(isExists.Branch_ID).FirstOrDefault();
                    if (branch != null)
                        tokenTownBus.CompanyID = branch.Company_ID;
                    tokenTownBus.AccessToken = SimpleAesUtil.EncryptAES(isExists.User_ID + isExists.Password + DateTime.Now.ToString("yyyy-MM-dd hh:00:00"), EwalletConstant.keyAES);
                    tokenTownBus.UserID = isExists.User_ID;
                    //save notificateToken to config
                    if(string.IsNullOrWhiteSpace(requestParams.NotificationToken))
                    {
                        isExistToken = new TownBusNotificationQueryBuilder(townBusEntity).HasNotificationUniqueId(requestParams.CarPlate, tokenTownBus.CompanyID, requestParams.NotificationUniqueId.Trim());
                    }
                    else
                    {
                        isExistToken = new TownBusNotificationQueryBuilder(townBusEntity).HasNotifcationToken(requestParams.CarPlate, tokenTownBus.CompanyID, requestParams.NotificationToken.Trim());
                    }
                  
                    if (!isExistToken)
                    {
                       // var busType = new TownBusTypeQueryBuilder(townBusEntity).GetBusIdByCompanyIdnBusNo(tokenTownBus.CompanyID, requestParams.CarPlate).FirstOrDefault();
                        using (WalletTransactionUnitOfWork = new WalletTransactionUow(townBusEntity))
                        {

                            TownBusNotification townBusNotification = new TownBusNotification();
                            townBusNotification.CarPlate = requestParams.CarPlate;
                            townBusNotification.CompanyID = tokenTownBus.CompanyID;
                            //townBusNotification.BusID = busType.Bus_ID;
                            townBusNotification.NotificationToken = requestParams.NotificationToken.Trim();
                            townBusNotification.NotificationUniqueId = requestParams.NotificationUniqueId.Trim();
                            WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead)
                                .DoInsert(townBusNotification)
                                .EndTransaction();

                        }
                    }
                  
                }
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), requestParams.Credential, ex, "");
                return tokenTownBus;
            }
            return tokenTownBus;
        }
        public List<Transaction> GetTransactionLstTownBusByCarIDnDateTime(TripsRequestTownBus request)
        {
            var transactionlst = new List<Transaction>();
            try
            {
                transactionlst = new TransactionQueryBuilder(new WalletEntities())
                    .GetTransBySource("TownBus")
                    .FromDate(DateTime.Parse(request.StartDateTimeRange.Replace("T"," ")))
                    .ToDate(DateTime.Parse(request.EndDateTimeRange.Replace("T"," "))).ToList();
               
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), request.CarId, ex, "");
                return null;
            }
            return transactionlst;
        }
        public List<Transaction> GetListPendingTransactionByUserID(string userID)
        {
            var transactionlst = new List<Transaction>();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    transactionlst = new TransactionQueryBuilder(new WalletEntities())
                         .GetPendingTransByUserIDnStatus("NULL", userID, "TownBus").ToList();

                }

            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), userID, ex, "");
                return null;
            }
            return transactionlst;
        }

        public ListEligibleTownBusesResponse GetListEligibleTownBuses()
        {
            var EligibleTownBuseslst = new ListEligibleTownBusesResponse();
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    var townBuses_supportArea = new WalletTownBus_SupportedAreaQueryBuilder(new TownBusEntities())
                         .GetTownBus_SupportedAreaByStatus().ToList();

                    foreach(var item_1 in townBuses_supportArea.GroupBy(x=>x.Area_Name))
                    {
                        Areas areaItem = new Areas();
                        areaItem.Area_Name = item_1.Key;
                        foreach (var item_2 in townBuses_supportArea.Where(x=>x.Area_Name==item_1.Key))
                        {
                            TownBusSupportedArea townbusSA = new TownBusSupportedArea();
                            townbusSA.TownBusType_Bus_No = item_2.TownBusType_Bus_No;
                            townbusSA.TownBusType_Bus_ID = item_2.TownBusType_Bus_ID.Value;
                            areaItem.TownBuslst.Add(townbusSA);
                        }
                      
                        EligibleTownBuseslst.ListEligibleTownBuses.Add(areaItem);
                    }
                }


            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return null;
            }
            return EligibleTownBuseslst;
        }
        public bool IsExsistListPendingTransactionByUserID(string userID)
        {
            var IsExsist = false;
            try
            {
                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    IsExsist = new TransactionQueryBuilder(new WalletEntities())
                         .IsExistPendingTransByUserIDnStatus("NULL", userID, "TownBus");

                }

            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), userID, ex, "");
                return false;
            }
            return IsExsist;
        }

        public TownBusCoordinate GetTownBusCoordinateByCoordinateID(int coordinateID)
        {
            var townBusCoordinate = new TownBusCoordinate();
            try
            {
                townBusCoordinate = new WalletTownBusCoordinateQueryBuilder(new TownBusEntities()).GetTownBusCoordinateByCoordinateID(coordinateID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), townBusCoordinate.TownBus_Coordinate_ID, ex, "");
                return null;
            }
            return townBusCoordinate;
        }

        public Transaction GetTranSinceFirstStation(string userID,int tripID,string tranID, string carID)
        {
            Transaction tran = new Transaction();
            try
            {
                var  transactionlst = new TransactionQueryBuilder(new WalletEntities())
                    .GetTransBySource("TownBus")
                    .GetTranByTranID(tranID)
                    .GetPendingTransByUserIDnStatus("NULL", userID)
                    .ToList();
           
                tran = transactionlst.Where(tr => tr.Remarks.Split('|')[2].Contains(carID) && tr.Remarks.Split('|')[0].Contains(tripID.ToString()) && tr.Remarks.Contains("TOWNBUS-FULLROUTE")).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), userID, ex, "");
               // return null;
            }
            return tran;
        }
        public bool HasFreeRides(int FreeRidesNumber, string userID, string tripID)
        {
            return new TransactionQueryBuilder(new WalletEntities()).HasFreeRides(FreeRidesNumber, userID, tripID);
        }
        
  
    }
}
