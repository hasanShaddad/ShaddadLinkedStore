using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.Products
{
    public class ProductRepository : DataRepository<Core.Models.Products>
    {
        public ProductRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
