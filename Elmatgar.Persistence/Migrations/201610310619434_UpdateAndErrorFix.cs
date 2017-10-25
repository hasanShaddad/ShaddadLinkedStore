namespace Elmatger.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAndErrorFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttributeHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AhAttributeName = c.String(nullable: false, maxLength: 100),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaAttHeaderId = c.Int(),
                        PaAttValue = c.String(nullable: false, maxLength: 100),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        ProductId = c.Int(nullable: false),
                        AttributeHeaders_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttributeHeaders", t => t.AttributeHeaders_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.AttributeHeaders_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KitProductsId = c.Int(),
                        BrandId = c.Int(),
                        CategoriesId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ShortDescription = c.String(),
                        KitFlag = c.Boolean(nullable: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        TotalVote = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoriesId)
                .ForeignKey("dbo.KitProducts", t => t.KitProductsId)
                .Index(t => t.KitProductsId)
                .Index(t => t.BrandId)
                .Index(t => t.CategoriesId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.CategoryName, unique: true, name: "AK_Category_CategoryName")
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.DiscountRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        DrQty = c.Int(nullable: false),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.InventoryTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItTransDate = c.DateTime(),
                        ItSupplierNo = c.Int(),
                        ItStoreNo = c.Int(),
                        ItTransType = c.String(maxLength: 20, unicode: false),
                        ItQty = c.Int(),
                        ItOrderLineNo = c.Int(),
                        ItProdId = c.Int(),
                        ItPoLineNo = c.Int(),
                        ItTransAmt = c.Int(),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderDetails", t => t.ItOrderLineNo)
                .ForeignKey("dbo.SupplierStores", t => t.ItStoreNo)
                .ForeignKey("dbo.Suppliers", t => t.ItSupplierNo)
                .ForeignKey("dbo.SupplyOrderDetails", t => t.ItPoLineNo)
                .ForeignKey("dbo.Products", t => t.ItProdId)
                .Index(t => t.ItSupplierNo)
                .Index(t => t.ItStoreNo)
                .Index(t => t.ItOrderLineNo)
                .Index(t => t.ItProdId)
                .Index(t => t.ItPoLineNo);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OrderStatus = c.String(nullable: false, maxLength: 20, unicode: false),
                        OrderQuantity = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        OrderStatus = c.String(nullable: false, maxLength: 30, unicode: false),
                        DeliveryOption = c.String(nullable: false, maxLength: 15),
                        EstimatedDeliveryTime = c.Int(nullable: false),
                        TestingTime = c.Int(),
                        PaymentType = c.String(nullable: false, maxLength: 20),
                        DeliveryId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        Dleivery_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.DeliveryMethods", t => t.Dleivery_ID)
                .Index(t => t.CustomerId)
                .Index(t => t.Dleivery_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CCustNo = c.Int(nullable: false, identity: true),
                        CFirstName = c.String(maxLength: 100, unicode: false),
                        CLastName = c.String(maxLength: 100, unicode: false),
                        CEmail = c.String(maxLength: 100, unicode: false),
                        CMobile = c.String(maxLength: 100, unicode: false),
                        CHomePhone = c.String(maxLength: 100, unicode: false),
                        CWorkPhone = c.String(maxLength: 100, unicode: false),
                        CCountryId = c.Int(),
                        CCityId = c.Int(),
                        CAreaId = c.Int(),
                        CAddress = c.String(maxLength: 200, unicode: false),
                        CLang = c.String(maxLength: 100, unicode: false),
                        CLong = c.String(maxLength: 100, unicode: false),
                        CCustStatus = c.String(maxLength: 100, unicode: false),
                        CCustClass = c.String(maxLength: 100, unicode: false),
                        CCustPoints = c.Int(),
                        CPassword = c.String(maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CCountryId)
                .ForeignKey("dbo.Zones", t => t.CAreaId)
                .ForeignKey("dbo.Cities", t => t.CCityId)
                .ForeignKey("dbo.ApplicationUsers", t => t.users_Id)
                .Index(t => t.CCountryId)
                .Index(t => t.CCityId)
                .Index(t => t.CAreaId)
                .Index(t => t.users_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CtCityName = c.String(nullable: false, maxLength: 100, unicode: false),
                        CtCountryId = c.Int(),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CtCountryId)
                .Index(t => t.CtCountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CnCountryName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DaCountryId = c.Int(nullable: false),
                        DaQuery = c.String(maxLength: 1, unicode: false),
                        DaExecute = c.String(maxLength: 1, unicode: false),
                        DaDelete = c.String(maxLength: 1, unicode: false),
                        DaPrint = c.String(maxLength: 1, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.DaCountryId })
                .ForeignKey("dbo.Countries", t => t.DaCountryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.DaCountryId)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false, identity: true),
                        UUserName = c.String(maxLength: 100, unicode: false),
                        UPositionNo = c.Int(),
                        DepartmentId = c.Int(),
                        UActive = c.String(maxLength: 100, unicode: false),
                        UEmail = c.String(maxLength: 100, unicode: false),
                        UMobile = c.String(maxLength: 100, unicode: false),
                        UId = c.String(maxLength: 100, unicode: false),
                        UPassword = c.String(maxLength: 100, unicode: false),
                        UManagerNo = c.Int(),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Positions", t => t.UPositionNo)
                .ForeignKey("dbo.ApplicationUsers", t => t.users_Id)
                .Index(t => t.UPositionNo)
                .Index(t => t.DepartmentId)
                .Index(t => t.users_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DDepartmentName = c.String(maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.ExceptionAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EaPageNo = c.Int(nullable: false),
                        EaQuery = c.String(maxLength: 1, unicode: false),
                        EaExecute = c.String(maxLength: 1, unicode: false),
                        EaDelete = c.String(maxLength: 1, unicode: false),
                        EaPrint = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.EaPageNo })
                .ForeignKey("dbo.ModulePages", t => t.EaPageNo)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.EaPageNo)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.ModulePages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MpModuleNo = c.Int(),
                        MpPageName = c.String(maxLength: 100, unicode: false),
                        MpPageUrl = c.String(maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.MpModuleNo)
                .Index(t => t.MpModuleNo);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MModuleName = c.String(maxLength: 100, unicode: false),
                        MModuleUrl = c.String(maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageCloumns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PcPageNo = c.Int(),
                        PcColumnLable = c.String(maxLength: 100, unicode: false),
                        PcTableName = c.String(maxLength: 100, unicode: false),
                        PcColumnName = c.String(maxLength: 100, unicode: false),
                        PcColumnSeq = c.Int(),
                        DataLogFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModulePages", t => t.PcPageNo)
                .Index(t => t.PcPageNo);
            
            CreateTable(
                "dbo.PositionAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaPageNo = c.Int(nullable: false),
                        PaQuery = c.String(maxLength: 1, unicode: false),
                        PaExecute = c.String(maxLength: 1, unicode: false),
                        PaDelete = c.String(maxLength: 1, unicode: false),
                        PaPrint = c.String(maxLength: 1, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.PaPageNo })
                .ForeignKey("dbo.Positions", t => t.Id)
                .ForeignKey("dbo.ModulePages", t => t.PaPageNo)
                .Index(t => t.Id)
                .Index(t => t.PaPageNo);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PPositionName = c.String(maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LasttName = c.String(),
                        IsCustomer = c.Boolean(nullable: false),
                        IsUser = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ProductOriginalPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountryId = c.Int(),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.CountryId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SupplierStores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        StoreName = c.String(nullable: false, maxLength: 100, unicode: false),
                        CountryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        AreaId = c.Int(),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.Zones", t => t.AreaId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.SupplierId)
                .Index(t => t.CountryId)
                .Index(t => t.CityId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupplyOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.SupplyOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplyOrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SupplyOrders", t => t.SupplyOrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.SupplyOrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SupplyOrderPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        SupplyOrderId = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SupplyOrders", t => t.SupplyOrderId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.SupplierId)
                .Index(t => t.SupplyOrderId);
            
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ACityId = c.Int(),
                        AAreaName = c.String(nullable: false, maxLength: 100, unicode: false),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.ACityId)
                .Index(t => t.ACityId);
            
            CreateTable(
                "dbo.CustomerPaymentMethods",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        PaymentMethodId = c.Int(nullable: false),
                        PaymentcontactDetails = c.String(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => new { t.CustomerID, t.PaymentMethodId })
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId)
                .Index(t => t.CustomerID)
                .Index(t => t.PaymentMethodId);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentMethodName = c.String(nullable: false, maxLength: 50),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        OrderId = c.Int(),
                        Paid = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.CustomerId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.DeliveryMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDetailsId = c.Int(nullable: false),
                        ReturnCause = c.String(nullable: false, maxLength: 150),
                        ReturnDate = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderDetails", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.KitProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActiveFlag = c.Boolean(nullable: false),
                        KitName = c.String(),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductBarcodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarcodeNo = c.Int(nullable: false),
                        Barcode = c.String(nullable: false, maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                        ImageExist = c.Boolean(nullable: false),
                        ImageLink = c.String(),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductPromotions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        PromoPrice = c.Decimal(precision: 18, scale: 2),
                        CountryId = c.Int(),
                        ActiveFlag = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.FromDate })
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.CountryId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductVotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteValue = c.Int(),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        LastUpdateDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.DataLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DlUserNo = c.Int(),
                        DlTransDate = c.DateTime(),
                        DlTableName = c.String(maxLength: 100, unicode: false),
                        DlColumnName = c.String(maxLength: 100, unicode: false),
                        DlNewValue = c.String(maxLength: 4000, unicode: false),
                        DlOldValue = c.String(maxLength: 4000, unicode: false),
                        DlPageNo = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        DlTransType = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DataLogs", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.SupplyOrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductVotes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductVotes", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ProductPromotions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductPromotions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ProductOriginalPrices", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductBarcodes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductAttributes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "KitProductsId", "dbo.KitProducts");
            DropForeignKey("dbo.InventoryTrans", "ItProdId", "dbo.Products");
            DropForeignKey("dbo.Returns", "Id", "dbo.OrderDetails");
            DropForeignKey("dbo.OrderPayments", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Dleivery_ID", "dbo.DeliveryMethods");
            DropForeignKey("dbo.Customers", "users_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.OrderPayments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerPaymentMethods", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.CustomerPaymentMethods", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Zones", "ACityId", "dbo.Cities");
            DropForeignKey("dbo.SupplierStores", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Customers", "CCityId", "dbo.Cities");
            DropForeignKey("dbo.SupplierStores", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.SupplierStores", "AreaId", "dbo.Zones");
            DropForeignKey("dbo.Customers", "CAreaId", "dbo.Zones");
            DropForeignKey("dbo.SupplyOrderPayments", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupplyOrders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupplyOrderPayments", "SupplyOrderId", "dbo.SupplyOrders");
            DropForeignKey("dbo.SupplyOrderDetails", "SupplyOrderId", "dbo.SupplyOrders");
            DropForeignKey("dbo.InventoryTrans", "ItPoLineNo", "dbo.SupplyOrderDetails");
            DropForeignKey("dbo.SupplierStores", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.InventoryTrans", "ItSupplierNo", "dbo.Suppliers");
            DropForeignKey("dbo.InventoryTrans", "ItStoreNo", "dbo.SupplierStores");
            DropForeignKey("dbo.ProductOriginalPrices", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Users", "users_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ExceptionAccesses", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.PositionAccesses", "PaPageNo", "dbo.ModulePages");
            DropForeignKey("dbo.Users", "UPositionNo", "dbo.Positions");
            DropForeignKey("dbo.PositionAccesses", "Id", "dbo.Positions");
            DropForeignKey("dbo.PageCloumns", "PcPageNo", "dbo.ModulePages");
            DropForeignKey("dbo.ModulePages", "MpModuleNo", "dbo.Modules");
            DropForeignKey("dbo.ExceptionAccesses", "EaPageNo", "dbo.ModulePages");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DataAccesses", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.DataAccesses", "DaCountryId", "dbo.Countries");
            DropForeignKey("dbo.Customers", "CCountryId", "dbo.Countries");
            DropForeignKey("dbo.Cities", "CtCountryId", "dbo.Countries");
            DropForeignKey("dbo.InventoryTrans", "ItOrderLineNo", "dbo.OrderDetails");
            DropForeignKey("dbo.DiscountRules", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoriesId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.DataLogs", new[] { "Users_Id" });
            DropIndex("dbo.ProductVotes", new[] { "ProductId" });
            DropIndex("dbo.ProductVotes", new[] { "UserId" });
            DropIndex("dbo.ProductPromotions", new[] { "ProductId" });
            DropIndex("dbo.ProductPromotions", new[] { "CountryId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.ProductBarcodes", new[] { "ProductId" });
            DropIndex("dbo.Returns", new[] { "Id" });
            DropIndex("dbo.OrderPayments", new[] { "OrderId" });
            DropIndex("dbo.OrderPayments", new[] { "CustomerId" });
            DropIndex("dbo.CustomerPaymentMethods", new[] { "PaymentMethodId" });
            DropIndex("dbo.CustomerPaymentMethods", new[] { "CustomerID" });
            DropIndex("dbo.Zones", new[] { "ACityId" });
            DropIndex("dbo.SupplyOrderPayments", new[] { "SupplyOrderId" });
            DropIndex("dbo.SupplyOrderPayments", new[] { "SupplierId" });
            DropIndex("dbo.SupplyOrderDetails", new[] { "ProductId" });
            DropIndex("dbo.SupplyOrderDetails", new[] { "SupplyOrderId" });
            DropIndex("dbo.SupplyOrders", new[] { "SupplierId" });
            DropIndex("dbo.SupplierStores", new[] { "AreaId" });
            DropIndex("dbo.SupplierStores", new[] { "CityId" });
            DropIndex("dbo.SupplierStores", new[] { "CountryId" });
            DropIndex("dbo.SupplierStores", new[] { "SupplierId" });
            DropIndex("dbo.ProductOriginalPrices", new[] { "ProductId" });
            DropIndex("dbo.ProductOriginalPrices", new[] { "CountryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PositionAccesses", new[] { "PaPageNo" });
            DropIndex("dbo.PositionAccesses", new[] { "Id" });
            DropIndex("dbo.PageCloumns", new[] { "PcPageNo" });
            DropIndex("dbo.ModulePages", new[] { "MpModuleNo" });
            DropIndex("dbo.ExceptionAccesses", new[] { "Users_Id" });
            DropIndex("dbo.ExceptionAccesses", new[] { "EaPageNo" });
            DropIndex("dbo.Users", new[] { "users_Id" });
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropIndex("dbo.Users", new[] { "UPositionNo" });
            DropIndex("dbo.DataAccesses", new[] { "Users_Id" });
            DropIndex("dbo.DataAccesses", new[] { "DaCountryId" });
            DropIndex("dbo.Cities", new[] { "CtCountryId" });
            DropIndex("dbo.Customers", new[] { "users_Id" });
            DropIndex("dbo.Customers", new[] { "CAreaId" });
            DropIndex("dbo.Customers", new[] { "CCityId" });
            DropIndex("dbo.Customers", new[] { "CCountryId" });
            DropIndex("dbo.Orders", new[] { "Dleivery_ID" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.InventoryTrans", new[] { "ItPoLineNo" });
            DropIndex("dbo.InventoryTrans", new[] { "ItProdId" });
            DropIndex("dbo.InventoryTrans", new[] { "ItOrderLineNo" });
            DropIndex("dbo.InventoryTrans", new[] { "ItStoreNo" });
            DropIndex("dbo.InventoryTrans", new[] { "ItSupplierNo" });
            DropIndex("dbo.DiscountRules", new[] { "ProductId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.Categories", "AK_Category_CategoryName");
            DropIndex("dbo.Products", new[] { "CategoriesId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "KitProductsId" });
            DropIndex("dbo.ProductAttributes", new[] { "AttributeHeaders_Id" });
            DropIndex("dbo.ProductAttributes", new[] { "ProductId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DataLogs");
            DropTable("dbo.ProductVotes");
            DropTable("dbo.ProductPromotions");
            DropTable("dbo.ProductImages");
            DropTable("dbo.ProductBarcodes");
            DropTable("dbo.KitProducts");
            DropTable("dbo.Returns");
            DropTable("dbo.DeliveryMethods");
            DropTable("dbo.OrderPayments");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.CustomerPaymentMethods");
            DropTable("dbo.Zones");
            DropTable("dbo.SupplyOrderPayments");
            DropTable("dbo.SupplyOrderDetails");
            DropTable("dbo.SupplyOrders");
            DropTable("dbo.Suppliers");
            DropTable("dbo.SupplierStores");
            DropTable("dbo.ProductOriginalPrices");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Positions");
            DropTable("dbo.PositionAccesses");
            DropTable("dbo.PageCloumns");
            DropTable("dbo.Modules");
            DropTable("dbo.ModulePages");
            DropTable("dbo.ExceptionAccesses");
            DropTable("dbo.Departments");
            DropTable("dbo.Users");
            DropTable("dbo.DataAccesses");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.InventoryTrans");
            DropTable("dbo.DiscountRules");
            DropTable("dbo.Categories");
            DropTable("dbo.Brands");
            DropTable("dbo.Products");
            DropTable("dbo.ProductAttributes");
            DropTable("dbo.AttributeHeaders");
        }
    }
}
