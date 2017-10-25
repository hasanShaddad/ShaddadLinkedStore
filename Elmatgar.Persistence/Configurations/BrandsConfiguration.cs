using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    public class BrandsConfiguration:EntityTypeConfiguration<Brands>
    {
        /// <summary>
        /// Configuation for Brands table
        /// </summary>
        public BrandsConfiguration()
        {

           this.Property(e => e.BrandName)
          .IsUnicode(false);
           this.HasMany(e => e.Products)
             .WithOptional(e => e.Brands)
             .HasForeignKey(e => e.BrandId);
        }
    }
}