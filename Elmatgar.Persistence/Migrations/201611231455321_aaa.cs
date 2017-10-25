namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int());
        }
    }
}
