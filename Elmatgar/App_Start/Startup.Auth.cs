using System;
using System.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using ApplicationUser = Elmatgar.Core.Models.ApplicationUser;

namespace Elmatgar
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the Db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(StoreDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            //create application role manager
            app.CreatePerOwinContext< ApplicationRoleManager>( ApplicationRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: ConfigurationManager.AppSettings["FacebookApi"],
            //   appSecret: ConfigurationManager.AppSettings["FacebookAppSecret"]);

            //facebook Authentication -hassan shaddad
            var faceBookOptions = new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions()
            {
                AppId = ConfigurationManager.AppSettings["FacebookApi"],
                AppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"],
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (Context) =>
                    {
                        Context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken",Context.AccessToken));
                        return Task.FromResult(0);
                    }
                },
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                SendAppSecretProof = true


            };

            faceBookOptions.Scope.Add("email");
            app.UseFacebookAuthentication(faceBookOptions);

            //User_frinds user_likes user_photos







           



            //loging with google plus sample - hassan shaddad
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "957463036491-hjkq1en01daui6mghfn9vquk41vkebjh.apps.googleusercontent.com",
                ClientSecret = "HM_o_HzffFtO2JIxRwU3nasi"
            });
        }
    }
}