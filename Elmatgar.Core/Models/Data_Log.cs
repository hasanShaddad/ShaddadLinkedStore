using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class DataLog 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? DlUserNo { get; set; }

        public DateTime? DlTransDate { get; set; }

        [StringLength(100)]
        public string DlTableName { get; set; }

        [StringLength(100)]
        public string DlColumnName { get; set; }

        [StringLength(4000)]
        public string DlNewValue { get; set; }

        [StringLength(4000)]
        public string DlOldValue { get; set; }

        [StringLength(10)]
        public string DlPageNo { get; set; }

        [StringLength(10)]
        public string DlTransType { get; set; }
        public virtual Users Users { get; set; }
    }
}
