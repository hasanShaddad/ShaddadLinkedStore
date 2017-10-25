using Elmatgar.Core.Models;
using System.Collections.Generic;

namespace Elmatgar.ViewModels
{
    public class CheckOutViewModel2
    {

        public Orders Orders { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}