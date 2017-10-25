using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Elmatgar.Core.Units
{
    public   interface IProductVoteUnit
    {

        IDataRepository<ProductVote> ProductVoteRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        void AddProductVote(ProductVote entity);

        void UpdateProductVote(ProductVote entity);

        void DeleteProductVote(int id);

        Task SaveChangesAsync();
        Task<Core.Models.ProductVote> FindAsync(int? id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<ProductVote> GetAllProductVotees();
        IQueryable<Products> GetAllProducts();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();



    }
}
