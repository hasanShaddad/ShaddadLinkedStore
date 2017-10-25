using Elmatgar.Core.Models;
using Elmatgar.persistence;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Elmatgar.ViewModels
{
    public class OrderViewModel
    {
        private readonly StoreDbContext _db = new StoreDbContext();
        public PaymentType PaymentType { get; set; }// online , by delivery 

        public OrderStatus OrderStatus { get; set; }

        public DeliveryOption DeliveryOption { get; set; } // fast shiping or ordinary shiping

       // public Orders Orders { get; set; }
        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        public IEnumerable<Countries> Countries { get; set; }
        [Display(Name = "City")]
        public int? CityId { get; set; }

        public IEnumerable<Cities> Cities { get; set; }
        [Display(Name = "Area")]
        public int? AreaId { get; set; }

        public IEnumerable<Zones> Zones { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Use 4 numbers only")]
        public int ZipCode { get; set; }

        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        //public IEnumerable< Customers> Customer { get; set; }

        public OrderViewModel()
        {
            Cities = _db.Cities;
            Countries = _db.Countries;
            Zones = _db.Zones;

        }
        //[Display(Name = "Order ID")]
        //public int Id { get; set; }

        // customer id 


        //// pending  shipping  delivered   Partialreturned fullreturned   testing done &  canceld 
        //[Required]
        //[StringLength(30)]
        //[Display(Name = "Order Status")]
        //public string OrderStat { get; set; }
        //[Display(Name = "Country")]
        //public int CountryId { get; set; }

        //public IEnumerable<Countries> Countries { get; set; }
        //[Display(Name = "City")]
        //public int CityId { get; set; }

        //public IEnumerable<Cities> Cities { get; set; }
        //[Display(Name = "Area")]
        //public int AreaId { get; set; }

        //public IEnumerable<Zones> Zones { get; set; }
        //[Display(Name = "Address")]
        //public String CAddress { get; set; }

        //[StringLength(15)]
        //[Display(Name = "Delivery Option")]
        //public string DeliveryOptions { get; set; } // fast shiping or ordinary shiping 

        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[Display(Name = "Estimated Delivery Time")]
        //public int EstimatedDeliveryTime { get; set; }
        //[Display(Name = "Testing Time")]
        //public int? TestingTime { get; set; }

        //[Display(Name = "Delivery ID")]
        //public int DeliveryId { get; set; }// delivery company 
        //public decimal? Total { get; set; }  //total payment for all items
        //                                     //  [RegularExpression("[0-9]{1,4}")]
        //                                     // supposed to be fine .. but it wasn't
        //[RegularExpression("[0-9]{4}", ErrorMessage = "Use 4 numbers only")]
        //public int ZipCode { get; set; }  //ZipCode for order

        //public int CustomerId { get; set; }
        //public Customers Customer { get; set; }

        //[Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        //[Display(Name = "Order Quantity")]
        //public int OrderQuantity { get; set; }
        //[Display(Name = "total price")]
        //public decimal LineTotal { get; set; }
        //[Display(Name = "Creation Date")]
        //public DateTime? CreationDate { get; set; }
        //[Display(Name = "Last Update Date")]
        //public DateTime? LastUpdateDate { get; set; }
        //[Display(Name = "Created By")]
        //public string CreatedBy { get; set; }
        //[Display(Name = "Last Updated By")]
        //public string LastUpdatedBy { get; set; }
        //[Display(Name = "Locked")]
        //public bool ActiveFlag { get; set; } = true;
        //public PaymentType PaymentType { get; set; }// online , by delivery 

        //public OrderStatus OrderStatus { get; set; }

        //public DeliveryOption DeliveryOption { get; set; } // fast shiping or ordinary shiping

    }

    public enum DeliveryOption
    {
        Fast, Regular
    }
    public enum PaymentType
    {
        visa, Cash
    }

    public enum OrderStatus
    {
        Bending
            , Promoted
            , Shipping
            , Delevered
            , Testing
            , Returned
            , Canceled
            , Done
        //Partialreturned fullreturned 
    }
}