using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    public class AttributeConfiguration: EntityTypeConfiguration<AttributeHeaders>
    {
        /// <summary>
        /// Configuation for Attribute table
        /// </summary>
        public AttributeConfiguration()
        {

          this.Property(e => e.AttributeName )
                .IsRequired()
                 .IsUnicode(false);
           this.HasMany(e => e.ProductAttributes)
            .WithRequired(e => e.AttributeHeaders)
            .HasForeignKey(e => e.AttributeHeaders_Id)
            .WillCascadeOnDelete(false);
        }
    }
}