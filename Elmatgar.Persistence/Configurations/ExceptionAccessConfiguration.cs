using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class ExceptionAccessConfiguration : EntityTypeConfiguration<ExceptionAccess>
    {
        public ExceptionAccessConfiguration()
        {

             
                 Property(e => e.EaQuery)
                 .IsUnicode(false);

             
                Property(e => e.EaExecute)
                .IsUnicode(false);

             
                Property(e => e.EaDelete)
                .IsUnicode(false);

             
                Property(e => e.EaPrint)
                .IsFixedLength()
                .IsUnicode(false);
        }   
    }
}