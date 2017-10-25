using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.persistence;
using System.Data.Entity;
using System.Linq;

namespace Elmatgar.Service.Services
{
    public class InventoryServices : IInventoryServices
    {
        private readonly StoreDbContext _context = new StoreDbContext();
        /// <summary>
        /// get an InventoryTrans filtered by ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public InventoryTrans GetTransaction(int? productId)
        {
            return _context.InventoryTrans.ToList().LastOrDefault(e => e.ItProdId == productId);
        }
        public InventoryTrans GetTransactionByOrderDetilsId(int orderDetailsId)
        {
            return _context.InventoryTrans.ToList().LastOrDefault(e => e.ItPoLineNo == orderDetailsId);
        }

        /// <summary>
        /// get an querable of inventoryTransc including OrderDetails ,Products ,SupplyOrderDetails, Suppliers & SupplierStores information 
        /// </summary>
        /// <returns></returns>
        public IQueryable<InventoryTrans> GetAllInventoryTranses()
        {
            IQueryable<InventoryTrans> inventoryTrans = _context.InventoryTrans
                .Include(i => i.OrderDetails)
                .Include(i => i.Products)
                .Include(i => i.SupplyOrderDetails)
                .Include(i => i.Suppliers)
                .Include(i => i.SupplierStores);
            return inventoryTrans;
        }

        public int GetQuntity(int productid)
        {
            //var getLastValue = _context.InventoryTrans.Where(e => e.ItPoLineNo == productid);
            //var value = getLastValue.OrderByDescending(e => e.Id).Take(1).Single().ItQty;
            //if (value != null)
            //    return value.Value;
            //return 0;
            // var lastOrDefault = _context.InventoryTrans.LastOrDefault(e => e.ItPoLineNo == productid);
            var lastOrDefault = _context.InventoryTrans.Where(e => e.ItProdId == productid).OrderByDescending(e => e.CreationDate).FirstOrDefault();
            if (lastOrDefault != null)
            {
                if (lastOrDefault.ItQty != null)
                    return lastOrDefault.ItQty.Value;
            }
            else
            {
                return 0;
            }
            return 0;
        }

    }

}
