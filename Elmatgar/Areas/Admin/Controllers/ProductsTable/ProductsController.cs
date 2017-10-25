using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Elmatgar.persistence;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{

    public class ProductsController : Controller
    {
        private readonly StoreDbContext _db = new StoreDbContext();

        private IProductUnit _productUnit;

        public ProductsController(IProductUnit productUnit)
        {
            _productUnit = productUnit;

        }



        //get index
        public ActionResult Index(string sort, string search, int? page)
        {
            ViewBag.ProductNameSort = string.IsNullOrEmpty(sort) ? "name_desc" : string.Empty;

            ViewBag.CDate = sort == "cdate" ? "cdate_desc" : "cdate";
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
            IQueryable<Products> products = _productUnit.GetAllProducts().Include(p => p.Brands).Include(p => p.Categories).Include(p => p.KitProducts);

            //_db.Products.Include(p => p.Brands).Include(p => p.Categories).Include(p => p.KitProducts);


            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(e => e.Name.StartsWith(search));
            }


            switch (sort)
            {
                case "name_desc":
                    products = products.OrderByDescending(e => e.Name)
                        .ThenBy(e => e.CreationDate);
                    break;



                case "cdate":
                    products = products.OrderBy(e => e.CreationDate);
                    break;
                case "cdate_desc":
                    products = products.OrderByDescending(e => e.CreationDate);
                    break;

                default:
                    products = products.OrderBy(e => e.Name)
                        .ThenBy(e => e.CreationDate);
                    break;
            }



            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View(products.ToPagedList(pageNumber, pageSize));

        }



        // GET: Admin/Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await _productUnit.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(_productUnit.GetAllBrands(), "Id", "BrandName");
            ViewBag.CategoriesId = new SelectList(_productUnit.GetAllCategories().Where(e => e.Children.Count == 0), "Id", "CategoryName");
            ViewBag.KitProductsId = new SelectList(_productUnit.GetAllKitProducts(), "Id", "KitName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,KitProductsId,BrandId,CategoriesId,Name,ShortDescription,KitFlag,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Products products)
        {
            if (ModelState.IsValid)
            {
                var existingRecord = _db.Products.Count(a => a.Name == products.Name);
                if (existingRecord == 0)
                {
                    // _db.Products.Add(products);


                    _productUnit.AddProducts(products);
                    await _productUnit.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "name is exist");
                }

            }

            ViewBag.BrandId = new SelectList(_productUnit.GetAllBrands(), "Id", "BrandName", products.BrandId);
            ViewBag.CategoriesId = new SelectList(_productUnit.GetAllCategories().Where(e => e.Children.Count == 0), "Id", "CategoryName", products.CategoriesId);
            ViewBag.KitProductsId = new SelectList(_productUnit.GetAllKitProducts(), "Id", "KitName", products.KitProductsId);
            return View(products);
        }

        // GET: Admin/Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await _productUnit.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(_productUnit.GetAllBrands(), "Id", "BrandName", products.BrandId);
            ViewBag.CategoriesId = new SelectList(_productUnit.GetAllCategories(), "Id", "CategoryName", products.CategoriesId);
            ViewBag.KitProductsId = new SelectList(_productUnit.GetAllKitProducts(), "Id", "KitName", products.KitProductsId);
            return View(products);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,KitProductsId,BrandId,CategoriesId,Name,ShortDescription,KitFlag,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Products products)
        {
            if (ModelState.IsValid)
            {
                //_db.Entry(products).State = EntityState.Modified;

                _productUnit.UpdateProducts(products);
                await _productUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(_productUnit.GetAllBrands(), "Id", "BrandName", products.BrandId);
            ViewBag.CategoriesId = new SelectList(_productUnit.GetAllCategories(), "Id", "CategoryName", products.CategoriesId);
            ViewBag.KitProductsId = new SelectList(_productUnit.GetAllKitProducts(), "Id", "KitName", products.KitProductsId);
            return View(products);
        }

        // GET: Admin/Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = await _productUnit.FindAsync(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //delete images for this product
            var img = _productUnit.GetImage(id);
            //_db.ProductImages.Where(e => e.ProductId == id);
            foreach (var productImages in img)
            {
                string link = HostingEnvironment.MapPath("~/" + productImages.ImageLink + "/" + productImages.ImageName);
                _db.ProductImages.Remove(productImages);

                try
                {
                    if (link != null)
                    {
                        FileInfo info1 = new FileInfo(link);
                        if (info1.Exists)
                        {
                            info1.Delete();

                        }
                    }
                }
                catch (Exception)
                {

                    ViewBag.Error = "can not delete file";

                }

            }

            //delete bar code for this SupplyOrderDetails
            var purch = _productUnit.GetSupplyOrderDetails(id);
            //await _db.SupplyOrderDetails.FirstOrDefaultAsync(e => e.ProductId == id);
            if (purch != null)
            {

                ModelState.AddModelError("", "can not delete record please delete Purchase Order Lines first");

            }



            //delete prices for this product
            //  var price = await _db.ProductOriginalPrices.FirstOrDefaultAsync(e => e.ProductId == id);
            // if (price!=null)
            //{
            //   _productUnit.DeleteProducts(id);
            //}

            //delete discount role for this product
            // var discount = _productUnit.GetDiscountRules(id);
            //await _db.DiscountRules.FirstOrDefaultAsync(e => e.ProductId == id);
            // if (discount != null)
            //{
            //  _db.DiscountRules.Remove(discount);
            //}



            //delete bar code for this product
            //var barCode = _productUnit.GetProductBarcodes(id);
            // await _db.ProductBarcodes.FirstOrDefaultAsync(e => e.ProductId == id);
            //if (barCode != null)
            //{
            //  _db.ProductBarcodes.Remove(barCode);
            //}
            _productUnit.DeleteRelatedProduct(id);


            //  Products products = await _db.Products.FindAsync(id);
            // _db.Products.Remove(products);

            _productUnit.DeleteProducts(id);
            await _productUnit.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
