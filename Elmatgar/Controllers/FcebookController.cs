using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Extensions;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Elmatgar.Controllers
{
    [Authorize]
    [FacebookAccessToken]
    public class FcebookController : Controller
    {

        // GET: Fcebook
        //before using this controller please go to LinkedDevsStores on facebook apps 
        //then settings =>advanced => app secret requerd and set it to yes
        //but then you can not use the facebook api in account controller  var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync()
        //ExternalLoginConfirmation
        public async Task<ActionResult> Index()
        {
            var access_token = HttpContext.Items["access_token"].ToString();
            if (access_token != null)
            {
                try
                {
                    //get new appSecret using access_token
                    //look at Elmatgar.Core.Extensions 
                    var app_secret_proof = access_token.GenerateAppSecretProof();
                    //get new facebook clint using accesstoken
                    var fb =new FacebookClient(access_token);
                    //get current user profile
                    //look at Elmatgar.Core.Extensions  for method GraphApiCall
                    //if facebook change his graphApi you can change there one time
                    dynamic myInfo =
                        await
                            fb.GetTaskAsync(
                                "me?fields=first_name,last_name,link,locale,email".GraphApiCall(app_secret_proof));
                    ////for image if we add it 
                    //dynamic myInfoForImage =
                    //   await
                    //       fb.GetTaskAsync(
                    //           "{0}/picture?width=200&height=200&redirect=false".GraphApiCall((string)myInfo.id,app_secret_proof));

                    ////if you want result in view model
                    //var facebookProfile = facebookProfileViewModel()
                    //{
                    //firstname = myInfo.first_name,
                    //lastname=myInfo.last_name
                    //}
                    //;
                    //return view(facebookProfile);

                }
                catch (FacebookApiLimitException ex)
                {
                    
                    throw new HttpException(500,ex.Message);
                }
                catch (FacebookOAuthException ex)
                {

                    throw new HttpException(500, ex.Message);
                }
                catch (FacebookApiException ex)
                {

                    throw new HttpException(500, ex.Message);
                }
                catch (Exception ex)
                {

                    throw new HttpException(500, ex.Message);
                }
            }
            return View();
        }
    }
    
    public class FacebookAccessTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ApplicationUserManager userManager =
                filterContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (userManager!=null)
            {
                var claimsForUser = userManager.GetClaimsAsync(filterContext.HttpContext.User.Identity.GetUserId());
                var firstOrDefault = claimsForUser.Result.FirstOrDefault(x => x.Type == "FacebookAccessToken");
                if (firstOrDefault != null)
                {
                    var accessToken = firstOrDefault.Value;
                    filterContext.HttpContext.Items.Add("access_token", accessToken);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}