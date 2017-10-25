using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence.GRepositories;
using Microsoft.AspNet.Identity.Owin;
using ApplicationRole = Elmatgar.Core.Models.ApplicationRole;

namespace Elmatgar.Areas.Admin.Controllers.BaseControllers
{
    public class ApplicationRolesController : Controller
    {
        public ApplicationRolesController()
        {

        }

        public ApplicationRolesController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        //if non have been provdided for _userManager we get the oreginal UserManager
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            set { _userManager = value; }
        }
        //if non have been provdided for _roleManager we get the oreginal RoleManager
        private ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }

        }

      

        /// ////////////////////////////////////////////////////////////////////////////////


        // GET: Admin/ApplicationRoles
        public  ActionResult Index()
        {
           IQueryable<Core.Models.ApplicationRole> rols =  RoleManager.Roles;
            return View(rols);
        }



        // GET: Admin/ApplicationRoles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Core.Models.ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // GET: Admin/ApplicationRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// post /create using ApplicationRoleViewModel to add new role name
        /// </summary>
        /// <param name="applicationRoleViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name")] ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole { Name = applicationRoleViewModel.Name };
                var result = await RoleManager.CreateAsync(applicationRole);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                return RedirectToAction("Index");
            }

            return View(applicationRoleViewModel);
        }

        // GET: Admin/ApplicationRoles/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            ApplicationRoleViewModel applicationRoleViewModel = new ApplicationRoleViewModel { Id = applicationRole.Id, Name = applicationRole.Name };
            return View(applicationRoleViewModel);
        }

        // POST: Admin/ApplicationRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {

                ApplicationRole applicationRole = await RoleManager.FindByIdAsync(applicationRoleViewModel.Id);
                string originalName = applicationRole.Name;
                if (originalName == "Admin" && applicationRole.Name != "Admin")
                {
                    ModelState.AddModelError("", "you can not change the name of Admin role");
                    return View(applicationRoleViewModel);
                }
                if (originalName != "Admin" && applicationRole.Name == "Admin")
                {
                    ModelState.AddModelError("", "you can not change the name of Admin role");
                    return View(applicationRoleViewModel);
                }
                applicationRole.Name = applicationRoleViewModel.Name;
                await RoleManager.UpdateAsync(applicationRole);
                return RedirectToAction("Index");
            }
            return View(applicationRoleViewModel);
        }

        // GET: Admin/ApplicationRoles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // POST: Admin/ApplicationRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationRole applicationRole = await RoleManager.FindByIdAsync(id);
            if (applicationRole.Name == "Admin")
            {
                ModelState.AddModelError("", "you can not delete the Admin role");
                return View(applicationRole);
            }
            await RoleManager.DeleteAsync(applicationRole);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RoleManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
