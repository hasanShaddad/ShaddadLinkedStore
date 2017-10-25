using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Countries : IAuditInfo,IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Countries()
        {
            Orders = new HashSet<Orders>();
            Cities = new HashSet<Cities>();
            Customers = new HashSet<Customers>();
           // DataAccess = new HashSet<DataAccess>();
            ProductOriginalPrices = new HashSet<ProductOriginalPrices>();
           // ProductPromotions = new HashSet<ProductPromotions>();
            SupplierStores = new HashSet<SupplierStores>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [StringLength(100)]
        [Required]
        [Display(Name = "Country Name")]
        public string CnCountryName { get; set; }

        public string SLCountryName { get; set; }
        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = true;

        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }

        [Display(Name = "Created By ")]
        public string CreatedBy { get; set; }

        [Display(Name = "Last Updated By")]
        public string LastUpdatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cities> Cities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customers> Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       public virtual ICollection<DataAccess> DataAccess { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOriginalPrices> ProductOriginalPrices { get; set; }

      //  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
      //  public virtual ICollection<ProductPromotions> ProductPromotions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierStores> SupplierStores { get; set; }
    }
}
