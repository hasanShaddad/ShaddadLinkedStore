namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingActiveFalgeToAllModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttributeHeaders", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductAttributes", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Brands", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.InventoryTrans", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetails", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.DataAccesses", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Departments", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ExceptionAccesses", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ModulePages", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Modules", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.PageCloumns", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.PositionAccesses", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Positions", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUsers", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductVotes", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplierStores", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Suppliers", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplyOrders", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplyOrderDetails", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.SupplyOrderPayments", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerPaymentMethods", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentMethods", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderPayments", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeliveryMethods", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Returns", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductBarcodes", "ActiveFlag", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderLineTrans", "ActiveFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLineTrans", "ActiveFlag");
            DropColumn("dbo.ProductBarcodes", "ActiveFlag");
            DropColumn("dbo.Returns", "ActiveFlag");
            DropColumn("dbo.DeliveryMethods", "ActiveFlag");
            DropColumn("dbo.OrderPayments", "ActiveFlag");
            DropColumn("dbo.PaymentMethods", "ActiveFlag");
            DropColumn("dbo.CustomerPaymentMethods", "ActiveFlag");
            DropColumn("dbo.SupplyOrderPayments", "ActiveFlag");
            DropColumn("dbo.SupplyOrderDetails", "ActiveFlag");
            DropColumn("dbo.SupplyOrders", "ActiveFlag");
            DropColumn("dbo.Suppliers", "ActiveFlag");
            DropColumn("dbo.SupplierStores", "ActiveFlag");
            DropColumn("dbo.ProductVotes", "ActiveFlag");
            DropColumn("dbo.ApplicationUsers", "ActiveFlag");
            DropColumn("dbo.Positions", "ActiveFlag");
            DropColumn("dbo.PositionAccesses", "ActiveFlag");
            DropColumn("dbo.PageCloumns", "ActiveFlag");
            DropColumn("dbo.Modules", "ActiveFlag");
            DropColumn("dbo.ModulePages", "ActiveFlag");
            DropColumn("dbo.ExceptionAccesses", "ActiveFlag");
            DropColumn("dbo.Departments", "ActiveFlag");
            DropColumn("dbo.Users", "ActiveFlag");
            DropColumn("dbo.DataAccesses", "ActiveFlag");
            DropColumn("dbo.Customers", "ActiveFlag");
            DropColumn("dbo.Orders", "ActiveFlag");
            DropColumn("dbo.OrderDetails", "ActiveFlag");
            DropColumn("dbo.InventoryTrans", "ActiveFlag");
            DropColumn("dbo.Categories", "ActiveFlag");
            DropColumn("dbo.Brands", "ActiveFlag");
            DropColumn("dbo.ProductAttributes", "ActiveFlag");
            DropColumn("dbo.AttributeHeaders", "ActiveFlag");
        }
    }
}
