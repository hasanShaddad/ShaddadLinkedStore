using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class SupplierStores : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierStores()
        {
            InventoryTrans = new HashSet<InventoryTrans>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Supplier ID")]
        public int SupplierId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
        [Required]
        [Display(Name = "Country ID")]
        public int? CountryId { get; set; }
        [Required]
        [Display(Name = "city ID")]
        public int? CityId { get; set; }
        [Display(Name = "Area ID")]
        public int? AreaId { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "LastUpdateDate")]
        public DateTime? LastUpdateDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Last Updated By")]
        public string LastUpdatedBy { get; set; }
        [Display(Name = "Locked")]
        public bool ActiveFlag { get; set; } = true;


        public virtual Zones Zones { get; set; }

        public virtual Cities Cities { get; set; }

        public virtual Countries Countries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTrans> InventoryTrans { get; set; }

        public virtual Suppliers Suppliers { get; set; }
        // audited by Eman herawy 

    }
}
