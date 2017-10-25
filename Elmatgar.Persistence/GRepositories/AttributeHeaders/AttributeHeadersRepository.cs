using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.AttributeHeaders
{
  public   class AttributeHeadersRepository :DataRepository<Core.Models.AttributeHeaders>
    {
        public AttributeHeadersRepository(DbContext dBcontext) : base(dBcontext)
        {
        }


       
    }
}
