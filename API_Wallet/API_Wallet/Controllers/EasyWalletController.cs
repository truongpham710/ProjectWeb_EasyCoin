using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet;
using Easybook.Api.BusinessLogic.ApiLogic.Logic;
using Easybook.Api.BusinessLogic.EasyWallet;
using API_Wallet.Models;
using API_Wallet.Providers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using Easybook.Api.Core.Model.EasyWallet.Models;
using System.Threading.Tasks;
using API_Wallet.Controllers.Helper;

namespace API_Wallet.Controllers
{
    [ClaimsAuthorization(ClaimType = "role", ClaimValue = "easybook,3rd")]
    [ClaimsAuthorization(ClaimType = "permission", ClaimValue = "all")]
    [RoutePrefix("api/easywallet")]
    public class EasyWalletController : ApiBaseController
    {
        private readonly WalletApiLogic _walletApiLogic;
        private readonly WalletAccountLogic _walletAccountLogic;
        public EasyWalletController()          
        {
            _walletApiLogic = new WalletApiLogic();         
        }
        public EasyWalletController(IValidatorFactory validatorFactory)
            : base(validatorFactory, "web")
        {
            _walletApiLogic = new WalletApiLogic();
        }

     

        /// <summary>
        /// GetWalletByWalletID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pWalletID">Request pWalletID</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetWalletByWalletID")]
        [ResponseType(typeof(WalletAccountResponse))]
        public IHttpActionResult GetWalletByWalletID(string sign, string pWalletID)
        {
            var inputParams = new { sign = sign, request = pWalletID };
            var response = new WalletAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetWalletAccountByWalletID(pWalletID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletByWalletID");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetWalletByUserID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pUser">Request UserID</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetWalletByUserID")]
        [ResponseType(typeof(WalletAccountResponse))]
        public IHttpActionResult GetWalletByUserID(string sign, string pUserID)
        {
            var inputParams = new { sign = sign, request = "UserID: " + pUserID };
            var response = new WalletAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetWalletAccountWithRewardMinusPendingWithdrawByUserID(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletByWalletID");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetWalletByUserID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pUser">Request UserID</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetWalletAccountByUserIDForMobile")]
        [ResponseType(typeof(WalletAccountResponse))]
        public IHttpActionResult GetWalletAccountByUserIDForMobile(string sign, string pUserID)
        {
            var inputParams = new { sign = sign, request = "UserID: " + pUserID };
            var response = new WalletAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetWalletAccountByUserIDForMobile(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletAccountByUserIDForMobile");
            }

            return Ok(response);
        }
        

        /// <summary>
        /// GetWalletByUserID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pUser">Request UserID</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetWalletIDByUserID")]
        [ResponseType(typeof(WalletUserResponse))]
        public IHttpActionResult GetWalletIDByUserID(string sign, string pUserID)
        {
            var inputParams = new { sign = sign, request = "UserID: " + pUserID };
            var response = new WalletUserResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetWalletIDByUserID(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletByWalletID");
            }

            return Ok(response);
        }       

        /// <summary>
        /// GetTranByTranID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pTranID">Request pTranID</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetTranByTranID")]
        [ResponseType(typeof(TransactionResponse))]
        public IHttpActionResult GetTranByTranID(string sign, string pTranID)
        {
            var inputParams = new { sign = sign, request = pTranID };
            var response = new TransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTranWalletByTranID(pTranID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletByWalletID");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetTranByTranID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>        
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetTranBQ")]
        [ResponseType(typeof(ListTransactionResponse))]
        public IHttpActionResult GetTranBQ(string sign)
        {
            var inputParams = new { sign = sign};
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransBQ();
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTranBQ");
            }

            return Ok(response);
        }


        /// <summary> 
        /// Get Trans By CartGuid Or Id Or Date.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>ListTransactionResponse</returns>
        [Route("GetTransByCartGuidOrIdOrDate")]
        [ResponseType(typeof(ListTransactionResponse))]
        public IHttpActionResult GetTransByCartGuidOrIdOrDate(string sign, string cartGuid = "", string transactionId = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            var inputParams = new { sign = sign };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransByCartGuidOrIdOrDate(cartGuid, transactionId, fromDate, toDate);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletByWalletID");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetTranWithdraw.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>        
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetTranWithdraw")]
        [ResponseType(typeof(ListTransactionResponse))]
        [HttpPost]
        public IHttpActionResult GetTranWithdraw(string sign)
        {
            var inputParams = new { sign = sign };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransWithdraw();
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTranWithdraw");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetTransWithdrawWithVerified.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>        
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetTransWithdrawWithVerified")]
        [ResponseType(typeof(ListTransactionResponse))]
        public IHttpActionResult GetTransWithdrawWithVerified(string sign)
        {
            var inputParams = new { sign = sign };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransWithdrawWithVerified();
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTransWithdrawWithVerified");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetTransWithdrawWithCancel.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>        
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetTransWithdrawWithCancel")]
        [ResponseType(typeof(ListTransactionResponse))]
        public IHttpActionResult GetTransWithdrawWithCancel(string sign)
        {
            var inputParams = new { sign = sign };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransWithdrawWithCancel();
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTransWithdrawWithCancel");
            }

            return Ok(response);
        }


        /// <summary>
        /// GetTranWithdraw.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>        
        /// <returns>Retrieved GetTranTopupWithoutVerifying</returns>
        [Route("GetTranTopupWithoutVerifying")]
        [ResponseType(typeof(ListTransactionResponse))]
        public IHttpActionResult GetTranTopupWithoutVerifying(string sign)
        {
            var inputParams = new { sign = sign };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransTopupWithoutVerified();
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTranTopupWithoutVerifying");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetWalletByUserID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pUser">Request UserID</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetAccIDByUserIDnCurrencyCode")]
        [ResponseType(typeof(WalletUserAccountResponse))]
        public IHttpActionResult GetAccIDByUserIDnCurrencyCode(string sign, string pUserID, string pCurrencyCode)
        {
            var inputParams = new { sign = sign, request = "UserID: " + pUserID + "|" + pCurrencyCode };
            var response = new WalletUserAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetWalletAccIDByCurrencyCodenUserID(pUserID, pCurrencyCode);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletByWalletID");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetTranByTranID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pTranID">Request pTranID</param>
        /// <param name="pCurrencyCode">Request pCurrencyCode</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetAvailableAmtByTranIDnCurrency")]
        [ResponseType(typeof(WalletAmountResponse))]
        public IHttpActionResult GetAvailableAmtByTranIDnCurrency(string sign, string pTranID, string pCurrencyCode)
        {
            var inputParams = new { sign = sign, request = pTranID };
            var response = new WalletAmountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetAvailableAmtAccbyTranID(pTranID, pCurrencyCode);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetWalletByWalletID");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetSubTranByTran_Amt.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pTranID">Request pTranID, Amount</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetSubTranByTran_Amt")]
        [ResponseType(typeof(SubTransactionReponse))]
        public IHttpActionResult GetSubTranByTran_Amt(string sign, string pTranID, decimal pAmount)
        {
            var inputParams = new { sign = sign, request = pTranID + "|" + pAmount };
            var response = new SubTransactionReponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetSubTranByTran_Amt(pTranID, pAmount);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query GetSubTranByTran_Amt");
            }

            return Ok(response);
        }


        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>ListUserCardResponse</returns>
        [Route("GetUserCardByUserIDnCurrency")]
        [ResponseType(typeof(ListUserCardResponse))]
        public IHttpActionResult GetUserCardByUserIDnCurrency(string sign, string pUserID, string pCurrencyCode)
        {
            var inputParams = new { sign = sign, request = "UseID: " + pUserID + "|Currency:" + pCurrencyCode };
            var response = new ListUserCardResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetUserCardByUserIDnCurrency(pUserID, pCurrencyCode);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query UserCard");
            }

            return Ok(response);
        }


        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>ListUserCardResponse</returns>
        [Route("GetUserCardByUserID")]
        [ResponseType(typeof(ListUserCardResponse))]
        public IHttpActionResult GetUserCardByUserID(string sign, string pUserID)
        {
            var inputParams = new { sign = sign, request = "UseID: " + pUserID};
            var response = new ListUserCardResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetUserCardByUserID(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query UserCard");
            }

            return Ok(response);
        }

        /// <summary>
        /// Get UserCard By User And Id.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>ListUserCardResponse</returns>
        [Route("GetUserCardByUserAndId")]
        [ResponseType(typeof(CreditCardResponse))]
        public IHttpActionResult GetUserCardByUserAndId(string sign, string pUserID, string pId)
        {
            var inputParams = new { sign = sign, request = "UseID: " + pUserID + "|Id:" + pId };
            var response = new CreditCardResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetUserCardByUserAndId(pUserID, pId);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query UserCard");
            }

            return Ok(response);
        }

        [Route("GetUserBankAccByUserID")]
        [ResponseType(typeof(ListUserBankAccountReponse))]
        public IHttpActionResult GetUserBankAccByUserID(string sign, string pUserID)
        {
            var inputParams = new { sign = sign, request = "UseID: " + pUserID };
            var response = new ListUserBankAccountReponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetUserBankAccByUserID(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query GetUserBankAccByUserID");
            }

            return Ok(response);
        }

        /// <summary>
        ///GetUserBankAccount_ByID
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pID">Request pTranID</param>        
        [Route("GetUserBankAccountByID")]
        [ResponseType(typeof(UserBankAccountResponse))]
        public IHttpActionResult GetUserBankAccountByID(string sign, string pID)
        {
            var inputParams = new { sign = sign };
            var response = new UserBankAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.UserBankAccountByID(pID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to GenerateNewInterest_ByDatenUserID");
            }
            return Ok(response);
        }

        [Route("GetTransByCartGuidOrIdOrCreateUser")]
        [ResponseType(typeof(ListTransactionResponse))]
        public IHttpActionResult GetTransByCartGuidOrIdOrCreateUser(string sign, string cartGuid = "", string transactionId = "", string createUser = "", string userID = "", string currencyCode = "")
        {
            var inputParams = new { sign = sign };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransByCartGuidOrIdOrCreateUser(cartGuid, transactionId, createUser, userID, currencyCode);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTransByCartGuidOrIdOrCreateUser");
            }

            return Ok(response);
        }

        [Route("GetTransByCartGuidOrIdOrCreateUserAll")]
        [ResponseType(typeof(ListTransactionResponse))]
        public IHttpActionResult GetTransByCartGuidOrIdOrCreateUserAll(string sign, string cartGuid = "", string transactionId = "", string createUser = "", string userID = "", string currencyCode = "")
        {
            var inputParams = new { sign = sign };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransByCartGuidOrIdOrCreateUserAll(cartGuid, transactionId, createUser, userID, currencyCode);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTransByCartGuidOrIdOrCreateUser");
            }

            return Ok(response);
        }

        [Route("GetAllUserBankAccount")]
        [ResponseType(typeof(lstUserBankAccountResponse))]
        public IHttpActionResult GetAllUserBankAccount(string sign)
        {
            var inputParams = new { sign = sign };
            var response = new lstUserBankAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetAllUserBankAccount();
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to GetAllUserBankAccount");
            }
            return Ok(response);
        }

        /// <summary>
        /// GetTranByTranID.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pTranID">Request pTranID</param>
        /// <param name="pTransactionType">1 - All, 2 - Payment, 3 - Topup, 4 - Withdrawal, 5 - Interest, 6 - Reward, 7 - Townbus</param>
        /// <param name="pTranID">Request pTranID</param>
        /// <param name="pTranID">Request pTranID</param>
        /// <returns>Retrieved Wallet Account</returns>
        [Route("GetHistorySubTrans")]
        [ResponseType(typeof(ListSubTransactionResponse))]
        public IHttpActionResult GetHistorySubTrans(string sign, string pUserID, string pPageNumber, string pRow, string pDateFrom, string pDateTo, int pTransactionType, string pCurrencyCode = "")
        {
            var inputParams = new { sign = sign, request = pUserID };
            var response = new ListSubTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetSubTransByUserId(pUserID, int.Parse(pPageNumber), int.Parse(pRow), pDateFrom, pDateTo, pTransactionType, pCurrencyCode);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query ListSubTransactionResponse");
            }

            return Ok(response);
        }



        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>InterestResponse</returns>
        [Route("GetWalletAccountWithInterestByUserID")]
        [ResponseType(typeof(WalletAccountResponse))]
        public IHttpActionResult GetWalletAccountWithInterestByUserID(string sign, string pUserID)
        {
            var inputParams = new { sign = sign };
            var response = new WalletAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetWalletAccountWithInterestByUserID(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to GetWalletAccountWithInterestByUserID");
            }
            return Ok(response);
        }

        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>ViralTransactionResponse</returns>
        [Route("GetViralTransactionbyTranID")]
        [ResponseType(typeof(ViralTransactionResponse))]
        public IHttpActionResult GetViralTransactionbyTranID(string sign, string pTranID)
        {
            var inputParams = new { sign = sign };
            var response = new ViralTransactionResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetViralTransactionbyTranID(pTranID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to GetViralTransactionbyTranID");
            }
            return Ok(response);
        }

        /// <summary>
        /// InsertNewUserWallet.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pUser">Request UserID</param>
        /// <returns>Retrieved bus places</returns>
        [Route("InsertNewUserWallet")]
        [ResponseType(typeof(UserResponse))]
        public IHttpActionResult InsertNewUserWallet(string sign, UserRequest User)
        {
            var inputParams = new { sign = sign, request = User };
            var response = new UserResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertNewUser(User);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertNewUserWallet");
            }

            return Ok(response);
        }

        /// <summary>
        /// InsertNewUserWallet.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pUser">Request UserID</param>
        /// <returns>Retrieved bus places</returns>
        [Route("InsertNewUserWalletForEBW")]
        [ResponseType(typeof(WalletUserResponse))]
        [HttpPost]
        public IHttpActionResult InsertNewUserWalletForEBW(UserRequest User)
        {
            var inputParams = new { sign = User.Sign, request = User };
            var response = new WalletUserResponse();
            try
            {
                if (!VerifyAPICaller(User.Sign, response)) return Ok(response);
                response = _walletApiLogic.InsertNewUserForEBW(User);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertNewUserWallet");
            }

            return Ok(response);
        }


        /// <summary>
        /// InsertNewWalletAccount.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="pUserID">Request UserID</param>
        /// <param name="pUserCreate">Request User create this wallet</param>
        /// <returns>Retrieved bus places</returns>
        [Route("InsertNewWalletAccount")]
        [ResponseType(typeof(WalletUserResponse))]
        [HttpPost]
        public IHttpActionResult InsertNewWalletAccount(string sign, string UserID, string UserCreate)
        {
            var inputParams = new { sign = sign, request = "UserID: " + UserID + "|" + UserCreate };
            var response = new WalletUserResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);               
                response = _walletApiLogic.InsertNewWalletAccount(UserID, UserCreate);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertNewWalletAccount");
            }

            return Ok(response);
        }

