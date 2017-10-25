using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Elmatgar.persistence;
using Elmatgar.persistence.HelperClasses;
using PagedList;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class ProductImagesController : Controller
    {
      //  private StoreDbContext db = new StoreDbContext();

        private IProductImagesUnit _productImagesUnit;

        public ProductImagesController(IProductImagesUnit productImagesUnit)
        {
            _productImagesUnit = productImagesUnit;
        }

        // GET: Admen/ApplicationUsers
        public ActionResult Index(string sort, string search, int? page)
        {

            //for (int i = 2016; i < 2030; i++)
            //{
            //    for (int j = 1; j < 13; j++)
            //    {
            //        for (int k = 1; k < 32; k++)
            //        {
            //            System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/Products/" + i.ToString() + "/" + j.ToString() + "/" + k.ToString()));

            //        }
            //    }
               

            //}
         

            ViewBag.ImageNameSort = string.IsNullOrEmpty(sort) ? "name_desc" : string.Empty;
           
            ViewBag.CDate = sort == "cdate" ? "cdate_desc" : "cdate";
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
            IQueryable<ProductImages> productImages = _productImagesUnit.GetAllProductImageses()
                .Include(e => e.Products);
                //db.ProductImages.Include(p => p.Products);

            if (!string.IsNullOrEmpty(search))
            {
                productImages = productImages.Where(e => e.ImageName.StartsWith(search));
            }


            switch (sort)
            {
                case "name_desc":
                    productImages = productImages.OrderByDescending(e => e.ImageName)
                        .ThenBy(e => e.CreationDate);
                    break;
                
              

                case "cdate":
                    productImages = productImages.OrderBy(e => e.CreationDate);
                    break;
                case "cdate_desc":
                    productImages = productImages.OrderByDescending(e => e.CreationDate);
                    break;

                default:
                    productImages = productImages.OrderBy(e => e.ImageName)
                        .ThenBy(e => e.CreationDate);
                    break;
            }



            int pageSize = 3;
            int pageNumber = page ?? 1;
            return View( productImages.ToPagedList(pageNumber, pageSize));
        
        }



        // GET: Admin/ProductImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImages productImages = await _productImagesUnit .FindAsync(id);
            if (productImages == null)
            {
                return HttpNotFound();
            }
            return View(productImages);
        }

        // GET: Admin/ProductImages/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ProductId = id != null ? new SelectList(_productImagesUnit.GetAllProducts(), "Id", "Name", id) : new SelectList(_productImagesUnit.GetAllProducts(), "Id", "Name");

            return PartialView("_Create");
        }

       

        // POST: Admin/ProductImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ImageName,ImageLink,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductImages productImages)
        {
            productImages.ImageLink = $"{"Images/Products/"+DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day}";
            productImages.ImageName = _productImagesUnit.FindProductAsync(productImages.ProductId).Result.Name +"-"+ Convert.ToString(_productImagesUnit.GetProductImageCount()+1);

            if (ModelState.IsValid)
            {
                //var existingRecord = db.ProductImages.Count(a => a.ImageName == productImages.ImageName);
                //if (existingRecord == 0)
                //{
                   // db.ProductImages.Add(productImages);
                   _productImagesUnit.AddProductImage(productImages);


                    await _productImagesUnit.SaveChangesAsync();
                    return RedirectToAction("Details", new RouteValueDictionary(
    new { controller = "ProductImages", action = "Details", Id = productImages.Id }));
                //}
                //else
                //{
                //    ModelState.AddModelError("", "name is exist");
                //}
              
               
            }

            ViewBag.ProductId = new SelectList(_productImagesUnit.GetAllProducts(), "Id", "Name", productImages.ProductId);
            return View(productImages);
        }

        // GET: Admin/ProductImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImages productImages = await _productImagesUnit.FindAsync(id);
            if (productImages == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_productImagesUnit.GetAllProducts(), "Id", "Name", productImages.ProductId);
            return View(productImages);
        }

        // POST: Admin/ProductImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ImageName,ImageLink,ActiveFlag,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductImages productImages)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(productImages).State = EntityState.Modified;

                _productImagesUnit.UpdateProductImages(productImages);
                await _productImagesUnit.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_productImagesUnit.GetAllProducts(), "Id", "Name", productImages.ProductId);
            return View(productImages);
        }

        // GET: Admin/ProductImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImages productImages = await _productImagesUnit.FindAsync(id);
            if (productImages == null)
            {
                return HttpNotFound();
            }
            return View(productImages);
        }

        // POST: Admin/ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductImages productImages = await _productImagesUnit.FindAsync(id);
          //  db.ProductImages.Remove(productImages);

            _productImagesUnit.DeleteProductImages(id);
            await _productImagesUnit.SaveChangesAsync();
            string link = HostingEnvironment.MapPath("~/"+productImages.ImageLink + "/" + productImages.ImageName) ;

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

                ViewBag.Error ="can not delete file";
                return View("ImageResult");
            }
               
            

            return RedirectToAction("Index");
        }

        /// <summary>
        /// upload product image
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadImage(FormCollection formCollection)
        {
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                if (file.ContentLength == 0)
                    continue;
                if (file.ContentLength > 0)
                {
                    // width + height will force size, care for distortion
                    //Exmaple: ImageUpload imageUpload = new ImageUpload { Width = 800, Height = 700 };

                    // height will increase the width proportionally
                    //Example: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                    // width will increase the height proportionally
                    string extension = Path.GetExtension(file.FileName);
                    string s = Request["ImageName"];
                    string name = null;
                    if (extension != null && (s != null && !s.Contains(extension)))
                    {
                       name = Request["ImageName"].ToString() + extension;
                    }
                    else
                    {
                       name = Request["ImageName"].ToString();
                    }
                  
                    string link = "~/"+Request["ImageLink"].ToString() + "/";
                    string width = Request["imageWidth"];
                    if (!string.IsNullOrEmpty(width))
                    {
                        if (Convert.ToInt32(width) < 0 && Convert.ToInt32(width) > 2000)
                        {
                            width = "400";
                        }
                    }
                    else
                    {
                        width = "400";
                    }
                  
                   
                    ImageUpload imageUpload = new ImageUpload(link, name) { Width = Convert.ToInt32(width) };

                    // rename, resize, and upload
                    //return object that contains {bool Success,string ErrorMessage,string ImageName}
                    ImageResult imageResult = imageUpload.RenameUploadFile(file);
                    if (imageResult.Success)
                    {
                        if (!string.IsNullOrEmpty(Request["Id"]))
                        {
                            int id = Convert.ToInt32(Request["Id"]);
                            ProductImages productImages = _productImagesUnit.Find(id);
                            productImages.ImageExist = true;
                            productImages.ImageName = name;
                           // db.Entry(productImages).State = EntityState.Modified;

                            _productImagesUnit.UpdateProductImages(productImages);


                            _productImagesUnit.SaveChanges();
                        }
                    

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // use imageResult.ErrorMessage to show the error
                        ViewBag.Error = imageResult.ErrorMessage;
                    }
                }
            }

            return View("ImageResult");
        }

        public ActionResult ImageResult()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productImagesUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
