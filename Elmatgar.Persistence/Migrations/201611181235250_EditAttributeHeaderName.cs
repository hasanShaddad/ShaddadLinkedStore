namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditAttributeHeaderName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttributeHeaders", "AttributeName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AttributeHeaders", "AhAttributeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AttributeHeaders", "AhAttributeName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AttributeHeaders", "AttributeName");
        }
    }
}
