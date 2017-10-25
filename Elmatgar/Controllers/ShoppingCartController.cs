using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;
using Microsoft.AspNet.Identity;

namespace Elmatgar.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly StoreDbContext db = new StoreDbContext();
        private bool order = false;

        public ActionResult Index(int? id)
        {
            OrderDetails orderDetails = new OrderDetails();
            return PartialView(orderDetails);
        }
        public bool AddOrder(string status)
        {
            bool orderAdded = false;
            Order Order = new Order();

            try
            {
                string userid = User.Identity.GetUserId();
                Order.CustomerId = userid;
                Order.OrderStatus = status;
                db.Order.Add(Order);

                db.SaveChanges();
                orderAdded = true;


                ViewBag.OrderNo = Order.Id;
            }
            catch (Exception)
            {

                orderAdded = false;
            }
            return orderAdded;
        }
        public ActionResult AddOrderLine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userid = User.Identity.GetUserId();
            var lastOrDefault = db.Order.ToList().LastOrDefault(e => e.CustomerId == userid && e.OrderStatus == "bendeng");
            int? orderNo;
            if (lastOrDefault != null)
            {
                orderNo = lastOrDefault.Id;
            }
            else
            {
                order = AddOrder("bendeng");
                orderNo = ViewBag.OrderNo;
            }



            var OrderDetails = db.OrderDetails.ToList().LastOrDefault(e => e.OrderId == orderNo && e.ProductId == id);
            if (
                OrderDetails !=
                null)
            {

                OrderDetails.OrderQuantity = OrderDetails.OrderQuantity + 1;
                db.Entry(OrderDetails).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                OrderDetails = new OrderDetails
                {

                    OrderId = orderNo,
                    ProductId = id,
                    OrderQuantity = 1,
                    OrderStatus = "bending"
                };

                db.OrderDetails.Add(OrderDetails);
                db.SaveChanges();
            }
            List<OrderDetails> lines = new List<OrderDetails>();
            lines = db.OrderDetails.Where(e => e.OrderId == orderNo).ToList();

            return PartialView(lines);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
