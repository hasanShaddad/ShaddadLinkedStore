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
    public class DiscountRulesController : Controller
    {
       // private StoreDbContext db = new StoreDbContext();

        private IDiscountRulesUnit _discountRulesUnit;

        public DiscountRulesController( IDiscountRulesUnit discountRulesUnit)
        {
            _discountRulesUnit = discountRulesUnit;

        }

        // GET: Admen/DiscountRules
        public async Task<ActionResult> Index()
        {
           // IQueryable<DiscountRules> discountRules = db.DiscountRules.Include(d => d.Products);
            return View(await _discountRulesUnit.GetAllDiscount().ToListAsync());
        }

        // GET: Admin/DiscountRules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountRules discountRules = await _discountRulesUnit.FindAsync(id);
            if (discountRules == null)
            {
                return HttpNotFound();
            }
            return View(discountRules);
        }
        //todo we have not to get all products
        // GET: Admin/DiscountRules/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_discountRulesUnit.GetAllProducts(), "Id", "Name");
            return View();
        }

        // POST: Admin/DiscountRules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FromDate,EndDate,DrQty,DiscountPercentage,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] DiscountRules discountRules)
        {
            if (ModelState.IsValid)
            {
               // db.DiscountRules.Add(discountRules);


                _discountRulesUnit.DiscountRepository.Add(discountRules);
                await _discountRulesUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_discountRulesUnit.GetAllProducts(), "Id", "Name", discountRules.ProductId);
            return View(discountRules);
        }

        // GET: Admin/DiscountRules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountRules discountRules = await _discountRulesUnit.FindAsync(id);
            if (discountRules == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_discountRulesUnit.GetAllProducts(), "Id", "Name", discountRules.ProductId);
            return View(discountRules);
        }

        // POST: Admin/DiscountRules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FromDate,EndDate,DrQty,DiscountPercentage,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] DiscountRules discountRules)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(discountRules).State = EntityState.Modified;


                _discountRulesUnit.UpdateDiscount(discountRules);
                await _discountRulesUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_discountRulesUnit.GetAllProducts(), "Id", "Name", discountRules.ProductId);
            return View(discountRules);
        }

        // GET: Admin/DiscountRules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountRules discountRules = await _discountRulesUnit.FindAsync(id);
            if (discountRules == null)
            {
                return HttpNotFound();
            }
            return View(discountRules);
        }

        // POST: Admin/DiscountRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //DiscountRules discountRules = await _discountRulesUnit.FindAsync(id);
            _discountRulesUnit.DeleteDiscount(id);
            await _discountRulesUnit.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _discountRulesUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
