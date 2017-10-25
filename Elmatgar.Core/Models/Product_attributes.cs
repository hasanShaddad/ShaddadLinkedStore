using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class ProductAttributes : IAuditInfo, IActiveFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AttributeHeaders_Id { get; set; }
        [Required]
        [StringLength(100)]
        public string PaAttValue { get; set; }
        public string SLPaAttValue { get; set; }
        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        public virtual AttributeHeaders AttributeHeaders { get; set; }

        public int ProductId { get; set; }
        public virtual Products Products { get; set; }
        
    }
}
