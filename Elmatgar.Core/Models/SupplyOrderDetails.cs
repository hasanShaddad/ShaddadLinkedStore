using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class SupplyOrderDetails : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplyOrderDetails()
        {
            InventoryTrans = new HashSet<InventoryTrans>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Supply Order Details ID")]
        public int Id { get; set; }
        [Display(Name = "Supply Order ID")]
        public int SupplyOrderId { get; set; } // purchase order id 
        [Required]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; } // quantity

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
        public virtual ICollection<InventoryTrans> InventoryTrans { get; set; }
        public virtual Products Products { get; set; }
        public virtual SupplyOrders SupplyOrders { get; set; }

    }
}// audited by eman herawy
