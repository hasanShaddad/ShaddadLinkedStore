using System;
using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class BrandsController : Controller
    {
        // private StoreDbContext db = new StoreDbContext();



        private IBrandsUnit _brandsUnit;

        public BrandsController(IBrandsUnit brandsUnit)
        {
            _brandsUnit = brandsUnit;
        }

        // GET: Admin/Brands
        public async Task<ActionResult> Index( string sortOrder , string sortBy , string search=null)
        {

            ViewBag.sortOrder = sortOrder;
            ViewBag.sortBy = sortBy;



         //// IQueryable<Brands>   b = _brandsUnit.GetAllBrands();
         //// IQueryable<Brands>   b = _brandsUnit.GetAllBrands();


            IQueryable<Brands> brands = _brandsUnit.GetAllBrands();
            if (!String.IsNullOrEmpty(search))
            {
                brands =
                    brands.Where(
                        e =>
                            e.BrandName.ToLower().Contains(search.ToLower()) ||
                            e.CreatedBy.ToLower().Contains(search.ToLower()));
            }
            switch (sortBy)
            {
                case "BrandName":
                    switch (sortOrder)
                    {
                        case "Asc":
                            brands = brands.OrderBy(b => b.BrandName);
                            break;

                        case "Desc":
                            brands = brands.OrderByDescending(b => b.BrandName);
                            break;

                    }
                    break;

                case "CreationDate":


                    switch (sortOrder)
                    {
                        case "Asc":
                            brands = brands.OrderBy(b => b.CreationDate);
                            break;

                        case "Desc":
                            brands = brands.OrderByDescending(b => b.CreationDate);
                            break;

                    }
                    break;


                case "LastUpdateDate":
                    switch (sortOrder)
                    {
                        case "Asc":
                            brands = brands.OrderBy(b => b.LastUpdateDate);
                            break;

                        case "Desc":
                            brands = brands.OrderByDescending(b => b.LastUpdateDate);
                            break;

                    }
                    break;


                case "CreatedBy":
                    switch (sortOrder)
                    {
                        case "Asc":
                            brands = brands.OrderBy(b => b.CreatedBy);
                            break;

                        case "Desc":
                            brands = brands.OrderByDescending(b => b.CreatedBy);
                            break;

                    }
                    break;


                case "LastUpdatedBy":


                    switch (sortOrder)
                    {
                        case "Asc":
                            brands = brands.OrderBy(b => b.LastUpdatedBy);
                            break;

                        case "Desc":
                            brands = brands.OrderByDescending(b => b.LastUpdatedBy);
                            break;

                    }
                    break;

            }
            return View(await brands.ToListAsync());
        }

        // GET: Admin/Brands/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Brands brands = await _brandsUnit.FindAsync(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandName,SLBrandName")] Brands brands)
        {
            if (ModelState.IsValid)
            {
                // db.Brands.Add(brands);

                _brandsUnit.AddBrand(brands);
                /// await db.SaveChangesAsync();

                 _brandsUnit.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brands);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //  Brands brands = await db.Brands.FindAsync(id);

            Brands brands = await _brandsUnit.FindAsync(id);


            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandName,SLBrandName,CreationDate,CreatedBy")] Brands brands)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(brands).State = EntityState.Modified;
                _brandsUnit.UpdateBrand(brands);
                _brandsUnit.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brands);
        }

        // GET: Admin/Brands/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = await _brandsUnit.FindAsync(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Brands brands = await _brandsUnit.FindAsync(id);
            // db.Brands.Remove(brands);

            _brandsUnit.DeleteBrand(id);
            //  await db.SaveChangesAsync();

            await _brandsUnit.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _brandsUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
