using Elmatgar.Core.Models;
using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Inventory
{
    class ReturnRepository : DataRepository<Returns>
    {
        public ReturnRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
