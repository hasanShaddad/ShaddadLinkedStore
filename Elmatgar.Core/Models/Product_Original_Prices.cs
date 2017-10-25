using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class ProductOriginalPrices : IAuditInfo, IActiveFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; }
 
        public DateTime FromDate { get; set; }
        [Required(ErrorMessage = "Original Price is required")]
        [Range(0.01, 10000000.00,
             ErrorMessage = "Original Price must be between 0.01 and 100.00")]
        public decimal? OriginalPrice { get; set; }

        public int? CountryId { get; set; }

        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = true;

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

          public string LastUpdatedBy { get; set; }

        public virtual Countries Countries { get; set; }
        public int ProductId { get; set; }
        public virtual Products Products { get; set; }
    }
}
