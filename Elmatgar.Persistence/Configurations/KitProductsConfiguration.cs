using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class KitProductsConfiguration : EntityTypeConfiguration<KitProducts>
    {
        public KitProductsConfiguration()
        {

            HasMany(e => e.Productses)
            .WithOptional(e => e.KitProducts)
            .HasForeignKey(e => e.KitProductsId)
            .WillCascadeOnDelete(false);
        }   
    }
}