using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{


    public class SupplierUnit : IDisposable, ISupplierUnit
    { /// <summary>
      /// an unit of work for all generic repositories only 
      ///  an unit of work for suppliers and any related classes 

        /// 
        /// </summary>

        private IDataRepository<Suppliers> _Suppliers = null;
        private IDataRepository<SupplierStores> _SupplierStores = null;
        private IDataRepository<SupplyOrderDetails> _SupplyOrderDetails = null;
        private IDataRepository<SupplyOrderPayments> _SupplyOrderPayments = null;
        private IDataRepository<SupplyOrders> _SupplyOrder = null;
        private StoreDbContext _context = new StoreDbContext();



        //public SupplierUnit(StoreDbContext context)
        //{
        //    _context = context;
        //    SupplierStores = new DataRepository<SupplierStores>(context);
        //    Suppliers = new DataRepository<Suppliers>(context);
        //}
        public IDataRepository<Suppliers> Suppliers
        {
            get
            {
                if (this._Suppliers == null)

                {
                    this._Suppliers = new DataRepository<Suppliers>(this._context);
                }
                return this._Suppliers;
            }
        }

        public IDataRepository<SupplyOrderDetails> SupplyOrderDetails
        {
            get
            {
                if (this._SupplyOrderDetails == null)
                {
                    this._SupplyOrderDetails = new DataRepository<SupplyOrderDetails>(_context);
                }
                return this._SupplyOrderDetails;
            }
        }

        public IDataRepository<SupplyOrders> SupplyOrders
        {
            get
            {
                if (this._SupplyOrder == null)
                {
                    this._SupplyOrder = new DataRepository<SupplyOrders>(_context);
                }
                return this._SupplyOrder;
            }
        }

        public IDataRepository<SupplyOrderPayments> SupplyOrderPayments
        {
            get
            {
                if (this._SupplyOrderPayments == null)
                {
                    this._SupplyOrderPayments = new DataRepository<SupplyOrderPayments>(_context);
                }
                return this._SupplyOrderPayments;
            }
        }
        public IDataRepository<SupplierStores> SupplierStores
        {
            get
            {
                if (this._SupplierStores == null)

                {
                    this._SupplierStores = new DataRepository<SupplierStores>(this._context);
                }
                return this._SupplierStores;
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
