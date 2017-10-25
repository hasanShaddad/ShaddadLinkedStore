using System.Web.Mvc;
using Elmatgar.Core.Services;


namespace Elmatgar.Controllers
{
    public class HomeCategoryController : Controller
    {
      

        private readonly ICategoryServices _categoryService;

        public HomeCategoryController(ICategoryServices categoryService)
        {

            _categoryService = categoryService;



        }
        /// <summary>
        /// category for side menu -hassan shaddad
        /// </summary>
        /// <returns></returns>
        public ActionResult _Index()
        {


            string fullString = _categoryService.CreateCategories();
            return PartialView((object)fullString);
        }






    }
}
 