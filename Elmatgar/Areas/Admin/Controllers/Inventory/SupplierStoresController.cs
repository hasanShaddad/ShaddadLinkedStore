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
    public class SupplierStoresController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly IAdressUnit AdressUnit;
        private readonly ISupplierUnit db;

        public SupplierStoresController(ISupplierUnit _db, ISupplierService supplierService, IAdressUnit _AdressUnit)
        {
            db = _db;
            AdressUnit = _AdressUnit;
            _supplierService = supplierService;
        }

        // GET: Admin/SupplierStores
        public ActionResult Index(string sort, string search, int? page)
        {// store then supplier then area 

            // supplier then store then area 
            // area then store then supplier
            ViewBag.SupplierStoreNameSort = string.IsNullOrEmpty(sort) ? "name_desc" : string.Empty;
            ViewBag.SupplierNameSort = sort == "SupplierName" ? "Supplier_desc" : "SupplierName";
            ViewBag.AreaNameSort = sort == " AreaName" ? "Area_desc" : "AreaName";
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
            string searchby = Request.QueryString["SearchBy"];

            IQueryable<SupplierStores> supplierStores = _supplierService.GetAllSupplierStores(true);
            if (!string.IsNullOrEmpty(search))
            {
                switch (searchby)
                {
                    case "StoreSearch":
                        supplierStores = supplierStores.Where(e => e.StoreName.StartsWith(search));
                        break;
                    case "SupplierSearch":
                        supplierStores = supplierStores.Where(e => e.Suppliers.SupplierName.StartsWith(search));
                        break;
                    case "AreaSearch":
                        supplierStores = supplierStores.Where(e => e.Zones.AAreaName.StartsWith(search));
                        break;
                }

            }


            switch (sort)
            {
                case "name_desc":
                    supplierStores = supplierStores.OrderByDescending(e => e.StoreName)
                        .ThenBy(e => e.Suppliers.SupplierName)
                        .ThenBy(x => x.Zones.AAreaName);

                    break;
                case "Supplier_desc":

                    supplierStores = supplierStores.OrderByDescending(e => e.Suppliers.SupplierName)
                      .ThenBy(e => e.StoreName)
                      .ThenBy(x => x.Zones.AAreaName);
                    break;
                case "Area_desc":

                    supplierStores = supplierStores.OrderByDescending(e => e.Zones.AAreaName)
                        .ThenBy(x => x.StoreName)
                      .ThenBy(e => e.Suppliers.SupplierName)
                      ;
                    break;
                case "SupplierName":

                    supplierStores = supplierStores.OrderBy(e => e.Suppliers.SupplierName)
                      .ThenBy(e => e.StoreName)
                      .ThenBy(x => x.Zones.AAreaName);
                    break;
                case "AreaName":

                    supplierStores = supplierStores.OrderBy(e => e.Zones.AAreaName)
                        .ThenBy(x => x.StoreName)
                      .ThenBy(e => e.Suppliers.SupplierName)
                      ;
                    break;

                default:
                    supplierStores = supplierStores.OrderBy(e => e.StoreName)
                                .ThenBy(e => e.Suppliers.SupplierName)
                        .ThenBy(x => x.Zones.AAreaName);
                    break;
            }



            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View(supplierStores.ToPagedList(pageNumber, pageSize));
            //  return Vie

        }

        public async Task<ActionResult> DeletedStoresIndex()
        {

            return View(await _supplierService.GetAllSupplierStores(false).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unlock(int id)
        {


            if (_supplierService.SupplierStoresDependanceyStatus(id, true, User.Identity.Name))
            {

                return RedirectToAction("Index");
            }

            return HttpNotFound();


        }
        // GET: Admin/SupplierStores/Details/5
        public async Task<ActionResult> Details(int id)
        {

            SupplierStores supplierStores = await db.SupplierStores.GetByIdAsync(id);
            if (supplierStores == null)
            {
                return HttpNotFound();
            }
            return View(supplierStores);
        }

        // GET: Admin/SupplierStores/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(AdressUnit.City.GetAll(), "Id", "CtCityName");
            ViewBag.CountryId = new SelectList(AdressUnit.Country.GetAll(), "Id", "CnCountryName");
            ViewBag.SupplierId = new SelectList(db.Suppliers.GetAll(), "Id", "SupplierName");
            ViewBag.AreaId = new SelectList(AdressUnit.Area.GetAll(), "Id", "AAreaName");
            return View();
        }

        // POST: Admin/SupplierStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SupplierId,StoreName,CountryId,CityId,AreaId,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplierStores supplierStores)
        {
            if (ModelState.IsValid)
            {
                supplierStores.CreatedBy = User.Identity.Name;
                supplierStores.CreationDate = DateTime.Now;
                db.SupplierStores.Add(supplierStores);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(AdressUnit.City.GetAll(), "Id", "CtCityName", supplierStores.CityId);
            ViewBag.CountryId = new SelectList(AdressUnit.Country.GetAll(), "Id", "CnCountryName", supplierStores.CountryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.GetAll(), "Id", "SupplierName", supplierStores.SupplierId);
            ViewBag.AreaId = new SelectList(AdressUnit.Area.GetAll(), "Id", "AAreaName", supplierStores.AreaId);
            return View(supplierStores);
        }
        // changing the viewbag variable names
        // GET: Admin/SupplierStores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierStores supplierStores = await db.SupplierStores.GetByIdAsync(id);
            if (supplierStores == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(AdressUnit.City.GetAll(), "Id", "CtCityName", supplierStores.CityId);
            ViewBag.CountryId = new SelectList(AdressUnit.Country.GetAll(), "Id", "CnCountryName", supplierStores.CountryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.GetAll(), "Id", "SupplierName", supplierStores.SupplierId);
            ViewBag.AreaId = new SelectList(AdressUnit.Area.GetAll(), "Id", "AAreaName", supplierStores.AreaId);
            return View(supplierStores);
        }

        // POST: Admin/SupplierStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SupplierId,StoreName,CountryId,CityId,AreaId,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] SupplierStores supplierStores)
        {
            if (ModelState.IsValid)
            {
                supplierStores.LastUpdateDate = DateTime.Now;
                supplierStores.LastUpdatedBy = User.Identity.Name;
                db.SupplierStores.Update(supplierStores);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(AdressUnit.City.GetAll(), "Id", "CtCityName", supplierStores.CityId);
            ViewBag.CountryId = new SelectList(AdressUnit.Country.GetAll(), "Id", "CnCountryName", supplierStores.CountryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers.GetAll(), "Id", "SupplierName", supplierStores.SupplierId);
            ViewBag.AreaId = new SelectList(AdressUnit.Area.GetAll(), "Id", "AAreaName", supplierStores.AreaId);
            return View(supplierStores);
        }

        // GET: Admin/SupplierStores/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            SupplierStores supplierStores = await db.SupplierStores.GetByIdAsync(id);
            if (supplierStores == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryTransCount = _supplierService.InventoryTransCountForSupplierStore(id);
            return View(supplierStores);
        }

        // POST: Admin/SupplierStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (_supplierService.SupplierStoresDependanceyStatus(id, false, User.Identity.Name))
            {
                return RedirectToAction("Index");

            }
            return HttpNotFound();

        }
        public ActionResult RestoreSupplierStore(int id)
        {

            if (_supplierService.SupplierStoresDependanceyStatus(id, true, User.Identity.Name))
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
    }
}
