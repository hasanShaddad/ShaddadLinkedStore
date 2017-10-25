using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class DiscountRules : IAuditInfo, IActiveFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime FromDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "quntity is required")]
        [Range(1, 10000000,
            ErrorMessage = "quntity  must be between 1 and 10000000")]
        public int DrQty { get; set; }
        [Required(ErrorMessage = "Discount Percentage is required")]
        [Range(0.01, 1.00,
            ErrorMessage = "Original Price must be between 0.01 and 1.00")]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = true;

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public int ProductId { get; set; }
        public virtual Products Products { get; set; }
    }
}
