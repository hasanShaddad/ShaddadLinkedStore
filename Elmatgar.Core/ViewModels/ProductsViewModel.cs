using System.Collections.Generic;
using System.Linq;
using Elmatgar.Core.Models;

namespace Elmatgar.Core.ViewModels
{
    public class ProductsViewModel  
    {
        public ProductsViewModel()
        {
            
        }

        public IQueryable<Products> Products{ get; set; }

        public decimal? FinalPrice { get; set; }
        public List<ProductImages> Images { get; set; }
        public List<ProductAttributes> ProductsDetails { get; set; }
       
        public int? InventoryStock { get; set; }
    }
}