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
  public  class DiscountRulesUnit :IDisposable , IDiscountRulesUnit
  {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Core.Models.DiscountRules> discountRepository = null;
        private IDataRepository<Core.Models.Products> _Products = null;

        public IDataRepository<Core.Models.DiscountRules> DiscountRepository
      {



            get
            {
                if (this.discountRepository == null)

                {
                    this.discountRepository = new DataRepository<Elmatgar.Core.Models.DiscountRules>(this._context);
                }
                return this.discountRepository;
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

       

        public void AddDiscount(Core.Models.DiscountRules entity)
        {
                DiscountRepository.Add(entity);
        }

        public void UpdateDiscount(Core.Models.DiscountRules entity)
        {
            DiscountRepository.Update(entity);
        }

        public void DeleteDiscount(int id)
        {
           DiscountRepository.Delete(id);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<Core.Models.DiscountRules> FindAsync(int? id)
        {
            return DiscountRepository.GetByIdAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<Core.Models.DiscountRules> GetAllDiscount()
        {
            return DiscountRepository.GetAll();
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }

        public IQueryable<Core.Models.Products> GetAllProducts()
        {
            return ProductRepository.GetAll();
        }
    }
}
