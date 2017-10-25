using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class ProductsConfiguration : EntityTypeConfiguration<Products>
    {
        public ProductsConfiguration()
        {

             
                  Property(e => e.Name)
                  .IsRequired()
                  .IsUnicode(false);

             
                HasMany(e => e.InventoryTrans)
                .WithOptional(e => e.Products)
                .HasForeignKey(e => e.ItProdId);

           

             
                HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            HasMany(e => e.SupplyOrderDetailses)
           .WithRequired(e => e.Products)
           .HasForeignKey(e => e.ProductId)
           .WillCascadeOnDelete(false);


            HasMany(e => e.ProductAttributes)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId);

             
                HasMany(e => e.ProductBarcodes)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

             
               HasMany(e => e.ProductVote)
               .WithRequired(e => e.Products)
               .HasForeignKey(e => e.ProductId)
               .WillCascadeOnDelete(false);

             
                        HasMany(e => e.DiscountRules)
                        .WithRequired(e => e.Products)
                        .HasForeignKey(e => e.ProductId)
                        .WillCascadeOnDelete(false);

             
                HasMany(e => e.ProductOriginalPrices)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

             
                HasMany(e => e.ProductPromotions)
                .WithRequired(e => e.Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

             
                HasRequired(e => e.Categories)
                .WithMany(prod => prod.Productses)
                .WillCascadeOnDelete(false);


          
              HasMany(e => e.ProductImages)
              .WithRequired(e => e.Products)
              .HasForeignKey(e => e.ProductId)
              .WillCascadeOnDelete(false);
        }
    }
}