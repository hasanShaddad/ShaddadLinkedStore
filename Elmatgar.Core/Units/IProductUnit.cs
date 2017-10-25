using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Elmatgar.Core.Units
{
    public interface IProductUnit
    {
        IDataRepository<Core.Models.Products> Products { get; }

        /// <summary>
        IDataRepository<Categories> CategoryDataRepository { get; }

      
        IDataRepository<Brands> BrandDataRepository { get; }
        IDataRepository<KitProducts> KitDataRepository { get; }

       
      IDataRepository<ProductOriginalPrices> ProductOriginalPricesDataRepository { get; }

        IDataRepository<DiscountRules> discountRulessDataRepository { get; }

        IDataRepository<ProductPromotions> ProductPromotions { get; }
        IDataRepository<ProductBarcodes> productBarCodeDataRepository { get; }

        void AddProducts(Products entity);

        void UpdateProducts(Products entity);

        void DeleteProducts(int id);

        Task SaveChangesAsync();
        Task<Core.Models.Products> FindAsync(int? id);


       // Core.Models.Products GetFirstProducts(int Id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<Products> GetAllProducts();

        IQueryable<ProductPromotions> GetAllProductpromotions();

        IQueryable<Categories> GetAllCategories();


        IQueryable<Elmatgar.Core.Models.ProductImages> GetImage(int id);


        Elmatgar.Core.Models.SupplyOrderDetails GetSupplyOrderDetails(int id);

        


        IQueryable<KitProducts> GetAllKitProducts();

        IQueryable<Brands> GetAllBrands();
        //get all products attributes with the same dbContext for productsUnit - hassan shaddad
        IQueryable<ProductAttributes> GetAllProductAttributes();

        void DeleteRelatedProduct(int id);
       


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();
    }
}