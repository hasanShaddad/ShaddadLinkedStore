namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTotalDetails : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Total", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
