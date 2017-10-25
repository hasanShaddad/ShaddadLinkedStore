namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supplierOrdersDateNotReq : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SupplyOrders", "OrderDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SupplyOrders", "OrderDate", c => c.DateTime(nullable: false));
        }
    }
}
