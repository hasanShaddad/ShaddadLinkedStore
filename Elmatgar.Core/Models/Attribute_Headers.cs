using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class AttributeHeaders : IAuditInfo,IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttributeHeaders()
        {
            ProductAttributes = new HashSet<ProductAttributes>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string AttributeName { get; set; }
        public string SLAttributeName { get; set; }
        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAttributes> ProductAttributes { get; set; }

       
    }
}
