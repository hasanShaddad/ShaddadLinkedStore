using System.Linq;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
    public interface IAdressUnit
    {
        /// <summary>
        /// handel the Countries with singleToon patern
        /// </summary>
        IDataRepository<Countries> Country { get; }

        /// <summary>
        /// handel the cities  with singleToon patern
        /// </summary>
        IDataRepository<Cities> City { get; }

        /// <summary>
        /// handel the areas  with singleToon patern
        /// </summary>
        IDataRepository<Zones> Area { get; }

      IQueryable<Cities> CitiesesJoincountries { get; }
        IQueryable<Zones> ZonesJoinCitieses { get; }
        /// <summary>
        /// save all changes at once
        /// </summary>
        void SaveChanges();

        void Dispose();
    }
}