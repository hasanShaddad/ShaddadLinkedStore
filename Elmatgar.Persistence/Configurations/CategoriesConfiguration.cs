using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class CategoriesConfiguration : EntityTypeConfiguration<Categories>
    {
        public CategoriesConfiguration()
        {

            
                 Property(e => e.Id).HasColumnName("CategoryId");


            
                Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("AK_Category_CategoryName") { IsUnique = true }));

            
                HasOptional(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .WillCascadeOnDelete(false);

        }
    }
}