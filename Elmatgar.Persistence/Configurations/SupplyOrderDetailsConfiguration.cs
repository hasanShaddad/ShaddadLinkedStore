using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class SupplyOrderDetailsConfiguration : EntityTypeConfiguration<SupplyOrderDetails>
    {
        public SupplyOrderDetailsConfiguration()
        {
        
           
         HasMany(e => e.InventoryTrans)
         .WithOptional(e => e.SupplyOrderDetails)
         .HasForeignKey(e => e.ItPoLineNo)
             .WillCascadeOnDelete(false);
        }   
    }
}