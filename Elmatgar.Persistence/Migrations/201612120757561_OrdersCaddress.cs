namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersCaddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CAddress", c => c.String());
            DropColumn("dbo.Orders", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Address", c => c.String());
            DropColumn("dbo.Orders", "CAddress");
        }
    }
}
