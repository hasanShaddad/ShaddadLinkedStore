using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;

namespace Elmatgar.Areas.Admin.Controllers.BaseControllers
{
    public class ZonesController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: Admin/Zones
        public ActionResult Index()
        {
            IQueryable<Zones> areas = db.Zones.Include(a => a.Cities);
            return View(areas.ToList());
        }

        // GET: Admin/Zones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Zones areas = db.Zones.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // GET: Admin/Zones/Create
        public ActionResult Create()
        {
            ViewBag.ACityId = new SelectList(db.Cities, "Id", "CtCityName");
            return View();
        }

        // POST: Admin/Zones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ACityId,AAreaName,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Zones areas)
        {
            if (ModelState.IsValid)
            {
                db.Zones.Add(areas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ACityId = new SelectList(db.Cities, "Id", "CtCityName", areas.ACityId);
            return View(areas);
        }

        // GET: Admin/Zones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zones areas = db.Zones.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACityId = new SelectList(db.Cities, "Id", "CtCityName", areas.ACityId);
            return View(areas);
        }

        // POST: Admin/Zones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ACityId,AAreaName,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Zones areas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACityId = new SelectList(db.Cities, "Id", "CtCityName", areas.ACityId);
            return  View(areas);
        }

        // GET: Admin/Zones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zones areas = db.Zones.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return  View(areas);
        }

        // POST: Admin/Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zones areas = db.Zones.Find(id);
            db.Zones.Remove(areas);
            db.SaveChanges();
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
