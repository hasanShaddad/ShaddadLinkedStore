using Elmatgar.Core.Models;
using Elmatgar.persistence;
using Elmatgar.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Elmatgar.Controllers_api
{
    public class OrdersController : ApiController
    {
        private readonly StoreDbContext _db = new StoreDbContext();
        // GET: api/Orders
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        public string Get(int id)
        {
            return "value";
        }
        // [Authorize]
        [HttpPost]
        [ActionName("checkOut")]
        public IHttpActionResult checkOut(CheckOutViewModel2 model)
        {
            var order = new Orders();
            string userid = User.Identity.GetUserId();
            string username = User.Identity.GetUserName();
            order.CustomerId = userid;
            order.OrderStatus = OrderStatus.Promoted.ToString();
            order.ZipCode = model.Orders.ZipCode;
            order.CAddress = model.Orders.CAddress;
            order.CAreaId = model.Orders.CAreaId;
            order.CCityId = model.Orders.CCityId;
            order.CCountryId = model.Orders.CCountryId;
            order.CreationDate = DateTime.Now;
            order.CreatedBy = username;
            order.DeliveryOption = model.Orders.DeliveryOption.ToString();
            order.PaymentType = model.Orders.PaymentType.ToString();
            order.Total = model.Orders.Total;
            _db.Orders.Add(order);
            _db.SaveChanges();
            //   var orderlines = _db.OrderDetails.Where(o => o.OrderId == model.Id && o.OrderStatus == OrderStatus.Bending.ToString()).ToList();

            int orderNo = order.Id;

            List<OrderDetails> lines = new List<OrderDetails>();
            foreach (var item in model.OrderDetails)
            {
                var orderLine = new OrderDetails();
                orderLine.OrderId = orderNo;
                orderLine.ProductId = item.ProductId;
                orderLine.OrderQuantity = item.OrderQuantity;
                orderLine.LineTotal = item.LineTotal;
                orderLine.CreationDate = DateTime.Now;
                orderLine.CreatedBy = username;
                orderLine.OrderStatus = OrderStatus.Promoted.ToString();

                lines.Add(orderLine);

            }


            // var order = _db.Orders.Include(c => c.Customers).FirstOrDefault(e => e.CustomerId == userid && e.OrderStatus == "bending");
            // order.OrderStatus = OrderStatus.Promoted.ToString();




            _db.OrderDetails.AddRange(lines);
            // _db.Entry(order).State = EntityState.Modified;
            _db.SaveChanges();



            lines = _db.OrderDetails.Where(e => e.OrderId == orderNo).ToList();
            UpdateInventoryStock(lines);
            // SendNoti();

            return Ok(new { Order = order, OrderLine = lines });
        }




        // DELETE: api/Orders/5
        public void Delete(int id)
        {
        }
        //private decimal? Total(List<OrderDetails> orderlines)
        //{
        //    decimal? total = 0;
        //    foreach (OrderDetails line in orderlines)
        //    {
        //        line.OrderStatus = "promoted";
        //        _db.Entry(line).State = EntityState.Modified;
        //        _db.SaveChanges();
        //        total += _productDetailsService.FinalPrice(line.ProductId) * line.OrderQuantity;
        //    }
        //    return total;
        //}

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
    }
}
