using Elmatgar.Core.Models;
using System.Linq;

namespace Elmatgar.Core.Services
{
    public interface ISupplierService
    {
        /// <summary>
        /// get all supplier stores 
        /// </summary>
        /// <returns> querable of supplier stores and related cites , coountry , zones and suppliers  inforamtion</returns>
        IQueryable<SupplierStores> GetAllSupplierStores(bool status);

        /// <summary>
        /// get all SupplyOrderPayments 
        /// </summary>
        /// <returns>an querable of SupplyOrderPayments inciluding SupplyOrders and Suppliers </returns>
        IQueryable<SupplyOrderPayments> GetAllSupplierOrderPayment();

        /// <summary>
        /// get a list of suppliers by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an querable  of suppliers filtered by id  </returns>
        IQueryable<Suppliers> GetSupplierById(int? id);

        /// <summary>
        /// get a list of suppliers by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an querable  of SupplyOrders filtered by id </returns>
        IQueryable<SupplyOrders> GetSupplyOrdersById(int? id);

        /// <summary>
        /// get all SupplyOrders and related  suppliers  inforamtion
        /// </summary>
        /// <returns> querable of  SupplyOrders and related  suppliers  inforamtion</returns>
        IQueryable<SupplyOrders> GetAllSupplierOrders(bool status);



        /// <summary>
        /// get all SupplyOrderDetails 
        /// </summary>
        /// <returns>an querable of SupplyOrderDetails inciluding SupplyOrders and Products </returns>
        IQueryable<SupplyOrderDetails> GetAllSupplyOrderDetails(bool status);

        /// <summary>
        /// allow use to autocomplete the supplier name 
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns> Queryable of supplier _ only the suppliername column</returns>
        IQueryable<Suppliers> SupplierNameComplete(string prefix);



        /// <summary>
        /// get the total number of supplier stores for spacefic supplier  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int SupplierStoresCountForSupplier(int id);


        /// <summary>
        /// get the total number of InventoryTrans for spacefic supplier  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int InventoryTransCountForSupplier(int id);
        /// <summary>
        /// get the total number of InventoryTrans for spacefic supplierStore  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int InventoryTransCountForSupplierStore(int id);
        /// <summary>
        /// get the total number of InventoryTrans for spacefic Supply Order Details by  supply order id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int InventoryTransCountForSupplyOrderDetailsBySorderId(int id);
        /// <summary>
        /// get the total number of InventoryTrans for spacefic Supply Order Details by Supply Order Details id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int InventoryTransCountForSupplyOrderDetailsBySOrderDeatilsId(int id);


        /// <summary>
        /// get the total number of Supply Order Details for spacefic supplier  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int SupplyOrderDetailsCountForSupplyorderBySupplierId(int id);
        /// <summary>
        /// get the total number of Supply Order Details    by Order id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int SupplyOrderDetailsCountForSupplyorderBySupplyOrderId(int id);


        /// <summary>
        /// get the total number of Supply Order for spacefic supplier  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int SupplyOrderCountForSupplier(int id);

        /// <summary>
        /// get the total number of supply order payment for spacefic supplier  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int SupplyorderpaymentCountForSupplier(int id);
        /// <summary>
        /// get the total number of supply order payment for spacefic SupplyOrder  by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int SupplyorderpaymentCountForSupplyOrder(int id);



        bool SupplierDependanceyStatus(int id, bool status, string user);
        bool SupplierStoresDependanceyStatus(int id, bool status, string user);
        bool SupplyOrderDependanceyStatus(int id, bool status, string user);
        bool SupplyOrderDetailsDependanceyStatus(int id, bool status, string user);
        #region
        // unused methods 
        // i've created them to do some work with but i found a better way , i prefered to keep them here to use them if we need 

        /// <summary>
        /// get all SupplyOrderDetails by SupplyOrderId 
        /// </summary>
        /// <returns>  </returns>
        IQueryable<SupplyOrderDetails> GetAllSupplyOrderDetailsBySupplyOrderId(int SupplyOrderId);
        /// <summary>
        /// get all SupplyOrder Payments by supplier id 
        /// </summary>
        /// <returns>  </returns>
        IQueryable<SupplyOrderPayments> GetAllSupplyOrderPaymentsBySupplierId(int supplierId);
        /// <summary>
        /// get all SupplyOrder  by supplier id 
        /// </summary>
        /// <returns>  </returns>
        IQueryable<SupplyOrders> GetAllSupplyOrderBySupplierId(int supplierId);
        /// <summary>
        /// get all InventoryTrans by supplier id 
        /// </summary>
        /// <returns>  </returns>
        IQueryable<InventoryTrans> GetAllInventoryTransBySupplierId(int supplierId);

        /// <summary>
        /// get all SupplierStores by supplier id 
        /// </summary>
        /// <returns>  </returns>
        IQueryable<SupplierStores> GetAllSupplierStoresBySupplierId(int? supplierId);



        /// <summary>
        /// Delete supply order details  by supplier id
        /// </summary>
        /// <param name="id"></param>
        bool DeleteSupplyOrderDetailsBySupplierId(int SupplierId);
        /// <summary>
        /// Delete InventoryTrans by supplier id
        /// </summary>
        /// <param name="id"></param>
        bool DeleteInventoryTransBySupplierId(IQueryable<InventoryTrans> _inventoryTrans);
        /// <summary>
        /// Delete Supply Order by supplier id
        /// </summary>
        /// <param name="id"></param>
        bool DeleteSupplyOrderBySupplierId(IQueryable<SupplyOrders> _supplyOrders);
        /// <summary>
        /// Delete supply order payment by supplier id
        /// </summary>
        /// <param name="id"></param>
        bool DeletesupplyorderpaymentBySupplierId(IQueryable<SupplyOrderPayments> _supplyOrderPayments);
        /// <summary>
        /// Delete Suppliers Stores by supplier  
        /// </summary>
        /// <param name="id"></param>
        bool DeleteSupplierStoreBySupplierId(IQueryable<SupplierStores> _SupplierStores);
        #endregion

    }
}