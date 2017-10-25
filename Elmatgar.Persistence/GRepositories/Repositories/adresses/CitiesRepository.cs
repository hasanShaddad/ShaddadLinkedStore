using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.oldrepositories.Repositories.adresses
{
    public class CitiesRepository : DataRepository<Cities>
    {
        public CitiesRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}