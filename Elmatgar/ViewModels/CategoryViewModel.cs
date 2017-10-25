using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Elmatgar.Core.Models;

namespace Elmatgar.ViewModels { 
    public class CategoryViewModel
    {
        public int Id { get; set; }

      
        [Display(Name = "parent Category")]
        public int? ParentId { get; set; }

        [StringLength(100)]
        [Required]
        [Remote("IsCategAvailble", "Categories", ErrorMessage = "category Already Exist.")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
     
        [Display(Name = "Snd Lang Category")]
        public string SLCategoryName { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImage { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }


        public virtual List<Products> Products { get; set; }
    }

    public class CategoryEditViewModel
    {
        public int Id { get; set; }


        [Display(Name = "parent Category")]
        public int? ParentId { get; set; }

        [StringLength(100)]
        [Required]
     
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
      
        [Display(Name = "Snd Lang Category")]
        public string SLCategoryName { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryImage { get; set; }
        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }


        public virtual List<Products> Products { get; set; }
    }
}