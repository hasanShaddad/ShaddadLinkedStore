using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Elmatgar.persistence;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class ProductPromotionsController : Controller
    {
       // private StoreDbContext db = new StoreDbContext();



        private IProductPromotionsUnit _productPromotionsUnit;

        public ProductPromotionsController(IProductPromotionsUnit productPromotionsUnit)
        {
            _productPromotionsUnit = productPromotionsUnit;
        }

        // GET: Admin/ProductPromotions
        public async Task<ActionResult> Index()
        {
            //IQueryable<ProductPromotions> productPromotions = db.ProductPromotions.Include(p => p.Countries).Include(p => p.Products);
            return View(await _productPromotionsUnit.GetAllProductPromotions().Include(p=>p.Products).Include(p=>p.Countries).ToListAsync());
        }

        // GET: Admin/ProductPromotions/Details/5
        public  ActionResult  Details(int id  )
        {
            if (id >0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPromotions productPromotions =  _productPromotionsUnit.GetFirstProductPromotions(id  );
            if (productPromotions == null)
            {
                return HttpNotFound();
            }
            return View(productPromotions);
        }

        // GET: Admin/ProductPromotions/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(_productPromotionsUnit.GetAllCountrieses(), "Id", "CnCountryName");
            ViewBag.ProductId = new SelectList(_productPromotionsUnit.GetAllProducts(), "Id", "Name");
            return View();
        }

        // POST: Admin/ProductPromotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FromDate,EndDate,PromoPrice,CountryId,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductPromotions productPromotions)
        {
            if (ModelState.IsValid)
            {
               // db.ProductPromotions.Add(productPromotions);
                


                _productPromotionsUnit.AddProductPromotions(productPromotions);
                await _productPromotionsUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(_productPromotionsUnit.GetAllCountrieses(), "Id", "CnCountryName", productPromotions.CountryId);
            ViewBag.ProductId = new SelectList(_productPromotionsUnit.GetAllProducts(), "Id", "Name", productPromotions.ProductId);
            return View(productPromotions);
        }

        // GET: Admin/ProductPromotions/Edit/5
        public  ActionResult  Edit(int id )
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ProductPromotions productPromotions =  _productPromotionsUnit.GetFirstProductPromotions(id);
            if (productPromotions == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(_productPromotionsUnit.GetAllCountrieses(), "Id", "CnCountryName", productPromotions.CountryId);
            ViewBag.ProductId = new SelectList(_productPromotionsUnit.GetAllProducts(), "Id", "Name", productPromotions.ProductId);
            return View(productPromotions);
        }

        // POST: Admin/ProductPromotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FromDate,EndDate,PromoPrice,CountryId,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductPromotions productPromotions)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(productPromotions).State = EntityState.Modified;


                _productPromotionsUnit.UpdateProductPromotions(productPromotions);
                await _productPromotionsUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(_productPromotionsUnit.GetAllCountrieses(), "Id", "CnCountryName", productPromotions.CountryId);
            ViewBag.ProductId = new SelectList(_productPromotionsUnit.GetAllProducts(), "Id", "Name", productPromotions.ProductId);
            return View(productPromotions);
        }

        // GET: Admin/ProductPromotions/Delete/5
        public ActionResult  Delete(int id)
        {
            if (id<0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ProductPromotions productPromotions =  _productPromotionsUnit.GetFirstProductPromotions(id);



            if (productPromotions == null)
            {
                return HttpNotFound();
            }
            return View(productPromotions);
        }

        // POST: Admin/ProductPromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            
            ProductPromotions productPromotions =  _productPromotionsUnit.GetFirstProductPromotions(id);
            _productPromotionsUnit.DeleteProductPromotions(productPromotions);
            await _productPromotionsUnit.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        


            
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productPromotionsUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
