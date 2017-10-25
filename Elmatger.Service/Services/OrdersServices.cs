using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.persistence;
using System.Data.Entity;
using System.Linq;

namespace Elmatgar.Service.Services
{
    public class OrdersServices : IOrdersServices
    {
        private readonly StoreDbContext _context = new StoreDbContext();



        /// <summary>
        /// get all Orders and related Customers  inforamtion
        /// </summary>
        /// <returns> querable of Orders and related Customers  inforamtion</returns>
        public IQueryable<Orders> GetAllOrders()
        {
            IQueryable<Orders> Orders = _context.Orders
                .Include(s => s.Customers);
            return Orders;
        }
        /// <summary>
        /// get all OrderPayments and related Customers And Order inforamtion
        /// </summary>
        /// <returns> querable of OrderPayments and related Customers And Order inforamtion</returns>
        public IQueryable<OrderPayments> GetAllOrderPayments()
        {
            IQueryable<OrderPayments> OrderPayments = _context.OrderPayments.Include(s => s.Customers).Include(s => s.Order);
            return OrderPayments;
        }

        /// <summary>
        /// get all OrderDetails  and related Products And Order inforamtion
        /// </summary>
        /// <returns> querable of OrderDetails and related Products And Order inforamtion</returns>
        public IQueryable<OrderDetails> GetAllOrderDetails()
        {
            IQueryable<OrderDetails> OrderDetails = _context.OrderDetails.Include(o => o.Products).Include(o => o.Order);
            return OrderDetails;
        }

        /// <summary>
        /// get all orders filtering by Order status
        /// </summary>
        /// <returns> querable of orders filtering by order status </returns>
        public IQueryable<Orders> GetAllOrdersByStatus(string orderStatus)
        {
            IQueryable<Orders> orders = _context.Orders.Where(e => e.OrderStatus == orderStatus);
            return orders;
        }

        public IQueryable<OrderDetails> GetAllOrderDetailsByStatus(string orderDetailStatus)
        {
            IQueryable<OrderDetails> orderDetails = _context.OrderDetails.Where(e => e.OrderStatus == orderDetailStatus);
            return orderDetails;
        }

        /// <summary>
        /// get LastOrDefault order filltering by id
        /// </summary>
        /// <returns> LastOrDefault order filltering by id </returns>
        public Orders GetLastOrDefaultOrder(int orderNo)
        {
            //set order line to be canceled
            var lastorder = _context.Orders.ToList().LastOrDefault(e => e.Id == orderNo);
            return lastorder;
        }



        /// <summary>
        /// get LastOrDefault OrderDetails filltering by id
        /// </summary>
        /// <returns> LastOrDefault OrderDetails filltering by id </returns>
        public OrderDetails GetLastOrDefaultOrderDetails(int orderDetailsId)
        {
            //set order line to be canceled
            var lastorder = _context.OrderDetails.ToList().LastOrDefault(e => e.Id == orderDetailsId);
            return lastorder;
        }

        public IQueryable<OrderDetails> GetAllOrderDetailsByOrderId(int orderid, string orderstatus)
        {
            IQueryable<OrderDetails> orderDetails = _context.OrderDetails
               .Include(o => o.Products).Include(o => o.Order)
               .Where(e => e.OrderId == orderid && e.OrderStatus == orderstatus);
            return orderDetails;
        }


        /// <summary>
        /// get (int ) the totall number of orders with  status promoted
        /// </summary>
        /// <returns> (int )  count  of order with status promoted</returns>
        public int GetOrderCount(string orderStatus)
        {
            int orderList = _context.Orders.Count(c => c.OrderStatus == orderStatus);
            return orderList;
        }
        public int GetOrderDetailsCount(string orderDetailsStatus)
        {
            int orderDetailsList = _context.OrderDetails.Count(c => c.OrderStatus == orderDetailsStatus);
            return orderDetailsList;
        }
    }
}
