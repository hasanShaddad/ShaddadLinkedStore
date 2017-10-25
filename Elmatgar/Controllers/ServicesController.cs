 using Elmatgar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
 using System.Threading.Tasks;
 using System.Web;
using System.Web.Mvc;
 using Microsoft.AspNet.Identity;

namespace Elmatgar.Controllers
{
    public class ServicesController : Controller
    {
        private VoteDatabase db = new VoteDatabase();

        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    if (db == null) db = new Database.Database();

        //    base.Initialize(requestContext);
        //}

        public async Task<ActionResult> RateProduct(int id, int rate)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }
            string userId = User.Identity.GetUserId();
            
            bool success = false;
            string error = "";

            try
            {
                success =await db.RegisterProductVote(userId, id, rate);
            }
            catch (System.Exception ex)
            {
                // get last error
                if (ex.InnerException != null)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;

                error = ex.Message;
            }

            return Json(new { error = error, success = success, pid = id }, JsonRequestBehavior.AllowGet);
        }
    }
}
