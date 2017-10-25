using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class SupplyOrderDetailsController : Controller
    {
        // phase one --- Refactoring  ****** Done 
        // still needs more and needs to work on search part and test th suppliernamecomplete javascript method and test its c# code
        private readonly ISupplierUnit _db;
        private readonly ISupplierService _supplierService;
        private readonly IProductUnit _productUnit;
        private readonly IInventoryUnit _inventoryUnit;
        private readonly IInventoryServices _inventoryServices;


        public SupplyOrderDetailsController(ISupplierUnit db, ISupplierService supplierService, IProductUnit productUnit,
            IInventoryUnit inventoryUnit, IInventoryServices inventoryServices)
        {
            this._db = db;
            this._supplierService = supplierService;
            this._productUnit = productUnit;
            _inventoryUnit = inventoryUnit;
            _inventoryServices = inventoryServices;
        }

        // GET: Admin/SupplyOrderDetails
        public ActionResult Index(int id, string sort, string search, int? page)
        {
            ViewBag.ProductNameSort = string.IsNullOrEmpty(sort) ? "name_desc" : string.Empty;
            ViewBag.QuntitySort = sort == "sortQuntity" ? "Quntity_desc" : "sortQuntity";

            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
            ViewBag.SorderId = id;

            IQueryable<SupplyOrderDetails> supplyOrderDetails =
                _supplierService.GetAllSupplyOrderDetailsBySupplyOrderId(id);

            if (!string.IsNullOrEmpty(search))
            {
                supplyOrderDetails = supplyOrderDetails.Where(e => e.Products.Name.StartsWith(search));
            }


            switch (sort)
            {
                case "name_desc":
                    supplyOrderDetails = supplyOrderDetails.OrderByDescending(e => e.Products.Name)
                        .ThenBy(e => e.Quantity);

                    break;

                case "sortQuntity":
                    supplyOrderDetails = supplyOrderDetails.OrderBy(e => e.Quantity)
                        .ThenBy(e => e.Products.Name);
                    break;

                case "Quntity_desc":
                    supplyOrderDetails = supplyOrderDetails.OrderByDescending(e => e.Quantity)
                        .ThenBy(e => e.Products.Name);

                    break;


                default:
                    supplyOrderDetails = supplyOrderDetails.OrderBy(e => e.Products.Name)
                        .ThenBy(e => e.Quantity);
                    break;
            }



            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View(supplyOrderDetails.ToPagedList(pageNumber, pageSize));

        }
        // public async Task<ActionResult> Index(string productName, string supplierName, DateTime? firstDate,
        //    DateTime? lastDate)
        //{
        //    var supplyOrderDetails = _supplierService.GetAllSupplyOrderDetails(true);


        //    if (lastDate.HasValue)
        //    {
        //        supplyOrderDetails = supplyOrderDetails.Where(e => e.CreationDate <= lastDate);
        //    }
        //    if (firstDate.HasValue)
        //    {
        //        supplyOrderDetails = supplyOrderDetails.Where(e => e.CreationDate >= firstDate);
        //    }
        //    if (!string.IsNullOrEmpty(productName) && !String.IsNullOrWhiteSpace(productName))
        //    {
        //        supplyOrderDetails = supplyOrderDetails.Where(e => e.Products.Name.StartsWith(productName));
        //    }
        //    if (!string.IsNullOrEmpty(supplierName) && !String.IsNullOrWhiteSpace(supplierName))
        //    {
        //        supplyOrderDetails =
        //            supplyOrderDetails.Where(e => e.SupplyOrders.Suppliers.SupplierName.StartsWith(supplierName));
        //    }
        //    ViewBag.PurchaseSNCurrentSearch = supplierName;
        //    ViewBag.PurchasePNCurrentSearch = productName;
        //    return View(await supplyOrderDetails.ToListAsync());
        //}

        public async Task<ActionResult> DeletedOrderIndex()
        {
            return View(await _supplierService.GetAllSupplyOrderDetails(false).ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unlock(int id)
        {


            if (_supplierService.SupplyOrderDetailsDependanceyStatus(id, true, User.Identity.Name))
            {

                return RedirectToAction("Index");
            }

            return HttpNotFound();


        }
        // GET: Admin/SupplyOrderDetails/Details/5
        public async Task<ActionResult> Details(int id)
        {

            SupplyOrderDetails supplyOrderDetails = await _db.SupplyOrderDetails.GetByIdAsync(id);
            if (supplyOrderDetails == null)
            {
                return HttpNotFound();
            }
            return View(supplyOrderDetails);
        }

        // GET: Admin/SupplyOrderDetails/Create
        public ActionResult Create(int orderNo)
        {
            var products = _productUnit.Products.GetAll();
            var supplyOrder = _supplierService.GetSupplyOrdersById(orderNo);

            ViewBag.OrderId = orderNo;
            ViewBag.ProductId = new SelectList(products, "Id", "Name");
            ViewBag.SupplyOrderId = new SelectList(supplyOrder, "Id", "Id");
            return View();
        }
        //  public ActionResult Create(string productName, string supplierName, int? orderNo)
        //{
        //    var products = _productUnit.Products.GetAll();
        //    IQueryable<SupplyOrders> supplyOrder = _db.SupplyOrders.GetAll();

        //    if (!string.IsNullOrEmpty(productName) && !String.IsNullOrWhiteSpace(productName))
        //    {
        //        products = products.Where(e => e.Name.StartsWith(productName));
        //    }
        //    if (!string.IsNullOrEmpty(supplierName) && !String.IsNullOrWhiteSpace(supplierName))
        //    {
        //        supplyOrder = supplyOrder.Where(e => e.Suppliers.SupplierName.StartsWith(supplierName));
        //    }
        //    if (orderNo != null)
        //    {
        //        supplyOrder = supplyOrder.Where(e => e.Id == orderNo);
        //    }
        //    ViewBag.PurchaseSNCurrentSearch = supplierName;
        //    ViewBag.PurchasePNCurrentSearch = productName;
        //    ViewBag.ProductId = new SelectList(products, "Id", "Name");
        //    ViewBag.SupplyOrderId = new SelectList(supplyOrder, "Id", "Id");
        //    return View();
        //}

        // POST: Admin/SupplyOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,SupplyOrderId,ProductId,Quantity,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplyOrderDetails supplyOrderDetails)
        {
            if (ModelState.IsValid)
            {
                SupplyOrders order = _db.SupplyOrders.GetById(supplyOrderDetails.SupplyOrderId);

                // _inventoryUnit.InventoryTrans.Add();
                _db.SupplyOrderDetails.Add(supplyOrderDetails);
                await _db.SaveChangesAsync();
                int orderDetailsid = supplyOrderDetails.Id;
                InventoryTrans inventory = new InventoryTrans
                {
                    ItSupplierNo = order.SupplierId,
                    ItQty = supplyOrderDetails.Quantity + _inventoryServices.GetQuntity(supplyOrderDetails.ProductId),
                    CreatedBy = User.Identity.Name
               ,
                    CreationDate = DateTime.Now
                   ,
                    ItPoLineNo = orderDetailsid


                   ,
                    ItTransAmt = supplyOrderDetails.Quantity
                   ,
                    ItProdId = supplyOrderDetails.ProductId
                   ,
                    ItTransType = "Add"

                   ,
                    ItTransDate = DateTime.Now  // ItTransDate = supplyOrder.OrderDate

                };
                //ViewBag.OrderId
                _inventoryUnit.InventoryTrans.Add(inventory);
                await _inventoryUnit.SaveChangesAsync();
                return RedirectToAction("Index", new { id = supplyOrderDetails.SupplyOrderId });

            }

            ViewBag.ProductId = new SelectList(_productUnit.Products.GetAll(), "Id", "Name",
                supplyOrderDetails.ProductId);
            ViewBag.SupplyOrderId = new SelectList(_db.SupplyOrders.GetAll(), "Id", "Id",
                supplyOrderDetails.SupplyOrderId);
            return View(supplyOrderDetails);
        }

        // GET: Admin/SupplyOrderDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplyOrderDetails supplyOrderDetails = await _db.SupplyOrderDetails.GetByIdAsync(id);
            if (supplyOrderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = id;
            ViewBag.ProductId = new SelectList(_productUnit.Products.GetAll(), "Id", "Name",
                supplyOrderDetails.ProductId);
            ViewBag.SupplyOrderId = new SelectList(_db.SupplyOrders.GetAll(), "Id", "Id",
                supplyOrderDetails.SupplyOrderId);
            return View(supplyOrderDetails);
        }

        // POST: Admin/SupplyOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,SupplyOrderId,ProductId,Quantity,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplyOrderDetails SupplyOrderDetails)
        {

            if (ModelState.IsValid)
            {

                _db.SupplyOrderDetails.Update(SupplyOrderDetails);
                await _db.SaveChangesAsync();


                //var inventory = _inventoryServices.GetTransactionByOrderDetilsId(SupplyOrderDetails.Id);
                //if (inventory.ItProdId == SupplyOrderDetails.ProductId)
                //{
                //    if (inventory.ItTransAmt > SupplyOrderDetails.Quantity)
                //    {
                //        var result = inventory.ItTransAmt - SupplyOrderDetails.Quantity;
                //        inventory.ItTransAmt = SupplyOrderDetails.Quantity;
                //        inventory.ItQty = inventory.ItQty + result;
                //        _db.InventoryTrans.Update(inventory);
                //        _inventoryUnit.InventoryTrans.Update(inventory);
                //        await _inventoryUnit.SaveChangesAsync();
                //    }
                //    else if (inventory.ItTransAmt < SupplyOrderDetails.Quantity)
                //    {
                //        var result = SupplyOrderDetails.Quantity - inventory.ItTransAmt;

                //        inventory.ItTransAmt = SupplyOrderDetails.Quantity;
                //        inventory.ItQty = inventory.ItQty + result;
                //        _inventoryUnit.InventoryTrans.Update(inventory);
                //        await _inventoryUnit.SaveChangesAsync();

                //    }
                //}
                //else
                //{
                //    inventory.ItProdId = SupplyOrderDetails.ProductId;
                //    inventory.ItQty = SupplyOrderDetails.Quantity +
                //                      _inventoryServices.GetQuntity(SupplyOrderDetails.ProductId);
                //    inventory.ItTransAmt = SupplyOrderDetails.Quantity;
                //    _inventoryUnit.InventoryTrans.Update(inventory);
                //    await _inventoryUnit.SaveChangesAsync();

                //}
                return RedirectToAction("Index", new { id = SupplyOrderDetails.SupplyOrderId });
            }
            ViewBag.ProductId = new SelectList(_productUnit.Products.GetAll(), "Id", "Name",
                SupplyOrderDetails.ProductId);
            ViewBag.SupplyOrderId = new SelectList(_db.SupplyOrders.GetAll(), "Id", "Id",
                SupplyOrderDetails.SupplyOrderId);
            return View(SupplyOrderDetails);
        }

        // GET: Admin/SupplyOrderDetails/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            SupplyOrderDetails supplyOrderDetails = await _db.SupplyOrderDetails.GetByIdAsync(id);
            if (supplyOrderDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryTransCount = _supplierService.InventoryTransCountForSupplyOrderDetailsBySOrderDeatilsId(id);
            return View(supplyOrderDetails);
        }

        // POST: Admin/SupplyOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_supplierService.SupplyOrderDetailsDependanceyStatus(id, false, User.Identity.Name))
            {

                return RedirectToAction("Index");
            }

            return HttpNotFound();
        }

        //auto complete action
        [HttpPost]
        public JsonResult SupNameComplete(string prefix)
        {
            //List<Suppliers> supplierses = db.Suppliers.ToList();
            //var supplierName = (from N in supplierses
            //                    where N.SupplierName.StartsWith(Prefix)
            //                    select new { SSupplierName = N.SupplierName });
            //return Json(supplierName, JsonRequestBehavior.AllowGet);

            return Json(_supplierService.SupplierNameComplete(prefix), JsonRequestBehavior.AllowGet);
        }

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
