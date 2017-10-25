namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderedit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "DeliveryOption", c => c.String(maxLength: 15));
            AlterColumn("dbo.Orders", "PaymentType", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Orders", "DeliveryOption", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
