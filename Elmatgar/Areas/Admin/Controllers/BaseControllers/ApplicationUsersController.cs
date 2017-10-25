using Elmatgar.Core.Models;
using Elmatgar.persistence.GRepositories.DomainUnits;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Elmatgar.Areas.Admin.Controllers.BaseControllers
{
    public class ApplicationUsersController : Controller
    {
        public ApplicationUsersController()
        {

        }
        /// <summary>
        /// Application User Manager conttroller to mange acount
        /// </summary>
        /// <param name="userManager">from identity config class</param>
        /// <param name="roleManager">from identity config class</param>
        public ApplicationUsersController(ApplicationUserManager userManager,  ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;

        }


        private ApplicationUserManager _userManager;

        private ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            set { _userManager = value; }
        }

        private  ApplicationRoleManager _roleManager;

        private  ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get< ApplicationRoleManager>(); }
            set { _roleManager = value; }
        }

        /// <summary>
        /// GET: Admin/ApplicationUsers 
        /// </summary>
        /// <param name="sort">using viewbages</param>
        /// <param name="search">using view bags</param>
        /// <param name="page">using ToPagedList extention</param>
        /// <returns></returns>
        public ActionResult Index(string sort, string search, int? page)
        {
            ViewBag.FNameSort = string.IsNullOrEmpty(sort) ? "fname_desc" : string.Empty;
            ViewBag.LNameSort = sort == "lname" ? "lname_desc" : "lname";
            ViewBag.CDate = sort == "cdate" ? "cdate_desc" : "cdate";
            ViewBag.LDate = sort == "ldate" ? "ldate_desc" : "ldate";
            ViewBag.CurrentSearch = search;
            ViewBag.CurrentSort = sort;
            var users = UserManager.Users;

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(e => e.FirstName.StartsWith(search) || e.LasttName.StartsWith(search) || e.Email.StartsWith(search));
            }


            switch (sort)
            {
                case "fname_desc":
                    users = users.OrderByDescending(e => e.FirstName)
                        .ThenBy(e => e.LasttName);
                    break;
                case "lname":
                    users = users.OrderBy(e => e.LasttName);
                    break;
                case "lname_desc":
                    users = users.OrderByDescending(e => e.LasttName);
                    break;

                case "cdate":
                    users = users.OrderBy(e => e.CreationDate);
                    break;
                case "cdate_desc":
                    users = users.OrderByDescending(e => e.CreationDate);
                    break;
                case "ldate":
                    users = users.OrderBy(e => e.LastUpdateDate);
                    break;
                case "ldate_desc":
                    users = users.OrderByDescending(e => e.LastUpdateDate);
                    break;

                default:
                    users = users.OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LasttName);
                    break;
            }



            int pageSize = 3;
            int pageNumber = page ?? 1;
            return View(users.ToPagedList(pageNumber, pageSize));
        }



        // GET: Admin/ApplicationUsers/Edit/5
       public async Task<ActionResult> Edit(string id)
        {
           if (id == null)
         {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
              ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
             if (applicationUser == null)
             {
                return HttpNotFound();
            }
            
             var userRoles = await UserManager.GetRolesAsync(applicationUser.Id);
            applicationUser.RolesList = RoleManager.Roles.ToList().Select(r => new SelectListItem
 
             {
               Selected = userRoles.Contains(r.Name),
               
                Value = r.Name
           });
            if (applicationUser.IsUser)
            {
                DepartmentsUnit departmentsUnit = new DepartmentsUnit();
                ViewBag.Users_DepartmentId = applicationUser.Users.Departments.DDepartmentName;
            }
            return View(applicationUser);
        }

        // POST: Admin/ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id")] ApplicationUser applicationUser, params string[] roleSelectedOnView)
        {
            if (ModelState.IsValid)
            {
                //if the user currently stored has the admin role
                var rolesCurrentlyPersestedForUser = await UserManager.GetRolesAsync(applicationUser.Id);
                bool isThisUserAnAdmin = rolesCurrentlyPersestedForUser.Contains("Admin");
                //and the user did not has the Admin role cheked
                roleSelectedOnView = roleSelectedOnView ?? new string[] { };
                bool isThisUserAdminDeselected = !roleSelectedOnView.Contains("Admin");
                //and the current stored count of Users has Admin role=1

                var role = await RoleManager.FindByNameAsync("Admin");
                bool isOnlyOneAdmin = role.Users.Count == 1;
                //pupulate the roles list in case we have to return to the edit view
              //  applicationUser = await UserManager.FindByIdAsync(applicationUser.Id);
                applicationUser.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem
                {
                    Selected = rolesCurrentlyPersestedForUser.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                });
                //refuse to remove the admin
                if (isThisUserAnAdmin && isThisUserAdminDeselected && isOnlyOneAdmin)
                {
                    ModelState.AddModelError("", "this is the only Admin you have .... you can not delete him ... you have to keep at least one Admin");
                    return View(applicationUser);
                }

                var result = await UserManager.AddToRolesAsync
                    (
                    applicationUser.Id,
                    roleSelectedOnView.Except(rolesCurrentlyPersestedForUser).ToArray()
                    );
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(applicationUser);
                }
                result = await UserManager.RemoveFromRolesAsync
                    (
                        applicationUser.Id,
                        rolesCurrentlyPersestedForUser.Except(roleSelectedOnView).ToArray()
                    );
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(applicationUser);
                }


                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "there is something wrong please call tec support");
            return View(applicationUser);
        }

        public async Task<ActionResult> LockAccount([Bind(Include = "Id")] string id)
        {
            await UserManager.ResetAccessFailedCountAsync(id);
            await UserManager.SetLockoutEndDateAsync(id, DateTime.UtcNow.AddYears(100));
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> UnLockAccount([Bind(Include = "Id")] string id)
        {
            await UserManager.ResetAccessFailedCountAsync(id);
            await UserManager.SetLockoutEndDateAsync(id, DateTime.UtcNow.AddYears(-1));
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
