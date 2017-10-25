
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.Core.ViewModels;

using Elmatgar.ViewModels;
using PagedList;

namespace Elmatgar.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductUnit _productUnit;
        private readonly IBrandsUnit _brandsUnit;
        private readonly IProductsForHomeService _productsForHomeService;
        private readonly IProductDetailsService _productDetailsService;
        private readonly ICategoryHomeService _pCategoryHomeService;
        public HomeController(IProductDetailsService productDetailsService, ICategoryHomeService pCategoryHomeService, IProductsForHomeService productsForHomeService, IBrandsUnit brandsUnit, IProductUnit productUnit, IProductAttributeUnit productAttributeUnit, IProductOriginalPricesUnit productOriginalPricesUnit)
        {
            _productDetailsService = productDetailsService;
            this._pCategoryHomeService = pCategoryHomeService;
            _productsForHomeService = productsForHomeService;
            _brandsUnit = brandsUnit;
            _productUnit = productUnit;

        }
        /// <summary>
        /// get list of brands for side bar in layout view -hassan shaddad
        /// </summary>
        /// <param name="categId">id of category</param>
        /// <returns>PartialView("BrandForHome", brands)</returns>
        public ActionResult BrandForHome(int? categId)
        {

            List<Brands> brands = _brandsUnit.GetAllBrands().ToList();

            return PartialView("BrandForHome", brands);


        }
        /// <summary>
        /// get category for categories menu for layout view top header
        /// </summary>
        /// <param name="categId"> int? ctegory Id </param>
        /// <returns> PartialView("CategForHome", _pCategoryHomeService.GetCategoriese(categId))</returns>
        public ActionResult CategForHome(int? categId)
        {

            return PartialView("CategForHome", _pCategoryHomeService.GetCategoriese(categId));


        }


        /// <summary>
        /// list of Categories for mobile edition - it is not used now
        /// </summary>
        /// <param name="categId">int? ctegory Id</param>
        /// <returns>PartialView("CategForHomeMob", _pCategoryHomeService.GetCategoriese(categId))</returns>
        public ActionResult CategForHomeMob(int? categId)
        {
            return PartialView("CategForHomeMob", _pCategoryHomeService.GetCategoriese(categId));

        }
        /// <summary>
        /// list of Categories for layout view Side Menu
        /// </summary>
        /// <param name="categId">int? ctegory Id </param>
        /// <returns>PartialView("CategForSideMenu", _pCategoryHomeService.GetCategoriese(categId))</returns>
        public ActionResult CategForSideMenu(int? categId)
        {
            return PartialView("CategForSideMenu", _pCategoryHomeService.GetCategoriese(categId));

        }
        /// <summary>
        ///list of top categories only for index  
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View("Index", _pCategoryHomeService.GetTopCategOnly());
        }
        /// <summary>
        /// to set the languge ar or en using  HttpCookie(langCookie)
        /// look at ~/global.aspx to see how use the coockie results
        /// </summary>
        /// <param name="lang">string lang (ar-SA) or (en-US)</param>
        /// <returns></returns>
        public ActionResult SetLang(string lang)
        {
            ViewBag.lang = "ar-SA";
            if (lang == "arabic")
            {
                HttpCookie langCookie = new HttpCookie("language") { Value = "ar-SA" };
                Response.Cookies.Add(langCookie);
                ViewBag.lang = "ar-SA";
            }
            else
            {
                HttpCookie langCookie = new HttpCookie("language") { Value = "en-US" };
                Response.Cookies.Add(langCookie);
                ViewBag.lang = "en-US";
            }


            return RedirectToAction("Index");
        }

        /// <summary>
        /// todo create contact page
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        /// <summary>
        /// gt products by default or by using side bar ctegory menu or side bar brand menu - hassan shaddad
        /// </summary>
        /// <param name="id">int ? category Id</param>
        /// <param name="brand">int? brand Id</param>
        /// <param name="page">int? page for pagelist</param>
        /// <returns> return View(products.ToPagedList(pageNumber, pageSize))</returns>

        public ActionResult Products(int? id, int? brand, int? page)
        {
            if (id != null)
            {
                ViewBag.categId = id;
                //todo:to much data hear public ActionResult products(int? id)
                IQueryable<Products> products = _productUnit.Products.GetAll().Where(e => e.CategoriesId == id).Include(a => a.ProductImages).Include(e => e.DiscountRules).Include(p => p.ProductOriginalPrices).Include(i => i.InventoryTrans);
                products = products.OrderBy(e => e.Name);
                int pageSize = 6;
                int pageNumber = page ?? 1;
                return View(products.ToPagedList(pageNumber, pageSize));
            }
            else if (brand != null)
            {
                ViewBag.categId = brand;
                //todo:to much data hear public ActionResult products(int? id)
                IQueryable<Products> products = _productUnit.Products.GetAll().Where(e => e.BrandId == brand).Include(a => a.ProductImages).Include(e => e.DiscountRules).Include(p => p.ProductOriginalPrices).Include(i => i.InventoryTrans);
                products = products.OrderBy(e => e.Name);
                int pageSize = 6;
                int pageNumber = page ?? 1;
                return View(products.ToPagedList(pageNumber, pageSize));

            }
            else
            {

                //todo:to much data hear public ActionResult products(int? id)
                IQueryable<Products> products = _productUnit.Products.GetAll().Include(a => a.ProductImages).Include(e => e.DiscountRules).Include(p => p.ProductOriginalPrices).Include(i => i.InventoryTrans);
                products = products.OrderBy(e => e.Name);
                int pageSize = 6;
                int pageNumber = page ?? 1;
                return View(products.ToPagedList(pageNumber, pageSize));

            }



        }
        /// <summary>
        /// gt products by search from products view  search by catigory or price or brand or color or by any of them
        ///  - hassan shaddad
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lessPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="brand"></param>
        /// <param name="color"></param>
        /// <param name="page"></param>
        /// <returns></returns>

        public ActionResult SearchProducts(int? id, int? lessPrice, int? maxPrice, int? brand, string color, int? page)
        {

            ViewBag.Brand = new SelectList(_brandsUnit.GetAllBrands(), "Id", "BrandName");
            ViewBag.categId = id;
            if (lessPrice == null)
            {
                lessPrice = 0;
            }
            if (maxPrice == null)
            {
                maxPrice = 400;
            }
            if (lessPrice > maxPrice)
            {
                maxPrice = lessPrice + 1;
            }


            ViewBag.categId = id;
            //todo:to much data hear public ActionResult products(int? id)
            IQueryable<Products> products = from p in _productUnit.Products.GetAll().Include(a => a.ProductImages).Include(e => e.DiscountRules).Include(p => p.ProductOriginalPrices).Include(i => i.InventoryTrans).Include(i => i.ProductAttributes)
                                            join atter in _productUnit.GetAllProductAttributes()
                                            on p.Id equals atter.Products.Id
                                            join price in _productUnit.ProductOriginalPricesDataRepository.GetAll() on p.Id equals price.ProductId
                                            where p.CategoriesId == id || id == null
                                            where p.Brands.Id == brand || brand == null
                                            where atter.PaAttValue == color || color == "" || color == null
                                            where price.OriginalPrice >= lessPrice && price.OriginalPrice <= maxPrice

                                            select p;


            products = products.Distinct();
            products = products.OrderBy(e => e.Name);

            int pageSize = 6;
            int pageNumber = page ?? 1;
            return PartialView("_SearchProducts", products.ToPagedList(pageNumber, pageSize));



        }

        /// <summary>
        /// last nodes of categories for using in index menu bar after promoted products partial view
        /// </summary>
        /// <param name="categId">int? caregory id</param>
        /// <returns>PartialView("LastCategForHome", categview)</returns>

        public ActionResult LastCategForHome(int? categId)
        {

            List<Categories> categ = _pCategoryHomeService.GetLastCategOnly().ToList();
            List<Products> products = _productUnit.Products.GetAll().Where(e => e.ActiveFlag == true).ToList();
            CategAndProductsViewModel categview = new CategAndProductsViewModel
            {
                Categories = categ,
                Products = products

            };

            return PartialView("LastCategForHome", categview);


        }

        /// <summary>
        /// list of promoted products for index view
        /// </summary>
        /// <returns>PartialView("PromotedProducts", products)</returns>
        public ActionResult PromotedProducts()
        {
            List<Products> products = _productsForHomeService.ProductsPromoted().ToList();


            return PartialView("PromotedProducts", products);


        }



        //ProductsDetails
        public ActionResult ProductsDetails(int? id)
        {
            ProductsViewModel view = _productDetailsService.GetProductsDetails(id);
            return View(view);
        }

    }
}