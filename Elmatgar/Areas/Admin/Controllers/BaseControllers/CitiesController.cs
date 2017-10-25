using System.Linq;
using System.Net;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Units;

namespace Elmatgar.Areas.Admin.Controllers.BaseControllers
{
    public class CitiesController : Controller
    {
        readonly IAdressUnit _db;
        public CitiesController(IAdressUnit db)
        {
            _db = db;
        }

        // GET: Admin/Cities
        public ActionResult Index()
        {
            IQueryable<Cities> cities = _db.CitiesesJoincountries;
            return View(cities.ToList());
        }

        // GET: Admin/Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = _db.City.GetById(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            return View(cities);
        }

        // GET: Admin/Cities/Create
        public ActionResult Create()
        {
            ViewBag.CtCountryId = new SelectList(_db.Country.GetAll(), "Id", "CnCountryName");
            return View();
        }

        // POST: Admin/Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CtCityName,CtCountryId,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                _db.City.Add(cities);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CtCountryId = new SelectList(_db.Country.GetAll(), "Id", "CnCountryName", cities.CtCountryId);
            return View(cities);
        }

        // GET: Admin/Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = _db.City.GetById(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            ViewBag.CtCountryId = new SelectList(_db.Country.GetAll(), "Id", "CnCountryName", cities.CtCountryId);
            return View(cities);
        }

        // POST: Admin/Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CtCityName,CtCountryId,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Cities cities)
        {
            if (ModelState.IsValid)
            {
                _db.City.Update(cities);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CtCountryId = new SelectList(_db.Country.GetAll(), "Id", "CnCountryName", cities.CtCountryId);
            return View(cities);
        }

        // GET: Admin/Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cities cities = _db.City.GetById(id);
            if (cities == null)
            {
                return HttpNotFound();
            }
            return View(cities);
        }

        // POST: Admin/Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cities cities = _db.City.GetById(id);
            _db.City.Delete(cities);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
