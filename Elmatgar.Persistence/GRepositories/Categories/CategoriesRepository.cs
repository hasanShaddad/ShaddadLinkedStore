using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Categories
{
    public class CategoriesRepository : DataRepository<Elmatgar.Core.Models.Categories>
    {
        public CategoriesRepository(DbContext dbContext):base(dbContext)
        {
            
        }
    }
}