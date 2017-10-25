using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrdersUnit _db;
        private readonly IProductUnit _productUnit;
        private readonly IOrdersServices _ordersServices;
        private readonly IInventoryUnit _inventoryUnit;
        private readonly IInventoryServices _inventoryServices;

        public OrderDetailsController(IInventoryUnit inventoryUnit, IProductUnit productUnit, IOrdersServices ordersServices, IOrdersUnit db, IInventoryServices inventoryServices)
        {
            _inventoryUnit = inventoryUnit;
            _productUnit = productUnit;
            _ordersServices = ordersServices;
            this._db = db;
            this._inventoryServices = inventoryServices;
        }

        // GET: Admin/OrderDetails
        public async Task<ActionResult> Index(int id)
        {
            ViewBag.OrderId = id;
            return View(await _ordersServices.GetAllOrderDetailsByOrderId(id, OrderStatus.Promoted.ToString()).ToListAsync());

        }
        // this action is used to change the order line status to "shipping " and ot change it' status to the other status  we gonna use the edit action 

        public ActionResult Shipping(int id, int orderid)
        {


            var orderDetails = _db.OrderDetails.GetById(id);
            if (orderDetails != null)
            {

                orderDetails.OrderStatus = OrderStatus.Shipping.ToString();
                _db.OrderDetails.Update(orderDetails);

                _db.SaveChangesAsync();
                var orders = _ordersServices.GetAllOrderDetailsByOrderId(orderid, OrderStatus.Promoted.ToString()).ToList();
                if (orders.Count == 0)
                {
                    ordershipping(orderid);
                }
                // SendNoti();
            }
            return RedirectToAction("Index", new { id = orderid });
        }

        public ActionResult Canceling(int id, int orderid)
        {
            //todo : handling the case when cancelling order paied by visa 

            var lastorder = _db.OrderDetails.GetById(id);
            if (lastorder != null)
            {
                var order = _db.Orders.GetById(orderid);
                if (order.PaymentType == PaymentType.visa.ToString())
                {
                    lastorder.OrderStatus = OrderStatus.Returned.ToString();
                    _db.OrderDetails.Update(lastorder);
                }
                else
                {
                    lastorder.OrderStatus = OrderStatus.Canceled.ToString();
                    _db.OrderDetails.Update(lastorder);
                }


                _db.SaveChangesAsync();
                var orders = _ordersServices.GetAllOrderDetailsByOrderId(orderid, OrderStatus.Promoted.ToString()).ToList();
                if (orders.Count == 0)
                {
                    ordershipping(orderid);
                }
                // SendNoti();
            }
            return RedirectToAction("Index", new { id = orderid });
        }

        void ordershipping(int orderid)
        {
            var lastorder = _db.Orders.GetById(orderid);
            if (lastorder != null)
            {// check if all orderlines is canceld then we'll set the order status canceld but if it has atleast one shipping order then the order status will be shipping 
                var orders = _ordersServices.GetAllOrderDetailsByOrderId(orderid, OrderStatus.Shipping.ToString()).ToList();
                if (orders.Count != 0)
                {
                    lastorder.OrderStatus = OrderStatus.Shipping.ToString();
                    _db.Orders.Update(lastorder);
                    _db.SaveChangesAsync();
                }

                else
                {
                    if (lastorder.PaymentType == PaymentType.visa.ToString())
                    {
                        lastorder.OrderStatus = OrderStatus.Returned.ToString();

                    }
                    else
                    {
                        lastorder.OrderStatus = OrderStatus.Canceled.ToString();
                    }

                    _db.Orders.Update(lastorder);
                    _db.SaveChangesAsync();
                }


            }
        }
        // GET: Admin/OrderDetails/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var OrderDetails = await _db.OrderDetails.GetByIdAsync(id);
        //    if (OrderDetails == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(OrderDetails);
        //}

        //// GET: Admin/OrderDetails/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProductId = new SelectList(_productUnit.Products.GetAll(), "Id", "Name");
        //    ViewBag.OrderId = new SelectList(_db.Orders.GetAll(), "Id", "CustomerId");
        //    return View();
        //}

        //// POST: Admin/OrderDetails/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,OrderId,ProductId,OrderQuantity,OrderStatus,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] OrderDetails OrderDetails)
        //{

        //    if (User.Identity.IsAuthenticated)
        //    {



        //        InventoryTrans inventory = new InventoryTrans
        //        {
        //            ItProdId = OrderDetails.ProductId,
        //            ItOrderLineNo = OrderDetails.Id,
        //            CreationDate = DateTime.Now,
        //            CreatedBy = User.Identity.Name,
        //            ItTransType = "buy"
        //        };



        //        var last = _inventoryServices.GetTransaction(OrderDetails.ProductId);
        //        var quantity = last?.ItQty;
        //        if (quantity != null)
        //        {
        //            int productCount = quantity.Value;

        //            if (ModelState.IsValid && productCount >= OrderDetails.OrderQuantity)
        //            {
        //                inventory.ItQty = productCount - OrderDetails.OrderQuantity;


        //                _inventoryUnit.InventoryTrans.Add(inventory);
        //                _db.OrderDetails.Add(OrderDetails);
        //                await _db.SaveChangesAsync();

        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "you can not buy more than qty number");
        //            }
        //        }
        //    }
        //    ViewBag.ProductId = new SelectList(_productUnit.Products.GetAll(), "Id", "Name", OrderDetails.ProductId);
        //    ViewBag.OrderId = new SelectList(_db.Orders.GetAll(), "Id", "CustomerId", OrderDetails.OrderId);
        //    return View(OrderDetails);
        //}

        // GET: Admin/OrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetails orderDetails = await _db.OrderDetails.GetByIdAsync(id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_productUnit.Products.GetAll(), "Id", "Name", orderDetails.ProductId);
            ViewBag.OrderId = new SelectList(_db.Orders.GetAll(), "Id", "CustomerId", orderDetails.OrderId);
            return View(orderDetails);
        }

        // POST: Admin/OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,OrderId,ProductId,OrderQuantity,OrderStatus,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] OrderDetails OrderDetails)
        //{

        //    var orderDetails = _ordersServices.GetLastOrDefaultOrderDetails(OrderDetails.Id);
        //    if (orderDetails != null)
        //    {
        //        int lastorde = orderDetails.OrderQuantity;

        //        var lastInv = _inventoryServices.GetTransaction(OrderDetails.ProductId);



        //        int defrance = lastorde - OrderDetails.OrderQuantity;

        //        if (lastInv != null) lastInv.ItQty = lastInv.ItQty + defrance;


        //        if (lastInv != null && (ModelState.IsValid && lastInv.ItQty >= 0))
        //        {
        //            _db.OrderDetails.Deatach(orderDetails);
        //            _db.OrderDetails.Update(OrderDetails);
        //            _inventoryUnit.InventoryTrans.Update(lastInv);
        //            await _db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "you can not buy more than qty number");
        //        }
        //    }
        //    ViewBag.ProductId = new SelectList(_productUnit.Products.GetAll(), "Id", "Name", OrderDetails.ProductId);
        //    ViewBag.OrderId = new SelectList(_db.Orders.GetAll(), "Id", "CustomerId", OrderDetails.OrderId);
        //    return View(OrderDetails);
        //}

        //// GET: Admin/OrderDetails/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var OrderDetails = await _db.OrderDetails.GetByIdAsync(id);
        //    if (OrderDetails == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(OrderDetails);
        //}

        //// POST: Admin/OrderDetails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    var OrderDetails = await _db.OrderDetails.GetByIdAsync(id);
        //    _db.OrderDetails.Delete(OrderDetails);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
