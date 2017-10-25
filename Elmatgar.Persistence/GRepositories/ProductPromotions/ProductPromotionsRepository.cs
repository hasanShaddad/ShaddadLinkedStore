using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.ProductPromotions
{
  public  class ProductPromotionsRepository :DataRepository<Core.Models.ProductPromotions>
    {
        public ProductPromotionsRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
