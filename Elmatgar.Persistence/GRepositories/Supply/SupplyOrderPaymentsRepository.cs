using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.Supply
{
    public class SupplyOrderPaymentsRepository :DataRepository<SupplyOrderPayments>
    {
        public SupplyOrderPaymentsRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}