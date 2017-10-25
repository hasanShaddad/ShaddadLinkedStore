using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.Supply
{
    public class SupplyOrderDetailsRepository :DataRepository<SupplyOrderDetails>
    {
        public SupplyOrderDetailsRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}