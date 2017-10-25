using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class ZonesConfiguration : EntityTypeConfiguration<Zones>
    {
        public ZonesConfiguration()
        {
 
         Property(e => e.AAreaName)
         .IsUnicode(false);

         HasMany(e => e.Customers)
              .WithOptional(e => e.Zones)
              .HasForeignKey(e => e.CAreaId);
            HasMany(e => e.SupplierStores)
                .WithOptional(e => e.Zones);
            this.HasMany(e => e.Orders)
              .WithOptional(e => e.Zones)
              .HasForeignKey(e => e.CAreaId);

            HasMany(e => e.SupplierStores)
                  .WithOptional(e => e.Zones)
                  .HasForeignKey(e => e.AreaId);
        }
    }
}