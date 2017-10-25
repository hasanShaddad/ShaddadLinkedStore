namespace Elmatgar.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int StockQuntity { get; set; }

        //public int StockQuntity
        //{
        //    get
        //    {
        //        this.qnt = _inventoryServices.GetQuntity(ProductId);
        //        return this.qnt;
        //    }
        //}

        //private readonly IInventoryServices _inventoryServices;

        //public ProductViewModel(IInventoryServices inventoryServices)
        //{
        //    _inventoryServices = inventoryServices;
        // }     

        //public int StockQuntity
        //{
        //    get
        //    {
        //        this.qnt = _inventoryServices.GetQuntity(ProductId);
        //        return this.qnt;
        //    }
        //}

        // private readonly IInventoryServices _inventoryServices = new InventoryServices();




    }
}