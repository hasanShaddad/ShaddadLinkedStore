using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    public class DiscountRulesConfiguration:EntityTypeConfiguration<DiscountRules>
    {
        /// <summary>
        /// Configuation for DiscountRules table
        /// </summary>
        public DiscountRulesConfiguration()
        {
            

           this.Property(e => e.DrQty)
          .IsRequired();
            this.Property(e => e.DiscountPercentage)
        .IsRequired();
            this.Property(e => e.FromDate)
        .IsRequired();

        }
    }
}