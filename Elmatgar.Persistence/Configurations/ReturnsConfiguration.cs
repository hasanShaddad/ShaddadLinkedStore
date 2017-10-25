using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    class ReturnsConfiguration : EntityTypeConfiguration<Returns>
    {
        public ReturnsConfiguration()
        {
            HasRequired(p => p.OrderDetail)
           .WithRequiredDependent(p => p.Return);
        }
    }// created by eman herawy
}
