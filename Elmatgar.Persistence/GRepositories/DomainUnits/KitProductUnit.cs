using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public class KitProductUnit :IKitProductsUnit, IDisposable


    {

        private   StoreDbContext _context =new StoreDbContext();
        private IDataRepository<KitProducts> _kitproduct = null;
        private IDataRepository<Core.Models.Products> _Products = null;

        public IDataRepository<Core.Models.KitProducts> KitRepository
        {



            get
            {
                if (this._kitproduct == null)

                {
                    this._kitproduct = new DataRepository<Elmatgar.Core.Models.KitProducts>(this._context);
                }
                return this._kitproduct;
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

        public void AddKitProduct(KitProducts entity)
        {
           KitRepository.Add(entity);
        }

        public void UpdateKitProduct(KitProducts entity)
        {
            KitRepository.Update(entity);
        }

        public void DeleteKitProduct(int id)
        {
           KitRepository.Delete(id);
        }

        public Task SaveChangesAsync()
        {
           return _context.SaveChangesAsync();
        }

        public Task<KitProducts> FindAsync(int? id)
        {
            return KitRepository.GetByIdAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<KitProducts> GetAllKitProductses()
        {
            return KitRepository.GetAll();
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
