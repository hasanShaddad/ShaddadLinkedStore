using Elmatgar.Core.Models;
using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Supply
{
    public class SupplierStoresRepository : DataRepository<SupplierStores>
    {
        public SupplierStoresRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}