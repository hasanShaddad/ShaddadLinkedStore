using Elmatgar.Core.Models;
using System.Linq;

namespace Elmatgar.Core.Services
{
    public interface IOrdersServices
    {
        /// <summary>
        /// get all Orders  and related Customers  inforamtion
        /// </summary>
        /// <returns> querable of Orders and related Customers  inforamtion</returns>
        IQueryable<Orders> GetAllOrders();

        /// <summary>
        /// get all OrderPayments and related Customers And Order inforamtion
        /// </summary>
        /// <returns> querable of OrderPayments and related Customers And Order inforamtion</returns>
        IQueryable<OrderPayments> GetAllOrderPayments();

        /// <summary>
        /// get all OrderDetails and related Products And Order inforamtion
        /// </summary>
        /// <returns> querable of OrderDetails and related Products And Order inforamtion</returns>
        IQueryable<OrderDetails> GetAllOrderDetails();

        /// <summary>
        /// get all orders filtering by Order status
        /// </summary>
        /// <returns> querable of orders filtering by order status </returns>
        IQueryable<Orders> GetAllOrdersByStatus(string orderStatus);
        /// <summary>
        /// get all OrderDetails filtering by Order status
        /// </summary>
        /// <returns> querable of OrderDetails filtering by order status </returns>
        IQueryable<OrderDetails> GetAllOrderDetailsByStatus(string orderDetailStatus);

        /// <summary>
        /// get LastOrDefault order 
        /// </summary>
        /// <returns> LastOrDefault order filltering by id </returns>
        Orders GetLastOrDefaultOrder(int orderNo);

        /// <summary>
        /// get (int ) the totall number of orders with  status promoted
        /// </summary>
        /// <returns> (int )  count  of order with status promoted</returns>
        int GetOrderCount(string orderStatus);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        int GetOrderDetailsCount(string orderStatus);

        /// <summary>
        /// get LastOrDefault OrderDetails filltering by id
        /// </summary>
        /// <returns> LastOrDefault OrderDetails filltering by id </returns>
        OrderDetails GetLastOrDefaultOrderDetails(int orderDetailsId);
        /// <summary>
        /// get all OrderDetails and related Products And Order inforamtion by order id 
        /// </summary>
        /// <returns> querable of OrderDetails and related Products And Order inforamtion</returns>
        IQueryable<OrderDetails> GetAllOrderDetailsByOrderId(int orderid, string orderstatus);
    }
}