using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.oldrepositories.Repositories.users 
{
    public class CustomersRepository:DataRepository<Customers>
    {
        public CustomersRepository(DbContext dbContext):base(dbContext)
        {
            
        }
    }
}