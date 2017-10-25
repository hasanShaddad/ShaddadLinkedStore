using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.ProductImage
{
    public class ProductImageRepository : DataRepository<ProductImages>
    {
        public ProductImageRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
