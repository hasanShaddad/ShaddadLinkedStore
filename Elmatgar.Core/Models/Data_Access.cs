using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class DataAccess : IAuditInfo, IActiveFlag
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DaCountryId { get; set; }

        [StringLength(1)]
        public string DaQuery { get; set; }

        [StringLength(1)]
        public string DaExecute { get; set; }

        [StringLength(1)]
        public string DaDelete { get; set; }

        [StringLength(1)]
        public string DaPrint { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        [ForeignKey("DaCountryId")]
        public virtual Countries Countries { get; set; }

        public virtual Users Users { get; set; }
     
    }
}
