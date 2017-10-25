using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class OrderDetailsConfiguration : EntityTypeConfiguration<OrderDetails>
    {
        public OrderDetailsConfiguration()
        {

            Property(e => e.OrderStatus)
               .IsRequired()
            .IsUnicode(false);



            HasMany(e => e.InventoryTrans)
            .WithOptional(e => e.OrderDetails)
            .HasForeignKey(e => e.ItOrderLineNo)
             .WillCascadeOnDelete(false);





            //HasMany(e => e.OrderLineTrans)
            //.WithRequired(e => e.OrderDetails)
            //.HasForeignKey(e => e.OltOrderLineNo)
            // .WillCascadeOnDelete(false);
        }
        // audited by eman herawy
    }
}