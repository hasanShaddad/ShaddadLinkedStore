using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class Products : IAuditInfo, IActiveFlag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            DiscountRules = new HashSet<DiscountRules>();
            InventoryTrans = new HashSet<InventoryTrans>();
            OrderDetails = new HashSet<OrderDetails>();
            ProductAttributes = new HashSet<ProductAttributes>();
            ProductBarcodes = new HashSet<ProductBarcodes>();
            ProductImages = new HashSet<ProductImages>();
            ProductOriginalPrices = new HashSet<ProductOriginalPrices>();
            ProductPromotions = new HashSet<ProductPromotions>();
            ProductVote = new HashSet<ProductVote>();
            SupplyOrderDetailses = new HashSet<SupplyOrderDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? KitProductsId { get; set; }
        public int? BrandId { get; set; }
        public int CategoriesId { get; set; }
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [StringLength(100)]
        public string SLName { get; set; }
        public string ShortDescription { get; set; }

        public string SLShortDescription { get; set; }

        [Display(Name = "Kit Flag")]
        public Boolean KitFlag { get; set; }

        [Display(Name = "Active Flag")]
        public Boolean ActiveFlag { get; set; } = false;

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string CreatedBy { get; set; }

        public string LastUpdatedBy { get; set; }
        public int? TotalVote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscountRules> DiscountRules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InventoryTrans> InventoryTrans { get; set; }//--

        //  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //  public virtual ICollection<KitProducts> KitProducts { get; set; }//--

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }//--

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAttributes> ProductAttributes { get; set; }//--

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductBarcodes> ProductBarcodes { get; set; }//--

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //todo:managing upload and save and delete images
        public virtual ICollection<ProductImages> ProductImages { get; set; }//--

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOriginalPrices> ProductOriginalPrices { get; set; }//--
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplyOrderDetails> SupplyOrderDetailses { get; set; }//--

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPromotions> ProductPromotions { get; set; }//--
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductVote> ProductVote { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual KitProducts KitProducts { get; set; }
        public virtual Brands Brands { get; set; }
    }
}
