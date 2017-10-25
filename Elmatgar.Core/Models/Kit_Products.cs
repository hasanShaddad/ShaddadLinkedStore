using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Models
{
    public partial class KitProducts : IAuditInfo, IActiveFlag
    {
        public KitProducts()
        {
            Productses = new HashSet<Products>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = true;

        [Display(Name = "Kit name")]
        public string KitName { get; set; }
        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

          public string LastUpdatedBy { get; set; }

      
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Productses { get; set; }//--
    }
}
