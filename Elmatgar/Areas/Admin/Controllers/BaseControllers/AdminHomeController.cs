using Elmatgar.ViewModels;
using System.Web;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.BaseControllers
{
    public class AdminHomeController : Controller
    {
        public ActionResult Index()
        {
            HttpCookie langCookie = new HttpCookie("language") { Value = "en-US" };
            Response.Cookies.Add(langCookie);
            ViewBag.lang = "en-US";
            var modelview = new PackAdminIndexViewModel();
            return View(modelview);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
    }
}