using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class OrderConfiguration : EntityTypeConfiguration<Orders>
    {
        public OrderConfiguration()
        {


             
                Property(e => e.OrderStatus)
                .IsUnicode(false);

             
                HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.OrderId)
             .WillCascadeOnDelete(false);

            HasMany(e => e.SalesOrderPayments)
                .WithOptional(e => e.Order)
                .HasForeignKey(e => e.OrderId)
                 .WillCascadeOnDelete(false);
        }   
    }
}