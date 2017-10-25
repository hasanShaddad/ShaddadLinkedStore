using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.DiscountRules
{
  public  class DiscountRuleasDipository :DataRepository<Core.Models.DiscountRules>
    {
        public DiscountRuleasDipository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
