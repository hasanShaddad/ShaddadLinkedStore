using System.Collections.Generic;
using System.Linq;

namespace Elmatgar.Core.Models
{
    public class TeamStat
    {
        public TeamStat()
        {
        }
        public decimal? finalPrice { get; set; }
        public List<ProductImages> Images { get; set; }
        public List<ProductAttributes> productsDetails { get; set; }
        public IQueryable<Products> products { get; set; }
        public int? inventoryStock { get; set; }
    }
}