using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public  class ProductAttributeUnit :IDisposable, IProductAttributeUnit
    {

        private StoreDbContext _context = new StoreDbContext();





        private IDataRepository<ProductAttributes> _ProductHeader = null;
        private IDataRepository<Core.Models.Products> _Products = null;
        private IDataRepository<Core.Models.AttributeHeaders> _attributeHraders = null;






        public IDataRepository<Core.Models.ProductAttributes> ProductDataRepository
        {


            get
            {
                if (this._ProductHeader == null)

                {
                    _ProductHeader = new DataRepository<Elmatgar.Core.Models.ProductAttributes>(this._context);
                }
                return this._ProductHeader;
            }
        }


        public IDataRepository<Core.Models.Products> ProductRepository
        {


            get
            {
                if (this._Products == null)

                {
                    _Products = new DataRepository<Elmatgar.Core.Models.Products>(this._context);
                }
                return this._Products;
            }
        }

        public IDataRepository<Core.Models.AttributeHeaders> AttributeHeadersRepository
        {
            get
            {
                if (this._attributeHraders == null)

                {
                    _attributeHraders = new DataRepository<Elmatgar.Core.Models.AttributeHeaders>(this._context);
                }
                return this._attributeHraders;
            }


        }


        public void AddProductAttributeHeader(ProductAttributes entity)
        {
            ProductDataRepository.Add(entity);
        }

        public void UpdateProductAttributeHeader(ProductAttributes entity)
        {
            ProductDataRepository.Update(entity);
        }

        public void DeleteProductAttributeHeader(int id)
        {
            ProductDataRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task< int> GetProductAttributeHeaderCount(int? PaAttHeaderId , int productId)
        {
            return  await  _context.ProductAttributes.CountAsync( a => a.AttributeHeaders_Id ==  PaAttHeaderId && a.ProductId == productId);
        }

        public async Task<ProductAttributes> FindAsync(int? id)
        {
            return await ProductDataRepository.GetByIdAsync(id);
        }
    
        public IQueryable<ProductAttributes> GetAllProductAttributeses()
        {
            return ProductDataRepository.GetAll();
        }

        public IQueryable<Core.Models.AttributeHeaders> GetAllAttributHeader()
        {
            return AttributeHeadersRepository.GetAll();
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }

        public IQueryable<Core.Models.Products> GetAllProductses()
        {
            return ProductRepository.GetAll();
        }
    }
}
