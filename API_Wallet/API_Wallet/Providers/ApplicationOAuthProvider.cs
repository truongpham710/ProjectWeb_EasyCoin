using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Easybook.Api.Core.CrossCutting.Utility;
using API_Wallet.Models;
//using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Easybook.Api.BusinessLogic.ApiLogic.Constant;
using Microsoft.AspNet.Identity;
using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Reflection;

namespace API_Wallet.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly Func<UserManager<User>> _userManagerFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="publicClientId"></param>
        /// <param name="userManagerFactory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ApplicationOAuthProvider(string publicClientId, Func<UserManager<User>> userManagerFactory)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            if (userManagerFactory == null)
            {
                throw new ArgumentNullException(nameof(userManagerFactory));
            }

            _publicClientId = publicClientId;
            _userManagerFactory = userManagerFactory;
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". This occurs when the user has provided name and password
        ///             credentials directly into the client application's user interface, and the client application is using those to acquire an "access_token" and 
        ///             optional "refresh_token". If the web application supports the
        ///             resource owner credentials grant type it must validate the context.Username and context.Password as appropriate. To issue an
        ///             access token the context.Validated must be called with a new ticket containing the claims about the resource owner which should be associated
        ///             with the access token. The application should take appropriate measures to ensure that the endpoint isn’t abused by malicious callers.
        ///             The default behavior is to reject this grant type.
        ///             See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string userName = "";
            string memberEmail = "";
            var logWallet = new LogWallet();
            try
            {
            
                var aesPassword = ApiCommonConstant.AESPassword;
                var data = await context.Request.ReadFormAsync();

                memberEmail = data["memberEmail"] != null ? SimpleAesUtil.DecryptAES(data["memberEmail"], aesPassword) : string.Empty;
                var memberPassword = data["memberPassword"] != null ? SimpleAesUtil.DecryptAES(data["memberPassword"], aesPassword) : string.Empty;

                userName = data["mobileapi"] != null && data["mobileapi"] == "1" ? SimpleAesUtil.DecryptAES(context.UserName, aesPassword) : context.UserName;
                var password = data["mobileapi"] != null && data["mobileapi"] == "1" ? SimpleAesUtil.DecryptAES(context.Password, aesPassword) : context.Password;

                var type = "apiuser";
                string errorMessage = "";
                bool failedMemberLogin = false;
                ClaimsIdentity oAuthIdentity = null;
                //ClaimsIdentity cookiesIdentity = null;

                if (data["memberEmail"] != null && data["memberPassword"] != null)
                {
                    //var memberUserManager =
                    //    new ApplicationUserManager(new UserStore<IdentityUser>());
                    //var memberUser = memberUserManager.FindByEmail(memberEmail);
                    //if (memberUser != null && memberUserManager.CheckPassword(memberUser, memberPassword))
                    //{
                    //    oAuthIdentity = await memberUserManager.CreateIdentityAsync(memberUser, context.Options.AuthenticationType);
                    //    cookiesIdentity = await memberUserManager.CreateIdentityAsync(memberUser, CookieAuthenticationDefaults.AuthenticationType);
                    //    type = "memberuser";
                    //}
                    //else
                    //{
                    //    errorMessage = "Invalid member email and/or password";
                    //    failedMemberLogin = true;
                    //}
                }
                using (UserManager<User> userManager = _userManagerFactory())
                {

                    var user = await userManager.FindAsync(userName, password);

                    if (user == null)
                    {
                        logWallet.Log(MethodBase.GetCurrentMethod(), "The user name or password is incorrect.", null, "");
                        context.SetError("invalid_grant", "The user name or password is incorrect.");
                        return;
                    }
                    if (oAuthIdentity == null)
                    {
                        oAuthIdentity = await userManager.CreateIdentityAsync(user,
                            context.Options.AuthenticationType);
                    }
                    oAuthIdentity.AddClaim(new Claim("clientId", userName));
                    oAuthIdentity.AddClaim(new Claim("permission", user.Permission));
                    oAuthIdentity.AddClaim(new Claim("role", user.Role));
                    oAuthIdentity.AddClaim(new Claim("password", password));
                    oAuthIdentity.AddClaim(new Claim("secretkey", user.ApiSecretkey));
                    oAuthIdentity.AddClaim(new Claim("type", type));
                    oAuthIdentity.AddClaim(new Claim("environment", "staging" ));
                    // For logging purposes. Accessed by User.Identity.Name in this project ONLY.
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, userName));

                    //if (cookiesIdentity == null)
                    //{
                    //    cookiesIdentity = await userManager.CreateIdentityAsync(user,
                    //        CookieAuthenticationDefaults.AuthenticationType);
                    //}
                    //cookiesIdentity.AddClaim(new Claim("clientId", userName));
                    //cookiesIdentity.AddClaim(new Claim("permission", user.Permission));
                    //cookiesIdentity.AddClaim(new Claim("role", user.Role));
                    //cookiesIdentity.AddClaim(new Claim("password", password));
                    //cookiesIdentity.AddClaim(new Claim("secretkey", user.ApiSecretkey));
                    //cookiesIdentity.AddClaim(new Claim("type", type));
                    //cookiesIdentity.AddClaim(new Claim("environment", "staging" ));

                    AuthenticationProperties properties = CreateProperties(user.UserName);
                    properties.Dictionary.Add("type", type);
                    properties.Dictionary.Add("error_message", errorMessage);

                    if (failedMemberLogin)
                    {
                        properties.Dictionary.Add("failed_member_login", "1");
                    }
                    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

                    context.Validated(ticket);
                    //context.Request.Context.Authentication.SignIn(cookiesIdentity);
                }
            }
            catch (Exception ex)
            {
                
                logWallet.Log(MethodBase.GetCurrentMethod(), "userName: " + userName + ". memberEmail: " + memberEmail, ex, "");
                EmailUtil.SendEmail("[Exception]-[OAUTH]", ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Called at the final stage of a successful Token endpoint request. An application may implement this call in order to do any final 
        ///             modification of the claims being used to issue access or refresh tokens. This call may also be used in order to add additional 
        ///             response parameters to the Token endpoint's json response body.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that the correct credentials for that client are
        ///             present on the request. If the web application accepts Basic authentication credentials, 
        ///             context.TryGetBasicCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request header. If the web 
        ///             application accepts "client_id" and "client_secret" as form encoded POST parameters, 
        ///             context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those values if present in the request body.
        ///             If context.Validated is not called the request will not proceed further. 
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Called to validate that the context.ClientId is a registered "client_id", and that the context.RedirectUri a "redirect_uri" 
        ///             registered for that client. This only occurs when processing the Authorize endpoint. The application MUST implement this
        ///             call, and it MUST validate both of those factors before calling context.Validated. If the context.Validated method is called
        ///             with a given redirectUri parameter, then IsValidated will only become true if the incoming redirect URI matches the given redirect URI. 
        ///             If context.Validated is not called the request will not proceed further. 
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>
        /// Task to enable asynchronous execution
        /// </returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Creates the properties.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "clientId", userName }
            };
            return new AuthenticationProperties(data);
        }

        //        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        //        {
        //            var originalClient = context.Ticket.Properties.Dictionary["clientId"];
        //            var currentClient = context.ClientId;
        //
        //            if (originalClient != currentClient)
        //            {
        //                context.Rejected();
        //            }
        //            
        //            var newId = new ClaimsIdentity(context.Ticket.Identity);
        //            newId.AddClaim(new Claim("newClaim", "refreshToken"));
        //
        //            var newTicket = new AuthenticationTicket(newId, context.Ticket.Properties);
        //            context.Validated(newTicket);
        //
        //            return Task.FromResult<object>(null);
        //        }
    }
}