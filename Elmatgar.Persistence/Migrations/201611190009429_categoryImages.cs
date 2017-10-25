namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryTitle", c => c.String());
            AddColumn("dbo.Categories", "CategoryDescription", c => c.String());
            AddColumn("dbo.Categories", "CategoryImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CategoryImage");
            DropColumn("dbo.Categories", "CategoryDescription");
            DropColumn("dbo.Categories", "CategoryTitle");
        }
    }
}
