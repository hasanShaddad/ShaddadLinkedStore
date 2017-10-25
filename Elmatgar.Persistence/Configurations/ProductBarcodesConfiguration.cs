using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class ProductBarcodesConfiguration : EntityTypeConfiguration<ProductBarcodes>
    {
        public ProductBarcodesConfiguration()
        {

             Property(e => e.Barcode)
                  .IsUnicode(false);
            Property(e => e.Barcode)
                .IsRequired();
            Property(e => e.BarcodeNo)
               .IsRequired();

        }
    }
}