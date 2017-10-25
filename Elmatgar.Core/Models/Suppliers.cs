using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Suppliers : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Suppliers()
        {
            SupplyOrderPayments = new HashSet<SupplyOrderPayments>();
            SupplyOrder = new HashSet<SupplyOrders>();
            SupplierStores = new HashSet<SupplierStores>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = " Supplier ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = " Supplier Name")]
        public string SupplierName { get; set; }
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
        public virtual ICollection<SupplyOrderPayments> SupplyOrderPayments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyOrders> SupplyOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierStores> SupplierStores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTrans> InventoryTranses { get; set; }
        // audited by eman herawy

    }
}
