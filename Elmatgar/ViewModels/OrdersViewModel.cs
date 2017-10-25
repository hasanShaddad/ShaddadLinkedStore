using System;
using System.ComponentModel.DataAnnotations;

namespace Elmatgar.ViewModels
{
    public class OrdersViewModel
    {
        public int OrderId { get; set; }

        // customer id 

        public string CustomerId { get; set; }

        // pending  shipping  delivered   Partialreturned fullreturned   testing done &  canceld 
        [Required]
        [StringLength(30)]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }
        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [Display(Name = "City")]
        public int? CityId { get; set; }
        [Display(Name = "Area")]
        public int? AreaId { get; set; }
        [Display(Name = "Address")]
        public String Address { get; set; }

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

    }
}