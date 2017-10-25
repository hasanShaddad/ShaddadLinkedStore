using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class ProductAttributesConfiguration : EntityTypeConfiguration<ProductAttributes>
    {
        public ProductAttributesConfiguration()
        {

            
              Property(e => e.PaAttValue)
                .IsUnicode(false);
            Property(e => e.PaAttValue)
                .IsRequired();
        }   
    }
}