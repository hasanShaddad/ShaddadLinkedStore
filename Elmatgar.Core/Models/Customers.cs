using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Customers : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customers()
        {
            SalesOrderPayments = new HashSet<OrderPayments>();
            Order = new HashSet<Orders>();
            CustomerPaymentMethod = new HashSet<CustomerPaymentMethod>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CCustNo { get; set; }


        public string Id { get; set; }
        [StringLength(100)]
        public string CFirstName { get; set; }

        [StringLength(100)]
        public string CLastName { get; set; }

        [StringLength(100)]
        public string CEmail { get; set; }

        [StringLength(100)]
        public string CMobile { get; set; }

        [StringLength(100)]
        public string CHomePhone { get; set; }

        [StringLength(100)]
        public string CWorkPhone { get; set; }

        public int? CCountryId { get; set; }

        public int? CCityId { get; set; }

        public int? CAreaId { get; set; }

        [StringLength(200)]
        public string CAddress { get; set; }

        [StringLength(100)]
        public string CLang { get; set; }

        [StringLength(100)]
        public string CLong { get; set; }

        [StringLength(100)]
        public string CCustStatus { get; set; }

        [StringLength(100)]
        public string CCustClass { get; set; }

        public int? CCustPoints { get; set; }

        [StringLength(100)]
        public string CPassword { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        public virtual Zones Zones { get; set; }

        public virtual Cities Cities { get; set; }

        public virtual Countries Countries { get; set; }
        public virtual ApplicationUser users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayments> SalesOrderPayments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerPaymentMethod> CustomerPaymentMethod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Order { get; set; }

     
    }
}
