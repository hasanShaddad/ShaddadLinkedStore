namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secoundlanguge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttributeHeaders", "SLAttributeName", c => c.String());
            AddColumn("dbo.ProductAttributes", "SLPaAttValue", c => c.String());
            AddColumn("dbo.Products", "SLName", c => c.String(maxLength: 100));
            AddColumn("dbo.Products", "SLShortDescription", c => c.String());
            AddColumn("dbo.Brands", "SLBrandName", c => c.String());
            AddColumn("dbo.Categories", "SLCategoryName", c => c.String());
            AddColumn("dbo.Cities", "SLCityName", c => c.String());
            AddColumn("dbo.Countries", "SLCountryName", c => c.String());
            AddColumn("dbo.Zones", "SLreaName", c => c.String());
            AddColumn("dbo.PaymentMethods", "SLPaymentMethodName", c => c.String());
            AddColumn("dbo.DeliveryMethods", "SLName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryMethods", "SLName");
            DropColumn("dbo.PaymentMethods", "SLPaymentMethodName");
            DropColumn("dbo.Zones", "SLreaName");
            DropColumn("dbo.Countries", "SLCountryName");
            DropColumn("dbo.Cities", "SLCityName");
            DropColumn("dbo.Categories", "SLCategoryName");
            DropColumn("dbo.Brands", "SLBrandName");
            DropColumn("dbo.Products", "SLShortDescription");
            DropColumn("dbo.Products", "SLName");
            DropColumn("dbo.ProductAttributes", "SLPaAttValue");
            DropColumn("dbo.AttributeHeaders", "SLAttributeName");
        }
    }
}
