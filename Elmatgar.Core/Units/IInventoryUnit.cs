using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
    public interface IInventoryUnit
    {
        IDataRepository<Returns> Returns { get; }
        IDataRepository<InventoryTrans> InventoryTrans { get; }

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