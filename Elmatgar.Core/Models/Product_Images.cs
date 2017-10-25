using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class ProductImages : IAuditInfo, IActiveFlag
    {
        [Key]
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ImageName { get; set; }
        public bool ImageExist { get; set; }
        public string ImageLink { get; set; } =
            $"{DateTime.Now.Year + "/" + DateTime.Now.Month+ "/" + DateTime.Now.Day}";

        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = true;

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

          public string LastUpdatedBy { get; set; }

        public virtual Products Products { get; set; }

        public int ProductId { get; set; }
    }
}
