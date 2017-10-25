using Elmatgar.Core.Models;
using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Supply
{
    public class SupplyOrderRepository : DataRepository<SupplyOrders>
    {
        public SupplyOrderRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
