using System;
using Easybook.Api.BusinessLogic.EasyWallet;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet;
using Easybook.Api.Core.Model.EasyWallet.Models;
using Easybook.Api.DataAccessLayer.QueryBuilder.Wallet;
using Easybook.Api.DataAccessLayer.UnitOfWork;
using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using Easybook.Api.Core.Model.EasyWallet.DataTransferObject;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Reflection;
using static Easybook.Api.BusinessLogic.EasyWallet.Utility.SecurityLogic;
using Easybook.Api.Core.CrossCutting.Utility;
using System.Collections.Generic;
using System.Linq;
using API_Wallet.PushyMeLib;
using System.Threading.Tasks;
using System.Configuration;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects;
using Easybook.Api.BusinessLogic.MoengagePush;

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
                    var CurDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                    user.User_ID = pUser.User_ID;
                    user.UpdateUser = pUser.UpdateUser;
                    user.CreateUser = pUser.CreateUser;
                    user.DOB = CurDate;
                    user.CreateDate = CurDate;
                    user.IsMemberAgent = pUser.IsMemberAgent;
                    user.UpdateDate = CurDate;
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
            var walletAccountLogic = new WalletAccountLogic();
            var tran = new Transaction();
            try
            {         

                result = CheckBalanceWalletAccount(pTran.AccID, pTran);
                if (result.Status == 0)
                {
                    return result;
                }

                tran.CreateDate = pTran.CreateDate;
                tran.CreateUser = pTran.CreateUser ?? "";
                tran.Description = pTran.Description;
                tran.Destination_Amount = Convert.ToDecimal(string.Format("{0:0.00}", pTran.Destination_Amount ?? 0));
                tran.Destination_Currency = pTran.Destination_Currency;
                tran.Merchant_ref = pTran.Merchant_ref;
                tran.PaymentGateway = pTran.PaymentGateWay;
                tran.Source_Amount = Convert.ToDecimal(string.Format("{0:0.00}", pTran.Source_Amount ?? 0));
                tran.Remarks = pTran.Remarks;
                if (pTran.Description == EwalletConstant.WITHDRAW)
                {
                    var WalletAcc = walletAccountLogic.GetWalletAccByUserIDnCurrencyCode(pTran.User_ID, pTran.Source_Currency);
                    if (WalletAcc == null)
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Code;
                        result.Status = 0;
                        result.Message = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Message;
                        return result;
                    }
                    var TransWithdraw = walletAccountLogic.GetTranWithdrawByUserIDNoVerify(pTran.User_ID, pTran.Source_Currency);
                    var totalPendingAmt = 0.0M;
                    foreach (var pendingAmtWithdraw in TransWithdraw)
                    {
                        totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                    }
                   
                    var AvailableAmount = (WalletAcc.Available_Balance ?? 0) - totalPendingAmt - tran.Source_Amount;
                    tran.Remarks = pTran.Remarks + "|" + AvailableAmount;
                }                    
                tran.Source = pTran.Source;       
                tran.Source_Currency = pTran.Source_Currency;
                tran.Status = "NULL";
                tran.User_ID = pTran.User_ID;
                tran.Wallet_ID = pTran.Wallet_ID;
                tran.Tran_ID = pTran.Tran_ID;
                tran.UpdateDate = pTran.UpdateDate;
                tran.UpdateUser = pTran.UpdateUser;
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

        public TownBusQRCode DecryptQRCode(TransactionRequestTownBus pTran)
        {
            TownBusQRCode townBusQRCode = new TownBusQRCode();
            var qRArray = SimpleAesUtil.DecryptAES(pTran.Credential, EwalletConstant.TownBusSerectKey, EwalletConstant.TownBusIVKey).Split('|');
            if(qRArray.Count()>0)
            {
                townBusQRCode.TripId = int.Parse(qRArray[0].Trim());
                townBusQRCode.CarPlate = qRArray[1].Trim();
                townBusQRCode.CurrentCoordinationId = int.Parse(qRArray[2].Trim());
                townBusQRCode.NextCoordinationId = int.Parse(qRArray[3].Trim());
                townBusQRCode.Latitude = decimal.Parse(qRArray[4].Trim());
                townBusQRCode.Longitude = decimal.Parse(qRArray[5].Trim());
                townBusQRCode.DistanceSinceFirstStation = decimal.Parse(qRArray[6].Trim());
                townBusQRCode.DistanceToNextStation = decimal.Parse(qRArray[7].Trim());
                townBusQRCode.DistanceToLastStation = decimal.Parse(qRArray[8].Trim());
                townBusQRCode.Timestamp = long.Parse(qRArray[9].Trim());
            }
           
            return townBusQRCode;
        }
        /// <summary>
        /// Insert InsertCheckinTransactionTownBus
        /// </summary>
        /// <returns></returns>
        public TranTownBusResponse InsertCheckInTransactionTownBus(TransactionRequestTownBus pTran)
        {
            var logWallet = new LogWallet();
            var result = new TranTownBusResponse();
            var walletLogic = new WalletTransactionLogic();
            var walletSnapshotLogic = new WalletSnapshotLogic();
            var walletAccountLogic = new WalletAccountLogic();
            var tran = new Transaction();
            var townbusLogic = new WalletTownbusLogic();
            var tranRequest = new TransactionRequest();
            var walletAccLogic = new WalletAccountLogic();
            var townBusTrip = new TownBusTrip();
            logWallet = new LogWallet();
            var user = new UserRequest();
            var walletUserLogic = new WalletUserLogic();
            string carPlate = string.Empty;
            bool hasFreeRides = false;

            try
            {

                TownBusQRCode infoQRcode = DecryptQRCode(pTran);
                carPlate = infoQRcode.CarPlate;
                if(townbusLogic.IsExsistListPendingTransactionByUserID(pTran.User_ID))
                {
                    var lstPedding = townbusLogic.GetListPendingTransactionByUserID(pTran.User_ID)
                                    //.Where(x => 
                                    //    x.Remarks.Split('|')[0].Trim().Equals(infoQRcode.TripId.ToString())
                                    //  && x.Remarks.Split('|')[2].Trim().Equals(infoQRcode.CarPlate.ToString())
                                    //&& x.UpdateDate.Value.ToString("yyyy-MM-dd") ==DateTime.Now.ToString("yyyy-MM-dd")
                                    ;
                    if (lstPedding.Any())
                    {
                        foreach (var lastPedding in lstPedding)
                        {
                            var value = lastPedding.Remarks.Split('|');
                            TransactionRequestTownBus request = new TransactionRequestTownBus();
                            request.Credential = value.Count() > 12 ? value[12].Trim() : "";
                            request.TranID = lastPedding.Tran_ID;
                            request.User_ID = lastPedding.User_ID;
                            InsertCheckOutTransactionTownBus(request, true);
                        }
                        //result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        //result.Status = 0;
                        //result.Message = "There is pending transaction.";
                        //return result;
                    }
                }
               
              
                if (!walletUserLogic.IsExistUserIDInWalletUser(pTran.User_ID))
                {
                    user.User_ID = pTran.User_ID;
                    var walletUser = InsertNewUserForEBW(user);
                }
                townBusTrip = new WalletTownbusLogic().GetMaxChargeByTripID(new TownBusEntities(), infoQRcode.TripId);
                var walletAccount = walletAccLogic.GetWalletAccByUserIDnCurrencyCode(pTran.User_ID, townBusTrip.Currency);

                result = CheckBalanceWalletAccountForTownBus(walletAccount, pTran, townBusTrip.Max_Charge.Value, townBusTrip.Currency);
                if (result.Status == 0)
                {
                    return result;
                }
                var coordination = new WalletTownbusLogic().GetTownBusCoordinateByCoordinateID(infoQRcode.CurrentCoordinationId);
                tran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                DateTime.Now.Minute, DateTime.Now.Second);
                tran.Description = "PAYMENT";
              
                //promotion for maketing 20-12-2019       
                hasFreeRides = new WalletTownbusLogic().HasFreeRides(townBusTrip.FreeRide.Value, walletAccount.User_ID, townBusTrip.TownBus_Trip_ID.ToString());
                tran.Remarks = infoQRcode.TripId + " | " + EwalletConstant.FULLTOWNBUS + " | " //0//1
                                                                           + infoQRcode.CarPlate + " | " //2
                                                                                                         // + pTran.TripId + " | " //
                                                                           + townBusTrip.Charge_Type + " | "//3
                                                                           + infoQRcode.CurrentCoordinationId + " | " /* 4 StationID*/
                                                                           + infoQRcode.NextCoordinationId + " | " /* 5 StationID*/
                                                                           + infoQRcode.Latitude + " | " //6
                                                                           + infoQRcode.Longitude + " | "//7
                                                                           + infoQRcode.DistanceSinceFirstStation + " | " //8
                                                                           + infoQRcode.DistanceToNextStation + " | " //9
                                                                           + infoQRcode.DistanceToLastStation + " | " //10
                                                                           + infoQRcode.Timestamp  //11
                                                                           + (townBusTrip.Charge_Type.Contains(EwalletConstant.FLATRATE) ? " | " + townBusTrip.Company_ID : " | " + pTran.Credential) //12
                                                                           + (hasFreeRides ? " | " + "HasFreeRides" : "")
                                                                            // + townBusTrip.Company_ID //13
                                                                            ;

                if (hasFreeRides)
                {
                    tran.Source_Amount = 0;
                }
                else
                {
                    tran.Source_Amount = townBusTrip.Max_Charge.Value;
                }
                tran.Source = "TownBus";
                tran.Status = "NULL";
                tran.PaymentGateway = "";
                tran.User_ID = pTran.User_ID;
                tran.Wallet_ID = walletAccount.Wallet_ID;
                tran.Source_Currency = townBusTrip.Currency;
                tran.Tran_ID = ("EW" + Guid.NewGuid().ToString().Replace("-", "")).Substring(0, 30);
                tran.UpdateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                tran.UpdateUser = "TownBus";
                tran.CreateUser = "TownBus";
                if (walletLogic.InsertTempTransaction(tran))
                {
                    if (townBusTrip.Charge_Type == EwalletConstant.ZONE || townBusTrip.Charge_Type == EwalletConstant.KM)
                    {

                        result.TranID = tran.Tran_ID;
                        result.FeeMaxCharge = townBusTrip.Max_Charge.Value;
                        result.CoordinationName = coordination.TownBus_Coordinate_Name;
                        result.CreateDate = tran.UpdateDate.Value;
                        result.ChargeType = townBusTrip.Charge_Type;
                        result.Currency = townBusTrip.Currency;
                        result.TotalFeeCharged = townBusTrip.Max_Charge.Value;

                        if (hasFreeRides)
                        {
                            result.TotalFeeCharged = 0;
                        }
                        else
                        {
                            if (result.Reward > 0)
                            {
                                result.MainCash = result.MainCash - (townBusTrip.Max_Charge.Value * (decimal)0.92);
                                result.Reward = result.Reward - (townBusTrip.Max_Charge.Value * (decimal)0.08);
                            }
                            else
                            {
                                result.MainCash = result.MainCash - townBusTrip.Max_Charge.Value;
                            }
                        }
                        result.AvailableBalance = result.Reward + result.MainCash;
                    }
                    else
                    {
                        var accID = walletAccount.ID;

                        result = InsertPaymentTransactionTownBus(logWallet, result, walletLogic, walletAccount, tran, pTran, accID);
                        if (result.Status == 1)
                        {
                            result.TranID = tran.Tran_ID;


                            result.CreateDate = tran.UpdateDate.Value;
                            result.CoordinationName = coordination.TownBus_Coordinate_Name;
                            result.FeeMaxCharge = townBusTrip.Max_Charge.Value;
                            result.BusTripFree = townBusTrip.Max_Charge.Value;
                            result.Currency = townBusTrip.Currency;
                            result.TotalFeeCharged = townBusTrip.Max_Charge.Value;
                            result.ChargeType = townBusTrip.Charge_Type;

                            if (hasFreeRides)
                            {
                                result.BusTripFree = 0;
                                result.TotalFeeCharged = 0;
                            }
                            else
                            {
                                if (result.Reward > 0)
                                {
                                    result.MainCash = result.MainCash - (townBusTrip.Max_Charge.Value * (decimal)0.92);
                                    result.Reward = result.Reward - (townBusTrip.Max_Charge.Value * (decimal)0.08);
                                }
                                else
                                {
                                    result.MainCash = result.MainCash - tran.Source_Amount.Value;
                                }
                            }
                            result.AvailableBalance = result.Reward + result.MainCash;
                        }
                        else
                        {
                            return result;
                        }

                    }

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
                logWallet.Log(MethodBase.GetCurrentMethod(), "WalletID: " + pTran.User_ID, ex, "");
                return result;
            }
            //Push notification ScanIn
            Task.Factory.StartNew(() => PushNotificationToDriverDevice(townBusTrip, carPlate, tran, hasFreeRides ?0:townBusTrip.Max_Charge.Value, true));
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            //logWallet.Log(MethodBase.GetCurrentMethod(), pTran, null , "", 1, 1);

            return result;
        }
        public void PushNotificationToDriverDevice(TownBusTrip townBusTrip, string carPlate, Transaction tran, decimal amountFee, bool isCheckIn, bool isError=false, string errorMessage="")
        {
            var logWallet = new LogWallet();
            string message = string.Empty;
            try
            {
                List<string> devicesToken = new List<string>();
                List<string> moengageDevicesToken = new List<string>();
                if(string.IsNullOrWhiteSpace(carPlate) || !townBusTrip.Company_ID.HasValue)
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + tran.User_ID,new Exception(), "CarPlate is not found or Company ID is not found");
                    return;
                }
                var tokenDriver = new TownBusNotificationQueryBuilder(new TownBusEntities()).GetNotifcationTokenLst(carPlate, townBusTrip.Company_ID).ToList();
                if(tokenDriver!=null)
                {
                    foreach (var itemToken in tokenDriver)
                    {
                        if (!string.IsNullOrWhiteSpace(itemToken.NotificationToken))
                        {
                            devicesToken.Add(itemToken.NotificationToken);
                        }
                        else
                        {
                            moengageDevicesToken.Add(itemToken.NotificationUniqueId);
                        }
                       
                    }
                    var UserName = new AspNetUsersQueryBuilder(new CommonEntities()).HasUserId(tran.User_ID).FirstOrDefault();
                    if (isError)
                    {
                        message = "{'Passenger' : '" + (string.IsNullOrWhiteSpace(UserName.FirstName) ? "" : UserName.FirstName) + "'"
                                 + ", 'ScanType' : " + (isCheckIn ? "'1'" : "'2'")
                                 + ", 'Error' : '" + true + "'"
                                 + ", 'ErrorMsg' : '" + errorMessage + "'"
                                 + ", 'ScanDateTime' : '" + tran.UpdateDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") + "'}";
                    }
                    else
                    {
                        message = "{'Passenger' : '" + (string.IsNullOrWhiteSpace(UserName.FirstName) ? "" : UserName.FirstName) + "'"
                                  + ", 'ScanType' : " + (isCheckIn ? "'1'" : "'2'")
                                  + ", 'ChargeAmout' : '" + amountFee + "'"
                                  + ", 'Currency' : '" + tran.Source_Currency + "'"
                                  + ", 'ScanDateTime' : '" + tran.UpdateDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") + "'}";
                    }
                   
                    if (devicesToken.Count > 0)
                    {
                        SendNotification.PushNotification(devicesToken, message);
                    }
                    if (moengageDevicesToken.Count > 0)
                    {
                        foreach (var item in moengageDevicesToken)
                        {
                            MoengagePushApi.PushNotification(item, message);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + tran.User_ID, ex, ex.Message);
            }
            
        }
        public TokenTownBusResponse GetAccessTokenTownBus(TokenRequestTownBus requests)
        {
            var logWallet = new LogWallet();
            var result = new TokenTownBusResponse();
            try
            {
                 result = new WalletTownbusLogic().GetAccessTokenTownBus(requests);
                if (string.IsNullOrEmpty(result.AccessToken))
                {

                    result.Status = 0;
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Message = "Failed to get access token for town bus";

                }
                else
                {
                  
                    result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                    result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                    result.Status = ApiReturnCodeConstant.SUCCESS.Code;
                }
            }
            catch(Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to get access token Town Bus. Please check log file!";
                logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "CarID: " + requests.Credential, ex, "");
                return result;
            }
            return result;
        }       
        public TransactionTownBusResponse GetTransactionLstTownBusByCarIDnDateTime(TripsRequestTownBus request)
        {
            var logWallet = new LogWallet();
            var result = new TransactionTownBusResponse();
            try
            {
                var transactionlst = new WalletTownbusLogic().GetTransactionLstTownBusByCarIDnDateTime(request).ToList();
                if(transactionlst.Any())
                {
                    foreach (var item in transactionlst)
                    {
                        var lstRemarks = item.Remarks.Split('|');
                        if (string.IsNullOrEmpty(lstRemarks[2]) &&lstRemarks[2].Trim() != request.CarId.Trim())
                            continue;
                        TranTownBus tran = new TranTownBus();
                        tran.ChargeAmout = item.Source_Amount.Value;
                        tran.Currency = item.Source_Currency;
                        tran.ScanDateTime = item.CreateDate.Value.ToString("yyyy-MM-ddTHH:mm:ss");
                        tran.ScanType = !string.IsNullOrEmpty(lstRemarks[1]) && lstRemarks[1].Trim() == "TOWNBUS-FULLROUTE" ? 1 : 2; // 1: scan in/2: scan out
                        var user = new AspNetUsersQueryBuilder(new CommonEntities()).HasUserId(item.User_ID).FirstOrDefault();
                        tran.PassengerUsername = user.FirstName;
                        result.Triplst.Add(tran);
                    }
                }
                result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                result.Status = ApiReturnCodeConstant.SUCCESS.Code;

            }
            catch(Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to get transaction list Town Bus by CarID n DateTime. Please check log file!";
                logWallet = new LogWallet();
               // logWallet.Log(MethodBase.GetCurrentMethod(), "CarID: " + request.CarID, ex, "");
                return result;
            }
            return result;
        }
        public TranTownBusResponse InsertCheckOutTransactionTownBus(TransactionRequestTownBus pTran, bool isAutoCheckOut = false)
        {
            var logWallet = new LogWallet();
            var result = new TranTownBusResponse();
            var walletLogic = new WalletTransactionLogic();
            var walletSnapshotLogic = new WalletSnapshotLogic();
            var walletAccountLogic = new WalletAccountLogic();
            var tran = new Transaction();
            var tranRequest = new TransactionRequest();
            var walletAccLogic = new WalletAccountLogic();
            var wallet_Account = new Wallet_Account();
            var accID = "";
            decimal totalDistance = 0;
            var townBusTrip = new TownBusTrip();
            string carPlate = string.Empty;
            var coordinationOut = new Core.Model.EasyWallet.Models.TownBus.TownBusCoordinate();
            //var TimestampIn = 0;
            try
            {
                TownBusQRCode infoQRcode = DecryptQRCode(pTran);
                carPlate = infoQRcode.CarPlate;
                townBusTrip = new WalletTownbusLogic().GetMaxChargeByTripID(new TownBusEntities(), infoQRcode.TripId);
                wallet_Account = walletAccLogic.GetWalletAccByUserIDnCurrencyCode(pTran.User_ID, townBusTrip.Currency);
                accID = wallet_Account.ID;
                result = CheckBalanceWalletAccountForTownBus(wallet_Account, pTran, townBusTrip.Max_Charge.Value, townBusTrip.Currency);
                if (result.Status == 0)
                {
                    return result;
                }
                //forgot check out case
                if (isAutoCheckOut)
                {
                    tran.Source_Amount = townBusTrip.Max_Charge;
                    totalDistance = (townBusTrip.Max_Charge / townBusTrip.Price).Value;
                }
                else
                {
                    var tranIn = new WalletTownbusLogic().GetTranSinceFirstStation(pTran.User_ID, infoQRcode.TripId, pTran.TranID, infoQRcode.CarPlate);
                    if (tranIn != null && !string.IsNullOrEmpty(tranIn.Remarks) && tranIn.Remarks.IndexOf("|") > -1 && infoQRcode.Timestamp >= long.Parse(tranIn.Remarks.Split('|')[11].Trim()))
                    {
                        var distanceSinceFirstStationIN = (tranIn.Remarks.Split('|').Length > 8) ? tranIn.Remarks.Split('|')[8] : "0";
                        totalDistance = infoQRcode.DistanceSinceFirstStation - decimal.Parse(distanceSinceFirstStationIN);
                    }
                    else
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "QR code is expired.";
                        return result;
                    }
                    // tran.Source_Amount = Convert.ToDecimal(string.Format("{0:0.00}", pTran.Source_Amount ?? 0));
                    if (townBusTrip.Charge_Type == EwalletConstant.KM)//|| pTran.ChargeType == "ZONE"
                    {
                        tran.Source_Amount = totalDistance == 0 ? townBusTrip.Min_Charge : (townBusTrip.Price * Math.Abs(totalDistance)); //
                    }
                    coordinationOut = new WalletTownbusLogic().GetTownBusCoordinateByCoordinateID(infoQRcode.CurrentCoordinationId);
                }
                var hasFreeRides = new WalletTownbusLogic().HasFreeRides(townBusTrip.FreeRide.Value, wallet_Account.User_ID, townBusTrip.TownBus_Trip_ID.ToString());
                if (hasFreeRides)
                {
                    tran.Source_Amount = 0;
                }

                tran.Remarks = pTran.TranID /*TranID of check IN*/ + " | " + EwalletConstant.SINGLETOWNBUS + " | " //0/1
                                                                            + infoQRcode.CarPlate + " | "//2
                                                                                                         // + pTran.TripId + " | "
                                                                            + townBusTrip.Charge_Type + " | "//3
                                                                            + infoQRcode.CurrentCoordinationId + " | " /* 4 StationID*/
                                                                            + infoQRcode.NextCoordinationId + " | " /* 5 StationID*/
                                                                            + infoQRcode.Latitude + " | " //6
                                                                            + infoQRcode.Longitude + " | "//7
                                                                            + infoQRcode.DistanceSinceFirstStation + " | "// 8
                                                                            + infoQRcode.DistanceToNextStation + " | "//9
                                                                            + infoQRcode.DistanceToLastStation + " | "//10
                                                                            + infoQRcode.Timestamp + " | "//11
                                                                            + townBusTrip.Company_ID + " | "//12
                                                                            + totalDistance + " | "//13
                                                                            + isAutoCheckOut//14
                                                                            + (hasFreeRides ? " | " + "HasFreeRides" : "")
                                                                            ;

                tran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                tran.CreateUser = "TownBus";
                tran.Description = "PAYMENT";
                tran.Source = "TownBus";
                tran.PaymentGateway = "";
                tran.Status = "NULL";
                tran.Source_Currency = townBusTrip.Currency;
                tran.User_ID = pTran.User_ID;
                tran.Wallet_ID = wallet_Account.Wallet_ID;
                tran.Tran_ID = ("EW" + Guid.NewGuid().ToString().Replace("-", "")).Substring(0, 30);
                tran.UpdateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                tran.UpdateUser = "TownBus";

                // var coordinateInId = (tran.Remarks.Split('|').Length > 5) ? int.Parse(tran.Remarks.Split('|')[5]) : 0;
                // var coordinationIn = new WalletTownbusLogic().GetTownBusCoordinateByCoordinateID(coordinateInId);

                if (walletLogic.InsertTempTransaction(tran))
                {
                    result.TranID = tran.Tran_ID;


                    result.CreateDate = tran.UpdateDate.Value;
                    result.CoordinationName = coordinationOut == null ? "" : coordinationOut.TownBus_Coordinate_Name;
                    result.FeeMaxCharge = townBusTrip.Max_Charge.Value;
                    result.BusTripFree = tran.Source_Amount.Value;
                    result.FeeRefunded = (townBusTrip.Max_Charge.Value - tran.Source_Amount.Value) < 0 ? 0 : (townBusTrip.Max_Charge.Value - tran.Source_Amount.Value);
                    result.Currency = townBusTrip.Currency;
                    result.TotalFeeCharged = (townBusTrip.Max_Charge.Value - tran.Source_Amount.Value) < 0 ? townBusTrip.Max_Charge.Value : tran.Source_Amount.Value;
                    result.TotalDistance = totalDistance;
                    result.ChargeType = townBusTrip.Charge_Type;

                    // result.TimestampIn = TimestampIn;
                    // result.CoordinationNameIn = coordinationIn.TownBus_Coordinate_Name;
                    if (hasFreeRides)
                    {
                        result.BusTripFree = 0;
                        result.TotalFeeCharged = 0;
                        result.FeeRefunded = 0;
                    }
                    else
                    {
                        if (result.Reward > 0)
                        {
                            result.MainCash = result.MainCash - (tran.Source_Amount.Value * (decimal)0.92);
                            result.Reward = result.Reward - (tran.Source_Amount.Value * (decimal)0.08);
                        }
                        else
                        {
                            result.MainCash = result.MainCash - tran.Source_Amount.Value;
                        }
                    }
                    result.AvailableBalance = result.Reward + result.MainCash;
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
                logWallet.Log(MethodBase.GetCurrentMethod(), "WalletID: " + pTran.User_ID, ex, "ScanOut");
                return result;
            }


            result = InsertPaymentTransactionTownBus(logWallet, result, walletLogic, wallet_Account, tran, pTran, accID, true);
            //Push notification ScanOut
            Task.Factory.StartNew(() => PushNotificationToDriverDevice(townBusTrip, carPlate, tran, result.BusTripFree, false));

            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }


        public TranTownBusResponse InsertPaymentTransactionTownBus(LogWallet logWallet,TranTownBusResponse result, WalletTransactionLogic walletLogic, Wallet_Account wallet_Account, Transaction tran, TransactionRequestTownBus pTran,string accID, bool isCheckOut=false)
        {
            var walletRewardLogic = new WalletRewardLogic();
            SubTransaction subTran = new SubTransaction();
            var RewardAcc = new Wallet_Account_Reward();
            string subID = "", lastCheckSum1 = "";
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            var walletEntity = new WalletEntities();
            try
            {
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(walletEntity))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    RewardAcc = WalletTransactionUnitOfWork.GetRewardByAccID(accID);

                    if (!walletLogic.UpdateIsProcessing(wallet_Account, WalletTransactionUnitOfWork, accID, true))
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


                    if (RewardAcc.Reward_Amount == 0) // Create 1 subtransaction: Main Balance
                    {
                        using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                        {
                            lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                        }

                        subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                        if (!string.IsNullOrEmpty(subID))
                        {
                            if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, tran.Tran_ID, subTran, ""))
                            {
                                WalletTransactionUnitOfWork.RollBack();
                                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                result.Status = 0;
                                result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                return result;
                            }

                            if (!walletLogic.UpdateBalanceByAccountID(wallet_Account, WalletTransactionUnitOfWork, accID, tran.Source_Amount ?? 0, subTran.Direction, "OUT"))
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
                    //else if (RewardAcc.Reward_Amount >= tran.Source_Amount) // Create 1 subtransaction: Reward
                    //{
                    //    using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                    //    {
                    //        lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                    //    }
                    //    tran.Remarks = tran.Remarks + " | Deducted from REWARD";

                    //    subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                    //    if (!string.IsNullOrEmpty(subID))
                    //    {
                    //        var amtAvailable = walletRewardLogic.UpdateRewardByAccIDForPayment(WalletTransactionUnitOfWork, RewardAcc, tran.Source_Amount ?? 0, "");
                    //        if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, tran.Tran_ID, subTran, ""))
                    //        {
                    //            WalletTransactionUnitOfWork.RollBack();
                    //            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    //            result.Status = 0;
                    //            result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                    //            return result;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    //        result.Status = 0;
                    //        result.Message = "Failed to create new Payment SubTransaction. Please check log file!";
                    //        WalletTransactionUnitOfWork.RollBack();
                    //        return result;
                    //    }
                    //}
                    else   // Create 2 subtransaction: Reward and Main Balance
                    {
                        using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                        {
                            lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                        }
                        
                        var totalPayment = tran.Source_Amount??0;
                        var rewardAmt = RewardAcc.Reward_Amount;

                        decimal calReward = 0;
                        decimal calMainCash = 0;
                        calReward = totalPayment * 8 / 100;
                        calMainCash = totalPayment * 92 / 100;
                        var realReward = walletRewardLogic.UpdateRewardByAccIDForPayment(WalletTransactionUnitOfWork, RewardAcc, calReward, "");
                        //if (amtAvailable > 0)
                        // {
                        if (realReward < calReward)
                        {
                            calMainCash = calMainCash + (calReward - realReward);
                        }
                        tran.Source_Amount = realReward;
                            tran.Remarks = tran.Remarks + " | Deducted from REWARD";
                            subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                            if (!string.IsNullOrEmpty(subID))
                            {
                                if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, tran.Tran_ID, subTran, ""))
                                {
                                    WalletTransactionUnitOfWork.RollBack();
                                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                                    result.Status = 0;
                                    result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                                    return result;
                                }
                                lastCheckSum1 = subTran.Checksum1;
                                tran.Source_Amount = calMainCash;
                                tran.Remarks = tran.Remarks.Replace(" | Deducted from REWARD", " | Deducted from WALLET");
                                subTran = new SubTransaction();
                                subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                                if (!string.IsNullOrEmpty(subID))
                                {
                                    if (!walletLogic.UpdateBalanceByAccountID(wallet_Account, WalletTransactionUnitOfWork, accID, calMainCash, subTran.Direction, "OUT"))
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

                  //  }
                  //asking a question for this
                   // wallet_Account = WalletTransactionUnitOfWork.GetWalletByID(accID);
                    if (!walletLogic.UpdateIsProcessing(wallet_Account, WalletTransactionUnitOfWork, accID, false))
                    {
                        WalletTransactionUnitOfWork.RollBack();
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "Failed to update status isProcessing false with ACCID: " + accID + ". Please check log file!";
                        return result;
                    }
                   // result.SubID = subID;
                    result.TranID = tran.Tran_ID;
                    var FullTranRoute = WalletTransactionUnitOfWork.GetTranByTranID(isCheckOut? pTran.TranID : tran.Tran_ID);
                    FullTranRoute.Status = "settled";
                    WalletTransactionUnitOfWork.DoUpdate(FullTranRoute);
                    
                    WalletTransactionUnitOfWork.EndTransaction();
                }
            }
            catch (Exception ex)
            {
                WalletTransactionUnitOfWork.RollBack();
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to create new payment transaction. Please check log file!";
                result.Status = 0;
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + tran.Tran_ID + "|AccountID: " + accID, ex, ex.Message);
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }
        public decimal GetMaxChargeByTripID(int TripID)
        {
            decimal MaxCharge = 0;
            TownBusEntities newTownBusEntities = new TownBusEntities();
            var walletTownbuslogic = new WalletTownbusLogic();
            TownBusTrip Townbus = new TownBusTrip();
            Townbus = walletTownbuslogic.GetMaxChargeByTripID(newTownBusEntities, TripID);
            if (Townbus != null)
            {
                MaxCharge = Townbus.Max_Charge??0;
            }
            return MaxCharge;
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
                    if (!string.IsNullOrEmpty(pTran.Remarks))
                    {
                        if (pTran.Remarks.ToLower().StartsWith("easybook reward"))
                        {
                            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
                            return result;
                        }       
                    }
                    if ((pTran.Source_Currency == "MYR" && pTran.Source_Amount < 20) || (pTran.Source_Currency == "SGD" && pTran.Source_Amount < 20))
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_MINIMUM_AMOUNT_TOPUP.Code;
                        result.Status = 0;
                        result.Message = "The minimum topup is 20" + pTran.Source_Currency + ". " + ApiReturnCodeConstant.Wallet.ERR_WALLET_MINIMUM_AMOUNT_TOPUP.Message;
                        return result;
                    }
                    else if (pTran.Source_Amount < WalletRule.Minimum_Topup_Amount)
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
                    decimal calReward = 0;
                    decimal calMainCash = 0;
                    decimal rewardAmount = 0;

                    foreach (var pendingAmtWithdraw in TransWithdraw)
                    {
                        totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                    }
                    var walletReward = walletRewardLogic.GetRewardByAccID(pTran.AccID);
                   
                    if (walletReward.Reward_Amount > 0)
                    {
                        CalculateRewardAmount(pTran.Remarks, pTran.Source_Amount ?? 0, out calReward, out calMainCash);
                        rewardAmount = walletReward.Reward_Amount;
                    }
                    else
                        calMainCash = pTran.Source_Amount??0;
                    if (calMainCash > WalletAcc.Available_Balance - totalPendingAmt)
                    {
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Code;
                        result.Status = 0;
                        result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Message;
                        return result;
                    }
                    if (calReward > rewardAmount)
                    {
                        var redunt = calReward - rewardAmount;
                        if (redunt > WalletAcc.Available_Balance - totalPendingAmt - calMainCash)
                        {
                            result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Code;
                            result.Status = 0;
                            result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Message;
                            return result;
                        }
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

        public TranTownBusResponse CheckBalanceWalletAccountForTownBus(Wallet_Account WalletAcc, TransactionRequestTownBus pTran, decimal maxChargedAmount,string currency )
        {
            var result = new TranTownBusResponse();
            var walletLogic = new WalletAccountLogic();
            var walletTranLogic = new WalletTransactionLogic(true);
            var walletRewardLogic = new WalletRewardLogic();
            try
            {
                if (!walletTranLogic.CheckSumWalletAccount(WalletAcc))
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_CHECKSUMACC_FAIL.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_CHECKSUMACC_FAIL.Message;
                    return result;
                }
                var TransPaymentTownBusnWithdraw = walletLogic.GetTranTownBusnWithdrawByUserIDNoVerify(pTran.User_ID, currency);
                var totalPendingAmt = 0.0M;
                
                foreach (var pendingAmtWithdraw in TransPaymentTownBusnWithdraw)
                {
                    totalPendingAmt += pendingAmtWithdraw.Source_Amount ?? 0;
                }
                var walletReward = walletRewardLogic.GetRewardByAccID(WalletAcc.ID);
                decimal rewardAmount = 0;
                if (walletReward != null)
                {
                    rewardAmount = walletReward.Reward_Amount;
                }

                //if(EwalletConstant.SetMinTopUpMYR20FreeRides == "1" && (currency == "SGD" || currency == "MYR") && WalletAcc.Available_Balance - totalPendingAmt < 20)
                //{
                //    result.IsGetFreeRides = true;
                //}

                if (maxChargedAmount > WalletAcc.Available_Balance - totalPendingAmt)
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_INSUFFICIENT_BALANCE.Message;
                    return result;
                }
                else
                {
                    result.MainCash = (WalletAcc.Available_Balance??0) - totalPendingAmt;
                    result.Reward = rewardAmount;
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to Check Balance Wallet Account";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "AccID: " + WalletAcc.ID, ex, "");
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
                accID = walletAccLogic.GetWalletAccByUserIDnCurrencyCode(TempTran.User_ID, TempTran.Source_Currency).ID;
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
              
                    lastCheckSum1 = WalletTransactionUnitOfWork.GetLastCheckSum1();

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
                    //if (walletLogic.VerifyTopUpTransaction(wallAcc, WalletTransactionUnitOfWork, subTran, lastCheckSum1))
                    //{
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
                    //}
                    //else
                    //{
                    //    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    //    result.Status = 0;
                    //    result.Message = "Failed to verify topup Transaction!";
                    //    WalletTransactionUnitOfWork.RollBack();
                    //    return result;
                    //}

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
                    EmailUtil.SendEmail("[Exception]-[Wallet Reward]", bodyEmail, "truong.pham@easybook.com");
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
            //ResetStampServerKey();
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
                    //else
                    //{
                    //    wallAcc.IsProcessing = true;
                    //}
                   
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

                            if (!walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, tran.Source_Amount ?? 0, subTran.Direction, "OUT"))
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
                    //else if (RewardAcc.Reward_Amount >= tran.Source_Amount) // Create 1 subtransaction: Reward
                    //{
                    //    using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                    //    {
                    //        lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                    //    }
                    //    tran.Remarks = tran.Remarks + " | Deducted from REWARD";

                    //    subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                    //    if (!string.IsNullOrEmpty(subID))
                    //    {
                    //        var amtAvailable = walletRewardLogic.UpdateRewardByAccIDForPayment(WalletTransactionUnitOfWork, RewardAcc, tran.Source_Amount ?? 0, "");
                    //        if (!walletLogic.UpdateStatusTranAndSubTran(WalletTransactionUnitOfWork, pTranID, subTran, ""))
                    //        {
                    //            WalletTransactionUnitOfWork.RollBack();
                    //            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    //            result.Status = 0;
                    //            result.Message = "Updated failed Status Tran and SubTran. Please check log file!";
                    //            return result;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    //        result.Status = 0;
                    //        result.Message = "Failed to create new Payment SubTransaction. Please check log file!";
                    //        WalletTransactionUnitOfWork.RollBack();
                    //        return result;
                    //    }
                    //}
                    else   // Create 2 subtransaction: Reward and Main Balance
                    {
                        using (var SubTransactionQueryBuilder = new SubTransactionQueryBuilder(new WalletEntities()))
                        {
                            lastCheckSum1 = SubTransactionQueryBuilder.GetLastCheckSum1();
                        }

                        var totalPayment = tran.Source_Amount??0;
                        var cartGuid = tran.Remarks;
                        var rewardAmt = RewardAcc.Reward_Amount;
                        decimal calReward = 0;
                        decimal calMainCash = 0;
                        CalculateRewardAmount(tran.Remarks, totalPayment, out calReward, out calMainCash);
                        var realReward = walletRewardLogic.UpdateRewardByAccIDForPaymentWithoutTownbus(WalletTransactionUnitOfWork, RewardAcc, calReward, "");
                        if (realReward < calReward)
                        {
                            calMainCash = calMainCash + (calReward - realReward);
                        }
                        tran.Source_Amount = realReward;

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
                            tran.Source_Amount = calMainCash;
                            tran.Remarks = tran.Remarks.Replace(" | Deducted from REWARD", " | Deducted from WALLET");
                            subTran = new SubTransaction();
                            subID = walletLogic.GenerateNewSubTransaction_Payment(WalletTransactionUnitOfWork, tran, accID, ref subTran, lastCheckSum1);
                            if (!string.IsNullOrEmpty(subID))
                            {
                                if (!walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, calMainCash, subTran.Direction, "OUT"))
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

        public TranResponse InsertInterestTransaction(WalletTransactionUow WalletTransactionUnitOfWork, Transaction tran, string accID, List<Wallet_Account> lstAccount)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            SubTransaction subTran = new SubTransaction();
            Wallet_Account wallAcc = new Wallet_Account();
            string subID = "", lastCheckSum1 = "";
            //WalletEntities newWalletEntities = new WalletEntities();
            
            //if (!walletLogic.CheckSumWalletAccount(wallAcc))
            //{
            //    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
            //    result.Status = 0;
            //    result.Message = "Failed to check checksum for wallet account. Please check log file!";
            //    return result;
            //}

            //Move insert Trans outsite to enhance performance
            //WalletTransactionUnitOfWork.DoInsert(tran).SaveAndContinue();

            //lastCheckSum1 = new SubTransactionQueryBuilder(newWalletEntities).GetLastCheckSum1();

            subID = walletLogic.GenerateNewSubTransaction_Interest(WalletTransactionUnitOfWork, tran, accID, ref subTran, "");
            if (!string.IsNullOrEmpty(subID))
            {
                wallAcc = lstAccount.SingleOrDefault( a=> a.ID == accID);
                if (walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, subTran.Amount ?? 0, subTran.Direction, "IN", false))
                {
                    tran.Status = "true";
                    WalletTransactionUnitOfWork.DoUpdate(tran);
                }
            }
            else
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to create new Payment SubTransaction. Please check log file!";
                return result;
            }


            result.SubID = subID;
            result.TranID = tran.Tran_ID;
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        /// <summary>
        /// Insert New Payment 
        /// </summary>
        /// <returns></returns>
        public TranResponse InsertInterestTransaction(Transaction tran, string accID)
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            SubTransaction subTran = new SubTransaction();
            Wallet_Account wallAcc = new Wallet_Account();
            string subID = "", lastCheckSum1 = "";
            WalletTransactionUow WalletTransactionUnitOfWork = null;
            WalletEntities newWalletEntities = new WalletEntities();
            try
            {
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(newWalletEntities))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    wallAcc = WalletTransactionUnitOfWork.GetWalletByID(accID);

                    //if (!walletLogic.CheckSumWalletAccount(wallAcc))
                    //{
                    //    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    //    result.Status = 0;
                    //    result.Message = "Failed to check checksum for wallet account. Please check log file!";
                    //    return result;
                    //}

                    WalletTransactionUnitOfWork.DoInsert(tran).SaveAndContinue();

                    //lastCheckSum1 = new SubTransactionQueryBuilder(newWalletEntities).GetLastCheckSum1();

                    subID = walletLogic.GenerateNewSubTransaction_Interest(WalletTransactionUnitOfWork, tran, accID, ref subTran, "");
                    if (!string.IsNullOrEmpty(subID))
                    {
                        if (walletLogic.UpdateBalanceByAccountID(wallAcc, WalletTransactionUnitOfWork, accID, subTran.Amount ?? 0, subTran.Direction, "IN"))
                        {
                            tran.Status = "true";
                            WalletTransactionUnitOfWork.DoUpdate(tran).SaveAndContinue();
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
                logWallet.Log(MethodBase.GetCurrentMethod(), "TranID: " + tran.Tran_ID + "|AccountID: " + accID, ex, "");
                return result;
            }

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
        public TranResponse InsertRewardTransaction(decimal amtTopup, string accID, string remark = "", bool isAPI = false, string merchant_ref = "")
        {
            var result = new TranResponse();
            var walletLogic = new WalletTransactionLogic(true);
            var walletRewardLogic = new WalletRewardLogic();
            var walletAccLogic = new WalletAccountLogic();
            var logWallet = new LogWallet();
            Wallet_Account wallAcc = new Wallet_Account();

            WalletTransactionUow WalletTransactionUnitOfWork = null;
            var walletEntity = new WalletEntities();
            wallAcc = walletAccLogic.GetWalletAccountByAccID(accID);
            if (wallAcc == null)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to find wallet account. Please check log file!";
                return result;
            }
            decimal RewardAmount = 0;
            try
            {
                if (!isAPI)
                {   
                    var dateStartUp = new DateTime(int.Parse(EwalletConstant.DateStartUpReward.Substring(0, 4)), int.Parse(EwalletConstant.DateStartUpReward.Substring(4, 2)), int.Parse(EwalletConstant.DateStartUpReward.Substring(6, 2)));                    
                    if (DateTime.Now.DayOfYear < dateStartUp.DayOfYear)
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "There is not any reward program for this moment.";
                        result.TranID = "Empty";
                        return result;
                    }

                    var dateExpire = new DateTime(int.Parse(EwalletConstant.DateExpireReward.Substring(0, 4)), int.Parse(EwalletConstant.DateExpireReward.Substring(4, 2)), int.Parse(EwalletConstant.DateExpireReward.Substring(6, 2)));
                    if (dateExpire.DayOfYear - DateTime.Now.DayOfYear < 0)
                    {
                        result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                        result.Status = 0;
                        result.Message = "There is not any reward program for this moment.";
                        result.TranID = "Empty";
                        return result;
                    }
                   
                    //if (!walletRewardLogic.CheckConditionRewardByDate(new DateTime(2019, 04, 05)))
                    //{
                    //    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    //    result.Status = 0;
                    //    result.Message = "There is not any reward program for this moment.";
                    //    result.TranID = "Empty";
                    //    return result;
                    //}                  
                    //if (!walletRewardLogic.CheckConditionRewardByDatenUserID(new DateTime(2019, 04, 05), wallAcc.User_ID))
                    //{
                    //    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_OVER_LIMIT_TOPUPBONUS.Code;
                    //    result.Status = 0;
                    //    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_OVER_LIMIT_TOPUPBONUS.Message;
                    //    result.TranID = "Empty";
                    //    logWallet.Log(MethodBase.GetCurrentMethod(), "", null, ApiReturnCodeConstant.Wallet.ERR_WALLET_OVER_LIMIT_TOPUPBONUS.Message + ": " + EwalletConstant.LimitTopupExtraPerUser);
                    //    return result;
                    //}
                }
                using (WalletTransactionUnitOfWork = new WalletTransactionUow(walletEntity))
                {
                    WalletTransactionUnitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);    
                    RewardAmount = isAPI ? amtTopup : walletRewardLogic.Calculate_Reward_Amount(amtTopup, wallAcc.Currency_Code);
                    remark = !string.IsNullOrEmpty(remark) ? remark : "Easybook Wallet Top Up Cash Rewards Promotion";
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
                    tran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                    tran.CreateUser = "";
                    tran.Description = "REWARD";
                    tran.Merchant_ref = merchant_ref;
                    tran.PaymentGateway = "";
                    tran.Remarks = remark;
                    tran.Source = "SYSTEM";
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
                    SubTran.CreateUser = "";
                    SubTran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                    SubTran.Sub_ID = SecurityLogic.GenerateKey(30);
                    SubTran.Checksum1 = walletLogic.BuildCheckSum1(lastCheckSum1, SubTran, false);
                    var strCheckSum2 = walletLogic.BuildCheckSum2(SubTran.Checksum1, SubTran, false);
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

                    var flg = walletRewardLogic.UpdateRewardByAccIDForTopup(WalletTransactionUnitOfWork, accID, RewardAmount, remark);
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
            //ResetStampServerKey();
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
        public WalletAccountResponse GetWalletAccountWithRewardMinusPendingWithdrawByUserID(string pUserID)
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
                            newwallACC.RewardAmount = 0;
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
                        newwallACC.Available_Balance = WalletAcc.Available_Balance - totalPendingAmt;
                        newwallACC.Currency_Code = WalletAcc.Currency_Code;
                        newwallACC.ID = WalletAcc.ID;
                        newwallACC.Total_Balance = WalletAcc.Total_Balance;
                        newwallACC.RewardAmount = rewardAmount;
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

        public WalletAccountResponse GetWalletAccountWithRewardMinusPendingWithdrawByUserID_EnhanceV1(string pUserID, List<Transaction> lstTrans)
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
                            newwallACC.RewardAmount = 0;
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
                        var TransWithdraw = lstTrans.Where(p => p.Description == "WITHDRAW" && p.Source_Currency == WalletAcc.Currency_Code && (string.IsNullOrEmpty(p.Status) || p.Status == "NULL" || p.Status == "pending"));
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
                        newwallACC.Available_Balance = WalletAcc.Available_Balance - totalPendingAmt;
                        newwallACC.Currency_Code = WalletAcc.Currency_Code;
                        newwallACC.ID = WalletAcc.ID;
                        newwallACC.Total_Balance = WalletAcc.Total_Balance;
                        newwallACC.RewardAmount = rewardAmount;
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

        public WalletAccountResponse GetWalletAccountByUserID(string pUserID, List<Wallet_Account> lstAccounts)
        {
            var result = new WalletAccountResponse();
            var walletLogic = new WalletAccountLogic();
            var walletRewardLogic = new WalletRewardLogic();
            var lstWalletReward = new List<Wallet_Account_Reward>();
            result.lstWalletAccount = new List<WalletTransferObject>();
            var logWallet = new LogWallet();
            try
            {     
                var lstWalletAcc = lstAccounts.Where(cart => cart.User_ID.Equals(pUserID));
                //lstWalletAcc = walletLogic.GetWalletAccountByUserID(pUserID);
                foreach (var WalletAcc in lstWalletAcc)
                {
                    var newwallACC = new WalletTransferObject();
                    newwallACC.Available_Balance = WalletAcc.Available_Balance ;
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
                            newwallACC.RewardAmount = 0;
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
                        newwallACC.Available_Balance = WalletAcc.Available_Balance - totalPendingAmt;
                        newwallACC.Currency_Code = WalletAcc.Currency_Code;
                        newwallACC.ID = WalletAcc.ID;
                        newwallACC.Total_Balance = WalletAcc.Total_Balance;
                        newwallACC.RewardAmount = rewardAmount;
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
                result.Wallet_ID = tranResponse.Wallet_ID;
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
            var bankaccLogic = new UserBankAccountLogic();
            try
            {
                var tranResponse = walletAccLogic.GetTransWithdraw();
                var bankAcc = bankaccLogic.GetBankAccwithoutPending();
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
                    if (tran.Remarks.Split('|')[1].LastIndexOf('=') > 0)
                    {
                        var verifyBankAcc = bankAcc.AsEnumerable().Where(p => SimpleAesUtil.DecryptAES(p.AccountNumber, EwalletConstant.keyAES).Replace(EwalletConstant.strWord, "") == SimpleAesUtil.Decrypt(tran.Remarks.Split('|')[1]));
                        tranObjectTransfer.Status_Verify = verifyBankAcc.Select(p => p.Verify).FirstOrDefault() != null ? verifyBankAcc.Select(p => p.Verify).FirstOrDefault() : "Pending";
                    }
                    else
                    {
                        var verifyBankAcc = bankAcc.AsEnumerable().Where(p => SimpleAesUtil.DecryptAES(p.AccountNumber, EwalletConstant.keyAES).Replace(EwalletConstant.strWord, "") == tran.Remarks.Split('|')[1]);
                        tranObjectTransfer.Status_Verify = verifyBankAcc.Select(p => p.Verify).FirstOrDefault() != null ? verifyBankAcc.Select(p => p.Verify).FirstOrDefault() : "Pending";
                    }

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

        public ListTransactionResponse GetTransWithdrawWithCancel()
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = walletAccLogic.GetTransWithdrawWithCancel();
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

                var walletReward = walletRewardLogic.GetRewardByAccID(WalletAcc.ID);
                decimal rewardAmount = 0;
                if (walletReward != null)
                {
                    rewardAmount = walletReward.Reward_Amount;
                }
                result.AvailableAmount = (WalletAcc.Available_Balance ?? 0) - totalPendingAmt ;
                result.RewardAmount = rewardAmount;
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
        public WalletAmountResponse GetAvailableAmtAccWithoutRewardbyTranID(string UserID, string pCurrencyCode)
        {
            var result = new WalletAmountResponse();
            var walletLogic = new WalletAccountLogic();
            var walletRewardLogic = new WalletRewardLogic();
            try
            {
              
                var WalletAcc = walletLogic.GetWalletAccByUserIDnCurrencyCode(UserID, pCurrencyCode);
                if (WalletAcc == null)
                {
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Code;
                    result.Status = 0;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_USERIDnCURRENCY_NOTEXIST_IN_WALLETACC.Message;
                    return result;
                }

                var TransWithdraw = walletLogic.GetTranWithdrawByUserIDNoVerify(UserID, pCurrencyCode);
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
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + UserID + "Currency Code: " + pCurrencyCode, ex, "");
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

        public ListPendingTranTownBusResponse GetListPendingTransactionByUserID(string pUserID)
        {

            var result = new ListPendingTranTownBusResponse();
            var townbusLogic = new WalletTownbusLogic();
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
                
                var listTranPending = townbusLogic.GetListPendingTransactionByUserID(pUserID);
                if(listTranPending.Count()==0)
                {
                    result.Code = 1;
                    result.Status = 1;
                    result.Message = "this is no pending transaction.";
                    return result;
                }
                else
                {
                    foreach (var item in listTranPending)
                    {
                        WalletAmountResponse balanceAmount = GetAvailableAmtAccWithoutRewardbyTranID(pUserID, item.Source_Currency);
                        if (balanceAmount.Status == 0)
                        {
                            result.Status = 0;
                            result.Code = balanceAmount.Code;
                            result.Message = balanceAmount.Message;
                        }
                        PendingTranTownBusResponse pendingtran = new PendingTranTownBusResponse();
                        var value = item.Remarks.Split('|');

                        var coordinateID = value.Count() > 4 ? int.Parse(value[4].Trim()) : 0;
                        var coordination = new WalletTownbusLogic().GetTownBusCoordinateByCoordinateID(coordinateID);
                        pendingtran.CoordinationName = coordination?.TownBus_Coordinate_Name ?? "";
                        pendingtran.FeeMaxCharge = item.Source_Amount.Value;
                        pendingtran.TotalFeeCharged = item.Source_Amount.Value;
                        pendingtran.CreateDate = item.UpdateDate.Value;
                        pendingtran.ChargeType = value.Count() > 3 ? value[3].Trim() : "";
                        pendingtran.Currency = item.Source_Currency;
                        pendingtran.TranID = item.Tran_ID;
                        pendingtran.AvailableBalance = balanceAmount.AvailableAmount;
                        pendingtran.TripId = value.Count() > 0 ? int.Parse(value[0].Trim()) : 0;
                        pendingtran.Timestamp = value.Count() > 11 ? long.Parse(value[11].Trim()) : 0;
                        pendingtran.Credential = value.Count() > 12 ? value[12].Trim() : "";
                        result.lstPeddingTrans.Add(pendingtran);
                    }
                }
                
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query GetListPendingTransByUserID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;

            return result;
        }
        public ListEligibleTownBusesResponse GetListEligibleTownBuses()
        {

            var result = new ListEligibleTownBusesResponse();
            var townbusLogic = new WalletTownbusLogic();
          
            try
            {
                result = townbusLogic.GetListEligibleTownBuses();

            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query GetListEligibleTownBuses";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: ", ex, "");
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
            public ListSubTransactionResponse GetSubTransByUserId(string pUserID, int pPageNumber, int pRow, string pDateFrom, string pDateTo, int pTransactionType, string pCurrencyCode)
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

                //if (dFrom.AddDays(+31).CompareTo(dTo) <= 0)
                //{
                //    dTo = dFrom.AddDays(+31);                 
                //}

                var RecordTotal = 0;
                var SubTrans = tranLogic.GetSubTransByUserId(pUserID, pPageNumber, pRow, dFrom, dTo, ref RecordTotal, pTransactionType, pCurrencyCode);
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

        public ListTransactionResponse GetTransByCartGuidOrIdOrCreateUser(string cartGuid = "", string transactionId = "", string createUser = "", string userID = "", string currencyCode = "")
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {                
                var tranResponse = new WalletTransactionLogic(false).GetTrans(currencyCode, cartGuid, transactionId, createUser, userID);
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet for withdraw. Please check log file";
                }
                var lstTran = new List<TransactionTransferObject>();
                var lstSubTran = new SubTransactionQueryBuilder(new WalletEntities()).GetAllSubTranByUserID(tranResponse?.FirstOrDefault()?.User_ID).ToList();
                decimal? balance = 0;
                tranResponse = tranResponse.Where(p => p.Description != "REWARD").ToList();
                foreach (var tran in tranResponse)
                {
                    var tranObjectTransfer = new TransactionTransferObject();
                    tranObjectTransfer.Wallet_ID = tran.Wallet_ID;
                    tranObjectTransfer.TranID = tran.Tran_ID;
                    tranObjectTransfer.User_ID = tran.User_ID;
                    tranObjectTransfer.TranStatus = tran.Status;
                    tranObjectTransfer.Source_Currency = tran.Source_Currency;
                    tranObjectTransfer.Source_Amount = tran.Source_Amount ?? 0;
                    tranObjectTransfer.Source = tran.Source;
                    tranObjectTransfer.Remarks = tran.Remarks;
                    tranObjectTransfer.Description = tran.Description;
                    tranObjectTransfer.PaymentGateWay = tran.PaymentGateway;
                    tranObjectTransfer.Merchant_ref = tran.Merchant_ref;
                    tranObjectTransfer.Destination_Currency = tran.Destination_Currency;
                    tranObjectTransfer.Destination_Amount = tran.Destination_Amount ?? 0;
                    tranObjectTransfer.CreateUser = tran.CreateUser;
                    tranObjectTransfer.CreateDate = tran.CreateDate;
                    tranObjectTransfer.Scheduler_Check = tran.Scheduler_Check;
                    tranObjectTransfer.Data_Check = !string.IsNullOrEmpty(tran.Status) && tran.Status != "NULL" ? tran.Status : null;
                    if (tran.Description == "TOPUP"  || tran.Description == "CASHBONUS" || tran.Description == "COMMISSION")
                    {
                        tranObjectTransfer.Money_In = "+" + tran.Source_Amount;
                        tranObjectTransfer.Money_Out = "";
                        balance = tran.Status == "true" ? balance + tran.Source_Amount : balance;
                    }
                    else if (tran.Description == "WITHDRAW")
                    {
                        tranObjectTransfer.Money_In = "";
                        tranObjectTransfer.Money_Out = "-" + tran.Source_Amount;
                        balance = tran.Status != "canceled" ? balance - tran.Source_Amount : balance;
                    }
                    else if (tran.Description == "PAYMENT")
                    {
                        var amtReward = lstSubTran.Where(p => p.Tran_ID == tran.Tran_ID && p.Remarks.Contains("REWARD")).Select(p => p.Amount).FirstOrDefault();
                        var amtMain = lstSubTran.Where(p => p.Tran_ID == tran.Tran_ID && p.Remarks.Contains("WALLET")).Select(p => p.Amount).FirstOrDefault();
                        amtMain = amtMain == null ? 0 : amtMain;
                        tranObjectTransfer.Money_In = "";
                        tranObjectTransfer.Money_Out = amtReward == null ? "-" + tran.Source_Amount : "-" + tran.Source_Amount + " (Main: -" + amtMain + ", Reward: -" + amtReward + ")";                      
                        balance = tran.Status == "NULL" ? balance : amtReward == null ? balance - tran.Source_Amount : balance - amtMain;
                    }                   
                    tranObjectTransfer.Balance = balance;
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

        public ListTransactionResponse GetTransByCartGuidOrIdOrCreateUserAll(string cartGuid = "", string transactionId = "", string createUser = "", string userID = "", string currencyCode = "")
        {
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = new WalletTransactionLogic(false).GetTrans(currencyCode, cartGuid, transactionId, createUser, userID);
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet for withdraw. Please check log file";
                }
                var lstTran = new List<TransactionTransferObject>();
                var lstSubTran = new SubTransactionQueryBuilder(new WalletEntities()).GetAllSubTranByUserID(tranResponse?.FirstOrDefault()?.User_ID).ToList();
                decimal? balance = 0;
                foreach (var tran in tranResponse)
                {
                    var tranObjectTransfer = new TransactionTransferObject();
                    tranObjectTransfer.Wallet_ID = tran.Wallet_ID;
                    tranObjectTransfer.TranID = tran.Tran_ID;
                    tranObjectTransfer.User_ID = tran.User_ID;
                    tranObjectTransfer.TranStatus = tran.Status;
                    tranObjectTransfer.Source_Currency = tran.Source_Currency;
                    tranObjectTransfer.Source_Amount = tran.Source_Amount ?? 0;
                    tranObjectTransfer.Source = tran.Source;
                    tranObjectTransfer.Remarks = tran.Remarks;
                    tranObjectTransfer.Description = tran.Description;
                    tranObjectTransfer.PaymentGateWay = tran.PaymentGateway;
                    tranObjectTransfer.Merchant_ref = tran.Merchant_ref;
                    tranObjectTransfer.Destination_Currency = tran.Destination_Currency;
                    tranObjectTransfer.Destination_Amount = tran.Destination_Amount ?? 0;
                    tranObjectTransfer.CreateUser = tran.CreateUser;
                    tranObjectTransfer.CreateDate = tran.CreateDate;
                    tranObjectTransfer.Scheduler_Check = tran.Scheduler_Check;
                    tranObjectTransfer.Data_Check = !string.IsNullOrEmpty(tran.Status) && tran.Status != "NULL" ? tran.Status : null;
                    if (tran.Description == "TOPUP" || tran.Description == "CASHBONUS" || tran.Description == "COMMISSION")
                    {
                        tranObjectTransfer.Money_In = "+" + tran.Source_Amount;
                        tranObjectTransfer.Money_Out = "";
                        balance = tran.Status == "true" ? balance + tran.Source_Amount : balance;
                    }
                    else if (tran.Description == "WITHDRAW")
                    {
                        tranObjectTransfer.Money_In = "";
                        tranObjectTransfer.Money_Out = "-" + tran.Source_Amount;
                        balance = tran.Status != "canceled" ? balance - tran.Source_Amount : balance;
                    }
                    else if (tran.Description == "PAYMENT")
                    {
                        var amtReward = lstSubTran.Where(p => p.Tran_ID == tran.Tran_ID && p.Remarks.Contains("REWARD")).Select(p => p.Amount).FirstOrDefault();
                        var amtMain = lstSubTran.Where(p => p.Tran_ID == tran.Tran_ID && p.Remarks.Contains("WALLET")).Select(p => p.Amount).FirstOrDefault();
                        amtMain = amtMain == null ? 0 : amtMain;
                        tranObjectTransfer.Money_In = "";
                        tranObjectTransfer.Money_Out = amtReward == null ? "-" + tran.Source_Amount : "-" + tran.Source_Amount + " (Main: -" + amtMain + ", Reward: -" + amtReward + ")";
                        balance = tran.Status == "NULL" ? balance : amtReward == null ? balance - tran.Source_Amount : balance - amtMain;
                    }
                    tranObjectTransfer.Balance = balance;
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
            double Num;
            
            try
            {
                var Card_Number = SimpleAesUtil.DecryptAES(pUserCard.Card_Number, EwalletConstant.keyAES);
                bool isNum = double.TryParse(Card_Number, out Num);
                if (Card_Number.Length < 12 || Card_Number.Length > 19 || !isNum)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Card Number is not valid.";
                    var logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "UserID: " + pUserCard.User_ID, null, result.Message);
                    return result;
                }
                var CurDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
                usercard.Card_Number = SimpleAesUtil.Encrypt(SimpleAesUtil.DecryptAES(pUserCard.Card_Number, EwalletConstant.keyAES) + Globals.StampServerKey);
                usercard.Bank_Name = pUserCard.Bank_Name;
                usercard.Card_ExpireDate = pUserCard.Card_ExpireDate;
                usercard.Card_HolderName = pUserCard.Card_HolderName;
                usercard.Card_Type = pUserCard.Card_Type;
                usercard.Create_date = CurDate;
                usercard.Currency_Code = pUserCard.Currency_Code;
                usercard.ID = Guid.NewGuid().ToString();
                usercard.Update_date = CurDate;
                usercard.User_ID = pUserCard.User_ID;
                usercard.Card_Country = pUserCard.Card_Country;

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
                result.Status = flg ? 1 : 0;
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
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID + "|Currency: " + pCurrency, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public CreditCardResponse GetUserCardByUserAndId(string pUserID, string pId)
        {
            CreditCardResponse result = new CreditCardResponse();
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
                result.card = walletUserLogic.GetUserCardByUserAndId(pUserID, pId);
                result.card.Card_Number = SimpleAesUtil.EncryptAES(SimpleAesUtil.Decrypt(result.card.Card_Number).Replace(Globals.StampServerKey, ""), EwalletConstant.keyAES);
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query User Card";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID + "|Currency: " + pId, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public UserBankAccountResponse DeleteUserBankAccByID(string pUserBankAccID, string pUserID)
        {
            var result = new UserBankAccountResponse();
            var UserBankAccountLogic = new UserBankAccountLogic();
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
                var flg = UserBankAccountLogic.DeleteUserBankAcc(pUserBankAccID);
                result.Status = flg ? 1 : 0;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query User Card";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID + "|Card ID: " + pUserBankAccID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
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
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID, ex, "");
                return result;
            }
            ResetStampServerKey();
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public ListUserBankAccountReponse GetUserBankAccByUserID(string pUserID)
        {
            var result = new ListUserBankAccountReponse();
            var walletLogic = new WalletAccountLogic();
            var userBankLogic = new UserBankAccountLogic();
            try
            {
                if (!walletLogic.IsExistUserID(pUserID))
                {
                    result.Code = 1;
                    result.Status = 1;
                    result.Message = "Please topup to use this feature.";
                    result.lstUserBankAcc = new List<User_Bank_Account>();
                    return result;
                }
                result.lstUserBankAcc = userBankLogic.GetUserBankAccByUserID(pUserID);
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query User Bank Account";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "User ID: " + pUserID, ex, "");
                return result;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return result;
        }

        public UserResponse UpdateChecksumWalletAccount(string dateTime, string toTime)
        {
            var result = new UserResponse();
            var walletLogic = new WalletAccountLogic();
            bool flg = false;
            try
            {
                flg = walletLogic.UpdateAllCheckSumWalletAccount(dateTime, toTime);
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
            result.Status = flg ? 1 : 0;
            return result;
        }

        public UserResponse UpdateAllCheckSumWalletAccountWithEmptyCS(string dateTime, string toTime)
        {
            var result = new UserResponse();
            var walletLogic = new WalletAccountLogic();
            bool flg = false;
            try
            {
                flg = walletLogic.UpdateAllCheckSumWalletAccountWithEmptyCS(dateTime, toTime);
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
            result.Status = flg ? 1 : 0;
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

        public InterestResponse InsertCashBonus_ForAll(string pSource)
        {
            var result = new InterestResponse();
            var logWallet = new LogWallet();

            var lstUserID = new List<String>();
            var lstAllTrans = new List<Transaction>();
            var lstTranWithdrawBeforeSnapshot = new List<Transaction>();
            var lstAllSubTrans = new List<SubTransaction>();
            var lstRewardAcc = new List<Wallet_Account_Reward>();
            var lstAccounts = new List<Wallet_Account>();
            var lstWalletInterest = new List<Wallet_Interest>();
            var lstWalletSnapshot = new List<Wallet_Snapshot>();
            var lstWalletSnapshotNew = new List<Wallet_Snapshot>();
            var insertTranList = new Dictionary<string,Transaction>();

            DateTime newSnapshotDate = DateTime.Now;           
            try
            {
                //Note (2019-11-08): 
                // 1. make sure snapshot into 1 block
                // 2. Move query Wallet Account outside 
                // 3. use Insert Range for Tran/SubTran, Update Range Wallet Account in NET loop in block as point 2 (Lock-Query-Update to avoid data integrity) || use Update SQL Command for Wallet Account + update checksum by sql or update checksum in NET loop (slow as first part)
                // 4. Add "Inserted live Cash Bonus" to setting Rewardstring to ignore in interest calculation
                //CHECK: !*Double check for transactions between first latestSnapshot & final latestSnapshot (around 3-4 mins) if having record then need to fix to include for accuracy

                var walletEntities = new WalletEntities();
                var lstBlockedUserID = new WalletUserLogic().GetListBlockedUserID().ToList().Select(p => p.Id);
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, new System.Transactions.TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "Insert CashBonus ALL: Step 1: lstBlockedUserID: " + lstBlockedUserID.Count(), walletEntities);
                    //Get Last Snapshot with Account Info
                    Wallet_Snapshot maxDateSnapshot = new WalletSnapshotQueryBuilder(walletEntities).GetMaxDateSnapshot();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "Insert CashBonus ALL: Step 2: maxDateSnapshot: " + maxDateSnapshot.CreateDateSnapshot, walletEntities);
                    var lstSnapshotWithAccount = new WalletSnapshotQueryBuilder(walletEntities).GetSnapshotByCheckSumWithAccountInfo(maxDateSnapshot.Snapshot);
                    lstWalletSnapshot = lstSnapshotWithAccount.Select(sa => sa.Item1).Distinct().ToList();
                    //Get Reward
                    lstRewardAcc = new WalletRewardQueryBuilder(walletEntities).GetRewardWithAmount().ToList();
                    //Get Transaction ( only Topup and Withdraw ) join with Account Info
                    var minSnapshotDate = lstWalletSnapshot.Min(s => s.CreateDateSnapshot);
                    var lstAllTransWithAccount = new TransactionQueryBuilder(walletEntities).GetAllTopupWithdrawWithAccountInfo(minSnapshotDate);
                    lstAllTrans = lstAllTransWithAccount.Select(ta => ta.Item1).Distinct().ToList();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "Insert CashBonus ALL: Step 3: minSnapshotDate: " + minSnapshotDate + "|" + lstAllTrans.Count(), walletEntities);
                    //Get SubTransaction (except reward subtrans)
                    //[optional] can use inline sql to enhance perfomance: walletEntities.Database.SqlQuery<SubTransaction>($"select * from {walletEntities.Database.Connection.Database}..[SubTransaction] nolock where CreateDate > '{minSnapshotDate.ToString("yyyy-MM-dd HH:mm:ss")}'").Where(s=>!EwalletConstant.Rewardstring.Any(x => s.Remarks.ToLower().Contains(x))).Skip(0).Take(500000).ToList();
                    lstAllSubTrans = new SubTransactionQueryBuilder(walletEntities).ExcludeReward(EwalletConstant.Rewardstring).FromDate(minSnapshotDate).ToList();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "Insert CashBonus ALL: Step 4: lstAllSubTrans: " + lstAllSubTrans.Count(), walletEntities);
                    newSnapshotDate = DateTime.Now;// lstAllSubTrans.Max(t => t.CreateDate).Value.AddSeconds(1); //Get date for new snapshot right after having transactions
                    //Get Wallet_Account = from snapshot + new topup users - user inserted cash bonus start from maxDateSnapshot
                    var accountsFromTransaction = lstAllTransWithAccount.Where(ta => ta.Item1.Description == "TOPUP" && ta.Item1.Status.Contains("true")).Select(ta => ta.Item2).Distinct().ToList();
                    var accIdsInTrans = accountsFromTransaction.Select(a => a.ID);
                    var accountsFromSnapshot = lstSnapshotWithAccount.Where(sa => sa.Item1.Balance > 0 && !accIdsInTrans.Contains(sa.Item2.ID)).Select(sa=>sa.Item2).Distinct().ToList();
                    var lstUserIDInsertedCB = new SubTransactionQueryBuilder(walletEntities).GetListUserIDWithInsertedCashBonusInCurrentMonth().ToList().Select(a => a.User_ID);
                    
                    lstAccounts = accountsFromTransaction.Union(accountsFromSnapshot).ToList();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "Insert CashBonus ALL: Step 5: lstUserIDInsertedCB: " + lstUserIDInsertedCB.Count() + "|lstAccounts: " + lstAccounts.Count(), walletEntities);
                    //Get All User
                    lstUserID = lstAccounts.Select(a => a.User_ID).Except(lstUserIDInsertedCB).Except(lstBlockedUserID).Distinct().OrderBy(x=>x).ToList();// new TransactionQueryBuilder(walletEntities).GetAllUserTopup();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "Insert CashBonus ALL: Step 6: lstUserID: " + lstUserID.Count(), walletEntities);
                    //Get Wallet_Interest
                    lstWalletInterest = new WalletInterestQueryBuilder(walletEntities).GetAllWalletInterest().ToList();
                    //Get List Tran Withdraw before Snapshot Date
                    lstTranWithdrawBeforeSnapshot = new TransactionQueryBuilder(walletEntities).GetListTranWithdrawBeforeSnapshotDate(maxDateSnapshot.CreateDateSnapshot);
                    transactionScope.Complete();
                }

                //var countUserID = lstUserID.Count();
                //var count = countUserID / 50;
                //var redunt = (countUserID % 50);
                //var i = 1;
                foreach (var userID in lstUserID)
                {                  
                    InsertInterest_ByUserID(pSource, userID, lstRewardAcc, lstAllTrans, lstTranWithdrawBeforeSnapshot, lstAllSubTrans, lstWalletInterest, lstWalletSnapshot, lstAccounts, ref insertTranList, ref lstWalletSnapshotNew);
                    //Insert Block Tran/SubTran/Snapshot, Update Block Amount
                    if (insertTranList.Count > 100 || ((lstUserID.IndexOf(userID) == lstUserID.Count - 1) && insertTranList.Count > 0)) // Run in block of 100 and last block
                    {
                        //set Snapshot with fixed CreateDate = Latest SubTransaction CreateDate to make sure not miss any new transactions during entire process for next(month) run
                        lstWalletSnapshotNew.ForEach(s => s.CreateDateSnapshot = newSnapshotDate);
                        InsertInterestBlockTransaction(insertTranList, lstWalletSnapshotNew, ref result);
                        insertTranList = new Dictionary<string, Transaction>();
                        lstWalletSnapshotNew = new List<Wallet_Snapshot>();
                        logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "Insert CashBonus ALL: Step 7: UserID: " + userID);
                    }

                    //if (index / 50 == 1 && i <= count)
                    //    {
                    //        InsertSnapshotForCashBonus(lstWalletSnapshotNew, newSnapshotDate);
                    //        lstWalletSnapshotNew = new List<Wallet_Snapshot>();
                    //        index = 0;
                    //        i++;
                    //    }
                    //if (index == redunt && i > count)
                    //{
                    //    InsertSnapshotForCashBonus(lstWalletSnapshotNew, newSnapshotDate);
                    //}
                    //index++;
                }
                UpdateSnapshotForCashBonus();
                result.Status = 1;
                return result;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertInterest_ForAll";
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
        }

        public WalletAccountResponse InsertCashBonus_By_UserID(string pSource, string pUserID, string pCurrency)
        {
            var result = new WalletAccountResponse();   
            var interestResponse = new InterestResponse();
            var logWallet = new LogWallet();

            var lstUserID = new List<String>();
            var lstAllTrans = new List<Transaction>();
            var lstTranWithdrawBeforeSnapshot = new List<Transaction>();
            var lstAllSubTrans = new List<SubTransaction>();
            var lstRewardAcc = new List<Wallet_Account_Reward>();
            var lstWalletInterest = new List<Wallet_Interest>();
            var lstWalletSnapshot = new List<Wallet_Snapshot>();
            var lstWalletSnapshotNew = new List<Wallet_Snapshot>();
            var lstAccounts = new List<Wallet_Account>();
            var insertTranList = new Dictionary<string, Transaction>();
            DateTime newSnapshotDate = DateTime.Now;
            try
            {
                var walletEntities = new WalletEntities();

                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    //Get Wallet_Account
                    lstAccounts = new WalletAccountQueryBuilder(walletEntities).GetWalletByUserID(pUserID).ToList();
                    //Get Reward
                    lstRewardAcc = new WalletRewardQueryBuilder(walletEntities).GetRewardWithAccsAmount(lstAccounts.Select(a => a.ID).Distinct().ToList()).ToList();
                    string AccID = lstAccounts.Where(p => p.Currency_Code == pCurrency).FirstOrDefault().ID;
                    //Get Last Snapshot
                    Wallet_Snapshot maxDateSnapshot = new WalletSnapshotQueryBuilder(walletEntities).GetMaxDateSnapshotByAccID(AccID);
                    lstWalletSnapshot = new WalletSnapshotQueryBuilder(walletEntities).GetSnapshotByCheckSum(maxDateSnapshot.Checksum).HasAccIDs(lstAccounts.Select(a => a.ID).Distinct().ToList()).ToList();
                    //Get Transaction
                    lstAllTrans = new TransactionQueryBuilder(walletEntities).GetAllTransByUserID(pUserID).FromDate(maxDateSnapshot.CreateDateSnapshot).ToList();
                    newSnapshotDate = DateTime.Now; //Get date for new snapshot right after having transactions
                    //Get SubTransaction
                    lstAllSubTrans = new SubTransactionQueryBuilder(walletEntities).ExcludeReward(EwalletConstant.Rewardstring).GetAllSubTranByUserID(pUserID).FromDate(maxDateSnapshot.CreateDateSnapshot).ToList();
                    //Get Wallet_Interest
                    lstWalletInterest = new WalletInterestQueryBuilder(walletEntities).GetAllWalletInterest().ToList();
                    lstTranWithdrawBeforeSnapshot = new TransactionQueryBuilder(walletEntities).GetListTranWithdrawBeforeSnapshotDate(maxDateSnapshot.CreateDateSnapshot);
                }

                InsertInterest_ByUserID(pSource, pUserID, lstRewardAcc, lstAllTrans, lstTranWithdrawBeforeSnapshot, lstAllSubTrans, lstWalletInterest, lstWalletSnapshot, lstAccounts, ref insertTranList, ref lstWalletSnapshotNew);
                lstWalletSnapshotNew.ForEach(s => s.CreateDateSnapshot = newSnapshotDate);
                InsertInterestBlockTransaction (insertTranList, lstWalletSnapshotNew, ref interestResponse);
                //InsertSnapshotForCashBonus(lstWalletSnapshotNew, newSnapshotDate);
                UpdateSnapshotForCashBonus();
                return result;
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertInterest_ForAllByUserID";
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
                return result;
            }
        }
      
        public WalletAccountResponse InsertInterest_ByUserID(string pSource, string pUserID, List<Wallet_Account_Reward> lstRewardAcc, List<Transaction> lstAllTrans, List<Transaction> lstTranWithdrawBeforeSnapshot, List<SubTransaction> lstAllSubTrans, List<Wallet_Interest>  lstWalletInterest, List<Wallet_Snapshot> lstWalletSnapshot, List<Wallet_Account> lstAccounts, ref Dictionary<string, Transaction> insertTranList, ref List<Wallet_Snapshot> lstWalletSnapshotNew)
        {
            var walletAPI = new WalletApiLogic();
            var logWallet = new LogWallet();
            var walletTranLogic = new WalletTransactionLogic();
            var lstWallet = new List<Wallet_Account>();
            var result = new WalletAccountResponse();
            var AccID = "";
          
            try
            {
                var lstAllWalletSnapshot = new List<Wallet_Snapshot>();
                
                var newWalletEntities = new WalletEntities();  
                var curDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                var startDateCount = curDateTime;
                result.lstWalletAccount = new List<WalletTransferObject>();
                decimal totalAmountPreviousMonth = 0;               

                //var WalletAcc = GetWalletAccountWithRewardMinusPendingWithdrawByUserID(pUserID);
               
                var WalletAcc = GetWalletAccountByUserID(pUserID, lstAccounts);
                if (WalletAcc.lstWalletAccount.Count() == 0)
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, WalletAcc.Message);
                    result.Status = 0;
                    result.Code = WalletAcc.Code;
                    result.Message = WalletAcc.Message;
                    return result;
                }
              
                var CutoffDateInterest = new DateTime(int.Parse(EwalletConstant.CutoffDateInterest.Substring(0, 4)), int.Parse(EwalletConstant.CutoffDateInterest.Substring(4, 2)), int.Parse(EwalletConstant.CutoffDateInterest.Substring(6, 2)));
                foreach (var acc in WalletAcc.lstWalletAccount)
                {
                    totalAmountPreviousMonth = 0;
                    var rate = lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_day;
                    acc.InterestRate = Convert.ToDecimal(lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_year);
                    var DateInterestSnapshot = lstWalletSnapshot.Where(p=> p.Account_ID == acc.ID);
                    startDateCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    if (DateInterestSnapshot != null)
                    {
                        if (DateInterestSnapshot.Count() > 0)
                        {
                            startDateCount = DateInterestSnapshot.FirstOrDefault().CreateDateSnapshot;
                            totalAmountPreviousMonth = DateInterestSnapshot.FirstOrDefault().Balance ?? 0;
                        }
                        else
                        {
                            //var subTran = new SubTransactionQueryBuilder(newWalletEntities).GetSubTranForFirstTopupByCurrency(pUserID, acc.Currency_Code).FirstOrDefault();
                            var subTran = lstAllSubTrans.Where(p => p.Currency_Code == acc.Currency_Code && p.User_ID == pUserID && !EwalletConstant.Rewardstring.Any(x => p.Remarks.ToLower().Contains(x))).OrderBy(p => p.Sequent_ID).FirstOrDefault();
                            if (subTran != null)
                            {
                                startDateCount = subTran.CreateDate;
                                totalAmountPreviousMonth = subTran.Amount ?? 0;
                            }
                        }
                    }

                    //var lstSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetSubTransByUserIDnMonth(pUserID, acc.Currency_Code, startDateCount, curDateTime.AddDays(-1)).ToList();
                    //var lstTrans = new TransactionQueryBuilder(newWalletEntities).GetTranWithdrawByUserIDNoVerifynDate(pUserID, startDateCount, curDateTime.AddDays(-1), acc.Currency_Code).ToList();
                    var lstSubTrans = lstAllSubTrans.Where(p => p.Currency_Code == acc.Currency_Code && p.CreateDate > startDateCount && p.User_ID == pUserID && p.CreateDate <= curDateTime.AddDays(-1) && !EwalletConstant.Rewardstring.Any(x => p.Remarks.ToLower().Contains(x))).ToList();
                    lstSubTrans.RemoveAll(p => p.Direction == "OUT" && p.CreateUser.ToLower() != "townbus" && p.CreateUser != ""); //Remove Tran Withaw submitted from finance

                    var lstTrans = lstAllTrans.Where(p => p.Description == "WITHDRAW" && p.Source_Currency == acc.Currency_Code && p.User_ID == pUserID && p.Status != "canceled" && p.CreateDate >= startDateCount && p.CreateDate <= curDateTime.AddDays(-1)).ToList();
                    
                    double TotalamtInterest = 0;
                    double amtInterest = 0;
                    foreach (var tranWithdraw in lstTrans)
                    {
                        var subTran = new SubTransaction();
                        subTran.CreateDate = tranWithdraw.CreateDate ?? DateTime.Now;
                        subTran.Amount = tranWithdraw.Source_Amount;
                        subTran.Direction = "OUT";
                        lstSubTrans.Add(subTran);
                    }

                    var TranWithdrawBeforeSnapshot = lstTranWithdrawBeforeSnapshot.Where(p => p.Source_Currency == acc.Currency_Code && p.User_ID == pUserID).ToList();
                    totalAmountPreviousMonth = totalAmountPreviousMonth - TranWithdrawBeforeSnapshot.Sum(p => p.Source_Amount ?? 0);
                   

                    //lstSubTrans.Add(new SubTransaction() { Currency_Code = "SGD", Account_ID = "feb6af5750854d428852c3ef7cb459", User_ID = "c866924a-0055-461c-a9c6-f2384494e340", Amount = 161, Direction = "IN", CreateDate = new DateTime(2019, 11, 23, 8, 39, 09) });
                    foreach (var subTran in lstSubTrans)
                    {
                        var dateTran = subTran.CreateDate;
                        int remaindate = 0;
                        if (dateTran < CutoffDateInterest)
                        {
                            var remaindate1 = Convert.ToInt32((CutoffDateInterest - dateTran).TotalDays) - 1;
                            var amtInterest1 = CalCashBonus(subTran.Amount ?? 0, EwalletConstant.OldDailyRateInterest, remaindate1, subTran.Direction);
                            var remaindate2 = Convert.ToInt32((curDateTime - CutoffDateInterest).TotalDays);
                            var amtInterest2 = CalCashBonus(subTran.Amount ?? 0, rate, remaindate2, subTran.Direction);
                            amtInterest = amtInterest1 + amtInterest2;
                        }
                        else if (dateTran >= CutoffDateInterest)
                        {
                            remaindate = Convert.ToInt32((curDateTime - dateTran).TotalDays);
                            amtInterest = CalCashBonus(subTran.Amount ?? 0, rate, remaindate, subTran.Direction);
                        }

                        TotalamtInterest += amtInterest;
                    }
                    int totalDateInMonth = 0;
                    double interestPriciple = 0;
                    if (startDateCount < CutoffDateInterest)
                    {
                        var remaindate1 = Convert.ToInt32((CutoffDateInterest - startDateCount).TotalDays) - 1;
                        var interestPriciple1 = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(EwalletConstant.OldDailyRateInterest) / 100, remaindate1));
                        interestPriciple1 = +(interestPriciple1 - Convert.ToDouble(totalAmountPreviousMonth));

                        var remaindate2 = Convert.ToInt32((curDateTime - CutoffDateInterest).TotalDays);
                        var interestPriciple2 = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(rate) / 100, remaindate2));
                        interestPriciple2 = +(interestPriciple2 - Convert.ToDouble(totalAmountPreviousMonth));
                        interestPriciple = interestPriciple1 + interestPriciple2;
                    }
                    else if (startDateCount >= CutoffDateInterest)
                    {
                        totalDateInMonth = Convert.ToInt32((curDateTime - startDateCount).TotalDays);
                        interestPriciple = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(rate) / 100, totalDateInMonth));
                        interestPriciple = +(interestPriciple - Convert.ToDouble(totalAmountPreviousMonth));
                    }
                    TotalamtInterest += interestPriciple;
                    acc.InterestEarned = Convert.ToDecimal(string.Format("{0:0.0000}", TotalamtInterest));

                    if (TotalamtInterest > 0)
                    {
                        var TempTran = new WalletInterestLogic().BuildTransactionInterest(acc.InterestEarned, acc.Currency_Code, acc.User_ID, acc.ID, pSource, acc.Wallet_ID, rate);
                        lstTrans.Add(TempTran);
                        insertTranList.Add(acc.ID,TempTran);                        
                    }
                    #region Insert Snapshot
                    if (TotalamtInterest > 0 || acc.Available_Balance > 0)
                    {
                        var walletSnapShot = new Wallet_Snapshot();
                        walletSnapShot.ID = Guid.NewGuid().ToString();
                        walletSnapShot.Account_ID = acc.ID;
                        walletSnapShot.Balance = acc.Available_Balance + acc.InterestEarned;
                        if (lstRewardAcc != null)
                        {
                            var rewardACC = lstRewardAcc.Find(p => p.ID == acc.ID);
                            if (rewardACC != null)
                                walletSnapShot.Reward_Amount = rewardACC.Reward_Amount;
                            else
                                walletSnapShot.Reward_Amount = 0;
                        }
                        else
                            walletSnapShot.Reward_Amount = 0;
                        walletSnapShot.CreateDate = acc.CreateDate ?? DateTime.Now;
                        walletSnapShot.UpdateDate = acc.UpdateDate ?? DateTime.Now;
                        //walletSnapShot.CreateDateSnapshot = DateTime.Now;
                        walletSnapShot.Currency_Code = acc.Currency_Code;
                        walletSnapShot.Checksum = new WalletSnapshotLogic().BuildCheckSum(walletSnapShot);
                        lstWalletSnapshotNew.Add(walletSnapShot);
                    }
                   
                    #endregion Insert Snapshot

                }
                
                result.lstWalletAccount = WalletAcc.lstWalletAccount;            
              
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to InsertInterest_ByDatenUserID";
                logWallet.Log(MethodBase.GetCurrentMethod(), AccID, ex, "");
                return result;
            }
            return result;
        }

        public void InsertInterestBlockTransaction(Dictionary<string, Transaction> insertTranList, List<Wallet_Snapshot> snapshot, ref InterestResponse result)
        {
            var logWallet = new LogWallet();
            using (var unitOfWork = new WalletTransactionUow(new WalletEntities()))
            {
                try
                {
                    unitOfWork.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
                    //Insert Trans first in block
                    unitOfWork.DoInsertMany(insertTranList.Values.ToList()).SaveAndContinue();
                    //Re-achieve list of Wallet_Account for latest data
                    var lstAccount = unitOfWork.GetWalletByIDs(insertTranList.Keys.ToList());
                    foreach (var t in insertTranList)
                    {
                        var tranResponse = InsertInterestTransaction(unitOfWork, t.Value, t.Key, lstAccount);
                        if (tranResponse.Status == 0)
                        {
                            logWallet.Log(MethodBase.GetCurrentMethod(), t.Key, null, "Inserted failed InsertInterestTransaction: " + t.Value + "AccID: " + t.Key);
                            result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                            result.Status = 0;
                            result.Message = "Inserted failed InsertInterestTransaction. Please check log file!";
                        }else
                            unitOfWork.DoInsert(snapshot.SingleOrDefault(s => s.Account_ID == t.Key));
                    }
                    unitOfWork.EndTransaction();
                }
                catch (Exception ex)
                {
                    unitOfWork.RollBack();
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Message = "Failed to insert interest transaction. Please check log file!";
                    result.Status = 0;
                    logWallet = new LogWallet();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Stop at block Account: " + string.Join(",", insertTranList.Select(t => t.Key), ex, ""), ex, "");
                    //return result;
                }
            }
        }

        public void InsertSnapshotForCashBonus(List<Wallet_Snapshot> lstWalletSnapshotNew, DateTime newSnapshotDate)
        {
            var logWallet = new LogWallet();
            var newWalletEntities = new WalletEntities();
            try
            {
                lstWalletSnapshotNew.ForEach(s => s.CreateDateSnapshot = newSnapshotDate);
                using (var WalletTransactionUnitOfWork1 = new WalletTransactionUow(newWalletEntities))
                {
                    WalletTransactionUnitOfWork1.BeginTransaction();
                    WalletTransactionUnitOfWork1.DoInsertMany(lstWalletSnapshotNew).EndTransaction();
                }
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), "Failed to InsertSnapshotForCashBonus", ex, "");
            }

        }
        public void UpdateSnapshotForCashBonus()
        {
            var logWallet = new LogWallet();
            try
            {
                var S1 = "";
                var S1Hash = "";
                var newWalletEntities = new WalletEntities();
                using (var WalletTransactionUnitOfWork1 = new WalletTransactionUow(newWalletEntities))
                {
                    WalletTransactionUnitOfWork1.BeginTransaction();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Start to UpdateSnapshotForCashBonus", null, "");
                    var lstAllWalletSnapshot = new WalletSnapshotQueryBuilder(newWalletEntities).GetAllSnapshotWithNearestDate().ToList();                    
                    foreach (Wallet_Snapshot walletSnapShot in lstAllWalletSnapshot)
                    {
                        S1 += walletSnapShot.ID + walletSnapShot.Account_ID + ConvertUtility.RoundToTwoDecimalPlaces(walletSnapShot.Balance) + ConvertUtility.RoundToTwoDecimalPlaces(walletSnapShot.Reward_Amount) + walletSnapShot.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + walletSnapShot.Currency_Code + walletSnapShot.Checksum;
                    }
                    S1Hash = SecurityLogic.GetSha1Hash(S1);
                    foreach (Wallet_Snapshot Wallet_Snapshot in lstAllWalletSnapshot)
                    {
                        Wallet_Snapshot.Snapshot = S1Hash;
                    }
                    WalletTransactionUnitOfWork1.DoUpdateMany(lstAllWalletSnapshot).EndTransaction();
                    logWallet.Log(MethodBase.GetCurrentMethod(), "Finished to UpdateSnapshotForCashBonus", null, "");
                }
            }
            catch (Exception ex)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), "Failed to UpdateSnapshotForCashBonus", ex, "");               
            }
        }
   

        public WalletAccountResponse GetWalletAccountWithInterestByUserID(string pUserID)
        {
            var walletAPI = new WalletApiLogic();
            var logWallet = new LogWallet();
            var walletTranLogic = new WalletTransactionLogic();
            var lstWallet = new List<Wallet_Account>();
            var lstAllTrans = new List<Transaction>();
            var lstAllSubTrans = new List<SubTransaction>();
            var result = new WalletAccountResponse();
            var lstTranWithdrawBeforeSnapshot = new List<Transaction>();

            try
            {
                var newWalletEntities = new WalletEntities();
                var curDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                var startDateCount = curDateTime;
                var WalletInterestQueryBuilder = new WalletInterestQueryBuilder(newWalletEntities);
                var lstWalletInterest = WalletInterestQueryBuilder.ToList();

                var transactionOptions = new System.Transactions.TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
                using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                {
                    lstAllTrans = new TransactionQueryBuilder(newWalletEntities).GetAllTransByUserID(pUserID).ToList();
                    lstAllSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetAllSubTranByUserID(pUserID).ToList();                    
                }
                
                var WalletAcc = GetWalletAccountWithRewardMinusPendingWithdrawByUserID_EnhanceV1(pUserID, lstAllTrans);

                result.lstWalletAccount = new List<WalletTransferObject>();
                decimal totalAmountPreviousMonth = 0;
                if (WalletAcc.lstWalletAccount.Count == 0)
                {
                    logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, WalletAcc.Message);
                    result.Status = 0;
                    result.Code = WalletAcc.Code;
                    result.Message = WalletAcc.Message;
                    return result;
                }
                var CutoffDateInterest = new DateTime(int.Parse(EwalletConstant.CutoffDateInterest.Substring(0, 4)), int.Parse(EwalletConstant.CutoffDateInterest.Substring(4, 2)), int.Parse(EwalletConstant.CutoffDateInterest.Substring(6, 2)));
                foreach (var acc in WalletAcc.lstWalletAccount)
                {
                    totalAmountPreviousMonth = 0;
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
                        else
                        {
                            //var subTran = new SubTransactionQueryBuilder(newWalletEntities).GetSubTranForFirstTopupByCurrency(pUserID, acc.Currency_Code).FirstOrDefault();
                            var subTran = lstAllSubTrans.Where(p => p.Currency_Code == acc.Currency_Code && !EwalletConstant.Rewardstring.Any(x => p.Remarks.ToLower().Contains(x))).OrderBy(p => p.Sequent_ID).FirstOrDefault();                            
                            if (subTran != null)
                            {
                                startDateCount = subTran.CreateDate;
                                totalAmountPreviousMonth = subTran.Amount ?? 0;                               
                            }
                        }
                    }
                    //startDateCount = new DateTime(2019, 6, 15, 15, 48, 00);
                    //totalAmountPreviousMonth = 1200M;

                    var lstSubTrans = lstAllSubTrans.Where(p => p.Currency_Code == acc.Currency_Code && p.CreateDate > startDateCount && p.CreateDate <= curDateTime.AddDays(-1) && !EwalletConstant.Rewardstring.Any(x => p.Remarks.ToLower().Contains(x))).ToList();
                    //Remove Withdraw from subTran
                    lstSubTrans.RemoveAll(p => p.Direction == "OUT" && p.CreateUser.ToLower() != "townbus" && p.CreateUser != "");
                    var lstTrans = lstAllTrans.Where(p => p.Description == "WITHDRAW" && p.Source_Currency == acc.Currency_Code && p.User_ID == pUserID && p.Status != "canceled" && p.CreateDate >= startDateCount && p.CreateDate <= curDateTime.AddDays(-1));

                    //lstSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetSubTransByUserIDnMonth(pUserID, acc.Currency_Code, startDateCount, curDateTime.AddDays(-1)).ToList();
                    //lstTrans = new TransactionQueryBuilder(newWalletEntities).GetTranWithdrawByUserIDNoVerifynDate(pUserID, startDateCount, curDateTime.AddDays(-1), acc.Currency_Code).ToList();
                    //lstSubTrans = new List<SubTransaction>();
                    //lstSubTrans.Add(new SubTransaction() { Amount = 82.00M, CreateDate = new DateTime(2019, 6, 20, 7,35,17), Direction = "OUT" });
                    //lstSubTrans.Add(new SubTransaction() { Amount = 82M, CreateDate = new DateTime(2019, 7, 14, 12,6,33), Direction = "OUT" });                    
                    //lstSubTrans.Add(new SubTransaction() { Amount = 82.00M, CreateDate = new DateTime(2019, 8, 18,22,19,04), Direction = "OUT" });
                    //lstSubTrans.Add(new SubTransaction() { Amount = 500M, CreateDate = new DateTime(2019, 8, 30, 21, 13, 06), Direction = "OUT" });
                    //lstSubTrans.Add(new SubTransaction() { Amount = 10.00M, CreateDate = new DateTime(2019, 9, 08,10,54,11), Direction = "IN" });
                    double TotalamtInterest = 0;
                    double amtInterest = 0;
                    foreach (var tranWithdraw in lstTrans)
                    {
                        var subTran = new SubTransaction();
                        subTran.CreateDate = tranWithdraw.CreateDate??DateTime.Now;
                        subTran.Amount = tranWithdraw.Source_Amount;
                        subTran.Direction = "OUT";
                        lstSubTrans.Add(subTran);
                    }

                    var TranWithdrawBeforeSnapshot = lstAllTrans.Join(lstAllSubTrans,
                               t => new { TranID = t.Tran_ID }, a => new { TranID = a.Tran_ID }, (t, a) => new { Transaction = t, SubTransaction = a })
                         .Where(ta => ta.Transaction.Description == "WITHDRAW" && ta.Transaction.Source_Currency == acc.Currency_Code && ta.Transaction.Status != "canceled" && ta.Transaction.CreateDate < startDateCount && ta.SubTransaction.CreateDate >= startDateCount).Select(ta => ta.Transaction).ToList();
                    totalAmountPreviousMonth = totalAmountPreviousMonth - TranWithdrawBeforeSnapshot.Sum(p => p.Source_Amount??0);                  

                    foreach (var subTran in lstSubTrans)
                    {
                        var dateTran = subTran.CreateDate;
                        int remaindate = 0;
                        if (dateTran < CutoffDateInterest)
                        {
                            var remaindate1 = Convert.ToInt32((CutoffDateInterest - dateTran).TotalDays) - 1;
                            var amtInterest1 = CalCashBonus(subTran.Amount ?? 0, EwalletConstant.OldDailyRateInterest, remaindate1, subTran.Direction);
                            var remaindate2 = Convert.ToInt32((curDateTime - CutoffDateInterest).TotalDays);
                            var amtInterest2 = CalCashBonus(subTran.Amount ?? 0, rate, remaindate2, subTran.Direction);
                            amtInterest = amtInterest1 + amtInterest2;
                        }
                        else if (dateTran >= CutoffDateInterest)
                        {
                            remaindate = Convert.ToInt32((curDateTime - dateTran).TotalDays);
                            amtInterest = CalCashBonus(subTran.Amount ?? 0, rate, remaindate, subTran.Direction);
                        }                     

                        TotalamtInterest += amtInterest;
                    }
                    int totalDateInMonth = 0;
                    double interestPriciple = 0;
                    if (startDateCount < CutoffDateInterest)
                    {
                        var remaindate1 = Convert.ToInt32((CutoffDateInterest - startDateCount).TotalDays) - 1;
                        var interestPriciple1 = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(EwalletConstant.OldDailyRateInterest) / 100, remaindate1));
                        interestPriciple1 = +(interestPriciple1 - Convert.ToDouble(totalAmountPreviousMonth));

                        var remaindate2 = Convert.ToInt32((curDateTime - CutoffDateInterest).TotalDays);
                        var interestPriciple2 = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(rate) / 100, remaindate2));
                        interestPriciple2 = +(interestPriciple2 - Convert.ToDouble(totalAmountPreviousMonth));
                        interestPriciple = interestPriciple1 + interestPriciple2;
                    }
                    else if (startDateCount >= CutoffDateInterest)
                    {
                        totalDateInMonth = Convert.ToInt32((curDateTime - startDateCount).TotalDays);
                        interestPriciple = (Convert.ToDouble(totalAmountPreviousMonth) * Math.Pow(1 + double.Parse(rate) / 100, totalDateInMonth));
                        interestPriciple = +(interestPriciple - Convert.ToDouble(totalAmountPreviousMonth));
                    }

                    TotalamtInterest += interestPriciple;
                    TotalamtInterest = TotalamtInterest < 0 ? 0 : TotalamtInterest;
                    acc.InterestEarned = Convert.ToDecimal(string.Format("{0:0.0000}", TotalamtInterest));

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
            result.Remarks = "Main Cash: Current cash balance + cash bonus earned till previous month. \n Cash Bonus: Rate per annum. \n Reward Cash: Given by marketing activities, non-withdrawable, not entitled for cash bonus. \n Reward Cash usage as follows: 8% of bus, ferry & car rental price; 4% of train & flight ticket price.";
            return result;
        }     

        public List<SubTransaction> SearchingWalletAccountWithInterestByUserIDnDateRange(string pUserID, DateTime pDateFrom, DateTime pDateTo, string pCurrencyCode, ref Dictionary<DateTime, decimal> lstTotalBalance, List<SubTransaction> lstAllSubTrans, List<Transaction> lstAllTrans)
        {
            var lstSubTran = new List<SubTransaction>();
            var logWallet = new LogWallet();
            var walletLogic = new WalletAccountLogic();
            var WalletAcc = walletLogic.GetWalletAccountByUserID(pUserID);
            
            var newWalletEntities = new WalletEntities();
            if (WalletAcc.Count == 0)
            {
                logWallet.Log(MethodBase.GetCurrentMethod(), pUserID, null, "There is no wallet Account found.");
                return lstSubTran;
            }
            //var tempWalletAcc = walletLogic.GetWalletAccByUserIDnCurrencyCode(pUserID, pCurrencyCode);
            var tempWalletAcc = WalletAcc.Where(cart => cart.Currency_Code.ToLower().Equals(pCurrencyCode.Trim().ToLower())).FirstOrDefault();
            var DateInterestSnapshot = new WalletSnapshotQueryBuilder(newWalletEntities).HasLatestAccID(tempWalletAcc.ID);
            var startDateCount = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            decimal totalAmountPreviousMonth = 0;
            if (DateInterestSnapshot != null)
            {
                if (DateInterestSnapshot.Count() > 0)
                {
                    startDateCount = DateInterestSnapshot.FirstOrDefault().CreateDateSnapshot;
                    totalAmountPreviousMonth = DateInterestSnapshot.FirstOrDefault().Balance ?? 0;
                }
                else
                {
                    ////var subTran = new SubTransactionQueryBuilder(newWalletEntities).GetSubTranForFirstTopupByCurrency(pUserID, tempWalletAcc.Currency_Code).FirstOrDefault();
                    var subTran = lstAllSubTrans.Where(p => p.Currency_Code == tempWalletAcc.Currency_Code).OrderBy(p => p.Sequent_ID).FirstOrDefault();
                    if (subTran != null)
                    {
                        startDateCount = subTran.CreateDate;
                        totalAmountPreviousMonth = subTran.Amount ?? 0;
                    }
                }
            }

            var diffDate = Convert.ToInt32((pDateTo - startDateCount).TotalDays) + 1;
            var date = new DateTime(startDateCount.Year, startDateCount.Month, startDateCount.Day, 23, 59, 59);

            //var lstAllSubTrans = new SubTransactionQueryBuilder(newWalletEntities).GetSubTransByUserIDnCurrencyCode(pUserID, pCurrencyCode).ToList();
            //var lstAllTrans = new TransactionQueryBuilder(newWalletEntities).GetTranWithdrawByUserIDNoVerify(pUserID, pCurrencyCode).ToList();
            //var lstAllSubTrans = lstAllSubTrans.Where(p => p.Currency_Code == acc.Currency_Code && p.CreateDate > startDateCount && p.CreateDate <= curDateTime.AddDays(-1) && !EwalletConstant.Rewardstring.Any(x => p.Remarks.ToLower().Contains(x))).ToList();
            lstAllSubTrans = lstAllSubTrans.Where(p => p.User_ID == pUserID && p.Currency_Code == pCurrencyCode && !EwalletConstant.Rewardstring.Any(x => p.Remarks.ToLower().Contains(x))).ToList();
            lstAllSubTrans.RemoveAll(p => p.Direction == "OUT" && p.CreateUser.ToLower() != "townbus" && p.CreateUser != "");

            lstAllTrans = lstAllTrans.Where(p => p.Description == "WITHDRAW" && p.Source_Currency == pCurrencyCode &&  p.Status != "canceled").ToList();
           
            decimal AccumulateDailyInterest = 0;
            var WalletInterestQueryBuilder = new WalletInterestQueryBuilder(newWalletEntities);
            var lstWalletInterest = WalletInterestQueryBuilder.ToList();
            for (int i = 1; i <= diffDate; i++)
            {
                //var date = new DateTime(startDateCount.Year, startDateCount.Month, startDateCount.Day, 23, 59, 59).AddDays(i);
                var SublstSubTran = SearchingWalletAccountWithDailyInterestByUserIDnDate(WalletAcc, date, pCurrencyCode, lstWalletInterest, lstAllSubTrans, lstAllTrans, ref lstTotalBalance, ref AccumulateDailyInterest);
                date = date.AddDays(1);
                if (SublstSubTran.Verified != null)
                lstSubTran.Add(SublstSubTran);
            }
            lstSubTran = lstSubTran.Where(p => p.CreateDate >= pDateFrom && p.CreateDate <= pDateTo).ToList();
            lstTotalBalance = lstTotalBalance.Where(p => p.Key >= pDateFrom && p.Key <= pDateTo).ToDictionary(x=>x.Key, t=>t.Value);
            return lstSubTran;
        }

        public SubTransaction SearchingWalletAccountWithDailyInterestByUserIDnDate(List<Wallet_Account> WalletAcc, DateTime pDateCount, string pCurrencyCode, List<Wallet_Interest> lstWalletInterest, List<SubTransaction> lstAllSubTrans, List<Transaction> lstAllTrans, ref Dictionary<DateTime, decimal> lstTotalBalance, ref decimal AccumulateDailyInterest)
        {
            var walletAPI = new WalletApiLogic();
            var logWallet = new LogWallet();
            var walletTranLogic = new WalletTransactionLogic();
            var lstWallet = new List<Wallet_Account>();
            var SubTran = new SubTransaction();
            var result = new WalletAccountResponse();
            try
            {
                result.lstWalletAccount = new List<WalletTransferObject>();           
                var newWalletEntities = new WalletEntities();
                var curDateTime = pDateCount;
          
                decimal totalPreDailyAmount = 0;
                foreach (var acc in WalletAcc)
                {
                    if (pCurrencyCode == acc.Currency_Code)
                    {                      
                        var subTranCB = new SubTransaction();
                        var rate = lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_day;
                        var InterestRate = Convert.ToDecimal(lstWalletInterest.Find(p => p.Country_Code == acc.Currency_Code).Interest_year);
                        var PreDailyAmount = GetSumDailyByUserIDnCurrencynDate(pDateCount.AddDays(-1), lstAllSubTrans, lstAllTrans);
                        var CurDailyAmount = GetSumDailyByUserIDnCurrencynDate(pDateCount, lstAllSubTrans, lstAllTrans);
                        totalPreDailyAmount = AccumulateDailyInterest + PreDailyAmount;
                        double amtInterest = 0;
                        var CutoffDateInterest = new DateTime(int.Parse(EwalletConstant.CutoffDateInterest.Substring(0, 4)), int.Parse(EwalletConstant.CutoffDateInterest.Substring(4, 2)), int.Parse(EwalletConstant.CutoffDateInterest.Substring(6, 2)));
                                             
                        if (curDateTime <= CutoffDateInterest)
                        {
                            amtInterest = CalCashBonus(totalPreDailyAmount, EwalletConstant.OldDailyRateInterest, 1, "IN");
                        }
                        else if (curDateTime > CutoffDateInterest)
                        {
                            amtInterest = CalCashBonus(totalPreDailyAmount, rate, 1, "IN");
                        }
                      
                        var PreInterestEarned = Convert.ToDecimal(string.Format("{0:0.0000}", amtInterest));           
                        if (PreInterestEarned > 0)
                        {
                            subTranCB.Amount = PreInterestEarned;
                            subTranCB.Currency_Code = acc.Currency_Code;
                            subTranCB.Account_ID = acc.ID;
                            subTranCB.Direction = "IN";
                            subTranCB.CreateDate = pDateCount;
                            subTranCB.CreateUser = "EBW";
                            if (curDateTime <= CutoffDateInterest)
                            {
                                subTranCB.Remarks = "Cash Bonus Earned with " + EwalletConstant.OldYearRateInterest + "%";
                            }
                            else if (curDateTime > CutoffDateInterest)
                            {
                                subTranCB.Remarks = "Cash Bonus Earned with " + InterestRate + "%";
                            }
                            AccumulateDailyInterest = AccumulateDailyInterest + PreInterestEarned;
                            var SumDailyAmount = Convert.ToDecimal(string.Format("{0:0.0000}", CurDailyAmount + AccumulateDailyInterest));
                            lstTotalBalance.Add(pDateCount,SumDailyAmount);
                            subTranCB.Verified = true;
                            
                            return subTranCB;
                        }                       
                    }
                }
            }

            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query Wallet ID";
                logWallet.Log(MethodBase.GetCurrentMethod(), "Wallet ID: " + WalletAcc[0].User_ID, ex, "");
                return SubTran;
            }
            result.Code = ApiReturnCodeConstant.SUCCESS.Code;
            result.Message = ApiReturnCodeConstant.SUCCESS.Message;
            result.Status = ApiReturnCodeConstant.SUCCESS.Code;
            return SubTran;
        }

        public decimal GetSumDailyByUserIDnCurrencynDate(System.DateTime pDateTime, List<SubTransaction> lstSubTrans, List<Transaction> lstTrans)
        {
            decimal totalMoneyIN = lstSubTrans.AsEnumerable().Where(p => p.CreateDate < pDateTime && p.Direction == "IN").Sum(p => p.Amount) ?? 0;
            decimal totalMoneyOUT = lstSubTrans.AsEnumerable().Where(p => p.CreateDate < pDateTime && p.Direction == "OUT").Sum(p => p.Amount) ?? 0;
            decimal totalMoneyOUTforWithDrawPending = lstTrans.AsEnumerable().Where(p => p.CreateDate < pDateTime).Sum(p => p.Source_Amount) ?? 0;
            decimal totalMoney = totalMoneyIN - totalMoneyOUT - totalMoneyOUTforWithDrawPending;
            return totalMoney;
        }

        public double CalCashBonus(decimal Amount, string rate, int remaindate, string Direction)
        {
            double amtInterest;
            if (remaindate == 0)
            {
                amtInterest = 0;
            }
            else if (Direction == "IN")
            {
                amtInterest = (Convert.ToDouble(Amount) * Math.Pow(1 + double.Parse(rate) / 100, remaindate));
                amtInterest = +(amtInterest - Convert.ToDouble(Amount));
            }
            else
            {
                amtInterest = (Convert.ToDouble(Amount) * Math.Pow(1 + double.Parse(rate) / 100, remaindate));
                amtInterest = -(amtInterest - Convert.ToDouble(Amount));
            }
            return amtInterest;
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
            bool IgnoreError = Convert.ToBoolean(ConfigurationManager.AppSettings["IgnoreError"] ?? "False");
            var result = new ListTransactionResponse();
            var walletAccLogic = new WalletAccountLogic();
            var transferWithdraws = new List<TransactionTransferObject>();
            string message1 = string.Empty, message2 = string.Empty;
            bool isSuccess = false;
            try
            {
                List<SubTransaction> subtrans = new List<SubTransaction>();
                List<Wallet_Snapshot> snapshot = null;

                //Verify Snapshot
                bool IsSnapshotVerified = new WalletSnapshotLogic().VerifySnapshot(snapshotChecksum, out snapshot);
                //Verify Transactions after snapshot
                bool IsTransactionVerified = IgnoreError ? true : new WalletTransactionLogic(true).VerifyTransactionsPreWithdraw(lastSubTransactionId, lastSubTransactionChecksum, snapshot?[0].CreateDateSnapshot ?? null, out subtrans, out message1);
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

        public CancelWithdrawResponse CancelWithdraw(string tranId, string username)
        {
            var result = new CancelWithdrawResponse();
            try
            {
                if (new WalletTransactionLogic().CancelWithdraw(tranId, username))
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

        public PendWithdrawResponse PendWithdraw(string tranId, string username)
        {
            var result = new PendWithdrawResponse();
            try
            {
                if (new WalletTransactionLogic().PendWithdraw(tranId, username))
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

        public RefundTransactionResponse RefundTransaction(string tranId, string refundAmount, string user, string username)
        {
            var result = new RefundTransactionResponse() { Code = 0, Status = 0 };
            bool isSuccess = false;
            try
            {
                var tran = new TransactionQueryBuilder(new WalletEntities()).GetTranByTranID(tranId).FirstOrDefault();
                var subtran = new SubTransactionQueryBuilder(new WalletEntities()).GetSubTransactionByTranID(tranId).ToList();
                if (tran != null && tran.Description == EwalletConstant.PAYMENT.ToString() && tran.Status != "refunded")
                {

                    if (!string.IsNullOrEmpty(refundAmount) && Convert.ToDecimal(refundAmount) > tran.Source_Amount)
                        isSuccess = false;
                    else if (string.IsNullOrEmpty(refundAmount) || (!string.IsNullOrEmpty(refundAmount) && Convert.ToDecimal(refundAmount) <= tran.Source_Amount))
                    {
                        decimal originRfAmount = (!string.IsNullOrEmpty(refundAmount) && Convert.ToDecimal(refundAmount) < tran.Source_Amount) ? Convert.ToDecimal(refundAmount) : tran.Source_Amount.Value;
                        decimal rfAmount = originRfAmount;
                        //Refund/Insert Tran/Subtran for refund for main account
                        var mainSubTran = subtran.SingleOrDefault(s => s.Remarks == tran.Remarks || (s.Remarks.StartsWith(tran.Remarks) && s.Remarks.Contains("WALLET")));
                        if (mainSubTran != null)
                        {
                            decimal mainRfAmount = rfAmount > mainSubTran.Amount ? mainSubTran.Amount.Value : rfAmount;
                            Transaction refundTran = BuildTransactionRefund(tran, user, mainRfAmount);
                            if (new WalletTransactionLogic().InsertTempTransaction(refundTran))
                            {
                                //new LogWallet().Log(MethodBase.GetCurrentMethod(),"" ,null, $"tranID:{tranId}, tranIDNew:{refundTran.Tran_ID}, refundAmount:{refundAmount}");
                                var subtranRes = InsertTopupTransaction(refundTran.Tran_ID);
                                rfAmount -= subtranRes.Code == ApiReturnCodeConstant.SUCCESS.Code ? mainRfAmount : 0;
                            }
                        }
                        //Refund/Insert Tran/Subtran for refund of reward
                        if (rfAmount > 0)
                        {
                            var rewardSubTran = subtran.SingleOrDefault(s => s.Remarks.Split('|').Count() == 2 && s.Remarks.Contains(tran.Remarks) && s.Remarks.Contains("REWARD"));
                            decimal rewardRfAmount = rfAmount > rewardSubTran.Amount ? rewardSubTran.Amount.Value : rfAmount;
                            TranResponse rewardResponse = rewardResponse = InsertRewardTransaction(rewardRfAmount, rewardSubTran.Account_ID, $"Refund {tran.Remarks}", true);
                            rfAmount -= rewardResponse.Code == ApiReturnCodeConstant.SUCCESS.Code ? rewardRfAmount : 0;
                        }
                        if (rfAmount < originRfAmount)
                        {
                            //Update: 
                            tran.Status = "refunded";
                            tran.UpdateDate = DateTime.Now;
                            tran.UpdateUser = username;
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
            ntran.CreateDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second);
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

        public void UpdateCheckSumSubTransaction(string DateTime, string LastCheckSum1)
        {
            var walletLogic = new WalletTransactionLogic();
            walletLogic.UpdateCheckSumSubTransaction(DateTime, LastCheckSum1);
        }

        public ViralTransactionResponse GetViralTransactionbyTranID(string tranId)
        {
            var result = new ViralTransactionResponse();
            var tran = new Transaction();
            var walletAccLogic = new WalletAccountLogic();
            try
            {
                var tranResponse = walletAccLogic.GetWalletTranByTranID(tranId);
                if (tranResponse == null)
                {
                    result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                    result.Status = 0;
                    result.Message = "Failed to query Transaction Wallet. Please check log file";
                }
                using (var TransactionBuilder = new TransactionQueryBuilder(new WalletEntities()))
                {                  
                    result.TotalTopUpAmount = TransactionBuilder.GetTotalTopupByUserIDnCurrency(tranResponse.User_ID, tranResponse.Source_Currency);
                    result.FirstTimeTopUp = TransactionBuilder.GetNumberofTimeTopupByUserID(tranResponse.User_ID) > 1 ? false : true;
                    result.CurrencyCode = tranResponse.Source_Currency;
                    result.TopupAmount = tranResponse.Source_Amount??0;
                    result.UserID = tranResponse.User_ID;
                }
            }
            catch (Exception ex)
            {
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Status = 0;
                result.Message = "Failed to query GetViralTransactionbyTranID";
                var logWallet = new LogWallet();
                logWallet.Log(MethodBase.GetCurrentMethod(), "", ex, "");
            }
            return result;
        }

        public lstUserBankAccountResponse LoadVerificationStatus(string userID, string currency)
        {
            var userBankLogic = new UserBankAccountLogic();
            var useBankRespone = new lstUserBankAccountResponse();
            useBankRespone.lstUserBankAcc = userBankLogic.LoadVerificationStatus(userID, currency);
            return useBankRespone;
        }

        public UserBankAccountResponse SubmitVerificationStatus(UserBankAccountRequest userBank)
        {
            var result = new UserBankAccountResponse();
            var userBankLogic = new UserBankAccountLogic();
            if (userBank.Verify == "New")
            {
                result.Status = userBankLogic.InsertVerificationStatus(userBank) ? 1 : 0;
                if (result.Status == 1)
                    result.Message = "Success";
                else
                {
                    result.Message = "Failed to insert User Bank Account";
                    result.Status = -1;
                    result.Code = -1;
                }
            }
            else
            {
                result.Status = userBankLogic.UpdateVerificationStatus(userBank) ? 1 : 0;
                if (result.Status == 1)
                    result.Message = "Success";
                else
                {
                    result.Message = "Failed to update User Bank Account";
                    result.Status = 0;
                    result.Code = 0;
                }
            }
            return result;
        }

        public CalculationWithdrawalFeesResponse CalculationWithdrawalFees(CalculationWithdrawalFeesRequest withdrawRequest)
        {
            WalletCalculationWithdrawFeeLogic calculationWithdrawFeeLogic = new WalletCalculationWithdrawFeeLogic();
            string FeeC_Percent = "";
            var result = new CalculationWithdrawalFeesResponse();
            try
            {
                result.FeeC_Percent = FeeC_Percent;
                result.CurrencySymbol = calculationWithdrawFeeLogic.GetCurrencySymbol(withdrawRequest.CurrencyCode);
                result.WithdrawAmount = withdrawRequest.WithdrawAmount;
                int withdrawalTime = calculationWithdrawFeeLogic.CountWithdrawTime(withdrawRequest.UserId);
                if (withdrawalTime >= EwalletConstant.LimitNumberOfWithdrawalTimePerMonth)
                {
                    result.ReceiptAmount = 0;
                    result.Status = 0;
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_OVER_LIMIT_WITHDRAWAL.Code;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_OVER_LIMIT_WITHDRAWAL.Message;
                    return result;
                }
                WalletAmountResponse balanceAmount = GetAvailableAmtAccWithoutRewardbyTranID(withdrawRequest.UserId, withdrawRequest.CurrencyCode);
                if(balanceAmount.Status==0)
                {
                    result.Status = 0;
                    result.Code = balanceAmount.Code;
                    result.Message = balanceAmount.Message;
                }
                //balanceAmountLeft after subtract withdraw amount
                result.BalanceAmountLeft = Math.Round(balanceAmount.AvailableAmount - withdrawRequest.WithdrawAmount,2);
              
                if (balanceAmount.AvailableAmount<=0 ||  result.BalanceAmountLeft < 0 )
                {
                    result.Status = 0;
                    result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_DIFF_BALANCE.Code;
                    result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_DIFF_BALANCE.Message;
                   
                }
                else
                {
                   
                    result.FeeA = calculationWithdrawFeeLogic.CalculationFeeA(withdrawRequest.CurrencyCode, withdrawRequest.WithdrawAmount);
                    result.FeeB = calculationWithdrawFeeLogic.CalculationFeeB(withdrawRequest.CurrencyCode, withdrawRequest.WithdrawAmount, result.BalanceAmountLeft);
                    result.FeeC = calculationWithdrawFeeLogic.CalculationFeeC(withdrawRequest.UserId, withdrawRequest.CurrencyCode, withdrawRequest.WithdrawAmount, balanceAmount.AvailableAmount, ref FeeC_Percent);
                    result.TotalFee = Math.Round(result.FeeA + result.FeeB + result.FeeC, 2);
                    result.ReceiptAmount = Math.Round(withdrawRequest.WithdrawAmount - result.TotalFee, 2);
                    result.FeeC_Percent = FeeC_Percent;
                    if (result.ReceiptAmount <= 0)
                    {
                        result.ReceiptAmount = 0;
                        result.Status = 0;
                        result.Code = ApiReturnCodeConstant.Wallet.ERR_WALLET_WITHDRAWAL_RECEIPTAMOUNT.Code;
                        result.Message = ApiReturnCodeConstant.Wallet.ERR_WALLET_WITHDRAWAL_RECEIPTAMOUNT.Message;
                    }
                    else
                    {
                        result.Status = ApiReturnCodeConstant.SUCCESS.Code;
                        result.Code = ApiReturnCodeConstant.SUCCESS.Code;
                        result.Message = ApiReturnCodeConstant.SUCCESS.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = 0;
                result.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
                result.Message = "Failed to Calculation Withdrawal Fees";
            }
           
            return result;
        }
        

        public lstUserBankAccountResponse GetAllUserBankAccount()
        {
            var result = new lstUserBankAccountResponse();            
            var userBankLogic = new UserBankAccountLogic();
            result.lstUserBankAcc =  userBankLogic.LoadAllUserBankAccount();
            return result;
        }

        public UserBankAccountResponse UserBankAccountByID(string id)
        {
            var result = new UserBankAccountResponse();
            var userBankLogic = new UserBankAccountLogic();
            result.userBank = userBankLogic.UserBankAccountByID(id);
            return result;
        }

        public void CalculateRewardAmount(string cartGuid, decimal totalPaidAmt, out decimal rewardAmt, out decimal mainAmt)
        {
            rewardAmt = 0;
            mainAmt = 0;
            if (cartGuid.StartsWith("TR") || cartGuid.StartsWith("FL"))
            {
                rewardAmt = totalPaidAmt * 4 / 100;
                mainAmt = totalPaidAmt * 96 / 100;
            }
            else if (cartGuid.StartsWith("BUS") || cartGuid.StartsWith("FR") || cartGuid.StartsWith("CR"))
            {
                rewardAmt = totalPaidAmt * 8 / 100;
                mainAmt = totalPaidAmt * 92 / 100;
            }
            rewardAmt = Convert.ToDecimal(string.Format("{0:0.00}", rewardAmt));
            mainAmt = Convert.ToDecimal(string.Format("{0:0.00}", mainAmt));
        }
       
    }
}
