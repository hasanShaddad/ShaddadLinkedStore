using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
  public   class ProductBarCodeUnit : IDisposable , IProductBarCodeUnit 
    {

        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Core.Models.ProductBarcodes > _ProductBarcodesRepository = null;
        private IDataRepository<Core.Models.Products> _Products = null;

        public IDataRepository<Core.Models.ProductBarcodes> ProductBarcodesRepository
        {



            get
            {
                if (this._ProductBarcodesRepository == null)

                {
                    this._ProductBarcodesRepository = new DataRepository<Elmatgar.Core.Models.ProductBarcodes>(this._context);
                }
                return this._ProductBarcodesRepository;
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

      

        
        public void AddProductBarcodes(ProductBarcodes entity)
        {
          ProductBarcodesRepository.Add(entity);
        }

        public void UpdateProductBarcodes(ProductBarcodes entity)
        {
           ProductBarcodesRepository.Update(entity);
        }

        public void DeleteProductBarcodes(int id)
        {
            ProductBarcodesRepository.Delete(id);
        }

        public Task SaveChangesAsync()
        {
           return _context.SaveChangesAsync();
        }

        public Task<ProductBarcodes> FindAsync(int? id)
        {
            return ProductBarcodesRepository.GetByIdAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<ProductBarcodes> GetAllProductBarcodes()
        {
            return ProductBarcodesRepository.GetAll();
        }

        public IQueryable<Core.Models.Products> GetAllProducts()
        {
            return ProductRepository.GetAll();
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }


    }
}
