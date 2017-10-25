using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class ExceptionAccess : IAuditInfo, IActiveFlag
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EaPageNo { get; set; }

        [StringLength(1)]
        public string EaQuery { get; set; }

        [StringLength(1)]
        public string EaExecute { get; set; }

        [StringLength(1)]
        public string EaDelete { get; set; }

        [StringLength(10)]
        public string EaPrint { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        public virtual ModulePages ModulePages { get; set; }

        public virtual Users Users { get; set; }
        
    }
}
