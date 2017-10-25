using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public  class ProductOriginalPricesUnit :IDisposable, IProductOriginalPricesUnit
    {

        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Core.Models.ProductOriginalPrices> _ProductOriginalPrices = null;
        private IDataRepository<Core.Models.Products> _Products = null;

        private IDataRepository<Core.Models.Countries> _Country = null;

        public IDataRepository<Core.Models.ProductOriginalPrices> ProductOriginalPricesRepository
        {
            get
            {
                if (this._ProductOriginalPrices == null)

                {
                    this._ProductOriginalPrices =
                        new DataRepository<Elmatgar.Core.Models.ProductOriginalPrices>(this._context);
                }
                return this._ProductOriginalPrices;
            }
        }

        public IDataRepository<Core.Models.Products> ProductRepository
        {


            get
            {
                if (this._Products == null)

                {
                    this._Products =
                        new DataRepository<Elmatgar.Core.Models.Products>(this._context);
                }
                return this._Products;
            }
        }

        public IDataRepository<Countries> CountryDataRepository
        {
            get
            {
                if (this._Country == null)

                {
                    this._Country =
                        new DataRepository<Elmatgar.Core.Models.Countries>(this._context);
                }
                return this._Country;
            }
        }




        public void AddProductOriginalPrices(Core.Models.ProductOriginalPrices entity)
        {
            
            ProductOriginalPricesRepository.Add(entity);
        }

        public void UpdateProductOriginalPrices(Core.Models.ProductOriginalPrices entity)
        {
            ProductOriginalPricesRepository.Update(entity);
        }

        public void DeleteProductOriginalPrices(int id)
        {
           ProductOriginalPricesRepository.Delete(id);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<Core.Models.ProductOriginalPrices> FindAsync(int? id)
        {
            return ProductOriginalPricesRepository.GetByIdAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<Core.Models.ProductOriginalPrices> GetAllProductOriginalPrices()
        {
            return ProductOriginalPricesRepository.GetAll();
        }

        public int ProductOriginalPriceCount(int productId)
        {

            int i;
            i= _context.ProductOriginalPrices.Where(p=> p.ProductId== productId).Count();
            return i;
        }

        public IQueryable<Core.Models.Products> GetAllProducts()
        {
            return ProductRepository.GetAll();
        }

        public IQueryable<Countries> GetAllCountrieses()
        {
            return CountryDataRepository.GetAll();
        }

       

        public void Dispose()
        {
            this._context?.Dispose();
        }

        public Core.Models.ProductOriginalPrices GetFirstProductOriginalPrices(int productId)
        {
            return  ProductOriginalPricesRepository.Get(e => e.ProductId == productId);
        }
    }
}
