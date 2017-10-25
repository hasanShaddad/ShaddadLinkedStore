using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class SupplyOrderPayments : IAuditInfo, IActiveFlag
    {
        // this class as i think is using to store purchasing payment data .. after customer pay and test
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Supply Order Payments ID")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Supplier ID")]
        public int? SupplierId { get; set; }// supplier id 
        [Required]
        [Display(Name = "Supply Order ID")]
        public int SupplyOrderId { get; set; } // Supply order id
        [Display(Name = "Paid")]
        public bool Paid { get; set; } // this is supposed to be used when we pay for the supplier .. so what's the needs for using this field ?!
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = " Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }
        [Display(Name = " Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = " Last Updated By")]
        public string LastUpdatedBy { get; set; }
        [Display(Name = "Locked")]
        public bool ActiveFlag { get; set; } = true;

        public virtual SupplyOrders SupplyOrders { get; set; }

        public virtual Suppliers Suppliers { get; set; }

    } // Audited by eman herawy
}
