using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Orders
{
    public class OrderRepository : DataRepository<Core.Models.Orders>
    {
        public OrderRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
