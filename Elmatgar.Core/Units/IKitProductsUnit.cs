using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Elmatgar.Core.Units
{
    public  interface IKitProductsUnit
    {


        IDataRepository<KitProducts> KitRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        void AddKitProduct(KitProducts entity);

        void UpdateKitProduct(KitProducts entity);

        void DeleteKitProduct(int id);

        Task SaveChangesAsync();
        Task<Core.Models.KitProducts> FindAsync(int? id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<KitProducts> GetAllKitProductses();
        IQueryable<Products> GetAllProducts();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();

    }
}
