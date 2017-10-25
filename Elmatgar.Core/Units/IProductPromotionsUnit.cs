using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
  public  interface IProductPromotionsUnit
    {


        IDataRepository<ProductPromotions> ProductPromotionsRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        IDataRepository<Countries> CountryDataRepository { get; }

        void AddProductPromotions(ProductPromotions entity);

        void UpdateProductPromotions(ProductPromotions entity);

        void DeleteProductPromotions(ProductPromotions entity);

        Task SaveChangesAsync();
        Task<Core.Models.ProductPromotions> FindAsync(int? id);


       Core.Models.ProductPromotions GetFirstProductPromotions(int id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<ProductPromotions> GetAllProductPromotions();
       // int ProductOriginalPriceCount(int productId);
        IQueryable<Products> GetAllProducts();

        IQueryable<Countries> GetAllCountrieses();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();




    }
}
