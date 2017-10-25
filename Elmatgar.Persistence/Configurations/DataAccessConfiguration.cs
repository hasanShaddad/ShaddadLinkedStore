using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class DataAccessConfiguration : EntityTypeConfiguration<DataAccess>
    {
        public DataAccessConfiguration()
        {
           

            Property(e => e.DaCountryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
              Property(e => e.DaQuery)
               .IsUnicode(false);

            
             Property(e => e.DaExecute)
                .IsUnicode(false);

            
              Property(e => e.DaDelete)
                .IsUnicode(false);

            
              Property(e => e.DaPrint)
                .IsUnicode(false);
        }   
    }
}