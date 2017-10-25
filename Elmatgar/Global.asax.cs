using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Elmatgar
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
 GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
            Bootstrapper.Run();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie lngCookie = HttpContext.Current.Request.Cookies["language"];
            if (lngCookie != null && lngCookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    new System.Globalization.CultureInfo(lngCookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo(lngCookie.Value);

            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture =
                  new System.Globalization.CultureInfo("ar-SA");
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo("ar-SA");

            }
        }
    }
}
