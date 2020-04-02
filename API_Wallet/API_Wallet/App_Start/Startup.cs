using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using API_Wallet.Providers;
using API_Wallet.Models;
using API_Wallet.Providers;

namespace API_Wallet
{
    public class Startup
    {
        /// <summary>
        /// Options
        /// </summary>
        /// <value>
        /// The o authentication options.
        /// </value>
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        /// <summary>
        /// Gets or sets the user manager factory.
        /// </summary>
        /// <value>
        /// The user manager factory.
        /// </value>
        public static Func<UserManager<User>> UserManagerFactory { get; set; }

        /// <summary>
        /// Gets the public client identifier.
        /// </summary>
        /// <value>
        /// The public client identifier.
        /// </value>
        public static string PublicClientId { get; private set; }

        /// <summary>
        /// Owin Configuration
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create); //Not Implemented due to customized DAL.
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);


            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            UserManagerFactory = () => new EasybookUserManager<User>();
            PublicClientId = "self";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/Authentication"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                AllowInsecureHttp = true
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
