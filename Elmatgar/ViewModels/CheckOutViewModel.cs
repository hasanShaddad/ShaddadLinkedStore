using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using CreditCardAttribute = Elmatgar.Core.Models.CreditCardAttribute;

namespace Elmatgar.ViewModels
{
    public class CheckOutViewModel
    {
        public int Id { get; set; }

      

        [Display(Name = "total payment")]
        public decimal? Total { get; set; }

     
        [Required]
        [Display(Name = "Card No")]
        [Core.Models.CreditCard(AcceptedCardTypes = CreditCardAttribute.CardType.Visa | CreditCardAttribute.CardType.MasterCard)]
        public string ccNo { get; set; }
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "please enter valid month no")]
        [Display(Name = "Exp month")]
        public string expMonth { get; set; }
        [Required]
        [MaxLength(4)]
        [MinLength(2)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "please enter valid year no")]
        [Display(Name = "Exp year")]
        public string expYear { get; set; }
        [Required]
        [MaxLength(3)]
        [MinLength(3)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "please enter valid Cvv no")]
        [Display(Name = "CVV No")]
        public string cvv { get; set; }
    }
}