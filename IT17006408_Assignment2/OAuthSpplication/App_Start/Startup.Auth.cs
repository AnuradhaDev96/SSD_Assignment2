using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.AspNet.Identity;
using OAuthSpplication.Providers;
using OAuthSpplication.Models;
using OAuthSpplication.App_Start;

namespace OAuthSpplication
{
	public partial class Startup
	{
		public static OAuthAuthorizationServerOptions OAuthAuthorizationServerOptions { get; private set; }
		public static string PublicClientId { get; private set; }

		public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //    LogoutPath = new PathString("/Account/LogOff"),
            //    ExpireTimeSpan = TimeSpan.FromMinutes(5.0),
            //});

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            PublicClientId = "self";
            OAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(4),
                AllowInsecureHttp = true
            };

            //app.UseOAuthBearerTokens(OAuthAuthorizationServerOptions);
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.UseOAuthAuthorizationServer(OAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "132617998045-honcrlov9bvejas80mdr9tlvmtqncfd0.apps.googleusercontent.com",
                ClientSecret = "afJQKibQ4yN_f7lzZOsFK2a5"
            });
        }
	}
}