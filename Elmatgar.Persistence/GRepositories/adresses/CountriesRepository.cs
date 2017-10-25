using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.adresses
{
    public class CountriesRepository : DataRepository<Countries>
    {
        public CountriesRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}