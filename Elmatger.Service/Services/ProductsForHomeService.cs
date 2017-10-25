using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.persistence.GRepositories.DomainUnits;

namespace Elmatgar.Service.Services
{
    public class ProductsForHomeService : IProductsForHomeService
    {
        private readonly IProductUnit _productUnit;
        private readonly IProductPromotionsUnit _productPromotionsUnit;

        public ProductsForHomeService(IProductUnit productUnit, IProductPromotionsUnit productPromotionsUnit)
        {
            _productUnit = productUnit;
            _productPromotionsUnit = productPromotionsUnit;
        }


        public List<Products> ProductsPromoted()
        {

            IQueryable<Products> products = from p in _productUnit.Products.GetAll().Include(a => a.ProductImages).Include(e => e.DiscountRules).Include(p => p.ProductOriginalPrices).Include(i => i.InventoryTrans).Include(i => i.ProductAttributes)
                                            join promo in _productUnit.GetAllProductpromotions()
                                            on p.Id equals promo.ProductId
                                            where promo.EndDate >= DateTime.Now
                                            select p;

            products = products.Distinct();
            products = products.OrderBy(e => e.Name);
            return products.ToList();
        }


    }
}