        /// <summary>
        /// Insert Temp Transaction.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="TransactionRequest">Request Transaction</param>
        /// <param name="Account">Request Account</param>
        /// <returns>TranResponse</returns>
        [Route("InsertTempTransaction")]
        [ResponseType(typeof(TranResponse))]
        public IHttpActionResult InsertTempTransaction(TransactionRequest Tran)
        {
            var inputParams = new { sign = Tran.sign, request = Tran };
            var response = new TranResponse();
            try
            {
                if (!VerifyAPICaller(Tran.sign, response)) return Ok(response);
                response = _walletApiLogic.InsertTempTransaction(Tran);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertTempTransaction");
            }

            return Ok(response);
        }
      

        /// <summary>
        /// Insert New Topup.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="TransactionRequest">Request Transaction</param>
        /// <param name="Account">Request Account</param>
        /// <returns>TranResponse</returns>
        [Route("InsertTopupTransaction")]
        [ResponseType(typeof(TranResponse))]
        public IHttpActionResult InsertTopupTransaction(string sign, string TranID)
        {
            var inputParams = new { sign = sign, request = "TranID: " + TranID };
            var response = new TranResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertTopupTransaction(TranID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertNewTopupTransaction");
            }

            return Ok(response);
        }

        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="TransactionRequest">Request Transaction</param>
        /// <param name="Account">Request Account</param>
        /// <returns>TranResponse</returns>
        [Route("InsertPaymentTransaction")]
        [ResponseType(typeof(TranResponse))]
        public IHttpActionResult InsertPaymentTransaction(string sign, string TranID, string AccId)
        {
            var inputParams = new { sign = sign, request = "TranID: " + TranID + "| AccID: " + AccId };
            var response = new TranResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertPaymentTransaction(TranID, AccId);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertPaymentTransaction");
            }

