using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.Core.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItems>();
        }
        [Key]
        public int Id { get; set; }
        public string SessionKey { get; set; }
        [Required]
        public Order Order { get; set; }
        public bool ActiveFlag { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItems> CartItems{ get; set; }

    }
}
