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
    public class SuppliersController : Controller
    {
        private readonly ISupplierUnit db;
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierUnit _db, ISupplierService supplierService)
        {
            db = _db;
            _supplierService = supplierService;
        }

        // GET: Admin/Suppliers
        public ActionResult Index(string sort, string search, int? page)
        {

            ViewBag.SupplierNameSort = string.IsNullOrEmpty(sort) ? "name_desc" : string.Empty;
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
           

            IQueryable<Suppliers> supplier = db.Suppliers.GetWhereFlag(true);
            if (!string.IsNullOrEmpty(search))
            {
                supplier = supplier.Where(e => e.SupplierName.StartsWith(search));
            }


            switch (sort)
            {
                case "name_desc":
                    supplier = supplier.OrderByDescending(e => e.SupplierName);

                    break;


                default:
                    supplier = supplier.OrderBy(e => e.SupplierName)
                            .ThenBy(e => e.CreationDate);
                    break;
            }



            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View(supplier.ToPagedList(pageNumber, pageSize));
        
        }

        public async Task<ActionResult> LockedIndex()
        {
            return View(await db.Suppliers.GetWhereFlag(false).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unlock(int id)
        {


            if (_supplierService.SupplierDependanceyStatus(id, true, User.Identity.Name))
            {

                return RedirectToAction("Index");
            }

            return HttpNotFound();


        }
        // GET: Admin/Suppliers/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.GetByIdAsync(id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        // GET: Admin/Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SupplierName,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                suppliers.CreatedBy = User.Identity.Name;
                suppliers.CreationDate = DateTime.Now;
                db.Suppliers.Add(suppliers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(suppliers);
        }

        // GET: Admin/Suppliers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.GetByIdAsync(id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        // POST: Admin/Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SupplierName,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ActiveFlag")] Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                suppliers.LastUpdateDate = DateTime.Now;
                suppliers.LastUpdatedBy = User.Identity.Name;
                db.Suppliers.Update(suppliers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        // GET: Admin/Suppliers/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            Suppliers suppliers = await db.Suppliers.GetByIdAsync(id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }

            ViewBag.SupplyOrderCount = _supplierService.SupplyOrderCountForSupplier(id);
            ViewBag.SupplyOrderpaymentCount = _supplierService.SupplyorderpaymentCountForSupplier(id);
            ViewBag.SupplierStoresCount = _supplierService.SupplierStoresCountForSupplier(id);
            ViewBag.SupplyOrderDetailsCount = _supplierService.SupplyOrderDetailsCountForSupplyorderBySupplierId(id);
            ViewBag.InventoryTransCount = _supplierService.InventoryTransCountForSupplier(id);
            return View(suppliers);
        }

        // POST: Admin/Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {



            if (_supplierService.SupplierDependanceyStatus(id, false, User.Identity.Name))
            {

                return RedirectToAction("Index");
            }

            return HttpNotFound();


        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //bool deletion(int id)
        //{
        //    //  supplier delete order:

        //    //  1 supplyorderpayment
        //    //2 supply order details

        //    //3 supply order
        //    //4 inventory trans
        //    //5 supplierstore
        //    //6supplier  

        //    var success = false;
        //    var SPayment = _supplierService.GetAllSupplyOrderPaymentsBySupplierId(id);
        //    if (SPayment != null)
        //    {
        //        success = _supplierService.DeletesupplyorderpaymentBySupplierId(SPayment);
        //        db.SaveChanges();
        //    }

        //    if (_supplierService.SupplyOrderDetailsCountForSupplyorderBySupplierId(id) != 0)
        //    {

        //        success = _supplierService.DeleteSupplyOrderDetailsBySupplierId(id);
        //        db.SaveChanges();
        //    }
        //    var Sorder = _supplierService.GetAllSupplyOrderBySupplierId(id);
        //    if (Sorder != null)
        //    {

        //        success = _supplierService.DeleteSupplyOrderBySupplierId(Sorder);
        //        db.SaveChanges();

        //    }
        //    var inven = _supplierService.GetAllInventoryTransBySupplierId(id);
        //    if (inven != null)
        //    {

        //        success = _supplierService.DeleteInventoryTransBySupplierId(inven);
        //        db.SaveChanges();
        //    }

        //    var Sstore = _supplierService.GetAllSupplierStoresBySupplierId(id);
        //    if (Sstore != null)
        //    {

        //        success = _supplierService.DeleteSupplierStoreBySupplierId(Sstore);
        //        db.SaveChanges();

        //    }
        //    return success;


        //}
    }
}
