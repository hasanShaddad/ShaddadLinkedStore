using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Departments : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departments()
        {
            Users = new HashSet<Users>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [StringLength(100)]
        public string DDepartmentName { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }

        
    }
}
