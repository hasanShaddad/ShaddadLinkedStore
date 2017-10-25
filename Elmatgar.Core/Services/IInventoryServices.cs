using Elmatgar.Core.Models;
using System.Linq;

namespace Elmatgar.Core.Services
{
    public interface IInventoryServices
    {
        /// <summary>
        /// get an InventoryTrans filtered by ProductId
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        InventoryTrans GetTransaction(int? productId);

        /// <summary>
        /// get an querable of inventoryTransc including OrderDetails ,Products ,SupplyOrderDetails, Suppliers & SupplierStores information 
        /// </summary>
        /// <returns></returns>
        IQueryable<InventoryTrans> GetAllInventoryTranses();

        /// <summary>
        /// get product quantity in inventory 
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        int GetQuntity(int productid);

        InventoryTrans GetTransactionByOrderDetilsId(int orderDetailsId);
    }
}