using Elmatgar.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Elmatgar.ViewModels
{
    public class SuplyOrderViewModel
    {
        public IEnumerable<Suppliers> Suppliers { get; set; }
        public IEnumerable<Products> Products { get; set; }
        [Required]
        public int SupplierId { get; set; }
        //[Required]
        public DateTime? OrderDate { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quntity { get; set; }

        public bool Paid { get; set; }
    }
}