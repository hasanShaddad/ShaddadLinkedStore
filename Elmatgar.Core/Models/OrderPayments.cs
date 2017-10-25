using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class OrderPayments : IAuditInfo, IActiveFlag
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Order Payments ID")]
        public int Id { get; set; }
        [Display(Name = "Customer Id")]
        public string CustomerId { get; set; }
        [Display(Name = "Order ID")]
        public int? OrderId { get; set; }
        [Display(Name = "Paid")]
        public bool Paid { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Last Updated By")]
        public string LastUpdatedBy { get; set; }
        [Display(Name = "Locked")]
        public bool ActiveFlag { get; set; } = true;

        public virtual Customers Customers { get; set; }

        public virtual Orders Order { get; set; }

    } // audited by eman herawy
}
