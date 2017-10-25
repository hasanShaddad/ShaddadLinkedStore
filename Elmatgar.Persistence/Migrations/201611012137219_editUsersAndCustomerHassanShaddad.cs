using System.Data.Entity.Migrations;

namespace Elmatgar.persistence.Migrations
{
    public partial class editUsersAndCustomerHassanShaddad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductVotes", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.CustomerPaymentMethods", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OrderPayments", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Customers", new[] { "users_Id" });
            //DropIndex("dbo.ProductVotes", new[] { "UserId" });
            //DropColumn("dbo.Customers", "Id");
            //RenameColumn(table: "dbo.Customers", name: "users_Id", newName: "Id");
            DropPrimaryKey("dbo.Customers");
            //AddColumn("dbo.ProductVotes", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ProductVotes", "UserId", c => c.String());
            AddPrimaryKey("dbo.Customers", "Id");
            //CreateIndex("dbo.Customers", "Id");
            //CreateIndex("dbo.ProductVotes", "User_Id");
            //AddForeignKey("dbo.ProductVotes", "User_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.CustomerPaymentMethods", "CustomerID", "dbo.Customers", "Id");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.OrderPayments", "CustomerId", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderPayments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerPaymentMethods", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.ProductVotes", "User_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.ProductVotes", new[] { "User_Id" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.ProductVotes", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Customers", "Id", c => c.String(maxLength: 128));
            DropColumn("dbo.ProductVotes", "User_Id");
            AddPrimaryKey("dbo.Customers", "Id");
            RenameColumn(table: "dbo.Customers", name: "Id", newName: "users_Id");
            AddColumn("dbo.Customers", "Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ProductVotes", "UserId");
            CreateIndex("dbo.Customers", "users_Id");
            AddForeignKey("dbo.OrderPayments", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.CustomerPaymentMethods", "CustomerID", "dbo.Customers", "Id");
            AddForeignKey("dbo.ProductVotes", "UserId", "dbo.ApplicationUsers", "Id");
        }
    }
}
