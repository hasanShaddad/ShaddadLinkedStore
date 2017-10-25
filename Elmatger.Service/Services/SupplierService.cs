using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Elmatgar.Service.Services
{


    public class SupplierService : ISupplierService
    {
        private readonly StoreDbContext _context = new StoreDbContext();


        //public int GetLastSupplyOrderIDby

        /// <summary>
        /// get all supplier stores 
        /// </summary>
        /// <returns> querable of supplier stores and related cites , coountry , zones and suppliers  inforamtion</returns>
        public IQueryable<SupplierStores> GetAllSupplierStores(bool status)
        {
            IQueryable<SupplierStores> supplierStores = _context.SupplierStores
                .Include(s => s.Cities)
                .Include(s => s.Countries)
                .Include(s => s.Suppliers)
                .Include(s => s.Zones).Where(e => e.ActiveFlag == status);
            return supplierStores;
        }
        /// <summary>
        /// get all SupplyOrders and related  suppliers  inforamtion
        /// </summary>
        /// <returns> querable of  SupplyOrders and related  suppliers  inforamtion</returns>
        public IQueryable<SupplyOrders> GetAllSupplierOrders(bool status)
        {
            IQueryable<SupplyOrders> supplyOrders = _context.SupplyOrder.Include(d => d.Suppliers).Where(e => e.ActiveFlag == status);
            return supplyOrders;
        }
        /// <summary>
        /// get all SupplyOrderDetails 
        /// </summary>
        /// <returns>an querable of SupplyOrderDetails inciluding SupplyOrders and Products </returns>
        public IQueryable<SupplyOrderDetails> GetAllSupplyOrderDetails(bool status)
        {
            IQueryable<SupplyOrderDetails> supplyOrderDetails = _context.SupplyOrderDetails
              .Include(p => p.Products)
              .Include(p => p.SupplyOrders)
              .Where(e => e.ActiveFlag == status);
            return supplyOrderDetails;
        }

        /// <summary>
        /// allow use to autocomplete the supplier name 
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns> Queryable of supplier _ only the suppliername column</returns>
        public IQueryable<Suppliers> SupplierNameComplete(string prefix)
        {
            return _context.Suppliers
              .Where(e => e.SupplierName.StartsWith(prefix));
        }



        public int InventoryTransCountForSupplier(int id)
        {
            int count = _context.InventoryTrans.Count(x => x.ItSupplierNo == id);
            return count;
        }

        public int InventoryTransCountForSupplierStore(int id)
        {
            int count = _context.InventoryTrans.Count(x => x.ItStoreNo == id);
            return count;
        }

        public int InventoryTransCountForSupplyOrderDetailsBySorderId(int id)
        {
            int count = 0;
            List<SupplyOrderDetails> sOrderDetails = GetAllSupplyOrderDetailsBySupplyOrderId(id).ToList();
            foreach (var item in sOrderDetails)
            {
                count = +_context.InventoryTrans.Count(x => x.ItPoLineNo == id);

            }

            return count;
        }

        public int InventoryTransCountForSupplyOrderDetailsBySOrderDeatilsId(int id)
        {
            int totalinvent = _context.InventoryTrans.Count(x => x.ItOrderLineNo == id);
            return totalinvent;
        }

        public int SupplierStoresCountForSupplier(int id)
        {
            int count = _context.SupplierStores.Count(x => x.SupplierId == id);
            return count;

        }

        public int SupplyOrderDetailsCountForSupplyorderBySupplierId(int id)
        {
            int count = 0;
            List<SupplyOrders> SOrder = GetAllSupplyOrderBySupplierId(id).ToList();
            foreach (var item in SOrder)
            {
                count = +_context.SupplyOrderDetails.Count(x => x.SupplyOrderId == id);
            }

            return count;
        }

        public int SupplyOrderDetailsCountForSupplyorderBySupplyOrderId(int id)
        {
            return _context.SupplyOrderDetails.Count(x => x.SupplyOrderId == id);
        }

        public int SupplyOrderCountForSupplier(int id)
        {
            int count = _context.SupplyOrder.Count(x => x.SupplierId == id);
            return count;
        }
        public int SupplyorderpaymentCountForSupplier(int id)
        {
            int count = _context.SupplyOrderPayments.Count(x => x.SupplierId == id);
            return count;
        }

        public int SupplyorderpaymentCountForSupplyOrder(int id)
        {
            int count = _context.SupplyOrderPayments.Count(x => x.SupplyOrderId == id);
            return count;
        }


        public bool SupplierDependanceyStatus(int id, bool status, string user)
        {
            var supplier = _context.Suppliers
                .Include(e => e.SupplierStores)
                .Include(e => e.InventoryTranses)
                .Include(e => e.SupplyOrder)
                .Include(e => e.SupplyOrderPayments)
                .SingleOrDefault(e => e.Id == id);

            if (supplier != null)
            {
                foreach (var pay in supplier.SupplyOrderPayments.ToList())
                {
                    pay.ActiveFlag = status;
                    pay.LastUpdateDate = DateTime.Now;
                    pay.LastUpdatedBy = user;

                }
                foreach (var order in supplier.SupplyOrder.ToList())
                {
                    foreach (var odetails in order.SupplyOrderDetails.ToList())
                    {
                        odetails.ActiveFlag = status;
                        odetails.LastUpdateDate = DateTime.Now;
                        odetails.LastUpdatedBy = user;
                    }
                    order.ActiveFlag = status;
                    order.LastUpdateDate = DateTime.Now;
                    order.LastUpdatedBy = user;
                }
                foreach (var trans in supplier.InventoryTranses.ToList())
                {
                    trans.ActiveFlag = status;
                    trans.LastUpdateDate = DateTime.Now;
                    trans.LastUpdatedBy = user;


                }
                foreach (var store in supplier.SupplierStores.ToList())
                {
                    store.ActiveFlag = status;
                    store.LastUpdateDate = DateTime.Now;
                    store.LastUpdatedBy = user;
                }
                supplier.ActiveFlag = status;
                supplier.LastUpdateDate = DateTime.Now;
                supplier.LastUpdatedBy = user;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool SupplierStoresDependanceyStatus(int id, bool status, string user)
        {
            var store = _context.SupplierStores

                .Include(e => e.InventoryTrans)

                .SingleOrDefault(e => e.Id == id);

            if (store != null)
            {


                foreach (var trans in store.InventoryTrans.ToList())
                {
                    trans.ActiveFlag = status;
                    trans.LastUpdateDate = DateTime.Now;
                    trans.LastUpdatedBy = user;
                }

                store.ActiveFlag = status;
                store.LastUpdateDate = DateTime.Now;
                store.LastUpdatedBy = user;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool SupplyOrderDependanceyStatus(int id, bool status, string user)
        {
            var supplyOrder = _context.SupplyOrder

                .Include(E => E.SupplyOrdersPayments)
                 .Include(e => e.SupplyOrderDetails)
                 .SingleOrDefault(e => e.Id == id);

            if (supplyOrder != null)
            {
                foreach (var pay in supplyOrder.SupplyOrdersPayments.ToList())
                {
                    pay.ActiveFlag = status;
                    pay.LastUpdateDate = DateTime.Now;
                    pay.LastUpdatedBy = user;

                }
                foreach (var order in supplyOrder.SupplyOrderDetails.ToList())
                {
                    foreach (var odetails in order.InventoryTrans.ToList())
                    {
                        odetails.ActiveFlag = status;
                        odetails.LastUpdateDate = DateTime.Now;
                        odetails.LastUpdatedBy = user;
                    }
                    order.ActiveFlag = status;
                    order.LastUpdateDate = DateTime.Now;
                    order.LastUpdatedBy = user;
                }

                supplyOrder.ActiveFlag = status;
                supplyOrder.LastUpdateDate = DateTime.Now;
                supplyOrder.LastUpdatedBy = user;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool SupplyOrderDetailsDependanceyStatus(int id, bool status, string user)
        {
            var SOrderDtails = _context.SupplyOrderDetails
                .Include(e => e.InventoryTrans).SingleOrDefault(e => e.Id == id);
            if (SOrderDtails != null)
            {
                foreach (var odetails in SOrderDtails.InventoryTrans.ToList())
                {
                    odetails.ActiveFlag = status;
                    odetails.LastUpdateDate = DateTime.Now;
                    odetails.LastUpdatedBy = user;

                }
                SOrderDtails.ActiveFlag = status;
                SOrderDtails.LastUpdateDate = DateTime.Now;
                SOrderDtails.LastUpdatedBy = user;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        #region
        // unused methods 
        // i've created them to do some work with but i found a better way , i prefered to keep them here to use them if we need 

        public IQueryable<SupplyOrderDetails> GetAllSupplyOrderDetailsBySupplyOrderId(int SupplyOrderId)
        {
            IQueryable<SupplyOrderDetails> supplyOrderDetailses =
                _context.SupplyOrderDetails.Where(e => e.SupplyOrderId == SupplyOrderId);
            return supplyOrderDetailses;
        }

        public IQueryable<SupplyOrderPayments> GetAllSupplyOrderPaymentsBySupplierId(int supplierId)
        {
            IQueryable<SupplyOrderPayments> orderPaymentses =
                _context.SupplyOrderPayments.Where(e => e.SupplierId == supplierId);
            return orderPaymentses;
        }

        public IQueryable<SupplyOrders> GetAllSupplyOrderBySupplierId(int supplierId)
        {
            IQueryable<SupplyOrders> supplyOrderses
                = _context.SupplyOrder.Where(e => e.SupplierId == supplierId);
            return supplyOrderses;
        }

        public IQueryable<InventoryTrans> GetAllInventoryTransBySupplierId(int supplierId)
        {
            IQueryable<InventoryTrans> inventoryTranses =
                _context.InventoryTrans.Where(e => e.ItSupplierNo == supplierId);
            return inventoryTranses;
        }

        public IQueryable<SupplierStores> GetAllSupplierStoresBySupplierId(int? supplierId)
        {
            IQueryable<SupplierStores> supplierStoreses =
                _context.SupplierStores.Where(e => e.SupplierId == supplierId);
            return supplierStoreses;
        }


        /// <summary>
        /// get all SupplyOrderPayments 
        /// </summary>
        /// <returns>an querable of SupplyOrderPayments inciluding SupplyOrders and Suppliers </returns>
        public IQueryable<SupplyOrderPayments> GetAllSupplierOrderPayment()
        {
            IQueryable<SupplyOrderPayments> supplyOrderPayments = _context.SupplyOrderPayments
                .Include(p => p.SupplyOrders)
                .Include(p => p.Suppliers);
            return supplyOrderPayments;
        }

        /// <summary>
        /// get a list of suppliers by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an querable  of suppliers filtered by id  </returns>
        public IQueryable<Suppliers> GetSupplierById(int? id)
        {
            return _context.Suppliers.Where(e => e.Id == id);
        }


        /// <summary>
        /// get a list of suppliers by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an querable  of SupplyOrders filtered by id </returns>
        public IQueryable<SupplyOrders> GetSupplyOrdersById(int? id)
        {
            return _context.SupplyOrder.Where(e => e.Id == id);
        }


        public bool DeleteSupplyOrderBySupplierId(IQueryable<SupplyOrders> _supplyOrders)
        {

            try
            {
                _context.SupplyOrder.RemoveRange(_supplyOrders);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteSupplyOrderDetailsBySupplierId(int SupplierId)
        {

            try
            {
                List<SupplyOrders> SOrder = GetAllSupplyOrderBySupplierId(SupplierId).ToList();
                foreach (var item in SOrder)
                {
                    IQueryable<SupplyOrderDetails> supplyOrderDetails =
                        _context.SupplyOrderDetails.Where(e => e.SupplyOrderId == item.Id);
                    _context.SupplyOrderDetails.RemoveRange(supplyOrderDetails);
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeletesupplyorderpaymentBySupplierId(IQueryable<SupplyOrderPayments> _supplyOrderPayments)
        {

            try
            {
                _context.SupplyOrderPayments.RemoveRange(_supplyOrderPayments);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteSupplierStoreBySupplierId(IQueryable<SupplierStores> _SupplierStores)
        {
            try
            {
                _context.SupplierStores.RemoveRange(_SupplierStores);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteInventoryTransBySupplierId(IQueryable<InventoryTrans> _inventoryTrans)
        {

            try
            {
                _context.InventoryTrans.RemoveRange(_inventoryTrans);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion
    }
}
