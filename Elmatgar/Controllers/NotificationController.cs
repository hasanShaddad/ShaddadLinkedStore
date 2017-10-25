using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;
using Microsoft.AspNet.SignalR.Client;

namespace Elmatgar.Controllers
{
    public class NotificationController : Controller
    {
        StoreDbContext db = new StoreDbContext();
        //
        // GET: /chat/


        public  ActionResult Index()
        {
            IQueryable<Orders> Order = db.Orders.Where(c => c.OrderStatus == "bending");
            int orderList = db.Orders.Count(c => c.OrderStatus == "bending");
            ViewBag.OrdersCount = orderList;
       
            return View();
        }

    }
}