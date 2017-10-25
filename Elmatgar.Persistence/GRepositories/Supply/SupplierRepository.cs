using Elmatgar.Core.Models;
using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Supply
{
    public class SupplierRepository : DataRepository<Suppliers>
    {
        public SupplierRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
