using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.KitProduct
{
  public  class KitPRoductRepository:DataRepository<Elmatgar.Core.Models.KitProducts>
    {
        public KitPRoductRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
