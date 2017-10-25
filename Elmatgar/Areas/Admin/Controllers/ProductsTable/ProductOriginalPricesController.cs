using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Elmatgar.persistence;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class ProductOriginalPricesController : Controller
    {
        // private StoreDbContext db = new StoreDbContext();


        private IProductOriginalPricesUnit _productOriginalPricesUnit;

        public ProductOriginalPricesController(IProductOriginalPricesUnit productOriginalPricesUnit)
        {
            _productOriginalPricesUnit = productOriginalPricesUnit;
        }

        // GET: Admin/ProductOriginalPrices
        public async Task<ActionResult> Index()
        {
            // IQueryable<ProductOriginalPrices> productOriginalPrices =
            //  db.ProductOriginalPrices.Include(p => p.Countries).Include(p => p.Products);



            ;
            return View(await _productOriginalPricesUnit.ProductOriginalPricesRepository.GetAll()
                .Include(p => p.Products)
                .Include(p => p.Countries).ToListAsync());
        }

        // GET: Admin/ProductOriginalPrices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOriginalPrices productOriginalPrices = await _productOriginalPricesUnit.FindAsync(id);
            if (productOriginalPrices == null)
            {
                return HttpNotFound();
            }
            return View(productOriginalPrices);
        }

        // GET: Admin/ProductOriginalPrices/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ProductId = id != null
                ? new SelectList(_productOriginalPricesUnit.GetAllProducts(), "Id", "Name", id)
                : new SelectList(_productOriginalPricesUnit.GetAllProducts(), "Id", "Name");
            ViewBag.done = "";


            ViewBag.CountryId = new SelectList(_productOriginalPricesUnit.GetAllCountrieses(), "Id", "CnCountryName");

            return PartialView("_CreatePrice");
        }

        // POST: Admin/ProductOriginalPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(
            [Bind(Include = "OriginalPrice,CountryId,ActiveFlag,ProductId")] ProductOriginalPrices
                productOriginalPrices)
        {

            productOriginalPrices.FromDate = DateTime.Now;
            if (ModelState.IsValid)
            {

                //  var existingRecord = db.ProductOriginalPrices.Count(a => a.ProductId == productOriginalPrices.ProductId);


                if (_productOriginalPricesUnit.ProductOriginalPriceCount(productOriginalPrices.ProductId) == 0)
                {

                    // db.ProductOriginalPrices.Add(productOriginalPrices);

                    _productOriginalPricesUnit.AddProductOriginalPrices(productOriginalPrices);
                    await _productOriginalPricesUnit.SaveChangesAsync();
                    ViewBag.CountryId = new SelectList(_productOriginalPricesUnit.GetAllCountrieses(), "Id", "CnCountryName",
                        productOriginalPrices.CountryId);
                    ViewBag.ProductId = new SelectList(_productOriginalPricesUnit.GetAllProducts(), "Id", "Name", productOriginalPrices.ProductId);
                    ViewBag.done = "you just added price price!";
                    return PartialView("_CreatePrice");
                }
                else
                {
                    try
                    {
                        ProductOriginalPrices priceId = new ProductOriginalPrices();
                        priceId =
                            _productOriginalPricesUnit.GetFirstProductOriginalPrices(productOriginalPrices.ProductId);
                        //  db.ProductOriginalPrices.FirstOrDefault(e => e.ProductId == productOriginalPrices.ProductId);

                        if (priceId != null) priceId.OriginalPrice = productOriginalPrices.OriginalPrice;
                      //  productOriginalPrices.Id = priceId.Id;

                        // db.Entry(priceId).State = EntityState.Modified;

                        _productOriginalPricesUnit.UpdateProductOriginalPrices(priceId);
                        await _productOriginalPricesUnit.SaveChangesAsync();

                        ModelState.AddModelError("OriginalPrice", "you updated the old price");
                    }
                    catch (Exception exception  ) // catches all exceptions
                    {
                        Console.Write(exception.Message);


                        ViewBag.CountryId = new SelectList(_productOriginalPricesUnit.GetAllCountrieses(), "Id", "CnCountryName", productOriginalPrices.CountryId);
                        ViewBag.ProductId = new SelectList(_productOriginalPricesUnit.GetAllProducts(), "Id", "Name", productOriginalPrices.ProductId);

                        return PartialView("_CreatePrice");
                    }

                }
            }

            ViewBag.CountryId = new SelectList(_productOriginalPricesUnit.GetAllCountrieses(), "Id", "CnCountryName", productOriginalPrices.CountryId);
            ViewBag.ProductId = new SelectList(_productOriginalPricesUnit.GetAllProducts(), "Id", "Name", productOriginalPrices.ProductId);
            return PartialView("_CreatePrice");
        }

        // GET: Admin/ProductOriginalPrices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOriginalPrices productOriginalPrices = await _productOriginalPricesUnit.FindAsync(id);
            if (productOriginalPrices == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(_productOriginalPricesUnit.GetAllCountrieses(), "Id", "CnCountryName", productOriginalPrices.CountryId);
            ViewBag.ProductId = new SelectList(_productOriginalPricesUnit.GetAllProducts(), "Id", "Name", productOriginalPrices.ProductId);
            return View(productOriginalPrices);
        }

        // POST: Admin/ProductOriginalPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FromDate,OriginalPrice,CountryId,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductOriginalPrices productOriginalPrices)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(productOriginalPrices).State = EntityState.Modified;
                _productOriginalPricesUnit.UpdateProductOriginalPrices(productOriginalPrices);


                await _productOriginalPricesUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(_productOriginalPricesUnit.GetAllCountrieses(), "Id", "CnCountryName", productOriginalPrices.CountryId);
            ViewBag.ProductId = new SelectList(_productOriginalPricesUnit.GetAllProducts(), "Id", "Name", productOriginalPrices.ProductId);
            return View(productOriginalPrices);
        }

        // GET: Admin/ProductOriginalPrices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOriginalPrices productOriginalPrices = await _productOriginalPricesUnit.FindAsync(id);




            if (productOriginalPrices == null)
            {
                return HttpNotFound();
            }
            return View(productOriginalPrices);
        }

        // POST: Admin/ProductOriginalPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //   ProductOriginalPrices productOriginalPrices = await db.ProductOriginalPrices.FindAsync(id);
            _productOriginalPricesUnit.DeleteProductOriginalPrices(id);
            await _productOriginalPricesUnit.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productOriginalPricesUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
