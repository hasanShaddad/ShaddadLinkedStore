using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.BaseControllers
{
    public class CountriesController : Controller
    {
        readonly IAdressUnit _db;
        public CountriesController(IAdressUnit db)
        {
            _db = db;
        }



        // GET: Admen/Countries
        public ActionResult Index()
        {
            IQueryable<Countries> countrieses = _db.Country.GetAll();

        return View(countrieses.ToList());
        }

        // GET: Admen/Countries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = _db.Country.GetById(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // GET: Admen/Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admen/Countries/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CnCountryName,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                string error = _db.Country.AddWhenNew((Countries e) => e.CnCountryName == countries.CnCountryName, countries);
                if (string.IsNullOrEmpty(error))
                {

                    _db.Country.Add(countries);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", error);
                }


            }

            return View(countries);
        }

        // GET: Admen/Countries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = _db.Country.GetById(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // POST: Admen/Countries/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CnCountryName,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                _db.Country.Update(countries);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countries);
        }

        // GET: Admen/Countries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = _db.Country.GetById(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        // POST: Admen/Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Countries countries = _db.Country.GetById(id);
            _db.Country.Delete(countries);
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
