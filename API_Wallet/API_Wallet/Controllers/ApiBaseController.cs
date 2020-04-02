using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FluentValidation;
using Easybook.Api.BusinessLogic.ApiLogic.BusinessObjects;
using Newtonsoft.Json;
using NLog;
using Microsoft.AspNet.Identity;
using Easybook.Api.Core.CrossCutting.Utility;
using Easybook.Api.Core.CrossCutting.Extension;
using System.Reflection;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Easybook.Api.BusinessLogic.EasyCommon;
using Easybook.Api.BusinessLogic.EasyWallet.Constant;
using API_Wallet.UserInfo;
using API_Wallet.Providers;
using Easybook.Api.BusinessLogic.ApiLogic.Constant;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Threading.Tasks;

namespace API_Wallet.Controllers
{
    /// <summary>
    /// Base API Controller
    /// </summary>
    /// <seealso cref="ApiController" />
    public class ApiBaseController : ApiController
    {
        
        private readonly IValidatorFactory _validatorFactory;
        private readonly ILogger _logger;

        //-------- Logging Purpose --------//
        private string _apiCallerType = "";
        private ProductEnum _product = ProductEnum.Default;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private string _password = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiBaseController"/> class.
        /// </summary>
        protected ApiBaseController() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiBaseController"/> class.
        /// </summary>
        /// <param name="validatorFactory">The validator factory.</param>
        protected ApiBaseController(IValidatorFactory validatorFactory, string ApiType, ProductEnum product = ProductEnum.Default)
        {
            _validatorFactory = validatorFactory;
            _logger = LogManager.GetCurrentClassLogger();
            _apiCallerType = ApiType;
            _product = product;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Validates the place sub place identifier.
        /// </summary>
        /// <param name="placeSubPlaceId"></param>
        /// <exception cref="Exception">Id is less than or equals to 0.</exception>
        protected void ValidatePlaceSubPlaceId(int placeSubPlaceId)
        {
            if (placeSubPlaceId <= 0) { throw new Exception("Invalid Id."); }
        }

        /// <summary>
        /// Validates the place sub place identifier list.
        /// </summary>
        /// <param name="placeSubPlaceIds"></param>
        /// <exception cref="Exception"></exception>
        protected void ValidatePlaceSubPlaceIds(IEnumerable<int> placeSubPlaceIds)
        {
            if(placeSubPlaceIds == null || placeSubPlaceIds.Any(id => id <= 0)) { throw new Exception("Invalid Id."); }
        }

        /// <summary>
        /// Validates the request.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <exception cref="Exception"></exception>
        protected bool ValidateRequest<TRequest>(TRequest request, Response response)
        {
            FluentValidation.Results.ValidationResult validationResults = new FluentValidation.Results.ValidationResult();

            if (request != null)
                validationResults = _validatorFactory.GetValidator<TRequest>().Validate(request);

            if (request == null || !validationResults.IsValid)
            {
                response.Status = 0;
                response.Code = ApiReturnCodeConstant.ERR_INVALID_INPUT.Code;
                response.Message = validationResults.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid request.";
                //throw new Exception(validationResults.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid request.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verify API Caller.
        /// </summary>
        /// <param name="agent_id">Agent username/id.</param>
        /// <param name="sign">verification signature.</param>
        /// <param name="values">The request.</param>
        /// <exception cref="Exception"></exception>
        /// THIS FUNCTION IS STILL NOT YET IMPLEMENTED!!
        protected void VerifyAPICaller(string agent_id, string sign, params object[] values)
        {
            //TOBE implement
            string dummy_agent_id = "eb_testagent";
            string dummy_agent_secret = "eb_testsecret";
            string dummy_sign = "123456";
            //if (!agent_id.Equals(dummy_agent_id) || !sign.Equals(dummy_sign))
            //{
            //    throw new Exception("Invalid agent or sign.");
            //}
        }

        /// <summary>
        /// Verify the request signature (MD5 hash - secret key + password)
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="response"></param>
        /// <param name="values"></param>
        protected bool VerifyAPICaller(string sign, Response response, params object[] values)
        {
            //var sign2 = CryptographyUtil.GenerateMd5Hash("easybook" + User.GetClaimValue("secretkey") + User.GetClaimValue("password"));

            //if (sign != sign2)
            //{
            //    response.Status = 0;
            //    response.Code = ApiReturnCodeConstant.ERR_INVALID_SIGNATURE.Code;
            //    response.Message = ApiReturnCodeConstant.ERR_INVALID_SIGNATURE.Message;
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// Verify response from business logic
        /// </summary>
        /// <param name="response"></param>
        /// <param name="method"></param>
        /// <param name="inputParameters"></param>
        protected void VerifyResponse(MethodBase method, Response response, object inputParameters)
        {
            if (response.Code >= 0)
                Success(method, response, inputParameters);
            else
                Fail(method, response, null, inputParameters);
        }

        /// <summary>
        /// Provides a succesful response.
        /// </summary>
        /// <param name="response">The response.</param>
        protected static void Success(Response response)
        {
            response.Status = 1;
            response.Code = 1;
            response.Message = "Success";
        }

        /// <summary>
        /// Provides a successful response and log
        /// </summary>
        /// <param name="method"></param>
        /// <param name="response"></param>
        /// <param name="inputParameters"></param>
        protected void Success(MethodBase method, Response response, object inputParameters)
        {
            response.Status = 1;
            response.Code = response.Code == 0 ? 1 : response.Code;
            response.Message = !string.IsNullOrEmpty(response.Message) ? response.Message : "Success";
            //Log(method, response, null, inputParameters);
        }

        /// <summary>
        /// Provides a failed response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="ex">The ex.</param>
        protected static void Fail(Response response, Exception ex)
        {
            response.Status = 0;
            response.Code = ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
            response.Message = ex.Message;
        }

        /// <summary>
        /// Provides a fail response with log
        /// </summary>
        /// <param name="method"></param>
        /// <param name="response"></param>
        /// <param name="ex"></param>
        /// <param name="inputParameters"></param>
        protected void Fail(MethodBase method, Response response, Exception ex, object inputParameters, string errorMessage = "")
        {
            response.Status = 0;
            response.Code = (response != null && response.Code != 0) ? response.Code : ApiReturnCodeConstant.ERR_SYSTEM_ERROR.Code;
            response.Message = (response != null && response.Message.IsNotNullAndEmpty()) ? response.Message :
                (errorMessage.IsNotNullAndEmpty() ? errorMessage : (ex?.Message ?? "Failed to call API!"));
            if (!response.Message.Contains("WalletID is not existed or is new") && !response.Message.Contains("UserID is not existed") && !response.Message.Contains("WalletID does not exist") && !response.Message.Contains("WalletID is not existed"))
            {
                Log(method, response, ex, inputParameters, errorMessage);
            }
            
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="response"></param>
        /// <param name="ex"></param>
        /// <param name="inputParameters"></param>
        protected void Log(MethodBase method, Response response, Exception ex, object inputParameters, string errorMessage = "")
        {
            var logWallet = new LogWallet();
            logWallet.Log(method, inputParameters, ex, response.Message, response.Code, response.Status);
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="inputParameters">The input parameters.</param>
        protected void LogError(Exception ex, object inputParameters)
        {
            _logger.Error(ex, JsonConvert.SerializeObject(inputParameters));
        }

        /// <summary>
        /// Get user id
        /// </summary>
        /// <returns></returns>
        protected string GetUserId()
        {
            return User.Identity?.GetUserId() ?? "";
        }

        protected string GetOrRegister_MemberUserId(CommonCollectorInfo collectorInfo, ProductEnum product = ProductEnum.Default)
        {
            var aspnetuserid = GetUserId();
            //var claimType = User.GetClaimValue("type");

           
            //if (string.IsNullOrEmpty(aspnetuserid) || claimType == null || claimType == "apiuser")
            //{
            //    var countryId = 0;
            //    var getCountry = new CountryLogic().GetbyCountryCode(collectorInfo.Nationality);
            //    if (getCountry.Any())
            //    {
            //        countryId = getCountry.FirstOrDefault().Id;
            //    }
            //    //use collectorinfo to get memberagent
            //    aspnetuserid = GetAspNetUserId(collectorInfo.Email, "", collectorInfo.ContactNumber, collectorInfo.Name, countryId, product, true);
            //}           
            return aspnetuserid;
        }


        #region Code copy from desktop project's Easybook.Api.Presentation.Desktop.Controllers.BaseController
        /// <summary>
        /// Register new member if not exists
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phoneCountryCode"></param>
        /// <param name="contact"></param>
        /// <param name="name"></param>
        /// <param name="countryId"></param>
        /// <param name="product"></param>
        /// <param name="RecheckUser"></param>
        /// <returns></returns>
        protected string GetAspNetUserId(string email, string phoneCountryCode, string contact, string name, int countryId = 1, ProductEnum product = ProductEnum.Default, bool RecheckUser = true)
        {
            try
            {
                var isUserLoggedIn = User.Identity.GetUserId() != null;
                string error;
                string nationalNumber = string.Empty;
                int? phonePrefixId;
                string dialCode;
                string regionCode = phoneCountryCode.ToUpper();


                var isValidPhoneNumber = new PhoneLogic().IsValidPhone(
                    contact, regionCode, out error, out phonePrefixId, out nationalNumber, out dialCode);

                // To find existing member using email.
                var user = UserManager.FindByEmailAsync(email).Result;

                if (isUserLoggedIn && !RecheckUser)
                {
                    if (user != null && User.Identity.GetUserId() == user.Id)
                    {
                        UpdateNecessaryInformation(countryId, product, nationalNumber, isValidPhoneNumber, user);
                    }
                    else
                    {
                        TrackingAspNetUser(email, "APIBaseController-GetAspNetUserId");
                    }

                    return User.Identity.GetUserId();
                }

                var isExistingMember = user != null;

                if (isExistingMember)
                {
                    UpdateNecessaryInformation(countryId, product, nationalNumber, isValidPhoneNumber, user);

                    return user.Id;
                }
                // Cannot validate phone number anymore because not compulsory from front end!!!

                //validate Phone
                //if (!isValidPhoneNumber)
                //{
                //    ModelState.AddModelError(ModelStateConstant.VALIDATION, error);
                //    return null;
                //}

                var password = SimpleAesUtil.RandomPassword(); ////"A1gv@d2^w3";//System.Web.Security.Membership.GeneratePassword(10,1);
                _password = password;
                var identityUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    CountryPhonePrefixId = phonePrefixId,
                    PhoneNumber = nationalNumber,
                    FirstName = name,
                    //LastName = model.LastName,
                    //FromCompanyId = GlobalVariables.WebsiteCompanyID,
                    CountryId = countryId
                };
                var createResult = UserManager.CreateAsync(identityUser, password).Result;

                if (createResult.Succeeded)
                {
                    return GetNewlyCreatedAspNetUserIdByEmail(email);
                }

                // Try creating again
                createResult = UserManager.CreateAsync(identityUser, password).Result;

                if (createResult.Succeeded)
                {
                    return GetNewlyCreatedAspNetUserIdByEmail(email);
                }

                //LogUtil.ErrorWithConditionalEmail(new Exception($"Unable to create AspNetUser with email: {email}."), $"[Desktop-BaseController-GetAspNetUserId]", EmailAddress.MemberLogRecipient);
                throw new Exception($"Unable to create AspNetUser with email: {email}.");
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError(ModelStateConstant.EXCEPTION, ex.Message);
                //LogUtil.Error(ex, "[Desktop-Exception-BaseController-GetAspNetUserId]");
                //return null;
                //LogUtil.ErrorWithConditionalEmail(ex, $"[Desktop-Exception-BaseController-GetAspNetUserId]", EmailAddress.MemberLogRecipient);
                //LogUtil.Error(ex, "[Desktop-Exception-BaseController-GetAspNetUserId]");
                throw;
            }
        }

        private void UpdateNecessaryInformation(int countryId, ProductEnum product, string nationalNumber, bool isValidPhoneNumber, IdentityUser user)
        {
            if (UpdateIdentityUser(user, countryId))
            {
                UserManager.UpdateAsync(user);
            }

            if (isValidPhoneNumber && UpdateContactNumber(user, product, nationalNumber))
            {
                UserManager.UpdateAsync(user);
            }

            TrackingAspNetUser(user.Email, "APIBaseController-GetAspNetUserId-UpdateNecessaryInformation");

        }

        private static bool UpdateIdentityUser(IdentityUser user, int countryId)
        {
            if (countryId == 0 || user.CountryId.HasValue) { return false; }

            user.CountryId = countryId;
            return true;
        }

        private static bool UpdateContactNumber(IdentityUser user, ProductEnum product, string contactNumber)
        {
            if (product == ProductEnum.Default) { return false; }

            user.PhoneNumber = contactNumber;
            return true;
        }

        private string GetNewlyCreatedAspNetUserIdByEmail(string email)
        {
            var aspNetUserId = UserManager.FindByEmailAsync(email).Result.Id;

            if (aspNetUserId.IsNullOrWhiteSpace())
            {
                aspNetUserId = UserManager.FindByEmailAsync(email).Result.Id;
            }

            if (aspNetUserId.IsNullOrWhiteSpace())
            {
                throw new Exception($"Unable to retrieve AspNetUserId with email: {email}.");
            }

            TrackingAspNetUser(email, "APIBaseController-GetAspNetUserId-GetNewlyCreatedAspNetUserIdByEmail");

            //var forgotPasswordEmailViewModel = new ForgotPasswordEmailViewModel()
            //{
            //    Email = email,
            //    Password = _password,
            //    EasybookBanner = "https://easycdn.blob.core.windows.net/easybook/easybook-logo.png",
            //    AgentBanner = GlobalVariables.IsAgentSite.Equals(true) ? $"{GlobalVariables.ServerUrl}/images/AgentSite/{GlobalVariables.WebSiteCompanyEnumName}/logo.png" : "",
            //    IsAgentSite = GlobalVariables.IsAgentSite,
            //    WebSiteCompanyEnumName = GlobalVariables.WebSiteCompanyEnumName,
            //    ServerUrl = GlobalVariables.ServerUrl
            //};
            //SendNewRegisterUserEmail(forgotPasswordEmailViewModel);
            return aspNetUserId;
        }

        protected void TrackingAspNetUser(string collectorEmail, string module = "")
        {
            //Tracking AspNetUser
            if (ConfigUtil.Instance.ReadConfig().EnableMembershipTracking)
            {
                //If error, just skipped it to avoid main flow error
                try
                {
                    var collectorAspNetUserId = "empty";
                    // To find existing member using email.
                    if (!string.IsNullOrEmpty(collectorEmail))
                    {
                        var user = UserManager.FindByEmailAsync(collectorEmail).Result;
                        if (user != null)
                        {
                            collectorAspNetUserId = user.Id;
                        }
                    }
                    //LogUtil.Log($"[{module}-AspNetUserId-Tracking] || IsAthenticated: {(User.Identity.IsAuthenticated.Equals(null) ? "NULL" : User.Identity.IsAuthenticated.ToString())} | Identity userId: {(User.Identity.GetUserId() ?? "empty")} | Identity email: {(string.IsNullOrEmpty(User.Identity.GetUserName()) ? "empty" : User.Identity.GetUserName())} | collector userId: {collectorAspNetUserId}  | collector email: {collectorEmail ?? "null"} | Ip Address: {GetIP()} ", "AspNetUserId-Tracking");
                }
                catch (Exception ex) { }
            }
        }

        protected string GetIP()
        {
            return IpAddressUtil.GetClientIpAddress();
        }

        //protected void SendNewRegisterUserEmail(ForgotPasswordEmailViewModel forgotpassemail)
        //{
        //    var serverUrl = GlobalVariables.EasybookServerUrl;
        //    var result = "";
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            string url = serverUrl + "/common/SendNewRegisterUserEmail";
        //            var json = Newtonsoft.Json.JsonConvert.SerializeObject(forgotpassemail);
        //            var stringContent = new System.Net.Http.StringContent(json,
        //                 System.Text.UnicodeEncoding.UTF8,
        //                 "application/json");
        //            var response = client.PostAsync(url, stringContent).Result;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result = Newtonsoft.Json.JsonConvert.SerializeObject(forgotpassemail);
        //        LogUtil.Log(ex.ToString(), "[ApiBaeController][SendNewRegisterUserEmail]");
        //    }
        //}
        #endregion
    }
}
