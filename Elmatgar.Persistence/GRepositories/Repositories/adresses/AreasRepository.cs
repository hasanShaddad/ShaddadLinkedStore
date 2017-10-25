using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.oldrepositories.Repositories.adresses
{
    public  class AdressRepository:DataRepository<Zones>
    {
        public AdressRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}