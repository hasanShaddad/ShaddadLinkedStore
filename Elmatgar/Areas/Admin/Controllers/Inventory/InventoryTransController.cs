using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class InventoryTransController : Controller
    {
        private readonly IInventoryUnit db;
        private readonly IInventoryServices InventoryServices;
        private readonly IProductUnit ProductUnit;
        private readonly ISupplierUnit SupplierUnit;
        private readonly IOrdersUnit OrdersUnit;

        public InventoryTransController(IInventoryUnit db, IInventoryServices inventoryServices, IProductUnit productUnit, ISupplierUnit supplierUnit, IOrdersUnit ordersUnit)
        {
            this.db = db;
            InventoryServices = inventoryServices;
            ProductUnit = productUnit;
            SupplierUnit = supplierUnit;
            OrdersUnit = ordersUnit;
        }




        // GET: Admin/InventoryTrans
        public async Task<ActionResult> Index()
        {
            return View(await InventoryServices.GetAllInventoryTranses().ToListAsync());
        }

        // GET: Admin/InventoryTrans/Details/5
        public async Task<ActionResult> Details(int id)
        {
            InventoryTrans inventoryTrans = await db.InventoryTrans.GetByIdAsync(id);
            if (inventoryTrans == null)
            {
                return HttpNotFound();
            }
            return View(inventoryTrans);
        }

        // GET: Admin/InventoryTrans/Create
        public ActionResult Create()
        {
            ViewBag.ItOrderLineNo = new SelectList(OrdersUnit.OrderDetails.GetAll(), "Id", "Id");
            ViewBag.ItProdId = new SelectList(ProductUnit.Products.GetAll(), "Id", "Name");
            ViewBag.ItPoLineNo = new SelectList(SupplierUnit.SupplyOrderDetails.GetAll(), "Id", "Id");
            ViewBag.ItSupplierNo = new SelectList(SupplierUnit.Suppliers.GetAll(), "Id", "SupplierName");
            ViewBag.ItStoreNo = new SelectList(SupplierUnit.SupplierStores.GetAll(), "Id", "StoreName");
            return View();
        }

        // POST: Admin/InventoryTrans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ItTransDate,SupplierId,StoreId,TransactionType,Quantity,OrderDetailsId,ProductId,SupplyOrderDetailsId,ItTransAmt,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] InventoryTrans inventoryTrans)
        {
            if (ModelState.IsValid)
            {
                db.InventoryTrans.Add(inventoryTrans);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ItOrderLineNo = new SelectList(OrdersUnit.OrderDetails.GetAll(), "Id", "Id", inventoryTrans.ItOrderLineNo);
            ViewBag.ItProdId = new SelectList(ProductUnit.Products.GetAll(), "Id", "Name", inventoryTrans.ItProdId);
            ViewBag.ItPoLineNo = new SelectList(SupplierUnit.SupplyOrderDetails.GetAll(), "Id", "Id", inventoryTrans.ItPoLineNo);
            ViewBag.ItSupplierNo = new SelectList(SupplierUnit.Suppliers.GetAll(), "Id", "SupplierName", inventoryTrans.ItSupplierNo);
            ViewBag.ItStoreNo = new SelectList(SupplierUnit.SupplierStores.GetAll(), "Id", "StoreName", inventoryTrans.ItStoreNo);
            return View(inventoryTrans);
        }

        // GET: Admin/InventoryTrans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryTrans inventoryTrans = await db.InventoryTrans.GetByIdAsync(id);
            if (inventoryTrans == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItOrderLineNo = new SelectList(OrdersUnit.OrderDetails.GetAll(), "Id", "Id", inventoryTrans.ItOrderLineNo);
            ViewBag.ItProdId = new SelectList(ProductUnit.Products.GetAll(), "Id", "Name", inventoryTrans.ItProdId);
            ViewBag.ItPoLineNo = new SelectList(SupplierUnit.SupplyOrderDetails.GetAll(), "Id", "Id", inventoryTrans.ItPoLineNo);
            ViewBag.ItSupplierNo = new SelectList(SupplierUnit.Suppliers.GetAll(), "Id", "SupplierName", inventoryTrans.ItSupplierNo);
            ViewBag.ItStoreNo = new SelectList(SupplierUnit.SupplierStores.GetAll(), "Id", "StoreName", inventoryTrans.ItStoreNo);
            return View(inventoryTrans);
        }

        // POST: Admin/InventoryTrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ItTransDate,SupplierId,StoreId,TransactionType,Quantity,OrderDetailsId,ProductId,SupplyOrderDetailsId,ItTransAmt,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] InventoryTrans inventoryTrans)
        {
            if (ModelState.IsValid)
            {
                db.InventoryTrans.Update(inventoryTrans);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ItOrderLineNo = new SelectList(OrdersUnit.OrderDetails.GetAll(), "Id", "Id", inventoryTrans.ItOrderLineNo);
            ViewBag.ItProdId = new SelectList(ProductUnit.Products.GetAll(), "Id", "Name", inventoryTrans.ItProdId);
            ViewBag.ItPoLineNo = new SelectList(SupplierUnit.SupplyOrderDetails.GetAll(), "Id", "Id", inventoryTrans.ItPoLineNo);
            ViewBag.ItSupplierNo = new SelectList(SupplierUnit.Suppliers.GetAll(), "Id", "SupplierName", inventoryTrans.ItSupplierNo);
            ViewBag.ItStoreNo = new SelectList(SupplierUnit.SupplierStores.GetAll(), "Id", "StoreName", inventoryTrans.ItStoreNo);
            return View(inventoryTrans);
        }

        // GET: Admin/InventoryTrans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inventoryTrans = await db.InventoryTrans.GetByIdAsync(id);
            if (inventoryTrans == null)
            {
                return HttpNotFound();
            }
            return View(inventoryTrans);
        }

        // POST: Admin/InventoryTrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var inventoryTrans = await db.InventoryTrans.GetByIdAsync(id);
            db.InventoryTrans.Delete(inventoryTrans);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
