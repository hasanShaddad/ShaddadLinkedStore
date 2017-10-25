using System.Data.Entity;

namespace Elmatgar.persistence.GRepositories.ProductOriginalPrices
{
    public  class ProductOriginalPricesDataRepository :DataRepository<Core.Models.ProductOriginalPrices>
    {
        public ProductOriginalPricesDataRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
