namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productAttributeFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders");
            DropIndex("dbo.ProductAttributes", new[] { "AttributeHeaders_Id" });
            DropColumn("dbo.ProductAttributes", "PaAttHeaderId");
            RenameColumn(table: "dbo.ProductAttributes", name: "AttributeHeaders_Id", newName: "PaAttHeaderId");
            AlterColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductAttributes", "PaAttHeaderId");
            AddForeignKey("dbo.ProductAttributes", "PaAttHeaderId", "dbo.AttributeHeaders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductAttributes", "PaAttHeaderId", "dbo.AttributeHeaders");
            DropIndex("dbo.ProductAttributes", new[] { "PaAttHeaderId" });
            AlterColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int());
            AlterColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int());
            RenameColumn(table: "dbo.ProductAttributes", name: "PaAttHeaderId", newName: "AttributeHeaders_Id");
            AddColumn("dbo.ProductAttributes", "PaAttHeaderId", c => c.Int());
            CreateIndex("dbo.ProductAttributes", "AttributeHeaders_Id");
            AddForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders", "Id");
        }
    }
}
