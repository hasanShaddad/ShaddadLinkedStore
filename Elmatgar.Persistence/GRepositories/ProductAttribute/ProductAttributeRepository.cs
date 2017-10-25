using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.ProductAttribute
{
  public  class ProductAttributeRepository : DataRepository<Core.Models.ProductAttributes>
    {
        public ProductAttributeRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
