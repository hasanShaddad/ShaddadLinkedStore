using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.Orders
{
    public class OrderPaymentRepository :DataRepository<OrderPayments>
    {
        public OrderPaymentRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}