            return Ok(response);
        }

        /// <summary>
        /// Insert New Convert Currency.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="TransactionRequest">Request Transaction</param>
        /// <param name="Account">Request Account</param>
        /// <returns>TranResponse</returns>
        [Route("InsertConvertCurrencyTransaction")]
        [ResponseType(typeof(TranResponse))]
        public IHttpActionResult InsertConvertCurrencyTransaction(string sign, string TranID, string SourceAccId, string DestAccId)
        {
            var inputParams = new { sign = sign, request = "TranID: " + TranID + "| SourceAccId: " + SourceAccId + "| DestAccId: " + DestAccId };
            var response = new TranResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertConvertCurrency(TranID, SourceAccId, DestAccId);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertConvertCurrencyTransaction");
            }

            return Ok(response);
        }

        /// <summary>
        /// Insert Withdraw Transaction.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="TranID">Request Transaction</param>
        /// <param name="AccId">Request Account</param>
        /// <returns>TranResponse</returns>
        [Route("InsertWithdrawTransaction")]
        [ResponseType(typeof(TranResponse))]
        public IHttpActionResult InsertWithdrawTransaction(string sign, string TranID, string AccId)
        {
            var inputParams = new { sign = sign, request = "TranID: " + TranID + "| AccId: " + AccId };
            var response = new TranResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertWithdrawTransaction(TranID, AccId);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to InsertWithdrawTransaction");
            }

            return Ok(response);
        }

      
        /// <summary>
        /// Insert Reward Transaction.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="Amtreward">Amount of reward</param>
        /// <param name="Remark">Remark</param>        
        /// <param name="UserID">UserID</param>        
        /// <param name="CurrencyCode">CurrencyCode</param>        
        /// <returns>TranResponse</returns>
        [Route("InsertRewardTransaction")]
        [ResponseType(typeof(TranResponse))]
        public IHttpActionResult InsertRewardTransaction(string sign, decimal Amtreward, string Remark, string UserID, string CurrencyCode, string Merchant_ref)
        {
            var inputParams = new { sign = sign, request = "Amtreward: " + Amtreward + "| UserID: " + UserID };
            var response = new TranResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                var Acc = _walletApiLogic.GetWalletAccIDByCurrencyCodenUserID(UserID, CurrencyCode);
                var AccID = Acc.AccountID;
                if (string.IsNullOrEmpty(AccID))
                {
                    var User = new UserRequest();
                    User.User_ID = UserID;
                    var newUser = _walletApiLogic.InsertNewUserForEBW(User);
                    AccID = _walletApiLogic.GetWalletAccIDByCurrencyCodenUserID(UserID, CurrencyCode).AccountID;
                }
                
                response = _walletApiLogic.InsertRewardTransaction(Amtreward, AccID, Remark, true, Merchant_ref);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertRewardTransaction");
            }

            return Ok(response);
        }


        /// <summary>
        /// UpdateStatusBQForTran.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <param name="TransactionRequest">Request Transaction</param>
        /// <param name="Account">Request Account</param>
        /// <returns>UpdateTranBQResponse</returns>
        [Route("UpdateStatusBQForTran")]
        [ResponseType(typeof(UpdateTranBQResponse))]
        public IHttpActionResult UpdateStatusBQForTran(string sign, string TranID)
        {
            var inputParams = new { sign = sign, request = TranID };
            var response = new UpdateTranBQResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.UpdateStatusBQForTran(TranID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  UpdateStatusBQForTran");
            }

            return Ok(response);
        }
      

        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>TranResponse</returns>
        [Route("InsertNewUserCard")]
        [ResponseType(typeof(UserCardResponse))]
        [HttpPost]
        public IHttpActionResult InsertNewUserCard(string sign, UserCardRequest userCard)
        {
            var inputParams = new { sign = sign, request = "UserID: " + userCard.User_ID };
            var response = new UserCardResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertNewUserCard(userCard);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertUserCard");
            }

            return Ok(response);
        }

        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>TranResponse</returns>
        [Route("DeleteUserCardByID")]
        [ResponseType(typeof(UserCardResponse))]
        [HttpPost]
        public IHttpActionResult DeleteUserCardByID(string sign, string pCardID, string pUserID)
        {
            var inputParams = new { sign = sign, request = "CardID: " + pCardID };
            var response = new UserCardResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.DeleteUserCardByID(pCardID, pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to DeleteUserCardByID");
            }

            return Ok(response);
        }

        [Route("DeleteUserBankAccByID")]
        [ResponseType(typeof(UserBankAccountResponse))]
        [HttpPost]
        public IHttpActionResult DeleteUserBankAccByID(string sign, string pBankAccID, string pUserID)
        {
            var inputParams = new { sign = sign, request = "BankAccID: " + pBankAccID };
            var response = new UserBankAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.DeleteUserBankAccByID(pBankAccID, pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to DeleteUserBankAccByID");
            }

            return Ok(response);
        }

        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>ListUserCardResponse</returns>
        [Route("UpdateChecksumWalletAccount")]
        [ResponseType(typeof(UserResponse))]
        public IHttpActionResult UpdateChecksumWalletAccount(string sign, string fromDate, string toDate)
        {
            var inputParams = new { sign = sign};
            var response = new UserResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.UpdateChecksumWalletAccount(fromDate, toDate);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to UpdateChecksumWalletAccount");
            }
            return Ok(response);
        }

        [Route("UpdateAllCheckSumWalletAccountWithEmptyCS")]
        [ResponseType(typeof(UserResponse))]
        public IHttpActionResult UpdateAllCheckSumWalletAccountWithEmptyCS(string sign, string fromDate, string toDate)
        {
            var inputParams = new { sign = sign };
            var response = new UserResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.UpdateAllCheckSumWalletAccountWithEmptyCS(fromDate, toDate);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to UpdateChecksumWalletAccount");
            }
            return Ok(response);
        }

        

        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>ListUserCardResponse</returns>
        [Route("UpdateChecksumWalletAccountByUserID")]
        [ResponseType(typeof(UserResponse))]
        public IHttpActionResult UpdateChecksumWalletAccountByUserID(string sign, string pUserID)
        {
            var inputParams = new { sign = sign };
            var response = new UserResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.UpdateChecksumWalletAccountByUserID(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to UpdateChecksumWalletAccountByUserID");
            }
            return Ok(response);
        }

        /// <summary>
        /// Get Withdraw.
        /// </summary>
        /// <param name="param"> includes sign,snapshotChecksum,lastTransactionId,lastTransactionChecksum</param>
        /// <returns>ListTransactionResponse</returns>
        [Route("GetWithdraw")]
        [ResponseType(typeof(ListTransactionResponse))]
        [HttpPost]
        public IHttpActionResult GetWithdraw(Dictionary<string, string> param)
        {
            var inputParams = new { sign = param["sign"] };
            var response = new ListTransactionResponse();
            try
            {
                if (!VerifyAPICaller(param["sign"], response)) return Ok(response);
                response = _walletApiLogic.GetTransWithdraw(param["snapshotChecksum"], param["lastSubTransactionId"], param["lastSubTransactionChecksum"]);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to Insert_InterestTransaction");
            }
            return Ok(response);
        }

        /// <summary>
        /// Cancel Withdraw.
        /// </summary>
        /// <param name="param"> includes sign,TranID</param>
        /// <returns>CancelWithdrawResponse</returns>
        [Route("CancelWithdraw")]
        [ResponseType(typeof(CancelWithdrawResponse))]
        [HttpPost]
        public IHttpActionResult CancelWithdraw(Dictionary<string, string> param)
        {
            var inputParams = new { sign = param["sign"] };
            var response = new CancelWithdrawResponse();
            try
            {
                if (!VerifyAPICaller(param["sign"], response)) return Ok(response);
                response = _walletApiLogic.CancelWithdraw(param["tranId"], User.Identity?.Name?? string.Empty);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to Insert_InterestTransaction");
            }
            return Ok(response);
        }

        /// <summary>
        /// Cancel Withdraw.
        /// </summary>
        /// <param name="param"> includes sign,TranID</param>
        /// <returns>PendWithdrawResponse</returns>
        [Route("PendWithdraw")]
        [ResponseType(typeof(PendWithdrawResponse))]
        [HttpPost]
        public IHttpActionResult PendWithdraw(Dictionary<string, string> param)
        {
            var inputParams = new { sign = param["sign"] };
            var response = new PendWithdrawResponse();
            try
            {
                if (!VerifyAPICaller(param["sign"], response)) return Ok(response);
                response = _walletApiLogic.PendWithdraw(param["tranId"], User.Identity?.Name ?? string.Empty);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to Insert_InterestTransaction");
            }
            return Ok(response);
        }

        /// <summary>
        /// Refund Transaction.
        /// </summary>
        /// <param name="param"> includes sign,TranID</param>
        /// <returns>RefundTransactionResponse</returns>
        [Route("RefundTransaction")]
        [ResponseType(typeof(RefundTransactionResponse))]
        [HttpPost]
        public IHttpActionResult RefundTransaction(Dictionary<string, string> param)
        {
            var inputParams = new { sign = param["sign"] };
            var response = new RefundTransactionResponse();
            try
            {
                if (!VerifyAPICaller(param["sign"], response)) return Ok(response);
                //--Add log for monitor refund--
                string temp = response.Message;
                response.Message = $"tranID:{param["tranId"]}, refundAmount:{param["refundAmount"]}";
                Log(MethodBase.GetCurrentMethod(), response, null, inputParams);
                response.Message = temp;
                //--End Add log for monitor refund--
                response = _walletApiLogic.RefundTransaction(param["tranId"], param["refundAmount"], param["user"], User.Identity?.Name ?? string.Empty);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to Refund Transaction");
            }
            return Ok(response);
        }

        /// <summary>
        /// Insert New Payment.
        /// </summary>
        /// <param name="sign">api signature. please refer to documentation on how to generate signature</param>
        /// <returns>SnapShotResponse</returns>
        [Route("InsertSnapshot")]
        [ResponseType(typeof(SnapShotResponse))]
        public IHttpActionResult InsertSnapshot(string sign, string pNow)
        {
            var inputParams = new { sign = sign };
            var response = new SnapShotResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertSnapshot(pNow);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to InsertSnapshot");
            }
            return Ok(response);
        }     

        [Route("InsertCashBonus_ForAll")]
        [ResponseType(typeof(InterestResponse))]
        public IHttpActionResult InsertCashBonus_ForAll(string sign, string pSource)
        {
            var inputParams = new { sign = sign };
            var response = new InterestResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertCashBonus_ForAll(pSource);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to InsertCashBonus_ForAll");
            }
            return Ok(response);
        }

        [Route("InsertCashBonus_By_UserID")]
        [ResponseType(typeof(WalletAccountResponse))]
        public IHttpActionResult InsertCashBonus_By_UserID(string sign, string pSource, string pUserID, string pCurrency)
        {
            var inputParams = new { sign = sign };
            var response = new WalletAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertCashBonus_By_UserID(pSource, pUserID, pCurrency);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to InsertCashBonus_By_UserID");
            }
            return Ok(response);
        }


        //[Route("InsertInterest_RootDate")]
        //[ResponseType(typeof(InterestResponse))]
        //public IHttpActionResult InsertInterest_RootDate(string sign, string pRootDate)
        //{
        //    var inputParams = new { sign = sign };
        //    var response = new InterestResponse();
        //    try
        //    {
        //        if (!VerifyAPICaller(sign, response)) return Ok(response);
        //        response = _walletApiLogic.InsertInterest_RootDate(pRootDate);
        //        VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to InsertInterest_ByDate");
        //    }
        //    return Ok(response);
        //}

        [Route("UpdateCheckSumSnapshot")]
        [ResponseType(typeof(InterestResponse))]
        public IHttpActionResult UpdateCheckSumSnapshot(string sign, string pCheckSum)
        {
            var inputParams = new { sign = sign , pCheckSum = pCheckSum };
            var response = new InterestResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response.Status =_walletApiLogic.UpdateCheckSumSnapshot(pCheckSum) ? 1:0;
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to UpdateCheckSumSnapshot");
            }
            return Ok(response);
        }

        [Route("UpdateCheckSumSubTransaction")]
        [ResponseType(typeof(InterestResponse))]
        public IHttpActionResult UpdateCheckSumSubTransaction(string sign, string pDateTime, string pCheckSum)
        {
            var inputParams = new { sign = sign };
            var response = new InterestResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                _walletApiLogic.UpdateCheckSumSubTransaction(pDateTime, pCheckSum);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to UpdateCheckSumSubTransaction");
            }
            return Ok(response);
        }


        [Route("GetVerificationStatus")]
        [ResponseType(typeof(lstUserBankAccountResponse))]
        public IHttpActionResult GetVerificationStatus(string sign, string pUserID, string pCurrencyCode)
        {
            var inputParams = new { sign = sign, request = "UserID: " + pUserID };
            var response = new lstUserBankAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.LoadVerificationStatus(pUserID, pCurrencyCode);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to LoadVerificationStatus");
            }
            return Ok(response);
        }
        [Route("SubmitVerification")]
        [ResponseType(typeof(UserBankAccountResponse))]
        public IHttpActionResult SubmitVerification(string sign, UserBankAccountRequest userBank)
        {
            var inputParams = new { sign = sign };
            var response = new UserBankAccountResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.SubmitVerificationStatus(userBank) ;
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to SubmitVerification");
            }
            return Ok(response);
        }
        /// <summary>
        /// CalculationWithdrawalFees
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="withdrawalFeesRequest"></param>
        /// <returns></returns>
        [Route("CalculationWithdrawalFees")]
        [ResponseType(typeof(CalculationWithdrawalFeesResponse))]
        public IHttpActionResult CalculationWithdrawalFees(string sign, CalculationWithdrawalFeesRequest withdrawalFeesRequest)
        {
            var inputParams = new { sign = sign };
            var response = new CalculationWithdrawalFeesResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.CalculationWithdrawalFees(withdrawalFeesRequest);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to CalculationWithdrawalFees");
            }
            return Ok(response);
        }

        ///// <summary>
        ///// GetPromotionContenForWallet.
        ///// </summary>
        ///// <param name="sign"></param>
        ///// <param name="pcountry"></param>
        ///// <returns></returns>
        //[Route("GetPromotionContenForWallet")]
        //[HttpPost]
        //[ResponseType(typeof(PromotionContentResponse))]
        //public IHttpActionResult GetPromotionContenForWallet(string sign, string pcountry)
        //{
        //    var response = new PromotionContentResponse();
        //    var inputParams = new { sign = sign };

        //    try
        //    {
        //        if (!VerifyAPICaller(sign, response)) return Ok(response);
        //        response = ControllerHelper.GetPromotionContenForWallet(pcountry);
        //        VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to retrieve Promotion Conten for wallet.");
        //    }

        //    return Ok(response);
        //}

    }
}
