using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.ViewModels;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class SupplyOrderController : Controller
    {
        private readonly ISupplierUnit _db;
        private readonly ISupplierService _supplierService;
        private readonly IInventoryServices _inventoryService;
        private readonly IInventoryUnit _inventoryUnit;
        private readonly IProductUnit _productUnit;


        public SupplyOrderController(ISupplierUnit db, ISupplierService supplierService, IInventoryUnit inventoryUnit, IProductUnit productUnit, IInventoryServices inventoryService)
        {
            this._db = db;
            this._supplierService = supplierService;
            _inventoryUnit = inventoryUnit;
            _productUnit = productUnit;
            _inventoryService = inventoryService;
        }

        // GET: Admin/SupplyOrders
        public ActionResult Index(string sort, string search, int? page, DateTime? firstDate,
           DateTime? lastDate)
        {
            ViewBag.SupplierNameSort = string.IsNullOrEmpty(sort) ? "name_desc" : "Name";

            ViewBag.CDate = sort == "cdate" ? "cdate_desc" : string.Empty;
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
            IQueryable<SupplyOrders> supplyOrders = _supplierService.GetAllSupplierOrders(true);
            //_db.Products.Include(p => p.Brands).Include(p => p.Categories).Include(p => p.KitProducts);



            if (firstDate != null && lastDate != null)
            {
                supplyOrders = supplyOrders.Where(e => e.Suppliers.SupplierName.StartsWith(search))
                       .Where(e => e.OrderDate <= lastDate).Where(e => e.OrderDate >= firstDate);

            }
            else if (!string.IsNullOrEmpty(search) && firstDate == null && lastDate == null)
            {
                supplyOrders = supplyOrders.Where(e => e.Suppliers.SupplierName.StartsWith(search));
            }
            else if (firstDate != null && lastDate == null)
            {
                supplyOrders = supplyOrders.Where(e => e.Suppliers.SupplierName.StartsWith(search))
                   .Where(e => e.OrderDate >= firstDate);
            }
            else if (firstDate == null && lastDate != null)
            {
                supplyOrders = supplyOrders.Where(e => e.Suppliers.SupplierName.StartsWith(search))
                   .Where(e => e.OrderDate <= lastDate);
            }





            switch (sort)
            {
                case "name_desc":
                    supplyOrders = supplyOrders.OrderByDescending(e => e.Suppliers.SupplierName)
                        .ThenBy(e => e.OrderDate);
                    break;



                case "cdate":
                    supplyOrders = supplyOrders.OrderBy(e => e.OrderDate);
                    break;
                case "Name":
                    supplyOrders = supplyOrders.OrderBy(e => e.Suppliers.SupplierName);
                    break;

                default:
                    supplyOrders = supplyOrders.OrderByDescending(e => e.OrderDate)
                        .ThenBy(e => e.Suppliers.SupplierName);
                    break;
            }



            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View(supplyOrders.ToPagedList(pageNumber, pageSize));

        }


        public async Task<ActionResult> DeletedOrderIndex()
        {
            return View(await _supplierService.GetAllSupplierOrders(false).ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unlock(int id)
        {


            if (_supplierService.SupplyOrderDependanceyStatus(id, true, User.Identity.Name))
            {

                return RedirectToAction("Index");
            }

            return HttpNotFound();


        }
        // GET: Admin/SupplyOrders/Details/5
        public async Task<ActionResult> Details(int id)
        {

            SupplyOrders supplyOrder = await _db.SupplyOrders.GetByIdAsync(id);
            if (supplyOrder == null)
            {
                return HttpNotFound();
            }
            return View(supplyOrder);
        }

        // GET: Admin/SupplyOrders/Create
        public ActionResult Create(int? supplerno)
        {
            SuplyOrderViewModel viewmodle;
            if (supplerno != null)
            {

                viewmodle = new SuplyOrderViewModel { Suppliers = _supplierService.GetSupplierById(supplerno), Products = _productUnit.GetAllProducts() };
                return View(viewmodle);
            }



            viewmodle = new SuplyOrderViewModel { Suppliers = _db.Suppliers.GetAll(), Products = _productUnit.GetAllProducts() };
            return View(viewmodle);
            //return View();
        }

        // POST: Admin/SupplyOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SuplyOrderViewModel supplyOrder)
        {

            // note here i put OrderDate = datetime.now till i fex my date formate error 


            if (ModelState.IsValid)
            {
                var sorder = new SupplyOrders
                {
                    SupplierId = supplyOrder.SupplierId,
                    CreatedBy = User.Identity.Name
             ,
                    CreationDate = DateTime.Now
             ,
                    OrderDate = supplyOrder.OrderDate //OrderDate = supplyOrder.OrderDate



                };
                _db.SupplyOrders.Add(sorder);
                await _db.SaveChangesAsync();

                int orderid = sorder.Id;
                var sorderdetails = new SupplyOrderDetails
                {
                    CreatedBy = User.Identity.Name
                ,
                    CreationDate = DateTime.Now
                    ,
                    Quantity = supplyOrder.Quntity
                    ,
                    ProductId = supplyOrder.ProductId
                    ,
                    SupplyOrderId = orderid


                };
                _db.SupplyOrderDetails.Add(sorderdetails);
                await _db.SaveChangesAsync();
                int orderDetailsid = sorderdetails.Id;

                var inventory = new InventoryTrans
                {
                    CreatedBy = User.Identity.Name
                ,
                    CreationDate = DateTime.Now
                    ,
                    ItPoLineNo = orderDetailsid
                    ,
                    ItQty = supplyOrder.Quntity + _inventoryService.GetQuntity(supplyOrder.ProductId)

                    ,
                    ItTransAmt = supplyOrder.Quntity
                    ,
                    ItProdId = supplyOrder.ProductId
                    ,
                    ItTransType = "Add"
                    ,
                    ItSupplierNo = supplyOrder.SupplierId
                    ,
                    ItTransDate = DateTime.Now  // ItTransDate = supplyOrder.OrderDate

                };
                _inventoryUnit.InventoryTrans.Add(inventory);
                await _inventoryUnit.SaveChangesAsync();

                var sorderpayment = new SupplyOrderPayments
                {
                    CreatedBy = User.Identity.Name
                ,
                    CreationDate = DateTime.Now
                    ,
                    Paid = supplyOrder.Paid
                    ,
                    SupplierId = supplyOrder.SupplierId
                    ,
                    SupplyOrderId = orderid
                };

                _db.SupplyOrderPayments.Add(sorderpayment);
                await _db.SaveChangesAsync();



                return RedirectToAction("Index");
            }

            var viewmodle = new SuplyOrderViewModel { Suppliers = _db.Suppliers.GetAll(), Products = _productUnit.GetAllProducts() };
            return View(viewmodle);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,SupplierId,OrderDate,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplyOrders supplyOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _db.SupplyOrders.Add(supplyOrder);
        //        await _db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.SupplierId = new SelectList(_db.Suppliers.GetAll(), "Id", "SupplierName", supplyOrder.SupplierId);
        //    return View(supplyOrder);
        //}

        // GET: Admin/SupplyOrders/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            SupplyOrders supplyOrder = await _db.SupplyOrders.GetByIdAsync(id);
            if (supplyOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(_db.Suppliers.GetAll(), "Id", "SupplierName", supplyOrder.SupplierId);
            return View(supplyOrder);
        }

        // POST: Admin/SupplyOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SupplierId,OrderDate,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplyOrders SupplyOrder)
        {
            if (ModelState.IsValid)
            {

                _db.SupplyOrders.Update(SupplyOrder);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierId = new SelectList(_db.Suppliers.GetAll(), "Id", "SupplierName", SupplyOrder.SupplierId);
            return View(SupplyOrder);
        }

        // GET: Admin/SupplyOrders/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            SupplyOrders supplyOrder = await _db.SupplyOrders.GetByIdAsync(id);
            ViewBag.InventoryTransCount = _supplierService.InventoryTransCountForSupplyOrderDetailsBySorderId(id);
            ViewBag.SupplyOrderDetailsCount = _supplierService.SupplyOrderDetailsCountForSupplyorderBySupplyOrderId(id);
            ViewBag.SupplyOrderpaymentCount = _supplierService.SupplyorderpaymentCountForSupplyOrder(id);
            if (supplyOrder == null)
            {
                return HttpNotFound();
            }
            return View(supplyOrder);
        }

        // POST: Admin/SupplyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_supplierService.SupplyOrderDependanceyStatus(id, false, User.Identity.Name))
            {

                return RedirectToAction("Index");
            }

            return HttpNotFound();
            //SupplyOrders SupplyOrder = await _db.SupplyOrders.GetByIdAsync(id);
            //_db.SupplyOrders.Delete(SupplyOrder);
            //await _db.SaveChangesAsync();
            //return RedirectToAction("Index");
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
