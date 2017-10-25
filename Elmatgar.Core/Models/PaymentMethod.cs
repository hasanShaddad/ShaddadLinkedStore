using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class PaymentMethod : IAuditInfo, IActiveFlag
    {
        // note this class and related one is for handling online payment 

        public PaymentMethod()
        {
            CustomerPaymentMethod = new HashSet<CustomerPaymentMethod>();


        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Payment Method ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Payment Method Name")]
        public string PaymentMethodName { get; set; }
        public string SLPaymentMethodName { get; set; }
        // public string PaymentMethodApi { get; set; }  ** i'm not sure about how to store the api
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


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerPaymentMethod> CustomerPaymentMethod { get; set; }
        //created by Eman Herawy 

    }
}