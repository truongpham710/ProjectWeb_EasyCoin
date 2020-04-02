using Easybook.Api.BusinessLogic.EasyWallet.Utility;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API_Wallet.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class ClaimsAuthorizationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Gets or sets the type of the claim.
        /// </summary>
        /// <value>
        /// The type of the claim.
        /// </value>
        public string ClaimType { get; set; }
        /// <summary>
        /// Gets or sets the claim value.
        /// </summary>
        /// <value>
        /// The claim value.
        /// </value>
        public string ClaimValue { get; set; }

        /// <summary>
        /// Called when [authorization asynchronous].
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            //var queryString = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
            //var agent_id = queryString["agent_id"];
            //var sign = queryString["sign"];
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            var logWallet = new LogWallet();
            if (principal != null && !principal.Identity.IsAuthenticated)
            {             
                //logWallet.Log(MethodBase.GetCurrentMethod(), principal.Identity.IsAuthenticated.ToString(), null, "1");
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }
            if (principal != null && (!principal.HasClaim(x => x.Type == "environment") || principal.HasClaim(x => x.Type == "environment" && x.Value != ("staging"))))
            {
               
                //logWallet.Log(MethodBase.GetCurrentMethod(),"", null, "2");
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            if (principal != null && !(principal.HasClaim(x => x.Type == ClaimType && ClaimValue.Split(',').Intersect(x.Value.Split(',')).Any())))
            {
                //logWallet.Log(MethodBase.GetCurrentMethod(), "", null, "3");
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return Task.FromResult<object>(null);
            }

            //User is Authorized, complete execution
            return Task.FromResult<object>(null);

        }
    }
}