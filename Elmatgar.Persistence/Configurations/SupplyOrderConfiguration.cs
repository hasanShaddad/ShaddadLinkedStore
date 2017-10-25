using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class SupplyOrderConfiguration : EntityTypeConfiguration<SupplyOrders>
    {
        public SupplyOrderConfiguration()
        {

     
            

            HasMany(e => e.SupplyOrderDetails)
                .WithRequired(e => e.SupplyOrders)
                .HasForeignKey(e => e.SupplyOrderId)
                .WillCascadeOnDelete(false);

            
                HasMany(e => e.SupplyOrdersPayments)
                  .WithRequired(e => e.SupplyOrders)
                .HasForeignKey(e => e.SupplyOrderId)
               .WillCascadeOnDelete(false);
        }
    }
}