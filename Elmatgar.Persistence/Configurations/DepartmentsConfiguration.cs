using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class DepartmentsConfiguration : EntityTypeConfiguration<Departments>
    {
        public DepartmentsConfiguration()
        {

            
                  Property(e => e.DDepartmentName)
                  .IsUnicode(false);

            
                HasMany(e => e.Users)
                .WithOptional(e => e.Departments)
                .HasForeignKey(e => e.DepartmentId)
                 .WillCascadeOnDelete(false);
        }   
    }
}