using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.Inventory
{
    public class OrderPaymentsController : Controller
    {
        private readonly IOrdersUnit db;
        private readonly IOrdersServices OrdersServices;
        private readonly IUsersUnit UsersUnit;

        public OrderPaymentsController(IOrdersUnit db, IOrdersServices ordersServices, IUsersUnit usersUnit)
        {
            this.db = db;
            OrdersServices = ordersServices;
            UsersUnit = usersUnit;
        }

        // GET: Admin/OrderPayments
        public async Task<ActionResult> Index()
        {
            return View(await OrdersServices.GetAllOrderPayments().ToListAsync());
        }

        // GET: Admin/OrderPayments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPayments orderPayments = await db.OrderPayments.GetByIdAsync(id);
            if (orderPayments == null)
            {
                return HttpNotFound();
            }
            return View(orderPayments);
        }

        // GET: Admin/OrderPayments/Create
        public ActionResult Create()
        {
            ViewBag.SopCustNo = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName");
            ViewBag.SopOrderNo = new SelectList(db.Orders.GetAll(), "Id", "CustomerId");
            return View();
        }

        // POST: Admin/OrderPayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerId,OrderId,Paid,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] OrderPayments orderPayments)
        {
            if (ModelState.IsValid)
            {
                db.OrderPayments.Add(orderPayments);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SopCustNo = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName", orderPayments.CustomerId);
            ViewBag.SopOrderNo = new SelectList(db.Orders.GetAll(), "Id", "CustomerId", orderPayments.OrderId);
            return View(orderPayments);
        }

        // GET: Admin/OrderPayments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPayments orderPayments = await db.OrderPayments.GetByIdAsync(id);
            if (orderPayments == null)
            {
                return HttpNotFound();
            }
            ViewBag.SopCustNo = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName", orderPayments.CustomerId);
            ViewBag.SopOrderNo = new SelectList(db.Orders.GetAll(), "Id", "CustomerId", orderPayments.OrderId);
            return View(orderPayments);
        }

        // POST: Admin/OrderPayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerId,OrderId,Paid,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] OrderPayments orderPayments)
        {
            if (ModelState.IsValid)
            {
                db.OrderPayments.Update(orderPayments);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SopCustNo = new SelectList(UsersUnit.Customer.GetAll(), "Id", "CFirstName", orderPayments.CustomerId);
            ViewBag.SopOrderNo = new SelectList(db.Orders.GetAll(), "Id", "CustomerId", orderPayments.OrderId);
            return View(orderPayments);
        }

        // GET: Admin/OrderPayments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderPayments = await db.OrderPayments.GetByIdAsync(id);
            if (orderPayments == null)
            {
                return HttpNotFound();
            }
            return View(orderPayments);
        }

        // POST: Admin/OrderPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderPayments orderPayments = await db.OrderPayments.GetByIdAsync(id);
            db.OrderPayments.Delete(orderPayments);
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
