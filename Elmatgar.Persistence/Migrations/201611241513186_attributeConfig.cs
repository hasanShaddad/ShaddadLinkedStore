namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attributeConfig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id1", "dbo.AttributeHeaders");
            DropIndex("dbo.ProductAttributes", new[] { "AttributeHeaders_Id1" });
          
       
           
            AlterColumn("dbo.ProductAttributes", "AttributeHeaders_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductAttributes", "AttributeHeaders_Id");
            AddForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders");
            
        
           
            AddForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders", "Id");
        }
    }
}
