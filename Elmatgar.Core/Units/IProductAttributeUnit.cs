using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
  public  interface IProductAttributeUnit
    {
        IDataRepository<ProductAttributes> ProductDataRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        IDataRepository<AttributeHeaders> AttributeHeadersRepository { get; }

        void AddProductAttributeHeader(ProductAttributes entity);

        void UpdateProductAttributeHeader(ProductAttributes entity);

        void DeleteProductAttributeHeader(int id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        Task SaveChangesAsync();


         Task< int>  GetProductAttributeHeaderCount(int? PaAttHeaderId , int productId);

        Task<Core.Models.ProductAttributes> FindAsync(int? id);
        IQueryable<ProductAttributes> GetAllProductAttributeses();

        IQueryable<AttributeHeaders> GetAllAttributHeader();

        IQueryable<Products> GetAllProductses();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();

    }
}
