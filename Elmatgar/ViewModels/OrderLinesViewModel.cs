using System.ComponentModel.DataAnnotations;

namespace Elmatgar.ViewModels
{
    public class OrderLinesViewModel
    {
        // order details
        //[Display(Name = "Order Details ID")]
        //public int OrderLineId { get; set; }

        [Display(Name = "Product ID")]
        public int? ProductId { get; set; }// product id 

        [Display(Name = "Order Quantity")]
        public int Quntity { get; set; }
        [Display(Name = "total price")]
        public decimal LineTotal { get; set; }
        public string OrderStatus { get; set; }
    }
}