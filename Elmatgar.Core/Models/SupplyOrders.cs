using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class SupplyOrders : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplyOrders()
        {
            SupplyOrderDetails = new HashSet<SupplyOrderDetails>();
            SupplyOrdersPayments = new HashSet<SupplyOrderPayments>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Supply Order ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Supplier ID")]
        public int? SupplierId { get; set; }  // suplier id 

        
        [Display(Name = "Order Date")]
        public DateTime? OrderDate { get; set; } // purchase order date 

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyOrderDetails> SupplyOrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyOrderPayments> SupplyOrdersPayments { get; set; }

        public virtual Suppliers Suppliers { get; set; }

    }// audited by eman herawy
}
