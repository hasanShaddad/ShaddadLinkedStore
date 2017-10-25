using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using PagedList;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class SupplyOrderPaymentsController : Controller
    {// 
        private readonly ISupplierUnit _db;
        private readonly ISupplierService _supplierService;

        public SupplyOrderPaymentsController(ISupplierUnit db, ISupplierService supplierService)
        {
            this._db = db;
            _supplierService = supplierService;
        }
        //***
        // GET: Admin/SupplyOrderPayments
        public  ActionResult Index(string sort, string search, int? page)
        {

            ViewBag.SupplierNameSort = string.IsNullOrEmpty(sort) ? "name_desc" : string.Empty;
            ViewBag.PaidSort = sort == "PaidSort" ? "PaidSort_desc" : "PaidSort";
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;


            IQueryable<SupplyOrderPayments> supplyOrderPayments = _supplierService.GetAllSupplierOrderPayment();
            if (!string.IsNullOrEmpty(search))
            {
                supplyOrderPayments =   supplyOrderPayments.Where(e => e.Suppliers.SupplierName.StartsWith(search));
            }


            switch (sort)
            {
                case "name_desc":
                    supplyOrderPayments = supplyOrderPayments.OrderByDescending(e => e.Suppliers.SupplierName)
                        .ThenBy(e => e.Paid);

                    break;
                case "PaidSort_desc":
                    supplyOrderPayments = supplyOrderPayments.OrderByDescending(e => e.Paid)
                        .ThenBy(e => e.Suppliers.SupplierName);

                    break;
                case "PaidSort":
                    supplyOrderPayments = supplyOrderPayments.OrderBy(e => e.Paid)
                        .ThenBy(e => e.Suppliers.SupplierName);


                    break;


                default:
                    supplyOrderPayments = supplyOrderPayments.OrderBy(e => e.Suppliers.SupplierName)
                            .ThenBy(e => e.CreationDate)
                               .ThenBy(e => e.Paid);
                    break;
            }



            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View(supplyOrderPayments.ToPagedList(pageNumber, pageSize));

        }


        //***

        /// <summary>
        // GET: Admin/SupplyOrderPayments/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(int id)
        {

            SupplyOrderPayments supplyOrderPayments = await _db.SupplyOrderPayments.GetByIdAsync(id);
            if (supplyOrderPayments == null)
            {
                return HttpNotFound();
            }
            return View(supplyOrderPayments);
        }



        //***
        /// <summary>
        /// GET: Admen/SupplyOrderPayments/Create
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="supplierNo"></param>
        /// <returns></returns>
        public ActionResult Create(int? orderNo, int? supplierNo)
        {
            // this is the old code ...
            //IQueryable<SupplyOrders> SupplyOrder = db.SupplyOrder;
            //if (orderNo != null)
            //{
            //    SupplyOrder = SupplyOrder.Where(e => e.Id == orderNo);
            //}
            //IQueryable<Suppliers> suppliers = db.Suppliers;
            //if (supplierNo != null)
            //{
            //    suppliers = suppliers.Where(e => e.Id == supplierNo);
            //}
            //ViewBag.PopPoNo = new SelectList(SupplyOrder, "Id", "Id");
            //ViewBag.PopSupplierNo = new SelectList(suppliers, "Id", "SupplierName");

            // this is a refactoring for the old code .

            //var SupplyOrder=db.SupplyOrders.GetAll();

            //if (orderNo != null)
            //{
            //    SupplyOrder = SupplyOrder.Where(e => e.Id == orderNo);
            //}
            //var suppliers = db.Suppliers.GetAll();
            //if (supplierNo != null)
            //{
            //    suppliers = suppliers.Where(e => e.Id == supplierNo);
            //}
            //ViewBag.PopPoNo = new SelectList(SupplyOrder, "Id", "Id");
            //ViewBag.PopSupplierNo = new SelectList(suppliers, "Id", "SupplierName");

            // this is my handling ...
            // the reason to handel it this way is to reduce the overload on database by getting all data even if we don't need it 
            if (orderNo != null)
            {

                ViewBag.SupplyOrderId = new SelectList(_supplierService.GetSupplyOrdersById(orderNo), "Id", "Id");
            }
            else
            {
                ViewBag.SupplyOrderId = new SelectList(_db.SupplyOrders.GetAll(), "Id", "Id");
            }

            if (supplierNo != null)
            {


                ViewBag.SupplierId = new SelectList(_supplierService.GetSupplierById(supplierNo), "Id", "SupplierName");
            }
            else
            {
                ViewBag.SupplierId = new SelectList(_db.Suppliers.GetAll(), "Id", "SupplierName");
            }



            return View();
        }


        //***
        /// <summary>                                   
        /// POST: Admen/SupplyOrderPayments/Create    
        /// </summary>                                  
        /// <param name="SupplyOrderPayments"></param>
        /// <returns></returns>                                               
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SupplierId,Paid,SupplyOrderId,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplyOrderPayments SupplyOrderPayments)
        {
            if (ModelState.IsValid)
            {
                _db.SupplyOrderPayments.Add(SupplyOrderPayments);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PopPoNo = new SelectList(_db.SupplyOrders.GetAll(), "Id", "Id", SupplyOrderPayments.SupplyOrderId);
            ViewBag.PopSupplierNo = new SelectList(_db.Suppliers.GetAll(), "Id", "SupplierName", SupplyOrderPayments.SupplierId);
            return View(SupplyOrderPayments);
        }
        //***
        // GET: Admin/SupplyOrderPayments/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            SupplyOrderPayments supplyOrderPayments = await _db.SupplyOrderPayments.GetByIdAsync(id);
            if (supplyOrderPayments == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplyOrderId = new SelectList(_db.SupplyOrders.GetAll(), "Id", "Id", supplyOrderPayments.SupplyOrderId);
            ViewBag.SupplierId = new SelectList(_db.Suppliers.GetAll(), "Id", "SupplierName", supplyOrderPayments.SupplierId);
            return View(supplyOrderPayments);
        }
        //***
        // POST: Admen/SupplyOrderPayments/Edit/5
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SupplierId,SupplyOrderId,Paid,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplyOrderPayments SupplyOrderPayments)
        {
            if (ModelState.IsValid)
            {
                _db.SupplyOrderPayments.Update(SupplyOrderPayments);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PopPoNo = new SelectList(_db.SupplyOrders.GetAll(), "Id", "Id", SupplyOrderPayments.SupplyOrderId);
            ViewBag.PopSupplierNo = new SelectList(_db.Suppliers.GetAll(), "Id", "SupplierName", SupplyOrderPayments.SupplierId);
            return View(SupplyOrderPayments);
        }
        //***
        // GET: Admin/SupplyOrderPayments/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            var supplyOrderPayments = await _db.SupplyOrderPayments.GetByIdAsync(id);
            if (supplyOrderPayments == null)
            {
                return HttpNotFound();
            }
            return View(supplyOrderPayments);
        }
        //***
        // POST: Admin/SupplyOrderPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var supplyOrderPayments = await _db.SupplyOrderPayments.GetByIdAsync(id);
            _db.SupplyOrderPayments.Delete(supplyOrderPayments);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //***
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
