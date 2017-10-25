using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    public class CustomersConfiguration : EntityTypeConfiguration<Customers>
    {
        /// <summary>
        /// Configuation for Brands table
        /// </summary>
        public CustomersConfiguration()
        {
            
        HasKey(e => e.CCustNo);
            
           Property(c => c.CCustNo)
           .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //for make the id forgin key

     
           HasKey(e => e.Id);

            Property(e => e.CFirstName)
                .IsUnicode(false);

 
                Property(e => e.CLastName)
                .IsUnicode(false);

 
                Property(e => e.CEmail)
                .IsUnicode(false);

 
                Property(e => e.CMobile)
                .IsUnicode(false);

 
                Property(e => e.CHomePhone)
                .IsUnicode(false);

 
                Property(e => e.CWorkPhone)
                .IsUnicode(false);

 
                Property(e => e.CAddress)
                .IsUnicode(false);

 
                Property(e => e.CLang)
                .IsUnicode(false);

 
                Property(e => e.CLong)
                .IsUnicode(false);

 
                Property(e => e.CCustStatus)
                .IsUnicode(false);

 
                Property(e => e.CCustClass)
                .IsUnicode(false);

 
                Property(e => e.CPassword)
                .IsUnicode(false);

 
                HasMany(e => e.SalesOrderPayments)
                .WithOptional(e => e.Customers)
                .HasForeignKey(e => e.CustomerId);

 
                HasMany(e => e.Order)
                .WithOptional(e => e.Customers)
                .HasForeignKey(e => e.CustomerId);
        }
    }
}