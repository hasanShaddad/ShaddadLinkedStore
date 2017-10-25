using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Users : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            DataAccess = new HashSet<DataAccess>();
            ExceptionAccess = new HashSet<ExceptionAccess>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Id { get; set; }
        [StringLength(100)]
        public string UUserName { get; set; }

        public int? UPositionNo { get; set; }

        public int? DepartmentId { get; set; }

        [StringLength(100)]
        public string UActive { get; set; }

        [StringLength(100)]
        public string UEmail { get; set; }

        [StringLength(100)]
        public string UMobile { get; set; }

        [StringLength(100)]
        public string UId { get; set; }

        [StringLength(100)]
        public string UPassword { get; set; }

        public int? UManagerNo { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataAccess> DataAccess { get; set; }

        public virtual Departments Departments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExceptionAccess> ExceptionAccess { get; set; }

        public virtual Positions Positions { get; set; }
        public virtual ApplicationUser users { get; set; }
       
    }
}
