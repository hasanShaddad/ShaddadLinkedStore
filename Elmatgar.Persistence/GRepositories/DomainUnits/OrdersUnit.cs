using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public class OrdersUnit : IDisposable, IOrdersUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Core.Models.Orders> _Orders;
        private IDataRepository<OrderDetails> _OrderDetails;
        private IDataRepository<OrderPayments> _OrderPayments;


        public IDataRepository<Core.Models.Orders> Orders
        {
            get
            {
                if (this._Orders == null)
                {
                    this._Orders = new DataRepository<Core.Models.Orders>(_context);
                }
                return _Orders;
            }
        }

        public IDataRepository<OrderPayments> OrderPayments
        {
            get
            {
                if (this._OrderPayments == null)
                {
                    this._OrderPayments = new DataRepository<OrderPayments>(_context);
                }
                return this._OrderPayments;
            }
        }

        public IDataRepository<OrderDetails> OrderDetails
        {
            get
            {
                if (this._OrderDetails == null)
                {
                    this._OrderDetails = new DataRepository<OrderDetails>(_context);
                }
                return this._OrderDetails;
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
