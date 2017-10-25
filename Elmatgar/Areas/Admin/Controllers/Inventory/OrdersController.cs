using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class OrdersController : Controller
    {
        private readonly IOrdersUnit db;
        private readonly IOrdersServices OrdersServices;
        private readonly IUsersUnit UsersUnit;

        public OrdersController(IOrdersUnit db, IOrdersServices ordersServices, IUsersUnit usersUnit)
        {
            this.db = db;
            OrdersServices = ordersServices;
            UsersUnit = usersUnit;
        }

        // GET: Admin/Order
        public async Task<ActionResult> Index()
        {
            ;
            return View(await OrdersServices.GetAllOrders().ToListAsync());
        }

        // GET: Admin/Order/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders Order = await db.Orders.GetByIdAsync(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName");
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,CustomerId,OrderStatus,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Orders
                order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Orders.Add(order);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }


            ViewBag.CustomerId = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName", order.CustomerId);
            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders Order = await db.Orders.GetByIdAsync(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName", Order.CustomerId);
            return View(Order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerId,OrderStatus,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Orders Order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Update(Order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName", Order.CustomerId);
            return View(Order);
        }

        // GET: Admin/Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Order = await db.Orders.GetByIdAsync(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Orders Order = await db.Orders.GetByIdAsync(id);
            db.Orders.Delete(Order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
