using API_Wallet.Providers;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects.Wallet;
using Easybook.Api.BusinessLogic.ApiLogic.Logic;
using Easybook.Api.BusinessLogic.EasyWallet;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace API_Wallet.Controllers
{
    /// <summary>
    /// TownBusController
    /// </summary>
    [ClaimsAuthorization(ClaimType = "role", ClaimValue = "easybook,3rd")]
    [ClaimsAuthorization(ClaimType = "permission", ClaimValue = "all")]
    [RoutePrefix("api/townbus")]
    public class TownBusController : ApiBaseController
    {
        private readonly WalletApiLogic _walletApiLogic;
       // private readonly WalletAccountLogic _walletAccountLogic;
        /// <summary>
        /// TownBusController
        /// </summary>
        public TownBusController()
        {
            _walletApiLogic = new WalletApiLogic();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="validatorFactory"></param>
        public TownBusController(IValidatorFactory validatorFactory)
            : base(validatorFactory, "web")
        {
            _walletApiLogic = new WalletApiLogic();
        }


        /// <summary>
        /// InsertCheckInTransactionTownBus
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="Tran"></param>
        /// <returns></returns>
        [Route("InsertCheckInTransactionTownBus")]
        [ResponseType(typeof(TranTownBusResponse))]
        public IHttpActionResult InsertCheckInTransactionTownBus(string sign,TransactionRequestTownBus Tran)
        {
            var inputParams = new { sign = sign, request = Tran };
            var response = new TranTownBusResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertCheckInTransactionTownBus(Tran);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertCheckInTransactionTownBus");
            }

            return Ok(response);
        }

        /// <summary>
        /// InsertCheckOutTransactionTownBus
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="Tran"></param>
        /// <returns></returns>
        [Route("InsertCheckOutTransactionTownBus")]
        [ResponseType(typeof(TranTownBusResponse))]
        public IHttpActionResult InsertCheckOutTransactionTownBus(string sign,TransactionRequestTownBus Tran)
        {
            var inputParams = new { sign = sign, request = Tran };
            var response = new TranTownBusResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.InsertCheckOutTransactionTownBus(Tran, Tran.IsAutoCheckOut);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  InsertCheckOutTransactionTownBus");
            }

            return Ok(response);
        }
        /// <summary>
        /// TokenTownBus
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="requests"></param>
        /// <returns></returns>
        [Route("TokenTownBus")]
        [ResponseType(typeof(TokenTownBusResponse))]
        public IHttpActionResult TokenTownBus(string sign, TokenRequestTownBus requests)
        {
            var inputParams = new { sign = sign, request = requests };
            var response = new TokenTownBusResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetAccessTokenTownBus(requests);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  TokenTownBus ");
            }

            return Ok(response);
        }
       
        /// <summary>
        /// GetTransactionListTownBusByCarIDnDateTime
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="requests"></param>
        /// <returns></returns>
        [Route("GetTransactionListTownBusByCarIDnDateTime")]
        [ResponseType(typeof(TransactionTownBusResponse))]
        [HttpPost]
        public IHttpActionResult GetTransactionListTownBusByCarIDnDateTime( string sign,TripsRequestTownBus requests)
        {
            var inputParams = new { sign = sign, request = requests };
            var response = new TransactionTownBusResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetTransactionLstTownBusByCarIDnDateTime(requests);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to  GetTransactionListTownBusByPlaceNumbernDateTime");
            }

            return Ok(response);
        }
        /// <summary>
        /// GetLastTranByUserID
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        [Route("GetListPendingTransByUserID")]
        [ResponseType(typeof(ListPendingTranTownBusResponse))]
        public IHttpActionResult GetListPendingTransByUserID(string sign, string pUserID)
        {
            var inputParams = new { sign = sign, request = pUserID };
            var response = new ListPendingTranTownBusResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetListPendingTransactionByUserID(pUserID);
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query GetListPendingTransByUserID");
            }

            return Ok(response);
        }

        /// <summary>
        /// GetLastTranByUserID
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        [Route("GetListEligibleTownBuses")]
        [ResponseType(typeof(ListEligibleTownBusesResponse))]
        public IHttpActionResult GetListEligibleTownBuses(string sign)
        {
            var inputParams = new { sign = sign };
            var response = new ListEligibleTownBusesResponse();
            try
            {
                if (!VerifyAPICaller(sign, response)) return Ok(response);
                response = _walletApiLogic.GetListEligibleTownBuses();
                VerifyResponse(MethodBase.GetCurrentMethod(), response, inputParams);
            }
            catch (Exception ex)
            {
                Fail(MethodBase.GetCurrentMethod(), response, ex, inputParams, "Error : Failed to query GetListEligibleTownBuses");
            }

            return Ok(response);
        }
    }
}