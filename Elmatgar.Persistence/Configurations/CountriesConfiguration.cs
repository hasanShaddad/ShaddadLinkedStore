using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    public class CountriesConfiguration : EntityTypeConfiguration<Countries>
    {
        /// <summary>
        /// Configuation for Brands table
        /// </summary>
        public CountriesConfiguration()
        {

           this.Property(e => e.CnCountryName)
                  .IsUnicode(false);

                 this.HasMany(e => e.Cities)
                .WithOptional(e => e.Countries)
                .HasForeignKey(e => e.CtCountryId);
            this.HasMany(e => e.Orders)
              .WithOptional(e => e.Countries)
              .HasForeignKey(e => e.CCountryId);

            this.HasMany(e => e.Customers)
                 .WithOptional(e => e.Countries)
                 .HasForeignKey(e => e.CCountryId)
                  .WillCascadeOnDelete(false);

          //  this.HasMany(e => e.DataAccess)
            //     .WithRequired(e => e.Countries)
              //   .HasForeignKey(e => e.DaCountryId)
                //. .WillCascadeOnDelete(false);
                
                this.HasMany(e => e.ProductOriginalPrices)
                  .WithOptional(e => e.Countries)
                  .HasForeignKey(e => e.CountryId)
                   .WillCascadeOnDelete(false);

         //   this .HasMany(e => e.ProductPromotions)
               // .WithOptional(e => e.Countries)
               // .HasForeignKey(e => e.CountryId)
               //  .WillCascadeOnDelete(false);

            this.HasMany(e => e.SupplierStores)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.CountryId)
                 .WillCascadeOnDelete(false);

        }
    }
}
