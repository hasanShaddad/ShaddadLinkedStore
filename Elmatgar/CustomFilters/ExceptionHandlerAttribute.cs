using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Elmatgar.CustomFilters
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {

        public void  OnException(ExceptionContext filterContext)
        {
            EmailService es=new EmailService();


            //if (!filterContext.ExceptionHandled)
            //{
            //    string logTime = DateTime.Now.ToString();
            //    IdentityMessage message = new IdentityMessage() ;
            //    message.Body = "";
            //    message.Body += "ExceptionMessage :" + filterContext.Exception.Message;
            //    message.Body += "ExceptionStackTrace :" + filterContext.Exception.StackTrace;
            //    message.Body += "ControllerName :" + filterContext.RouteData.Values["controller"].ToString();
            //    message.Body += "LogTime:" + logTime;
            //    message.Subject = "error from linkeddevs store";
            //    message.Destination = "Hasanplural@outlook.com";
            //    es.Send(message);
          

            //    filterContext.ExceptionHandled = true;
            //}
        }
        
       
    }
 }
