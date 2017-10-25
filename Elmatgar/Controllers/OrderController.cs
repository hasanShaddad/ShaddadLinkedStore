using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.persistence;
using Elmatgar.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Controllers
{
    public class OrderController : Controller
    {
        private readonly StoreDbContext _db = new StoreDbContext();
        private IProductDetailsService _productDetailsService;

        public OrderController(IProductDetailsService productDetailsService)
        {
            _productDetailsService = productDetailsService;
        }

        // GET: Order
        [HttpGet]
        public ActionResult Edit()
        {
            //ViewBag.CCountryId = new SelectList(_db.Countries, "Id", "CnCountryName");
            //ViewBag.CCityId = new SelectList(_db.Cities, "Id", "CtCityName");
            //ViewBag.CAreaId = new SelectList(_db.Zones, "Id", "AAreaName");



            string userid = User.Identity.GetUserId();
            var customers = _db.Customers.FirstOrDefault(e => e.Id == userid);
            if (customers != null) ViewBag.DefaultAddress = customers.CAddress;
            var order = _db.Orders.Include(c => c.Customers).FirstOrDefault(e => e.CustomerId == userid && e.OrderStatus == "bending");
            var viewmodel = new OrderViewModel
            {
                Id = order.Id,
                ZipCode = order.ZipCode
            };
            return PartialView("_Edit", viewmodel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderlines = _db.OrderDetails.Where(o => o.OrderId == model.Id&&o.OrderStatus==OrderStatus.Bending.ToString()).ToList();
           var total = Total(orderlines); 
                    int orderNo = model.Id;
      string username = User.Identity.GetUserName();
                string userid = User.Identity.GetUserId();
                var order = _db.Orders.Include(c => c.Customers).FirstOrDefault(e => e.CustomerId == userid && e.OrderStatus == "bending");
                order.OrderStatus = OrderStatus.Promoted.ToString();
                order.ZipCode = model.ZipCode;
                order.CAddress = model.Address;
                order.Total = total;
                order.CAreaId = model.AreaId;
                order.CCityId = model.CityId;
                order.CCountryId = model.CountryId;
                order.DeliveryOption = model.DeliveryOption.ToString();
                order.PaymentType = model.PaymentType.ToString();
                order.LastUpdateDate = DateTime.Now;
                order.LastUpdatedBy = username;
                //var order =new Orders
                //{
                //    Id = model.Id
                //    ,OrderStatus = OrderStatus.Promoted.ToString()
                //    ,ZipCode = model.ZipCode
                //    , CAddress = model.Address
                //    ,Total = total
                //    ,CAreaId = model.AreaId
                //    ,CCityId = model.CityId
                //    ,CCountryId = model.CountryId
                //    ,CustomerId = User.Identity.GetUserId()
                //    ,DeliveryOption = model.DeliveryOption.ToString()
                //    ,PaymentType = model.PaymentType.ToString()
                //    ,LastUpdateDate = DateTime.Now
                //    ,LastUpdatedBy = username


                //};



                _db.Entry(order).State = EntityState.Modified;
                  await _db.SaveChangesAsync();

                List<OrderDetails> lines = new List<OrderDetails>();
                lines = await _db.OrderDetails.Where(e => e.OrderId == orderNo).ToListAsync();
                UpdateInventoryStock(lines);
                SendNoti();
                if (model.PaymentType  == PaymentType.visa)
                {
                    return RedirectToAction("Index", "OnlinePaid", new { orderId = order.Id });
                }
                else
                    return RedirectToAction("Thanks", "Order");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Thanks()
        {
            return View();
        }
        /// <summary>
        /// Update the Inventory Stock for each line in order lines
        /// </summary>
        /// <param name="lines">ordr line</param>
        private void UpdateInventoryStock(List<OrderDetails> lines)
        {
            foreach (OrderDetails item in lines)
            {
                var inventoryStock = _db.InventoryTrans.ToList().LastOrDefault(e => e.ItProdId == item.ProductId);
                InventoryTrans newInventoryTrans = new InventoryTrans();
                ;
                if (inventoryStock != null)
                {
                    if (inventoryStock.ItQty >= item.OrderQuantity)
                    {
                        newInventoryTrans.ItOrderLineNo = item.Id;
                        newInventoryTrans.ItProdId = item.ProductId;
                        newInventoryTrans.ItTransDate = DateTime.Now;
                        newInventoryTrans.ItTransType = "sale";
                        newInventoryTrans.ItQty = inventoryStock.ItQty - item.OrderQuantity;
                        newInventoryTrans.ItTransAmt = item.OrderQuantity;
                        _db.InventoryTrans.Add(newInventoryTrans);
                        _db.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("", "insufficient funds");
                    }
                }
            }
        }
      [System.Web.Mvc.Authorize]
        public ActionResult TrackingOrders()
        {
            string userid = User.Identity.GetUserId();
            var order = _db.Orders.Where(e => e.CustomerId == userid);
            return View(order);
        }
        /// <summary>
        /// clculate the total price for all order lines
        /// </summary>
        /// <param name="orderlines"></param>
        /// <returns> decimal? total</returns>
        private decimal? Total(List<OrderDetails> orderlines)
        {
            decimal? total = 0;
            foreach (OrderDetails line in orderlines)
            {
                line.OrderStatus = "promoted";
                _db.Entry(line).State = EntityState.Modified;
                _db.SaveChanges();
                total += _productDetailsService.FinalPrice(line.ProductId) * line.OrderQuantity;
            }
            return total;
        }
        /// <summary>
        /// send notification to admin using SendMessage() method
        /// </summary>
        public virtual void SendNoti()
        {
            int orderList = _db.Orders.Count(c => c.OrderStatus == "promoted");
            ViewBag.OrdersCount = orderList;
            SendMessage(orderList.ToString());
        }
        /// <summary>
        /// Send Message method using segnalR to the admin panel
        /// </summary>
        /// <param name="msg"></param>
        public static void SendMessage(string msg)
        {

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.addNewMessageToPage("", msg, "Noti");

        }

    }
}