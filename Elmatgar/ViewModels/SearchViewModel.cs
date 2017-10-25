using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmatgar.Core.Models;

namespace Elmatgar.ViewModels
{
    public class SearchViewModel
    {
        public List<Categories> Categories { get; set; }
        public virtual List<Products> Products { get; set; }
    
        public virtual List<ProductAttributes> ProductAttributes { get; set; }
        public virtual List<Brands> Brands { get; set; }
        

        public virtual  ProductOriginalPrices  LessOriginalPrices  { get; set; }
        public virtual ProductOriginalPrices MaxOriginalPrices { get; set; }
    }
}