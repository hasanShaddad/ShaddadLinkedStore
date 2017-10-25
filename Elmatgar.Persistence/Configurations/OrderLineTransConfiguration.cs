using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    
    public class OrderLineTransConfiguration : EntityTypeConfiguration<OrderLineTrans>
    {
        public OrderLineTransConfiguration()
        {

            Property(e => e.OltTransDate)
               .IsRequired();

            Property(e => e.OltStatus)
                .IsRequired()
                .IsUnicode(false);
        }
    }
}