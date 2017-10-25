using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;

namespace Elmatgar.Areas.Admin.Controllers.BaseControllers
{
    public class DepartmentsController : Controller
    {
        private StoreDbContext db= new StoreDbContext();

        // GET: Admin/Departments
        public async Task<ActionResult> Index()
        {
            IQueryable<Departments> departmentses = db.Departments;
            return View(await departmentses.ToListAsync());
        }

        // GET: Admin/Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = await db.Departments.FindAsync(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // GET: Admin/Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentId,DDepartmentName,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(departments);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(departments);
        }

        // GET: Admin/Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = await db.Departments.FindAsync(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // POST: Admin/Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepartmentId,DDepartmentName,CreationDate,LastUpdateDate,CreatedBy,LastUpdatedBy")] Departments departments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departments).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(departments);
        }

        // GET: Admin/Departments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departments departments = await db.Departments.FindAsync(id);
            if (departments == null)
            {
                return HttpNotFound();
            }
            return View(departments);
        }

        // POST: Admin/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Departments departments = await db.Departments.FindAsync(id);
            db.Departments.Remove(departments);
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
