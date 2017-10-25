namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productattrId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders");
            DropIndex("dbo.ProductAttributes", new[] { "AttributeHeaders_Id" });
            AddColumn("dbo.ProductAttributes", "AttributeHeaders_Id1", c => c.Int());
            AlterColumn("dbo.ProductAttributes", "AttributeHeaders_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductAttributes", "AttributeHeaders_Id1");
            AddForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id1", "dbo.AttributeHeaders", "Id");
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id1", "dbo.AttributeHeaders");
            DropIndex("dbo.ProductAttributes", new[] { "AttributeHeaders_Id1" });
            AlterColumn("dbo.ProductAttributes", "AttributeHeaders_Id", c => c.Int());
            DropColumn("dbo.ProductAttributes", "AttributeHeaders_Id1");
            CreateIndex("dbo.ProductAttributes", "AttributeHeaders_Id");
            AddForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders", "Id");
        }
    }
}
