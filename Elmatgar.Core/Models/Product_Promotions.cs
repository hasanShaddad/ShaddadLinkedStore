using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class ProductPromotions : IAuditInfo, IActiveFlag
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime FromDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Range(0.01, 1.00,
           ErrorMessage = "Original Price must be between 0.01 and 100000.00")]
        public decimal? PromoPrice { get; set; }

        public int? CountryId { get; set; }

        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = true;

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

          public string LastUpdatedBy { get; set; }

        [ForeignKey("CountryId")]
        public virtual Countries Countries { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }
    }
}
