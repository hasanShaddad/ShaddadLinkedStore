using System.Data.Entity.ModelConfiguration;
using System.Security.Cryptography.X509Certificates;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    internal class CartConfiguration : EntityTypeConfiguration<Cart>
    {
        public CartConfiguration()
        {
            this.HasKey(c => c.Id);
            this.HasMany(e => e.CartItems)
            .WithOptional(e => e.Cart)
            .HasForeignKey(e => e.CartId);
        }
    }
}