using Elmatgar.persistence;
using System.Linq;
using System.Web.Http;

namespace Elmatgar.Controllers_api
{
    public class UserController : ApiController
    {
        private readonly StoreDbContext db = new StoreDbContext();
        // GET: api/User
        [HttpGet]
        [Route("api/User/GetCountry")]
        public IHttpActionResult GetCountry()
        {
            var country = db.Countries.Select(x=> new {x.Id,x.CnCountryName}).ToList();
            return Ok(country);

        }
        [HttpGet]
        //[Route("api/User/GetCity/id")]
        [ActionName("GetCity")]
        public IHttpActionResult GetCity(int id)
        {
            var Cities = db.Cities.Where(x => x.CtCountryId == id).Select(x=> new {x.Id,x.CtCityName}).ToList();
            return Ok(Cities);

        }

        [HttpGet]
        //[Route("api/User/GetAria/id")]
        [ActionName("GetAria")]
        public IHttpActionResult GetAria(int id)
        {
            var Zones = db.Zones.Where(x => x.ACityId == id).Select(x => new {x.Id,x.AAreaName }).ToList();
            return Ok(Zones);

        }

        //// GET: api/User/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
