using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Orders : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
          //  Cart = new HashSet<Cart>();
            SalesOrderPayments = new HashSet<OrderPayments>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Order ID")]
        public int Id { get; set; }

        // customer id 
        [Display(Name = "Customer ID")]
        public string CustomerId { get; set; }

        // pending  shipping  delivered   Partialreturned fullreturned   testing done &  canceld 
        [Required]
        [StringLength(30)]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }
        [Display(Name = "Country")]
        public int? CCountryId { get; set; }
        [Display(Name = "City")]
        public int? CCityId { get; set; }
        [Display(Name = "Area")]
        public int? CAreaId { get; set; }
        [Display(Name = "Address")]
        public String CAddress { get; set; }

        [StringLength(15)]
        [Display(Name = "Delivery Option")]
        public string DeliveryOption { get; set; } // fast shiping or ordinary shiping 

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Estimated Delivery Time")]
        public int EstimatedDeliveryTime { get; set; }
        [Display(Name = "Testing Time")]
        public int? TestingTime { get; set; }


        [StringLength(20)]
        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }// online , by delivery 
        [Display(Name = "Delivery ID")]
        public int DeliveryId { get; set; }// delivery company 

        public decimal? Total { get; set; }  //total payment for all items


        public int ZipCode { get; set; }  //ZipCode for order

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

        public virtual Customers Customers { get; set; }
        public virtual DeliveryMethod Dleivery { get; set; }
        public virtual Zones Zones { get; set; }

        public virtual Cities Cities { get; set; }

        public virtual Countries Countries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Cart> Cart { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayments> SalesOrderPayments { get; set; }

        // Audited by Eman Herawy 

    }
}
