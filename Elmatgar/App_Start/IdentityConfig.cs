using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Configuration;
using Elmatgar.persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Twilio;
using ApplicationUser = Elmatgar.Core.Models.ApplicationUser;


namespace Elmatgar
{
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// set mail using IdentityMessage - hassan shaddad
        /// </summary>
        /// <param name="message">IdentityMessage</param>
        /// <returns>smtpClint SendMailAsync mailMessage</returns>
        public Task SendAsync(IdentityMessage message)
        {
            //using configurationFile to get mailSettings for security
            Configuration configurationFile = WebConfigurationManager
    .OpenWebConfiguration("~/web.config");
            MailSettingsSectionGroup mailSettings = configurationFile
                .GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            var smtpClint = new SmtpClient();
            const string from = "admin@elmatgar.com";
            if (mailSettings != null)
            {
                string password = mailSettings.Smtp.Network.Password;

                string username = mailSettings.Smtp.Network.UserName;
                bool sslenabled = mailSettings.Smtp.Network.EnableSsl;


                smtpClint.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClint.UseDefaultCredentials = false;
                smtpClint.EnableSsl = sslenabled;
                smtpClint.Credentials = new NetworkCredential(username, password);
                smtpClint.Timeout = 100000;
            }
            //using IdentityMessage to set email
            var mailMessage = new MailMessage(from, message.Destination);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            //  email service here to send an email.
            return smtpClint.SendMailAsync(mailMessage);
        }












        public void Send(IdentityMessage message)
        {
            //using configurationFile to get mailSettings for security
            Configuration configurationFile = WebConfigurationManager
    .OpenWebConfiguration("~/web.config");
            MailSettingsSectionGroup mailSettings = configurationFile
                .GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            var smtpClint = new SmtpClient();
            const string from = "admin@elmatgar.com";
            if (mailSettings != null)
            {
                string password = mailSettings.Smtp.Network.Password;

                string username = mailSettings.Smtp.Network.UserName;
                bool sslenabled = mailSettings.Smtp.Network.EnableSsl;


                smtpClint.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClint.UseDefaultCredentials = false;
                smtpClint.EnableSsl = sslenabled;
                smtpClint.Credentials = new NetworkCredential(username, password);
                smtpClint.Timeout = 100000;
            }
            //using IdentityMessage to set email
            var mailMessage = new MailMessage(from, message.Destination);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            //  email service here to send an email.
            smtpClint.Send(mailMessage);
        }
















    }
    /// <summary>
    /// sms service using twilio and IdentityMessage
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string AccountSid = "AC46521884b1cebeadf6949050d1db9215";
            string AuthToken = "1528fc24be6268833bedd00c166e7eb2";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            twilio.SendMessage(
              "+12012974568", // From (Replace with your Twilio number)
               message.Destination, // To (Replace with your phone number)
               message.Body
                );

            ////here if we want using aspsms
            //ASPSMS Begin 
            // var soapSms = new MvcPWx.ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
            // soapSms.SendSimpleTextSMS(
            //   System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
            //   System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"],
            //   message.Destination,
            //   System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"],
            //   message.Body);
            // soapSms.Close();
            // return Task.FromResult(0);
            // ASPSMS End

            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity
    ///  and is used by the application. - hassan shaddad
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        /// <summary>
        /// Create  ApplicationUserManager - hassan shaddad
        /// </summary>
        /// <param name="options">IdentityFactoryOptions for ApplicationUserManager</param>
        /// <param name="context">IOwinContext </param>
        /// <returns>new ApplicationUserManager</returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            //usin app context (StoreDbContext) to create ApplicationUserManager
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<StoreDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(10);
            manager.MaxFailedAccessAttemptsBeforeLockout = 7;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"

            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    /// <summary>
    /// Configure the application sign-in manager which is used in this application.- hassan shaddad
    /// </summary>
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
        /// <summary>
        /// Create UserIdentity for User- hassan shaddad
        /// </summary>
        /// <param name="user">ApplicationUser</param>
        /// <returns>GenerateUserIdentityAsync for user</returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
        /// <summary>
        /// config ApplicationSignIn for user- hassan shaddad
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
    /// <summary>
    /// configur the roleManager added by develpers - hassan shaddad
    /// </summary>
    public class ApplicationRoleManager : RoleManager<Core.Models.ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<Core.Models.ApplicationRole, string> roleStore) : base(roleStore)
        {

        }

        /// <summary>
        /// create role - hassan shaddad
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns>ApplicationRole</returns>
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<Core.Models.ApplicationRole>(context.Get<StoreDbContext>()));
        }
    }


}
