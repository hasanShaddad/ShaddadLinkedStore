
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
namespace Elmatgar.Controllers
{
    /// <summary>
    ///  CategMenue for cascading categories menu to use in layout top header - hassan shaddad
    /// </summary>
    public class CategMenueController : Controller
    {
        /// <summary>
        /// service in elmatgar.servicse project to get listOfNodes of categories
        /// </summary>
        private readonly ICategoryHomeService _categoryHomeService ;

        public CategMenueController(ICategoryHomeService categoryHomeService)
        {
            _categoryHomeService = categoryHomeService;
        }


      /// <summary>
      /// Get categories to menue for first load when category id == null
      /// </summary>
      /// <returns></returns>
        public ActionResult Index()
        {
            IList<Categories> lestOfNoods = _categoryHomeService.GetListOfNodeForMenu(null);
            IList<Categories> topLevelCategorieses = TreeHelper.ConvertToForest(lestOfNoods);
            return PartialView(topLevelCategorieses.ToList());
        }
        /// <summary>
        ///  Get categories to menue when category id ==  not null
        /// </summary>
        /// <param name="categId"></param>
        /// <returns></returns>
        public ActionResult CategForHome(int?categId)
        {
            if (categId > 0)
            {
                IList<Categories> lestOfNoods = _categoryHomeService.GetListOfNodeForMenu(categId);

                return PartialView("CategForHome", lestOfNoods);
            }
            else
            {
                IList<Categories> lestOfNoods = _categoryHomeService.GetListOfNodeForMenu(null);
                IList<Categories> topLevelCategorieses = TreeHelper.ConvertToForest(lestOfNoods);
                return PartialView("CategForHome", topLevelCategorieses);
            }
        }

      

    }

}