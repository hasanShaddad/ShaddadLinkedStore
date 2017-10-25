using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System.Threading.Tasks;

namespace Elmatgar.Core.Units
{
    public interface IOrdersUnit
    {
        IDataRepository<Core.Models.Orders> Orders { get; }
        IDataRepository<OrderPayments> OrderPayments { get; }
        IDataRepository<OrderDetails> OrderDetails { get; }


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