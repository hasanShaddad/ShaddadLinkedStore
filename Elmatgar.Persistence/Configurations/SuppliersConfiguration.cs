using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class SuppliersConfiguration : EntityTypeConfiguration<Suppliers>
    {
        public SuppliersConfiguration()
        {

             
                Property(e => e.SupplierName)
                .IsRequired()
                .IsUnicode(false);

             
               HasMany(e => e.SupplyOrderPayments)
                .WithRequired(e => e.Suppliers)
                .HasForeignKey(e => e.SupplierId)
             .WillCascadeOnDelete(false);

            HasMany(e => e.SupplyOrder)
                .WithRequired(e => e.Suppliers)
                .HasForeignKey(e => e.SupplierId)
             .WillCascadeOnDelete(false);

            HasMany(e => e.SupplierStores)
                .WithRequired(e => e.Suppliers)
                .HasForeignKey(e => e.SupplierId)
             .WillCascadeOnDelete(false);

            HasMany(e => e.InventoryTranses)
             .WithOptional(e => e.Suppliers)
             .HasForeignKey(e => e.ItSupplierNo)
             .WillCascadeOnDelete(false);
        }   
    }
}