using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public   class ProductImageUnit :IDisposable, IProductImagesUnit
 {
        private  StoreDbContext _context= new StoreDbContext();

        private IDataRepository<ProductImages> _productImage = null;

        
       private IDataRepository<Core.Models.Products> _Products = null;

       

        public IDataRepository<Core.Models.ProductImages> productImageRepository
        {



            get
            {
                if (this._productImage == null)

                {
                    this._productImage = new DataRepository<Elmatgar.Core.Models.ProductImages>(this._context);
                }
                return this._productImage;
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
        public void AddProductImage(ProductImages entity)
        {
           productImageRepository.Add(entity);
        }

        public void UpdateProductImages(ProductImages entity)
        {
            productImageRepository.Update(entity);
        }

        public void DeleteProductImages(int id)
        {
            productImageRepository.Delete(id);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<ProductImages> FindAsync(int? id)
        {
            return productImageRepository.GetByIdAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<ProductImages> GetAllProductImageses()
        {
            return productImageRepository.GetAll();
        }

        public IQueryable<Core.Models.Products> GetAllProducts()
        {
            return ProductRepository.GetAll();
        }

        public   void Dispose()
        {
            this._context?.Dispose();
        }

        public Task<Core.Models.Products> FindProductAsync(int? id)
        {
           return ProductRepository.GetByIdAsync(id);
        }

        public int GetProductImageCount()
        {
            return productImageRepository.GetAll().Count();
        }

        public ProductImages Find(int? id)
        {
            return productImageRepository.GetById(id);
        }
    }
}
