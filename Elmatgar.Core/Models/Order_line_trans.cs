using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class OrderLineTrans : IAuditInfo, IActiveFlag
    {// note .. i think this is useless calss 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // order line id 
        [Required]
        public int? OltOrderLineNo { get; set; }

        // order line transaction date
        [Required]
        public DateTime? OltTransDate { get; set; }

        // order for single product status 
        [Required]
        [StringLength(100)]
        public string OltStatus { get; set; }// order line status // pending  shipping  delivered    returned  done &  canceld 

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;

        public virtual OrderDetails OrderDetails { get; set; }

        // Audited by Eman Herawy 
      
    }
}
