using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Returns : IAuditInfo, IActiveFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Return ID ")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Order Details Id")]
        public int OrderDetailsId { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Return Cause")]
        public string ReturnCause { get; set; } // what is the cause for returning the product   manifactorer errors , fake customer , etc

        [Required]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = " Last Update Date")]
        public DateTime? LastUpdateDate { get; set; }
        [Display(Name = " Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = " Last Updated By")]
        public string LastUpdatedBy { get; set; }
        [Display(Name = "Locked")]
        public bool ActiveFlag { get; set; } = true;

        public virtual OrderDetails OrderDetail { get; set; }


    }// created by eman herawy
}
