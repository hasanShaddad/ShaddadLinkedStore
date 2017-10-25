using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.Orders
{
    public class OrderDetailsRepository:DataRepository<OrderDetails>
    {
        public OrderDetailsRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}