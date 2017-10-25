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
    public class KitProductsController : Controller
    {
      //  private StoreDbContext db = new StoreDbContext();

        private IKitProductsUnit _kitProductsUnit;

        public KitProductsController(IKitProductsUnit kitProductsUnit)
        {
            _kitProductsUnit = kitProductsUnit;

        }

        // GET: Admin/KitProducts
        public async Task<ActionResult> Index()
        {
            //IQueryable<KitProducts> kitproducts = db.KitProducts;
            return View(await _kitProductsUnit.GetAllKitProductses().ToListAsync());
        }

        // GET: Admin/KitProducts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitProducts kitProducts = await _kitProductsUnit.FindAsync(id);
            if (kitProducts == null)
            {
                return HttpNotFound();
            }
            return View(kitProducts);
        }

        // GET: Admin/KitProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KitProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ActiveFlag,KitName,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] KitProducts kitProducts)
        {
            if (ModelState.IsValid)
            {
                //db.KitProducts.Add(kitProducts);
                //await db.SaveChangesAsync();


                _kitProductsUnit.AddKitProduct(kitProducts);
                await _kitProductsUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(kitProducts);
        }

        // GET: Admin/KitProducts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitProducts kitProducts = await _kitProductsUnit.FindAsync(id);
            if (kitProducts == null)
            {
                return HttpNotFound();
            }
            return View(kitProducts);
        }

        // POST: Admin/KitProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ActiveFlag,KitName,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] KitProducts kitProducts)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(kitProducts).State = EntityState.Modified;


                _kitProductsUnit.UpdateKitProduct(kitProducts);
                await _kitProductsUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kitProducts);
        }

        // GET: Admin/KitProducts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitProducts kitProducts = await _kitProductsUnit.FindAsync(id);
            if (kitProducts == null)
            {
                return HttpNotFound();
            }
            return View(kitProducts);
        }

        // POST: Admin/KitProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           // KitProducts kitProducts = await _kitProductsUnit.FindAsync(id);
           _kitProductsUnit.DeleteKitProduct(id);
            await _kitProductsUnit.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _kitProductsUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
