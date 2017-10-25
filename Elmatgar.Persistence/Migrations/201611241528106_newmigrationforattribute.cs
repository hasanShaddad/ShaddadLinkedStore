namespace Elmatgar.persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigrationforattribute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders");
            AddForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders");
            AddForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders", "Id", cascadeDelete: true);
        }
    }
}
