using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class ModulePages : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ModulePages()
        {
            ExceptionAccess = new HashSet<ExceptionAccess>();
            PageCloumns = new HashSet<PageCloumns>();
            PositionAccess = new HashSet<PositionAccess>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? MpModuleNo { get; set; }

        [StringLength(100)]
        public string MpPageName { get; set; }

        [StringLength(100)]
        public string MpPageUrl { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExceptionAccess> ExceptionAccess { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PageCloumns> PageCloumns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PositionAccess> PositionAccess { get; set; }

        public virtual Modules Modules { get; set; }
  
    }
}
