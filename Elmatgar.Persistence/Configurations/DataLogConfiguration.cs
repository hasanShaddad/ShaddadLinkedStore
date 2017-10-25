using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class DataLogConfiguration : EntityTypeConfiguration<DataLog>
    {
        public DataLogConfiguration()
        {

           
                Property(e => e.DlTableName)
                 .IsUnicode(false);

           
               Property(e => e.DlColumnName)
                .IsUnicode(false);

           

               Property(e => e.DlNewValue)
                .IsUnicode(false);

           
               Property(e => e.DlOldValue)
                .IsUnicode(false);

           
               Property(e => e.DlPageNo)
                .IsFixedLength()
                .IsUnicode(false);

           
               Property(e => e.DlTransType)
                .IsFixedLength()
                .IsUnicode(false);
        }   
    }
}