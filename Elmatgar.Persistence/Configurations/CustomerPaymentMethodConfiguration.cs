using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    class CustomerPaymentMethodConfiguration : EntityTypeConfiguration<CustomerPaymentMethod>
    {
        public CustomerPaymentMethodConfiguration()
        {
            HasKey(c => new { c.CustomerID, c.PaymentMethodId });

            HasRequired(x => x.Customer)
                .WithMany(x => x.CustomerPaymentMethod)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.PaymentMethod)
                .WithMany(x => x.CustomerPaymentMethod)
                .WillCascadeOnDelete(false);


        }// created by eman herawy
    }
}
