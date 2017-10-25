using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class OrderDetails : IAuditInfo, IEnumerable, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderDetails()
        {
            InventoryTrans = new HashSet<InventoryTrans>();
            //  OrderLineTrans = new HashSet<OrderLineTrans>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Order Details ID")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Order ID")]
        public int? OrderId { get; set; }
        [Display(Name = "Product ID")]
        public int? ProductId { get; set; }// product id 


        [Required(ErrorMessage = "order line status is required")]
        [StringLength(20)]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }// order  status // pending  shipping  delivered    returned  done &  canceld 


        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Order Quantity")]
        public int OrderQuantity { get; set; }
        [Display(Name = "total price")]
        public decimal LineTotal { get; set; }
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTrans> InventoryTrans { get; set; }

        //  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        // public virtual ICollection<OrderLineTrans> OrderLineTrans { get; set; }

        public virtual Products Products { get; set; }
        public virtual Returns Return { get; set; }
        public virtual Orders Order { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
        // Audited by Eman Herawy 

    }
}
