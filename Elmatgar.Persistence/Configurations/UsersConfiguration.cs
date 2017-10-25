using System.Data.Entity.ModelConfiguration;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.Configurations
{
    /// <summary>
    /// Configuation for areas table
    /// </summary>
    public class UsersConfiguration : EntityTypeConfiguration<Users>
    {
        public UsersConfiguration()
        {

            
                   Property(e => e.UUserName)
                   .IsUnicode(false);

            
                Property(e => e.UActive)
                .IsUnicode(false);

            
                Property(e => e.UEmail)
                .IsUnicode(false);

            
                Property(e => e.UMobile)
                .IsUnicode(false);

            
                Property(e => e.UId)
                .IsUnicode(false);

            
        HasKey(e => e.Id);

            
                Property(e => e.UPassword)
                .IsUnicode(false);
        }   
    }
}