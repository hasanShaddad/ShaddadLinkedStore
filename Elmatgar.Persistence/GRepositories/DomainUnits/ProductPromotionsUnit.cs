using Elmatgar.Core.Units;
using System;
using System.Linq;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public  class ProductPromotionsUnit : IProductPromotionsUnit , IDisposable
    {


        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Core.Models.ProductPromotions> _ProductOriginalPrices = null;
        private IDataRepository<Core.Models.Products> _Products = null;

        private IDataRepository<Core.Models.Countries> _Country = null;

        public IDataRepository<Core.Models.ProductPromotions> ProductPromotionsRepository
        {
            get
            {
                if (this._ProductOriginalPrices == null)

                {
                    this._ProductOriginalPrices =
                        new DataRepository<Elmatgar.Core.Models.ProductPromotions>(this._context);
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



        public void AddProductPromotions(Core.Models.ProductPromotions entity)
        {
           ProductPromotionsRepository.Add(entity);
        }

        public void UpdateProductPromotions(Core.Models.ProductPromotions entity)
        {
           ProductPromotionsRepository.Update(entity);
        }

        public void DeleteProductPromotions(Core.Models.ProductPromotions entity)
        {
           ProductPromotionsRepository.Delete(entity );
        }

        public Task SaveChangesAsync()
        {
           return _context.SaveChangesAsync();
        }

        public Task<Core.Models.ProductPromotions> FindAsync(int? id)
        {
            return ProductPromotionsRepository.GetByIdAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<Core.Models.ProductPromotions> GetAllProductPromotions()
        {
            return ProductPromotionsRepository.GetAll();
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

        public Core.Models.ProductPromotions GetFirstProductPromotions(int id )
        {
            return ProductPromotionsRepository.Get(p => p.Id == id);
        }
    }
}
