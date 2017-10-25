using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Elmatgar.Core.Models
{
    public class ApplicationUser : IdentityUser, IAuditInfo,IActiveFlag
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        //add properties and columns to the users table
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public Boolean IsCustomer { get; set; }
        public Boolean IsUser { get; set; }


        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }

        // [Required]
        public virtual Customers Customers { get; set; }
        public virtual ProductVote ProductVote { get; set; }


        public virtual Users Users { get; set; }
         public IEnumerable<SelectListItem> RolesList { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
