using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.oldrepositories.Repositories.adresses
{
    public class CountriesRepository : DataRepository<Countries>
    {
        public CountriesRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}