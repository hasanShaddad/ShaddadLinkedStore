namespace Elmatgar.Core.Models
{
    // replaced with  order details 


    //public partial class OrderLines : IAuditInfo, IEnumerable
    //{
    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    //    public OrderLines()
    //    {
    //        InventoryTrans = new HashSet<InventoryTrans>();
    //        OrderLineTrans = new HashSet<OrderLineTrans>();
    //    }

    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Required]
    //    public int? OlnOrderNo { get; set; }//sales orders

    //    public int? OlnProdId { get; set; }
    //    [Required(ErrorMessage = "order line status is required")]
    //    [StringLength(100)]
    //    public string OlnLineStatus { get; set; }
    //    [Required]
    //    [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
    //    public int OrderQty { get; set; }
    //    public DateTime? CreationDate { get; set; }

    //    public DateTime? LastUpdateDate { get; set; }

    //    public string CreatedBy { get; set; }

    //      public string LastUpdatedBy { get; set; }

    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    //    public virtual ICollection<InventoryTrans> InventoryTrans { get; set; }

    //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    //    public virtual ICollection<OrderLineTrans> OrderLineTrans { get; set; }

    //    public virtual Products Products { get; set; }

    //    public virtual SalesOrders SalesOrders { get; set; }
    //    public IEnumerator GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
