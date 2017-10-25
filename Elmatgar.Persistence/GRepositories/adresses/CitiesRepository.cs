using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.adresses
{
    public class CitiesRepository : DataRepository<Cities>
    {
        public CitiesRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}