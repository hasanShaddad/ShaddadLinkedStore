using System.Data.Entity;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Web.Mvc;
using Elmatgar.ViewModels;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{

    public class OrdersAdminController : Controller
    {
        private readonly IOrdersUnit _db;
        private readonly IOrdersServices OrdersServices;

        public OrdersAdminController(IOrdersUnit db, IOrdersServices ordersServices)
        {
            this._db = db;
            OrdersServices = ordersServices;
        }

        // GET: Admin/OrdersAdmin
        public ActionResult Index()
        {

            return View(OrdersServices.GetAllOrdersByStatus(OrderStatus.Promoted.ToString()).ToList());
        }
        public ActionResult AdminTrackingOrders()
        {
            
            var order = _db.Orders.GetAll().ToList();
            return View(order);
        }
        public static void SendMessage(string msg)
        {

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.addNewMessageToPage("promoted orders", msg, "Noti");

        }

        public virtual void SendNoti()
        {
            int orderList = OrdersServices.GetOrderCount("promoted");
            ViewBag.OrdersCount = orderList;
            SendMessage(orderList.ToString());
        }

        //Todo: i'm gonna change the way we handel order status as following :
        // let the user change order line status  then after changing all the order lines  it will change the order status 
        // 

        //public ActionResult Shipping(int orderNo)
        //{

        //    //set order line to be canceled
        //    var lastorder = _db.Orders.GetById(orderNo);
        //    if (lastorder != null)
        //    {

        //        lastorder.OrderStatus = "shipping";
        //        _db.Orders.Update(lastorder);
        //        OrderDetailsShipping(lastorder.OrderDetails.ToList());
        //        _db.SaveChangesAsync();
        //        SendNoti();
        //    }
        //    return RedirectToAction("Index");
        //}
        //private void OrderDetailsShipping(List<OrderDetails> orderlines)
        //{

        //    foreach (OrderDetails line in orderlines)
        //    {
        //        line.OrderStatus = "shipping";
        //        _db.OrderDetails.Update(line) ;
        //        _db.SaveChangesAsync();

        //    }

        //}
    }
}