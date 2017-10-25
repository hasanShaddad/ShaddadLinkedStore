using System.Collections.Generic;
using Elmatgar.persistence;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Elmatgar.ViewModels;

namespace Elmatgar
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        //public IEnumerable<Product> Get()
        //{
        //    ProductRepository prd = new ProductRepository();
        //    return prd.Retrieve();
        //} 
        private readonly StoreDbContext db = new StoreDbContext();
        [HttpGet]
        public IHttpActionResult Get()
        {//todo: 1- refactore the code 

            //  var product = db.Products.ToList();
            //var x = from prod in db.Products
            //    join image in db.ProductImages on prod.Id equals image.Id
            //    join price in db.ProductOriginalPrices on prod.Id equals price.Id
            //    join invent in db.InventoryTrans on prod.Id equals invent.ItProdId
            //    select new
            //    {
            //        ProductId = prod.Id,
            //        Title = prod.Name,
            //        ImageUrl = image.ImageLink + image.ImageName,
            //        Price = price.OriginalPrice,
            //        StockQuntity = invent.ItQty
            //    };



            List<ProductViewModel> model = new   List<ProductViewModel>(); 


            var product = db.Products.Include(e => e.ProductImages)
                .Include(w => w.ProductOriginalPrices)
                .Include(x => x.InventoryTrans)
                .Select(x => new
                {
                    ProductId = x.Id,
                    Title = x.Name,

                    ImageUrl = x.ProductImages.Select(s => new { url = "../../" + s.ImageLink + "/" + s.ImageName }).FirstOrDefault(),

                    Price = x.ProductOriginalPrices.Select(w => w.OriginalPrice).FirstOrDefault(),
                    StockQuntity = x.InventoryTrans.Where(e => e.ItProdId == x.Id).OrderByDescending(e => e.CreationDate).Select(e => e.ItQty).FirstOrDefault()
                })

                .ToList();
            foreach (var item in product)
            {
                var prod = new ProductViewModel
                {
                     ProductId = item.ProductId,
                     ImageUrl = item.ImageUrl.url,
                     Price = item.Price.Value,
                     Title = item.Title,
                     StockQuntity = item.StockQuntity.Value
                     

                };
               model.Add(prod);
                
            }

            // this code brings the original price as list  not single record ..
            //var product = db.Products.Include(e => e.ProductImages)
            //   .Include(w => w.ProductOriginalPrices)
            //   .Include(x => x.InventoryTrans)
            //   .Select(x => new
            //   {
            //       ProductId = x.Id,
            //       Title = x.Name,
            //       ImageUrl = x.ProductImages.Select(s => new { url = s.ImageLink + s.ImageName }),
            //       Price = x.ProductOriginalPrices.Select(w => w.OriginalPrice),
            //       StockQuntity = x.InventoryTrans.Where(e => e.ItProdId == x.Id).OrderByDescending(e => e.CreationDate).Select(e => e.ItQty).FirstOrDefault()
            //   })

            //   .ToList();
            //IEnumerable<ProductViewModel> model = new List<ProductViewModel>
            //{

            //};
            return Ok(product);
        }

        //public string GetImage(int productId)
        //{
        //    var path = db.ProductImages.Where(x => x.ProductId == productId).Select(x => new { ImageLink = x.ImageLink , ImageName= x.ImageName }).AsEnumerable().SingleOrDefault();
        //    string picPath = null; 
        //        if (path != null)
        //    {
        //        picPath = path.ImageLink + path.ImageName;

        //    }

        //    return picPath;
        //}

        //public decimal GetPrice(int productId)
        //{
        //    var price = db.ProductOriginalPrices.Select(w => w.OriginalPrice).SingleOrDefault();
        //    decimal productPrice = 0; 
        //        if (price != null)
        //        {
        //            productPrice = price.Value;

        //        }

        //    return productPrice;
        //}

        // GET: api/Products/5
        public string Get(int id)
        {

            return "value";
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
