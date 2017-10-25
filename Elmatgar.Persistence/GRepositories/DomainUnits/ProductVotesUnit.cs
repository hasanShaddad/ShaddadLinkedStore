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
   public class ProductVotesUnit: IProductVoteUnit ,IDisposable
    {


        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<ProductVote> _productVote = null;
        private IDataRepository<Core.Models.Products> _Products = null;

        public IDataRepository<Core.Models.ProductVote> ProductVoteRepository
        {



            get
            {
                if (this._productVote == null)

                {
                    this._productVote = new DataRepository<Elmatgar.Core.Models.ProductVote>(this._context);
                }
                return this._productVote;
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
        public void AddProductVote(ProductVote entity)
        {
            ProductVoteRepository.Add(entity);
        }

        public void UpdateProductVote(ProductVote entity)
        {
            ProductVoteRepository.Update(entity);
        }

        public void DeleteProductVote(int id)
        {
           ProductVoteRepository.Delete(id);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<ProductVote> FindAsync(int? id)
        {
            return ProductVoteRepository.GetByIdAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<ProductVote> GetAllProductVotees()
        {
            return ProductVoteRepository.GetAll();
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
