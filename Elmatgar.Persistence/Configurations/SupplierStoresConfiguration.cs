using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class SupplierStoresConfiguration : EntityTypeConfiguration<SupplierStores>
    {
        public SupplierStoresConfiguration()
        {


            Property(e => e.StoreName)
               .IsRequired()
               .IsUnicode(false);


            //Property(e => e.SsLang)
            //.IsUnicode(false);


            //Property(e => e.SsLong)
            //.IsUnicode(false);


            HasMany(e => e.InventoryTrans)
            .WithOptional(e => e.SupplierStores)
            .HasForeignKey(e => e.ItStoreNo)
          .WillCascadeOnDelete(false);
            // audited by eman herawy
        }
    }
}