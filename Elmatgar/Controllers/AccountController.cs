using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence.GRepositories.DomainUnits;
using Elmatgar.ViewModels;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Security;

namespace Elmatgar.Controllers
{
    [System.Web.Mvc.Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private AdressUnit _adressUnit = new AdressUnit();
        private UsersUnit _userunit = new UsersUnit();
        private readonly DepartmentsUnit _departments = new DepartmentsUnit();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            SignInManager = signInManager;
        }




        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = await UserManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                ApplicationUser adminUser = await UserManager.FindByEmailAsync("egyptioncoder@gmail.com");

                if (!await UserManager.IsEmailConfirmedAsync(user.Id) && user.IsCustomer)
                {
                    ViewBag.Email = user.Email;
                    return View("unconfirmedaccount");
                }
                if (!await UserManager.IsEmailConfirmedAsync(user.Id) && user.IsUser)
                {
                    ViewBag.Email = adminUser.Email;
                    return View("UnconfirmedUserAccount");
                }


            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            ViewBag.CCountryId = new SelectList(_adressUnit.Country.GetAll(), "Id", "CnCountryName");
            ViewBag.CCityId = new SelectList(_adressUnit.City.GetAll(), "Id", "CtCityName");
            ViewBag.CAreaId = new SelectList(_adressUnit.Area.GetAll(), "Id", "AAreaName");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            var customer = new Customers();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LasttName = model.LasttName,
                    PhoneNumber = model.CMobile,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    CreatedBy = "system",
                    LastUpdatedBy = "system",
                    IsCustomer = true
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    customer.Id = user.Id;
                    customer.CMobile = user.PhoneNumber;
                    customer.CEmail = user.Email;
                    customer.CFirstName = user.FirstName;
                    customer.CLastName = user.LasttName;
                    customer.CHomePhone = model.CHomePhone;
                    customer.CWorkPhone = model.CWorkPhone;
                    customer.CCountryId = model.CCountryId;
                    customer.CCityId = model.CCityId;
                    customer.CAreaId = model.CAreaId;
                    customer.CAddress = model.CAddress;

                    _userunit.Customer.Add(customer);

