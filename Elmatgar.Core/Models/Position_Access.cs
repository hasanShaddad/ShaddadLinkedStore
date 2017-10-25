using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class PositionAccess : IAuditInfo, IActiveFlag
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaPageNo { get; set; }

        [StringLength(1)]
        public string PaQuery { get; set; }

        [StringLength(1)]
        public string PaExecute { get; set; }

        [StringLength(1)]
        public string PaDelete { get; set; }

        [StringLength(1)]
        public string PaPrint { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        public virtual ModulePages ModulePages { get; set; }

        public virtual Positions Positions { get; set; }
        
    }
}
