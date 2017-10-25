using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.departments 
{
    public class DepartmentsRepository:DataRepository<Departments>
    {
        public DepartmentsRepository(DbContext dbContext):base(dbContext)
        {
            
        }
    }
}