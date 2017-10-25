using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Elmatgar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           
            //for cascading dropdown lists countries-cities-zones - hassan shaddad
            routes.MapRoute(
              name: "droplist",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "DropList", action = "FillCity", id = UrlParameter.Optional }
          );
          
           
        }
    }
}
