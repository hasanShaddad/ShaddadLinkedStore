using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Cities : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cities()
        {
            Zones = new HashSet<Zones>();
           Orders = new HashSet<Orders>();
            Customers = new HashSet<Customers>();
            SupplierStores = new HashSet<SupplierStores>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        [Display(Name = "City Name")]
        public string CtCityName { get; set; }
        public string SLCityName { get; set; }
        public int? CtCountryId { get; set; }

        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = true;

        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Last Updated By")]
        public string LastUpdatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zones> Zones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customers> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierStores> SupplierStores { get; set; }

        public virtual Countries Countries { get; set; }
    }
}
