using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    public class CitiesConfiguration : EntityTypeConfiguration<Cities>
    {
        /// <summary>
        /// Configuation for Brands table
        /// </summary>
        public CitiesConfiguration()
        {

          this.Property(e => e.CtCityName)
              .IsUnicode(false);


         this.HasMany(e => e.Zones)
                .WithOptional(e => e.Cities)
                .HasForeignKey(e => e.ACityId);
            this.HasMany(e => e.Orders)
               .WithOptional(e => e.Cities)
               .HasForeignKey(e => e.CCityId);

            this.HasMany(e => e.Customers)
                .WithOptional(e => e.Cities)
                .HasForeignKey(e => e.CCityId);

         this.HasMany(e => e.SupplierStores)
                .WithRequired(e => e.Cities)
                .HasForeignKey(e => e.CityId)
                 .WillCascadeOnDelete(false);
        }
    }
}