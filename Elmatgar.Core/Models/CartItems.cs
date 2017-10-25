using System.Security.AccessControl;
using Elmatgar.Core.Models;

namespace Elmatgar.Core.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int  CartId { get; set; }
        public int Quntity { get; set; }
        public int MaxQuntity { get; set; }
        public decimal FinalPriceDecimal { get; set; }
        public string ItemDesc { get; set; }
        public string ImageUrl { get; set; }
    public OrderLines OrderLines { get; set; }
        public Cart Cart{ get; set; }
    }
}