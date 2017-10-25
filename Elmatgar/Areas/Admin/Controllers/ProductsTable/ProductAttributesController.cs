using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Units;
using Elmatgar.persistence;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class ProductAttributesController : Controller
    {
      // private StoreDbContext db = new StoreDbContext();


        private IProductAttributeUnit _productAttributeUnit;

        public ProductAttributesController( IProductAttributeUnit productAttribute)
        {
            _productAttributeUnit = productAttribute;

        }

        // GET: Admin/ProductAttributes
        public async Task<ActionResult> Index()
        {
           // IQueryable<ProductAttributes> productAttributes = db.ProductAttributes.Include(p => p.AttributeHeaders).Include(p => p.Products);
             return View(await _productAttributeUnit.GetAllProductAttributeses().ToListAsync());
        }

        // GET: Admin/ProductAttributes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributes productAttributes = await _productAttributeUnit.FindAsync(id);
            if (productAttributes == null)
            {
                return HttpNotFound();
            }
            return View(productAttributes);
        }

        // GET: Admin/ProductAttributes/Create
        public ActionResult Create()

        {
            
            ViewBag.AttributeHeaders_Id = new SelectList(_productAttributeUnit.GetAllAttributHeader(), "Id", "AttributeName");
            ViewBag.ProductId = new SelectList(_productAttributeUnit.GetAllProductses(), "Id", "Name");
            return View();
        }

        // POST: Admin/ProductAttributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,AttributeHeaders_Id,PaAttValue,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductAttributes productAttributes)
        {
            if (ModelState.IsValid)
            {
               
                if (await _productAttributeUnit.GetProductAttributeHeaderCount(productAttributes.AttributeHeaders_Id, productAttributes.ProductId) == 0)
                {
                    //db.ProductAttributes.Add(productAttributes);


                    _productAttributeUnit.ProductDataRepository.Add(productAttributes);
                    await _productAttributeUnit.SaveChangesAsync();




                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "this product has value for this attrebute header is exist");
                }


              
            }

            ViewBag.AttributeHeaders_Id = new SelectList(_productAttributeUnit.GetAllAttributHeader(), "Id", "AttributeName", productAttributes.AttributeHeaders_Id);
            ViewBag.ProductId = new SelectList(_productAttributeUnit.GetAllProductses(), "Id", "Name", productAttributes.ProductId);
            return View(productAttributes);
        }

        // GET: Admin/ProductAttributes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributes productAttributes = await _productAttributeUnit.FindAsync(id);




            if (productAttributes == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributeHeaders_Id = new SelectList(_productAttributeUnit.GetAllAttributHeader(), "Id", "AttributeName", productAttributes.AttributeHeaders_Id);
            ViewBag.ProductId = new SelectList(_productAttributeUnit.GetAllProductses(), "Id", "Name", productAttributes.ProductId);
            return View(productAttributes);
        }

        // POST: Admin/ProductAttributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AttributeHeaders_Id,PaAttValue,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy,ProductId")] ProductAttributes productAttributes)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(productAttributes).State = EntityState.Modified;

                _productAttributeUnit.UpdateProductAttributeHeader(productAttributes);

              //  await db.SaveChangesAsync();

              _productAttributeUnit.SaveChanges();



                return RedirectToAction("Index");
            }
            ViewBag.PaAttHeaderId = new SelectList(_productAttributeUnit.GetAllAttributHeader(), "Id", "AttributeName", productAttributes.AttributeHeaders_Id);
            ViewBag.ProductId = new SelectList(_productAttributeUnit.GetAllProductses(), "Id", "Name", productAttributes.ProductId);
            return View(productAttributes);
        }

        // GET: Admin/ProductAttributes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributes productAttributes = await _productAttributeUnit.FindAsync(id);




            if (productAttributes == null)
            {
                return HttpNotFound();
            }
            return View(productAttributes);
        }

        // POST: Admin/ProductAttributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
          //  ProductAttributes productAttributes = await _productAttributeUnit.FindAsync(id);
            _productAttributeUnit.DeleteProductAttributeHeader(id);
             _productAttributeUnit.SaveChangesAsync();



            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productAttributeUnit.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
