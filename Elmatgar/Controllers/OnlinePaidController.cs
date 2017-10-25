using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;
using Elmatgar.ViewModels;
using TwoCheckout;

namespace Elmatgar.Controllers
{
    public class OnlinePaidController : Controller
    {
        private readonly StoreDbContext _db = new StoreDbContext();
        // GET: onlinepaid
        public ActionResult Index(int orderId)
        {
            Orders orders = _db.Orders.FirstOrDefault(o => o.Id == orderId)  ;
            CheckOutViewModel  checkOutViewModel = new CheckOutViewModel();
            ;
            if (orders != null)
            {
                checkOutViewModel.Total = orders.Total;
              
            }
            else
            {
                checkOutViewModel.Total = 0;
            }
            return View(checkOutViewModel);
        }


        public ActionResult Process([Bind(Include = "Id,Total")] Orders order)
        {
            TwoCheckoutConfig.SellerID = "103107139";
            TwoCheckoutConfig.PrivateKey = "B97C8E66-F9F8-4987-8254-BC0DDFF761A2";
            TwoCheckoutConfig.Sandbox = true;

            try
            {
                var Billing = new AuthBillingAddress();
                Billing.addrLine1 = "123 test st";
                Billing.city = "Columbus";
                Billing.zipCode = "43123";
                Billing.state = "OH";
                Billing.country = "USA";
                Billing.name = "Testing Tester";
                Billing.email = "example@2co.com";
                Billing.phoneNumber = "5555555555";

                var Customer = new ChargeAuthorizeServiceOptions();
                Customer.total =  order.Total;
                Customer.currency = "USD";
                Customer.merchantOrderId = "123";
                Customer.billingAddr = Billing;
                Customer.token = Request["token"];

                var Charge = new ChargeService();

                var result = Charge.Authorize(Customer);
                ViewBag.Message = result.responseMsg;
            }
            catch (TwoCheckoutException e)
            {
                ViewBag.Message = e.Message.ToString();
            }

            return View();
        }
    }
}
   