using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
  public  interface IProductBarCodeUnit
    {

        IDataRepository<ProductBarcodes> ProductBarcodesRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        void AddProductBarcodes(ProductBarcodes entity);

        void UpdateProductBarcodes(ProductBarcodes entity);

        void DeleteProductBarcodes(int id);

        Task SaveChangesAsync();
        Task<Core.Models.ProductBarcodes> FindAsync(int? id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<ProductBarcodes> GetAllProductBarcodes();
        IQueryable<Products> GetAllProducts();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();

    }
}
