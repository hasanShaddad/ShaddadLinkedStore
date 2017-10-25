using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.persistence.GRepositories.KitProduct;
using  Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.ProductBarCodes
{
  public   class ProducrBarCodeRepository : DataRepository<Elmatgar.Core.Models.ProductBarcodes>
    {
        public ProducrBarCodeRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
