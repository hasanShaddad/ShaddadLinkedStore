using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System.Threading.Tasks;

namespace Elmatgar.Core.Units
{
    public interface ISupplierUnit
    {
        IDataRepository<Suppliers> Suppliers { get; }
        IDataRepository<SupplierStores> SupplierStores { get; }
        IDataRepository<SupplyOrderDetails> SupplyOrderDetails { get; }
        IDataRepository<SupplyOrders> SupplyOrders { get; }
        IDataRepository<SupplyOrderPayments> SupplyOrderPayments { get; }
        /// <summary>
        /// saving all changes to database 
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// saving to database synchronously
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// dispose the database 
        /// </summary>
        void Dispose();
    }
}
