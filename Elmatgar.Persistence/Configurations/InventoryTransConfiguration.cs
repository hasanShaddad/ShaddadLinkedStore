using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class InventoryTransConfiguration : EntityTypeConfiguration<InventoryTrans>
    {
        public InventoryTransConfiguration()
        {

           
                  Property(e => e.ItTransType)
                  .IsUnicode(false);

        }
    }
}