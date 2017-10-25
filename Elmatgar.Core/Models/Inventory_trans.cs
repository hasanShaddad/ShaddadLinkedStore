using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elmatgar.Core.Models
{
    public partial class InventoryTrans : IAuditInfo, IActiveFlag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = " ID")]
        public int Id { get; set; }
        [Display(Name = " Date")]
        public DateTime? ItTransDate { get; set; }
        [Display(Name = "Supplier ID")]
        public int? ItSupplierNo { get; set; }//��� ������ supplier id
        [Display(Name = "Store ID")]
        public int? ItStoreNo { get; set; }//��� ������  store id

        [StringLength(20)]
        [Display(Name = " Type")]
        public string ItTransType { get; set; }//����� transaction type
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Quantity")]
        public int? ItQty { get; set; }//������ quantity
        [Display(Name = "Order Details ID")]
        public int? ItOrderLineNo { get; set; }// ��� ��� orderline id
        [Display(Name = "product id")]
        public int? ItProdId { get; set; } // product id
        [Display(Name = "Supply Order Details ID")]
        public int? ItPoLineNo { get; set; }// ��� ������ Purchase_Order_Lines
        [Display(Name = " Amount ")]
        public int? ItTransAmt { get; set; } //?!!!!
        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "LastUpdate Date")]
        public DateTime? LastUpdateDate { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "LastUpdated By")]
        public string LastUpdatedBy { get; set; }
        [Display(Name = "Locked")]
        public bool ActiveFlag { get; set; } = true;

        public virtual OrderDetails OrderDetails { get; set; }//������ ����� ���

        public virtual Products Products { get; set; }//������

        public virtual SupplyOrderDetails SupplyOrderDetails { get; set; }//������ ����� ���� 

        public virtual SupplierStores SupplierStores { get; set; }//��� ������

        public virtual Suppliers Suppliers { get; set; }//��� ������

        // audited by eman herawy

    }
}
