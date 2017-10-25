
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;

namespace Elmatgar.Controllers
{
    public class DropListController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        /// <summary>
        /// fill DropList by list of cities - hassan shaddad
        /// </summary>
        /// <param name="state">country id</param>
        /// <returns>Json result  List of Cities</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FillCity(int state)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(c => c.CtCountryId == state);
            if (!cities.Any())
            {
                Cities city = new Cities
                {
                    Id = 99999,
                    CtCityName = "nothing to select"
                };
                List<Cities> cityGroup = new List<Cities>();
                cityGroup.Add(city);
                return Json(cityGroup, JsonRequestBehavior.AllowGet);
            }
            return Json(cities, JsonRequestBehavior.AllowGet);
            //return this.Json(string.Empty);
        }
        /// <summary>
        /// fill DropList by list of Areas - hassan shaddad
        /// </summary>
        /// <param name="State">city id</param>
        /// <returns>Json result  List of areas</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FillArea(int state)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var areas = db.Zones.Where(c => c.ACityId == state);
            if (!areas.Any())
            {
                Zones area = new Zones
                {
                    Id = 99999,
                    AAreaName = "nothing to select"
                };

                List<Zones> areasGroup = new List<Zones>();
                areasGroup.Add(area);

                return Json(areasGroup, JsonRequestBehavior.AllowGet);
            }
            return Json(areas, JsonRequestBehavior.AllowGet);
            //return this.Json(string.Empty);
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