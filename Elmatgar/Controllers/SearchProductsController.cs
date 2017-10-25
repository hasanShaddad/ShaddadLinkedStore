using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmatgar.persistence;

namespace Elmatgar.Controllers
{
    public class SearchProductsController : Controller
    {
        StoreDbContext _db =new StoreDbContext();
        // GET: SearchProducts
        public ActionResult Index()
        {
      
            return View();
        }
    }
}