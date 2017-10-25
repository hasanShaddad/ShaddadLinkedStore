using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System;
using System.Threading.Tasks;
using Elmatgar.Core.Units;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public class InventoryUnit : IDisposable, IInventoryUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<InventoryTrans> _inventoryTrans;
        private IDataRepository<Returns> _returns;


        public IDataRepository<Returns> Returns
        {
            get
            {
                if (this._returns == null)
                {
                    this._returns = new DataRepository<Returns>(_context);
                }
                return this._returns;
            }
        }

        public IDataRepository<InventoryTrans> InventoryTrans
        {
            get
            {
                if (this._inventoryTrans == null)
                {
                    this._inventoryTrans = new DataRepository<InventoryTrans>(_context);
                }
                return this._inventoryTrans;
            }
        }


        /// <summary>
        /// saving all changes to database 
        /// </summary>

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// saving to database synchronously
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// dispose the database 
        /// </summary>

        public void Dispose()
        {
            this._context?.Dispose();
        }
    }
}