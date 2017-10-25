using System;
using System.ComponentModel.DataAnnotations;

namespace Elmatgar.Core.Models
{
    public partial class CustomerPaymentMethod : IAuditInfo, IActiveFlag
    {



        [Required]
        public string CustomerID { get; set; }
        [Required]
        public int PaymentMethodId { get; set; }
        [Required]
        public string PaymentcontactDetails { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool ActiveFlag { get; set; } = true;


        public virtual Customers Customer { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

        //created by Eman Herawy 
         
    }
}