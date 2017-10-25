using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.Brands.BrandsRepository
{
    public class BrandsRepository : DataRepository<Core.Models.Brands>
    {
        public BrandsRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}