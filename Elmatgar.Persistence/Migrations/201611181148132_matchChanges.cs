namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class matchChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AttributeHeaders", "AhAttributeName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AttributeHeaders", "AttributeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AttributeHeaders", "AttributeName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AttributeHeaders", "AhAttributeName");
        }
    }
}
