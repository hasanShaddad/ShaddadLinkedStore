using System;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elmatgar.Core.Units
{
    public  interface IProductOriginalPricesUnit
    {

        IDataRepository<ProductOriginalPrices> ProductOriginalPricesRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        IDataRepository<Countries> CountryDataRepository { get; }

        void AddProductOriginalPrices(ProductOriginalPrices entity);

        void UpdateProductOriginalPrices(ProductOriginalPrices entity);

        void DeleteProductOriginalPrices(int id);

        Task SaveChangesAsync();
        Task<Core.Models.ProductOriginalPrices> FindAsync(int? id);


        Core.Models.ProductOriginalPrices GetFirstProductOriginalPrices(int productId);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<ProductOriginalPrices> GetAllProductOriginalPrices();
        int ProductOriginalPriceCount(int productId);
        IQueryable<Products> GetAllProducts();

        IQueryable<Countries> GetAllCountrieses();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();
    }
}
