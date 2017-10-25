using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class ProductVote : IAuditInfo , IActiveFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? VoteValue { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; }


        public virtual ApplicationUser User { get; set; }
        public virtual Products Products { get; set; }
    }
}