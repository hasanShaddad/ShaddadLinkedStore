using System;
using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            
              HasOptional(s => s.Customers) // Mark customer property optional in applicationUser entity----hassan shaddad1-11-2016
              .WithRequired(ad => ad.users).WillCascadeOnDelete(false);

            //  HasRequired(s => s.Customers)it is not required----hassan shaddad1-11-2016
            //.WithRequiredDependent(ad => ad.users).WillCascadeOnDelete(false)
             HasOptional(s => s.ProductVote) // Mark customer property optional in applicationUser entity----hassan shaddad1-11-2016
            .WithOptionalPrincipal(ad => ad.User).WillCascadeOnDelete(false);

            HasOptional(s => s.Users) // Mark customer property optional in applicationUser entity----hassan shaddad1-11-2016
            .WithOptionalPrincipal(ad => ad.users).WillCascadeOnDelete(false);
            // Configure userId as FK for ProductVote

            //HasOptional(s => s.ProductVote)
            //.WithOptionalPrincipal(ad => ad.Users).WillCascadeOnDelete(false);//users is required ----hassan shaddad1-11-2016

            //  modelBuilder.Entity<Users>()
            //.HasKey(e => e.Id);

            // Configure userId as FK for customers

            //  HasRequired(s => s.Users)
            //.WithRequiredPrincipal(ad => ad.users).WillCascadeOnDelete(false);

            // Configure userId as FK for customers

            /////now Only by Amera Sadek    Ignore(s => s.RolesList);
            //// where is m:m roles? relation --- hassan shaddad 1-11-2016

        }

        private void Ignore(Func<ApplicationUser, object> p)
        {
            throw new NotImplementedException();
        }
    }
}