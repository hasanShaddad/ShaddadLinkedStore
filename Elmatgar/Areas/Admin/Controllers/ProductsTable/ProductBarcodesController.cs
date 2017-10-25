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
    public class ProductBarcodesController : Controller
    {
       // private StoreDbContext db = new StoreDbContext();

        private IProductBarCodeUnit _productBarCodeUnit;
        public ProductBarcodesController( IProductBarCodeUnit productBarCodeUnit)
        {
            _productBarCodeUnit = productBarCodeUnit;

        }

        // GET: Admen/ProductBarcodes
        public async Task<ActionResult> Index()
        {
           // IQueryable<ProductBarcodes> productBarcodes = db.ProductBarcodes.Include(p => p.Products);
            return View(await _productBarCodeUnit.GetAllProductBarcodes().ToListAsync());
        }

        // GET: Admen/ProductBarcodes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBarcodes productBarcodes = await _productBarCodeUnit.FindAsync(id);
            if (productBarcodes == null)
            {
                return HttpNotFound();
            }
            return View(productBarcodes);
        }

        // GET: Admin/ProductBarcodes/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_productBarCodeUnit.GetAllProducts(), "Id", "Name");
            return View();
        }

        // POST: Admin/ProductBarcodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,BarcodeNo,Barcode,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductBarcodes productBarcodes)
        {
            if (ModelState.IsValid)
            {
               // db.ProductBarcodes.Add(productBarcodes);

                _productBarCodeUnit.AddProductBarcodes(productBarcodes);


                await _productBarCodeUnit.SaveChangesAsync();


                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_productBarCodeUnit.GetAllProducts(), "Id", "Name", productBarcodes.ProductId);
            return View(productBarcodes);
        }

        // GET: Admin/ProductBarcodes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBarcodes productBarcodes = await _productBarCodeUnit.FindAsync(id);
            if (productBarcodes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_productBarCodeUnit.GetAllProducts(), "Id", "Name", productBarcodes.ProductId);
            return View(productBarcodes);
        }

        // POST: Admin/ProductBarcodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,BarcodeNo,Barcode,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductBarcodes productBarcodes)
        {
            if (ModelState.IsValid)
            {
              //  db.Entry(productBarcodes).State = EntityState.Modified;

                _productBarCodeUnit.UpdateProductBarcodes(productBarcodes);
                await _productBarCodeUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_productBarCodeUnit.GetAllProducts(), "Id", "Name", productBarcodes.ProductId);
            return View(productBarcodes);
        }

        // GET: Admin/ProductBarcodes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBarcodes productBarcodes = await _productBarCodeUnit.FindAsync(id);
            if (productBarcodes == null)
            {
                return HttpNotFound();
            }
            return View(productBarcodes);
        }

        // POST: Admin/ProductBarcodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
          //  ProductBarcodes productBarcodes = await _productBarCodeUnit.FindAsync(id);
          //  db.ProductBarcodes.Remove(productBarcodes);

            _productBarCodeUnit.DeleteProductBarcodes(id);
            await _productBarCodeUnit.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productBarCodeUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
