using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Elmatgar.persistence;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class ProductVotesController : Controller
    {
      //  private StoreDbContext db = new StoreDbContext();

        private IProductVoteUnit _productVoteUnit;


        public ProductVotesController(IProductVoteUnit productVoteUnit )
        {
            _productVoteUnit = productVoteUnit;

        }

        // GET: Admin/ProductVotes
        public async Task<ActionResult> Index()
        {
           // var productVotes = db.ProductVotes.Include(p => p.Products);
            return View(await _productVoteUnit.GetAllProductVotees().Include(p=> p.Products).ToListAsync());
        }

        // GET: Admin/ProductVotes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductVote productVote = await _productVoteUnit.FindAsync(id);
            if (productVote == null)
            {
                return HttpNotFound();
            }
            return View(productVote);
        }

        // GET: Admin/ProductVotes/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_productVoteUnit.GetAllProducts(), "Id", "Name");
            return View();
        }

        // POST: Admin/ProductVotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,VoteValue,UserId,ProductId,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] ProductVote productVote)
        {
            if (ModelState.IsValid)
            {
                _productVoteUnit.AddProductVote(productVote);

                await _productVoteUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_productVoteUnit.GetAllProducts() , "Id", "Name", productVote.ProductId);
            return View(productVote);
        }

        // GET: Admin/ProductVotes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductVote productVote = await _productVoteUnit.FindAsync(id);
            if (productVote == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_productVoteUnit.GetAllProducts() , "Id", "Name", productVote.ProductId);
            return View(productVote);
        }

        // POST: Admin/ProductVotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,VoteValue,UserId,ProductId,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] ProductVote productVote)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(productVote).State = EntityState.Modified;


                _productVoteUnit.UpdateProductVote(productVote);
                await _productVoteUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_productVoteUnit.GetAllProducts() , "Id", "Name", productVote.ProductId);
            return View(productVote);
        }

        // GET: Admin/ProductVotes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductVote productVote = await _productVoteUnit.FindAsync(id);
            if (productVote == null)
            {
                return HttpNotFound();
            }
            return View(productVote);
        }

        // POST: Admin/ProductVotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           // ProductVote productVote = await _productVoteUnit.FindAsync(id);
            _productVoteUnit.DeleteProductVote( id);
            await _productVoteUnit .SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productVoteUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
