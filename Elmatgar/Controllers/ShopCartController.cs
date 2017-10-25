using System;

using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Microsoft.AspNet.Identity;

using Elmatgar.persistence;
using Elmatgar.persistence.GRepositories.DomainUnits;
using Elmatgar.ViewModels;

namespace Elmatgar.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ShopCartController : Controller
    {
        private readonly StoreDbContext _db = new StoreDbContext();
        private readonly IOrdersUnit _ordersUnit;

        public ShopCartController(IOrdersUnit ordersUnit)
        {
            this._ordersUnit = ordersUnit;
        }

        // GET: ShopCart index
        public ActionResult Index()
        {
            return View();
        }

        // AddOrder if user has no bending order then this function will give him one
        public async Task<int> AddOrder(string status)
        {

            var order = new Orders();
            try
            {
                string userid = User.Identity.GetUserId();
                order.CustomerId = userid;
                order.OrderStatus = status;
                _ordersUnit.Orders.Add(order);

                await _ordersUnit.SaveChangesAsync();



                ViewBag.OrderNo = order.Id;
            }
            catch (Exception)
            {
                throw new HttpException(404, "order not found");
            }
            return order.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderNo"></param>
        public async void AddOrderDetails(Cart model, int? orderNo)
        {
            if (model.CartItems == null)
            {
                throw new Exception("there is no items for you in cart");
            }
            foreach (var item in model.CartItems)
            {
                var prodId = item.ProdId;
                var qty = item.Qty;

                var orderDetailsList =
                    await _db.OrderDetails.Where(e => e.OrderId == orderNo && e.ProductId == prodId).ToListAsync();
                var orderDetails = orderDetailsList.LastOrDefault();

                if (orderDetails != null)
                {
                    orderDetails.OrderQuantity = qty;
                    _db.Entry(orderDetails).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    orderDetails = new OrderDetails
                    {
                        OrderId = orderNo,
                        ProductId = prodId,
                        OrderQuantity = qty,
                        OrderStatus = "bending"
                    };
                    _db.OrderDetails.Add(orderDetails);
                    await _db.SaveChangesAsync();

                }

            } //end for each
        }




        /// <summary>
        /// post AddCart insert order lines for user cart`s items
        /// </summary>
        /// <param name="model">as a cart</param>
        /// <returns>redirect to add</returns>
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> AddCart(Cart model)
        {
            int? orderNo = null;
            if (model != null)
            {
                try
                {

              
                if (User.Identity.IsAuthenticated)
                {
                    string userid = User.Identity.GetUserId();

                    var lastOrder =
                        _ordersUnit.Orders.GetAll().ToList().LastOrDefault(e => e.CustomerId == userid && e.OrderStatus == "bending");
                    //if user has a bending order
                    if (lastOrder != null)
                    {
                        orderNo = lastOrder.Id;

                    }
                    else //if user has not a bending order
                    {

                        orderNo = await AddOrder("bending");
                    }
                    //method to add order details
                    AddOrderDetails(model, orderNo);
                    var result = new { Success = "true", Message = "success" };
                    return  Json(result, JsonRequestBehavior.AllowGet);
                   

                }
                else
                {
                    RedirectToAction("LogIn", "Account");

                }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            TempData["info"] = orderNo;
            return Json(new { success = false, responseText = "Your message not sent!" },
                       JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// cart promote to bring cart data from order and orderDetails tables and show them to customer again
        /// to confirm thae cart table and add details like adress , payment method , shipping way 
        /// </summary>
        /// <returns>return View(lines)</returns>

        public ActionResult CartPromote()
        {

            string userid = User.Identity.GetUserId();
           
           IQueryable<Orders> order = _ordersUnit.Orders.GetAll().Where(e => e.CustomerId == userid && e.OrderStatus == "bending").Include(a => a.OrderDetails);
            return View(order.ToList());
            
        }
      




        //Get Keep Order
        public ActionResult KeepOrder(int? orderNo)
        {

            if (User.Identity.IsAuthenticated)
            {
                string userid = User.Identity.GetUserId();
                var order = _ordersUnit.Orders.GetAll().ToList().LastOrDefault(e => e.CustomerId == userid && e.Id == orderNo);
                if (order != null)
                {
                    order.OrderStatus = "bending";
                    _ordersUnit.Orders.Update(order);
                    _ordersUnit.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }


       
    }

}