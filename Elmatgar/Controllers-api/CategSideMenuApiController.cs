using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elmatgar.Core.Services;
using Elmatgar.persistence;

namespace Elmatgar.Controllers_api
{
    public class CategSideMenuApiController : ApiController
    {

        

        private StoreDbContext _StoreDbContext = new StoreDbContext();
         

        
        public CategSideMenuApiController( )
        {
            
        }


        // GET api/<controller>
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;
          var categ= _StoreDbContext.Products.ToList();
            if (categ.Count > 0)
            {
                ret = Ok(categ);
            }
            else
            {
                ret = NotFound();
            }
            return ret;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}