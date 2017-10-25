using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class PageCloumns : IAuditInfo, IActiveFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PcPageNo { get; set; }

        [StringLength(100)]
        public string PcColumnLable { get; set; }

        [StringLength(100)]
        public string PcTableName { get; set; }

        [StringLength(100)]
        public string PcColumnName { get; set; }

        public int? PcColumnSeq { get; set; }

        [Display(Name = "Data Log Flag")]
        public Boolean DataLogFlag { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        public virtual ModulePages ModulePages { get; set; }
        
    }
}
