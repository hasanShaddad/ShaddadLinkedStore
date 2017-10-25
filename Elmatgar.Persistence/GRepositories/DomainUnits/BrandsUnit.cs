using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public  class BrandsUnit :IDisposable, IBrandsUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Elmatgar.Core.Models.Brands> brands = null;

        public IDataRepository<Core.Models.Brands> BrandsRepository
        {


             get
            {
                if (this.brands == null)

                {
                    this.brands = new DataRepository<Elmatgar.Core.Models.Brands>(this._context);
                }
                return this.brands;
            }
        }
        public  void SaveChanges()
        {
          
             _context.Commit();

        }

        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }

        public async Task<Core.Models.Brands> FindAsync(int? id)
        {
         return  await  BrandsRepository.GetByIdAsync(id);
        }


        public IQueryable<Core.Models.Brands> GetAllBrands()
        {
            return BrandsRepository.GetAll();
        }


        public void Dispose()
        {
            this._context?.Dispose();
        }

        public void AddBrand(Core.Models.Brands entity)
        {
          BrandsRepository.Add(entity);
        }

        public void UpdateBrand(Core.Models.Brands entity)
        {
           BrandsRepository.Update(entity);
        }

        public void DeleteBrand(int id)
        {
           BrandsRepository.Delete(id);
        }
    }
}
