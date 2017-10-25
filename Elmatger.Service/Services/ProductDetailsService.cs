using Elmatgar.persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.Core.ViewModels;
using Elmatgar.persistence.Infrastructure;

namespace Elmatgar.Service.Services
{
     
    public class ProductDetailsService:IProductDetailsService
    {
        private readonly IDbFactory _dbFactory;
        public ProductDetailsService(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public ProductsViewModel GetProductsDetails(int? id)
        {
            int? inventoryStock = null;
            if (id == null)
            {
                return null;
            }
            var lastOrDefault =_dbFactory.Init().InventoryTrans.FirstOrDefault(e => e.ItProdId == id);
            
            if (lastOrDefault != null)
            {
                inventoryStock = lastOrDefault.ItQty;


                //inventory stok to view

            }

            //todo:to much data hear public ActionResult products(int? id)
            var productsDetails = _dbFactory.Init().ProductAttributes.Where(e => e.ProductId == id).Include(a => a.AttributeHeaders).ToList();

            var images = _dbFactory.Init().ProductImages.Where(i => i.ProductId == id).ToList();
            var products = _dbFactory.Init().Products.Where(e => e.Id == id).Include(a => a.ProductImages).Include(e => e.DiscountRules).Include(p => p.ProductOriginalPrices);
            //check inventoryStock for product


            var finalPrice = FinalPrice(id);


            ProductsViewModel view = new ProductsViewModel()
            {
                ProductsDetails = productsDetails,
                Images = images,
                Products = products,
                FinalPrice = finalPrice,
                InventoryStock = inventoryStock
            };
            return view;
        }
        /// <summary>
        /// method to get final price for product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public decimal? FinalPrice(int? id)
        {

            decimal? finalPrice =null;
           decimal ? orPrice = null;
            var productOriginalPrices = _dbFactory.Init().ProductOriginalPrices.FirstOrDefault(e => e.ProductId == id);
           
            if (productOriginalPrices != null)
            {
                orPrice = productOriginalPrices.OriginalPrice;
            }
            var productPromotion = _dbFactory.Init().ProductPromotions.FirstOrDefault(e => e.ProductId == id);
            if (productPromotion != null&& productPromotion.EndDate>=DateTime.Now)
            {
                finalPrice = productPromotion.PromoPrice;
            }
            else
            {
                finalPrice = orPrice;
            }
         

            


           
         
            return finalPrice;
        }
    }
}
