namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderscountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CCountryId", c => c.Int());
            AddColumn("dbo.Orders", "CCityId", c => c.Int());
            AddColumn("dbo.Orders", "CAreaId", c => c.Int());
            AddColumn("dbo.Orders", "Address", c => c.String());
            CreateIndex("dbo.Orders", "CCountryId");
            CreateIndex("dbo.Orders", "CCityId");
            CreateIndex("dbo.Orders", "CAreaId");
            AddForeignKey("dbo.Orders", "CAreaId", "dbo.Zones", "Id");
            AddForeignKey("dbo.Orders", "CCountryId", "dbo.Countries", "Id");
            AddForeignKey("dbo.Orders", "CCityId", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CCityId", "dbo.Cities");
            DropForeignKey("dbo.Orders", "CCountryId", "dbo.Countries");
            DropForeignKey("dbo.Orders", "CAreaId", "dbo.Zones");
            DropIndex("dbo.Orders", new[] { "CAreaId" });
            DropIndex("dbo.Orders", new[] { "CCityId" });
            DropIndex("dbo.Orders", new[] { "CCountryId" });
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "CAreaId");
            DropColumn("dbo.Orders", "CCityId");
            DropColumn("dbo.Orders", "CCountryId");
        }
    }
}
