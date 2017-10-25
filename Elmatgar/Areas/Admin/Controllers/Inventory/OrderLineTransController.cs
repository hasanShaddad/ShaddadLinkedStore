using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class OrderLineTransController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: Admin/OrderLineTrans
        public async Task<ActionResult> Index()
        {
            IQueryable<OrderLineTrans> orderLineTrans = db.OrderLineTrans.Include(o => o.OrderDetails);
            return View(await orderLineTrans.ToListAsync());
        }

        // GET: Admin/OrderLineTrans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLineTrans orderLineTrans = await db.OrderLineTrans.FindAsync(id);
            if (orderLineTrans == null)
            {
                return HttpNotFound();
            }
            return View(orderLineTrans);
        }

        // GET: Admin/OrderLineTrans/Create
        public ActionResult Create()
        {
            ViewBag.OltOrderLineNo = new SelectList(db.OrderDetails, "Id", "OrderStatus");
            return View();
        }

        // POST: Admin/OrderLineTrans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OltOrderLineNo,OltTransDate,OltStatus,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] OrderLineTrans orderLineTrans)
        {
            if (ModelState.IsValid)
            {
                db.OrderLineTrans.Add(orderLineTrans);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OltOrderLineNo = new SelectList(db.OrderDetails, "Id", "OrderStatus", orderLineTrans.OltOrderLineNo);
            return View(orderLineTrans);
        }

        // GET: Admin/OrderLineTrans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLineTrans orderLineTrans = await db.OrderLineTrans.FindAsync(id);
            if (orderLineTrans == null)
            {
                return HttpNotFound();
            }
            ViewBag.OltOrderLineNo = new SelectList(db.OrderDetails, "Id", "OrderStatus", orderLineTrans.OltOrderLineNo);
            return View(orderLineTrans);
        }

        // POST: Admin/OrderLineTrans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OltOrderLineNo,OltTransDate,OltStatus,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] OrderLineTrans orderLineTrans)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderLineTrans).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OltOrderLineNo = new SelectList(db.OrderDetails, "Id", "OrderStatus", orderLineTrans.OltOrderLineNo);
            return View(orderLineTrans);
        }

        // GET: Admin/OrderLineTrans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLineTrans orderLineTrans = await db.OrderLineTrans.FindAsync(id);
            if (orderLineTrans == null)
            {
                return HttpNotFound();
            }
            return View(orderLineTrans);
        }

        // POST: Admin/OrderLineTrans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderLineTrans orderLineTrans = await db.OrderLineTrans.FindAsync(id);
            db.OrderLineTrans.Remove(orderLineTrans);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
