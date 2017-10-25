using Elmatgar.Core.Models;
using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Inventory
{
    class InventoryTransRepository : DataRepository<InventoryTrans>
    {
        public InventoryTransRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
