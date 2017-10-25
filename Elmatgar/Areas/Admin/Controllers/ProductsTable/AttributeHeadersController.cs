using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Elmatgar.persistence;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class AttributeHeadersController : Controller
    {
        //private StoreDbContext db = new StoreDbContext();



        private IAttributeHeadersUnit _attributeHeadersUnit;

        public AttributeHeadersController(IAttributeHeadersUnit attributeHeadersUnit)
        {
            _attributeHeadersUnit = attributeHeadersUnit;
        }

        // GET: Admin/AttributeHeaders
        public async Task<ActionResult> Index()
        {
           // IQueryable<AttributeHeaders> attributeHeaderses = db.AttributeHeaders;
            return View(await _attributeHeadersUnit.GetAllAttributeHeaders().ToListAsync());
        }

        // GET: Admin/AttributeHeaders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeHeaders attributeHeaders = await _attributeHeadersUnit.FindAsync(id);
            if (attributeHeaders == null)
            {
                return HttpNotFound();
            }
            return View(attributeHeaders);
        }

        // GET: Admin/AttributeHeaders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AttributeHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AttributeName,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] AttributeHeaders attributeHeaders)
        {
            if (ModelState.IsValid)
            {

               // var existingRecord = db.AttributeHeaders.Count(a => a.AttributeName == attributeHeaders.AttributeName);
                if (_attributeHeadersUnit.GetAttributeHeaderCount(attributeHeaders.AttributeName) ==0)
                {
                   // db.AttributeHeaders.Add(attributeHeaders);
                   // await db.SaveChangesAsync();


                    _attributeHeadersUnit.AddAttributeHeader(attributeHeaders);
                   await _attributeHeadersUnit.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "AttributeName  is exist");
                }

            }

            return View(attributeHeaders);
        }

        // GET: Admin/AttributeHeaders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeHeaders attributeHeaders = await _attributeHeadersUnit.FindAsync(id);
            if (attributeHeaders == null)
            {
                return HttpNotFound();
            }
            return View(attributeHeaders);
        }

        // POST: Admin/AttributeHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,AttributeName")] AttributeHeaders attributeHeaders)
        {
            if (ModelState.IsValid)
            {

                _attributeHeadersUnit.UpdateAttributeHeader(attributeHeaders);
               // db.Entry(attributeHeaders).State = EntityState.Modified;
                await _attributeHeadersUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attributeHeaders);
        }

        // GET: Admin/AttributeHeaders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeHeaders attributeHeaders = await _attributeHeadersUnit.FindAsync(id);
            if (attributeHeaders == null)
            {
                return HttpNotFound();
            }
            return View(attributeHeaders);
        }

        // POST: Admin/AttributeHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           // AttributeHeaders attributeHeaders = await _attributeHeadersUnit.FindAsync(id);

            _attributeHeadersUnit.DeleteAttributeHeader(id);
           // db.AttributeHeaders.Remove(attributeHeaders);
            await _attributeHeadersUnit.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _attributeHeadersUnit .Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
