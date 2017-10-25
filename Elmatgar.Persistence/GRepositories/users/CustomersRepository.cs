using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.users 
{
    public class CustomersRepository:DataRepository<Customers>
    {
        public CustomersRepository(DbContext dbContext):base(dbContext)
        {
            
        }
    }
}