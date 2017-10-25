using System.Web;
using System.Web.Mvc;
using Elmatgar.CustomFilters;


namespace Elmatgar
{
    public class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //for sending errors in email
            //filters.Add(new ExceptionHandlerAttribute());
            filters.Add(new HandleErrorAttribute());
            //todo:Un-comment AuthorizeAttribute,RequireHttpsAttribute when production -hassan shaddad
            ////force to use authorization
            //filters.Add(new AuthorizeAttribute());
            ////force to use https protocole
            // filters.Add(new RequireHttpsAttribute());

        }
    }
}