                    _userunit.SaveChanges();
                    _userunit.Dispose();



                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //todo:slove url contain html <here> problem
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.CCountryId = new SelectList(_adressUnit.Country.GetAll(), "Id", "CnCountryName", customer.CCountryId);
            ViewBag.CCityId = new SelectList(_adressUnit.City.GetAll(), "Id", "CtCityName", customer.CCityId);
            ViewBag.CAreaId = new SelectList(_adressUnit.Area.GetAll(), "Id", "AAreaName", customer.CAreaId);
            return View(model);
        }


        //  ///////////////////////////////////////////////////////////////////////////////////
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult RegisterUser()
        {

            ViewBag.DepartmentId = new SelectList(_departments.Department.GetAll(), "DepartmentId", "DDepartmentName");

            return View();
        }

        //
        // POST: /Account/ RegisterUser
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterUser(RegisterUserViewModel model)
        {

            var emp = new Users();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LasttName = model.LasttName,
                    PhoneNumber = model.UMobile,
                    CreationDate = DateTime.Now,
                    CreatedBy = "system",
                    LastUpdateDate = DateTime.Now,
                    LastUpdatedBy = "system",
                    IsCustomer = false,
                    IsUser = true
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    emp.Id = user.Id;
                    emp.UMobile = user.PhoneNumber;
                    emp.UEmail = user.Email;
                    emp.UUserName = user.UserName;
                    emp.DepartmentId = model.DepartmentId;

                    _userunit.Users.Add(emp);

                    _userunit.SaveChanges();
                    _userunit.Dispose();



                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    ApplicationUser adminUser = await UserManager.FindByEmailAsync("egyptioncoder@hotmail.com");
                    //todo:slove url contain html <here> problem
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //here we send confermation to the admin
                    await UserManager.SendEmailAsync(adminUser.Id, "Confirm account for" + " " + user.FirstName + "" + user.LasttName, "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.DepartmentId = new SelectList(_departments.Department.GetAll(), "DepartmentId", "DDepartmentName", emp.DepartmentId);

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
       
        public async Task<ActionResult> ReGenerateEmailConfirmation(string email)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(email);
            string code;
            if (user.Id != null)
            {
               code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            }
            else
            {
                throw new Exception("user id not found");

            }
         
            var callBackUrl = Url.Action(
                "ConfirmEmail",
                "Account",
                new { userId = user.Id, code = code }, protocol: Request.Url.Scheme
                );
            //todo:slove url contain html <here> problem
            await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callBackUrl + "\">here</a>");
            if (user.IsCustomer)
            {
                @ViewBag.ConfirmEmail = "please check your email for confirmation";
            }
            else
            {
                @ViewBag.ConfirmEmail = "please contact your manager or admin for confirmation";
            }
            return View("CheckYourMail");
        }

        // ///////////////////////////////////////////////////////////////////////////////////////

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            try
            {
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                return View("ConfirmEmail");
            }
            catch (InvalidOperationException)
            {

                return View("Error");
            }


        }


        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("Error");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:

                    //code for using user info from face book - hassan shaddad
                    var user = await UserManager.FindAsync(loginInfo.Login);
                    if (user != null)
                    {
                        await StoreFacebookAuthToken(user);
                        await SignInAsync(user, ispersistent: false);
                    }





                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool ispersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = ispersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        /// <summary>
        /// store user`s FacebookAuthToken - hassan shaddad
        /// </summary>
        /// <param name="user">ApplicationUser </param>
        /// <returns></returns>
        private async Task StoreFacebookAuthToken(ApplicationUser user)
        {
            var claimsIdentity = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

            if (claimsIdentity != null)
            {
                //retrive the existing claims for the user and add the facebook accesstokenClaim
                var currentClaims = await UserManager.GetClaimsAsync(user.Id);
                var facebookAccessToken = claimsIdentity.FindAll("FacebookAccessToken").First();
                if (currentClaims.Count() <= 0)
                {
                    await UserManager.AddClaimAsync(user.Id, facebookAccessToken);

                }
                else
                {
                    //remove current claim
                    await UserManager.RemoveClaimAsync(user.Id, currentClaims[0]);
                    // add new claim for current user
                    await UserManager.AddClaimAsync(user.Id, facebookAccessToken);
                }
            }
        }
        //todo use facebook data to fill first and last name and email
        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();

                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }



                //before using this code i edit LinkedDevsStores on facebook apps 
                //then settings =>advanced => app secret requerd and set it to no
                //now you can not use facebookController

                var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

                if (loginInfo == null)
                {
                    return RedirectToAction("Login");
                }
             string   firstName = "";
                string lastName = "";
                // added the following lines
                if (loginInfo.Login.LoginProvider == "Facebook")
                {
                    var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                    var access_token = identity.FindFirstValue("FacebookAccessToken");
                    var fb = new FacebookClient(access_token);

                    dynamic myInfo = fb.Get("/me?fields=first_name,last_name,email"); // specify the email field
                    loginInfo.Email = myInfo.email;
                    firstName = myInfo.first_name;
                    lastName = myInfo.last_name;
                }
                if (String.IsNullOrEmpty(model.Email))
                {
                    model.Email = loginInfo.Email;
                }


                //if first name is requird
                //string firstName = info.ExternalIdentity.Claims.First(c => c.Type.Contains("givenname")).Value ??
                //                   string.Empty;
                //string firstName = info.ExternalIdentity.Claims.First(c => c.Type == "urn:facebook:first_name").Value??string.Empty;
               
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = firstName,
                    LasttName = lastName,
                    IsCustomer = true,
                    CreatedBy = model.Email,
                    CreationDate = DateTime.Now
                };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    //Set Two Factor auth to user+
                    await UserManager.SetTwoFactorEnabledAsync(user.Id, true);
                    //regester as customer
                    var customer = new Customers();
                    customer.Id = user.Id;
                    customer.CEmail = user.Email;
                    customer.CFirstName = user.FirstName;

                    _userunit.Customer.Add(customer);

                    _userunit.SaveChanges();
                    _userunit.Dispose();


                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }




        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                if (_adressUnit != null)
                {
                    _adressUnit.Dispose();
                    _adressUnit = null;
                }
                if (_userunit != null)
                {
                    _userunit.Dispose();
                    _userunit = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}