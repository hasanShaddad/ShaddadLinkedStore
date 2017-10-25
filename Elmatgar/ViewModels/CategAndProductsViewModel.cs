using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Elmatgar.Core.Models;

namespace Elmatgar.ViewModels
{
    public class CategAndProductsViewModel
    {
        public List<Categories> Categories { get; set; }
        public virtual List<Products> Products { get; set; }
    }
}