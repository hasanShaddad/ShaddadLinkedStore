namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordersforcheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ZipCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ZipCode");
            DropColumn("dbo.Orders", "Total");
        }
    }
}
