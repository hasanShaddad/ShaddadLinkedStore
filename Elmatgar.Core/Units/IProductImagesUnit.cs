using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Elmatgar.Core.Units
{
    public  interface IProductImagesUnit
    {
        IDataRepository<ProductImages> productImageRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        void AddProductImage(ProductImages entity);

        void UpdateProductImages(ProductImages entity);

        void DeleteProductImages(int id);

        Task SaveChangesAsync();
        Task<Core.Models.ProductImages> FindAsync(int? id);
        Task<Core.Models.Products> FindProductAsync(int? id);
        ProductImages Find(int? id);

        int GetProductImageCount();
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<ProductImages> GetAllProductImageses();
        IQueryable<Products> GetAllProducts();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();

    }
}
