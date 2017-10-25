namespace Elmatgar.persistence.Migrations
{
    //public partial class Initial : DbMigration
    //{
    //    public override void Up()
    //    {
    //        CreateTable(
    //            "dbo.AttributeHeaders",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    AhAttributeName = c.String(nullable: false, maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.ProductAttributes",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    PaAttHeaderId = c.Int(),
    //                    PaAttValue = c.String(nullable: false, maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    ProductId = c.Int(nullable: false),
    //                    AttributeHeaders_Id = c.Int(),
    //                    Products_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.AttributeHeaders", t => t.AttributeHeaders_Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .Index(t => t.AttributeHeaders_Id)
    //            .Index(t => t.Products_Id);
            
    //        CreateTable(
    //            "dbo.Products",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    KitProductsId = c.Int(),
    //                    BrandId = c.Int(),
    //                    CategoriesId = c.Int(nullable: false),
    //                    Name = c.String(maxLength: 100),
    //                    ShortDescription = c.String(),
    //                    KitFlag = c.Boolean(nullable: false),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    TotalVote = c.Int(),
    //                    Brands_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Brands", t => t.Brands_Id)
    //            .ForeignKey("dbo.Categories", t => t.CategoriesId, cascadeDelete: true)
    //            .ForeignKey("dbo.KitProducts", t => t.KitProductsId)
    //            .Index(t => t.KitProductsId)
    //            .Index(t => t.CategoriesId)
    //            .Index(t => t.Brands_Id);
            
    //        CreateTable(
    //            "dbo.Brands",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    BrandName = c.String(maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.Categories",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    ParentCategoryId = c.Int(),
    //                    CategoryName = c.String(nullable: false, maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Parent_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Categories", t => t.Parent_Id)
    //            .Index(t => t.Parent_Id);
            
    //        CreateTable(
    //            "dbo.DiscountRules",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    FromDate = c.DateTime(nullable: false),
    //                    EndDate = c.DateTime(),
    //                    DrQty = c.Int(nullable: false),
    //                    DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    ProductId = c.Int(nullable: false),
    //                    Products_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .Index(t => t.Products_Id);
            
    //        CreateTable(
    //            "dbo.InventoryTrans",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    ItTransDate = c.DateTime(),
    //                    ItSupplierNo = c.Int(),
    //                    ItStoreNo = c.Int(),
    //                    ItTransType = c.String(maxLength: 100),
    //                    ItQty = c.Int(),
    //                    ItOrderLineNo = c.Int(),
    //                    ItProdId = c.Int(),
    //                    ItPoLineNo = c.Int(),
    //                    ItTransAmt = c.Int(),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    OrderDetails_Id = c.Int(),
    //                    SupplierStores_Id = c.Int(),
    //                    Suppliers_Id = c.Int(),
    //                    SupplyOrderDetails_Id = c.Int(),
    //                    Products_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.OrderDetails", t => t.OrderDetails_Id)
    //            .ForeignKey("dbo.SupplierStores", t => t.SupplierStores_Id)
    //            .ForeignKey("dbo.Suppliers", t => t.Suppliers_Id)
    //            .ForeignKey("dbo.SupplyOrderDetails", t => t.SupplyOrderDetails_Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .Index(t => t.OrderDetails_Id)
    //            .Index(t => t.SupplierStores_Id)
    //            .Index(t => t.Suppliers_Id)
    //            .Index(t => t.SupplyOrderDetails_Id)
    //            .Index(t => t.Products_Id);
            
    //        CreateTable(
    //            "dbo.OrderDetails",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    OrderId = c.Int(nullable: false),
    //                    ProductId = c.Int(),
    //                    OrderStatus = c.String(nullable: false, maxLength: 100),
    //                    OrderQuantity = c.Int(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Products_Id = c.Int(),
    //                    Order_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .ForeignKey("dbo.Order", t => t.Order_Id)
    //            .Index(t => t.Products_Id)
    //            .Index(t => t.Order_Id);
            
    //        CreateTable(
    //            "dbo.OrderLineTrans",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    OltOrderLineNo = c.Int(nullable: false),
    //                    OltTransDate = c.DateTime(nullable: false),
    //                    OltStatus = c.String(nullable: false, maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    OrderDetails_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.OrderDetails", t => t.OrderDetails_Id)
    //            .Index(t => t.OrderDetails_Id);
            
    //        CreateTable(
    //            "dbo.Order",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    CustomerId = c.String(),
    //                    OrderStatus = c.String(maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Customers_CCustNo = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Customers", t => t.Customers_CCustNo)
    //            .Index(t => t.Customers_CCustNo);
            
    //        CreateTable(
    //            "dbo.Customers",
    //            c => new
    //                {
    //                    CCustNo = c.Int(nullable: false, identity: true),
    //                    Id = c.String(maxLength: 128),
    //                    CFirstName = c.String(maxLength: 100),
    //                    CLastName = c.String(maxLength: 100),
    //                    CEmail = c.String(maxLength: 100),
    //                    CMobile = c.String(maxLength: 100),
    //                    CHomePhone = c.String(maxLength: 100),
    //                    CWorkPhone = c.String(maxLength: 100),
    //                    CCountryId = c.Int(),
    //                    CCityId = c.Int(),
    //                    CAreaId = c.Int(),
    //                    CAddress = c.String(maxLength: 200),
    //                    CLang = c.String(maxLength: 100),
    //                    CLong = c.String(maxLength: 100),
    //                    CCustStatus = c.String(maxLength: 100),
    //                    CCustClass = c.String(maxLength: 100),
    //                    CCustPoints = c.Int(),
    //                    CPassword = c.String(maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Countries_Id = c.Int(),
    //                    Zones_Id = c.Int(),
    //                    Cities_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.CCustNo)
    //            .ForeignKey("dbo.Countries", t => t.Countries_Id)
    //            .ForeignKey("dbo.Zones", t => t.Zones_Id)
    //            .ForeignKey("dbo.Cities", t => t.Cities_Id)
    //            .ForeignKey("dbo.AspNetUsers", t => t.Id)
    //            .Index(t => t.Id)
    //            .Index(t => t.Countries_Id)
    //            .Index(t => t.Zones_Id)
    //            .Index(t => t.Cities_Id);
            
    //        CreateTable(
    //            "dbo.Cities",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    CtCityName = c.String(nullable: false, maxLength: 100),
    //                    CtCountryId = c.Int(),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Countries_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Countries", t => t.Countries_Id)
    //            .Index(t => t.Countries_Id);
            
    //        CreateTable(
    //            "dbo.Countries",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    CnCountryName = c.String(nullable: false, maxLength: 100),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.DataAccesses",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    CountryId = c.Int(nullable: false),
    //                    DaQuery = c.String(maxLength: 1, unicode: false),
    //                    DaExecute = c.String(maxLength: 1, unicode: false),
    //                    DaDelete = c.String(maxLength: 1, unicode: false),
    //                    DaPrint = c.String(maxLength: 1, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Users_UserId = c.Int(),
    //                })
    //            .PrimaryKey(t => new { t.Id, t.CountryId })
    //            .ForeignKey("dbo.Countries", t => t.Id, cascadeDelete: true)
    //            .ForeignKey("dbo.Users", t => t.Users_UserId)
    //            .Index(t => t.Id)
    //            .Index(t => t.Users_UserId);
            
    //        CreateTable(
    //            "dbo.Users",
    //            c => new
    //                {
    //                    UserId = c.Int(nullable: false, identity: true),
    //                    Id = c.String(maxLength: 128),
    //                    UUserName = c.String(maxLength: 100),
    //                    UPositionNo = c.Int(),
    //                    DepartmentId = c.Int(),
    //                    UActive = c.String(maxLength: 100),
    //                    UEmail = c.String(maxLength: 100),
    //                    UMobile = c.String(maxLength: 100),
    //                    UId = c.String(maxLength: 100),
    //                    UPassword = c.String(maxLength: 100),
    //                    UManagerNo = c.Int(),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.UserId)
    //            .ForeignKey("dbo.Departments", t => t.DepartmentId)
    //            .ForeignKey("dbo.Positions", t => t.UPositionNo)
    //            .ForeignKey("dbo.AspNetUsers", t => t.Id)
    //            .Index(t => t.Id)
    //            .Index(t => t.UPositionNo)
    //            .Index(t => t.DepartmentId);
            
    //        CreateTable(
    //            "dbo.Departments",
    //            c => new
    //                {
    //                    DepartmentId = c.Int(nullable: false, identity: true),
    //                    DDepartmentName = c.String(maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.DepartmentId);
            
    //        CreateTable(
    //            "dbo.ExceptionAccesses",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    EaPageNo = c.Int(nullable: false),
    //                    EaQuery = c.String(maxLength: 1, unicode: false),
    //                    EaExecute = c.String(maxLength: 1, unicode: false),
    //                    EaDelete = c.String(maxLength: 1, unicode: false),
    //                    EaPrint = c.String(maxLength: 10, fixedLength: true, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Users_UserId = c.Int(),
    //                })
    //            .PrimaryKey(t => new { t.Id, t.EaPageNo })
    //            .ForeignKey("dbo.ModulePages", t => t.EaPageNo)
    //            .ForeignKey("dbo.Users", t => t.Users_UserId)
    //            .Index(t => t.EaPageNo)
    //            .Index(t => t.Users_UserId);
            
    //        CreateTable(
    //            "dbo.ModulePages",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    MpModuleNo = c.Int(),
    //                    MpPageName = c.String(maxLength: 100, unicode: false),
    //                    MpPageUrl = c.String(maxLength: 100, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Modules", t => t.MpModuleNo)
    //            .Index(t => t.MpModuleNo);
            
    //        CreateTable(
    //            "dbo.Modules",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    MModuleName = c.String(maxLength: 100, unicode: false),
    //                    MModuleUrl = c.String(maxLength: 100, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.PageCloumns",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    PcPageNo = c.Int(),
    //                    PcColumnLable = c.String(maxLength: 100, unicode: false),
    //                    PcTableName = c.String(maxLength: 100, unicode: false),
    //                    PcColumnName = c.String(maxLength: 100, unicode: false),
    //                    PcColumnSeq = c.Int(),
    //                    DataLogFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.ModulePages", t => t.PcPageNo)
    //            .Index(t => t.PcPageNo);
            
    //        CreateTable(
    //            "dbo.PositionAccesses",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    PaPageNo = c.Int(nullable: false),
    //                    PaQuery = c.String(maxLength: 1, unicode: false),
    //                    PaExecute = c.String(maxLength: 1, unicode: false),
    //                    PaDelete = c.String(maxLength: 1, unicode: false),
    //                    PaPrint = c.String(maxLength: 1, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => new { t.Id, t.PaPageNo })
    //            .ForeignKey("dbo.Positions", t => t.Id)
    //            .ForeignKey("dbo.ModulePages", t => t.PaPageNo)
    //            .Index(t => t.Id)
    //            .Index(t => t.PaPageNo);
            
    //        CreateTable(
    //            "dbo.Positions",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    PPositionName = c.String(maxLength: 100, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.AspNetUsers",
    //            c => new
    //                {
    //                    Id = c.String(nullable: false, maxLength: 128),
    //                    FirstName = c.String(),
    //                    LasttName = c.String(),
    //                    IsCustomer = c.Boolean(nullable: false),
    //                    IsUser = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Email = c.String(maxLength: 256),
    //                    EmailConfirmed = c.Boolean(nullable: false),
    //                    PasswordHash = c.String(),
    //                    SecurityStamp = c.String(),
    //                    PhoneNumber = c.String(),
    //                    PhoneNumberConfirmed = c.Boolean(nullable: false),
    //                    TwoFactorEnabled = c.Boolean(nullable: false),
    //                    LockoutEndDateUtc = c.DateTime(),
    //                    LockoutEnabled = c.Boolean(nullable: false),
    //                    AccessFailedCount = c.Int(nullable: false),
    //                    UserName = c.String(nullable: false, maxLength: 256),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
    //        CreateTable(
    //            "dbo.AspNetUserClaims",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    UserId = c.String(nullable: false, maxLength: 128),
    //                    ClaimType = c.String(),
    //                    ClaimValue = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
    //            .Index(t => t.UserId);
            
    //        CreateTable(
    //            "dbo.AspNetUserLogins",
    //            c => new
    //                {
    //                    LoginProvider = c.String(nullable: false, maxLength: 128),
    //                    ProviderKey = c.String(nullable: false, maxLength: 128),
    //                    UserId = c.String(nullable: false, maxLength: 128),
    //                })
    //            .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
    //            .Index(t => t.UserId);
            
    //        CreateTable(
    //            "dbo.AspNetUserRoles",
    //            c => new
    //                {
    //                    UserId = c.String(nullable: false, maxLength: 128),
    //                    RoleId = c.String(nullable: false, maxLength: 128),
    //                })
    //            .PrimaryKey(t => new { t.UserId, t.RoleId })
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
    //            .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
    //            .Index(t => t.UserId)
    //            .Index(t => t.RoleId);
            
    //        CreateTable(
    //            "dbo.ProductOriginalPrices",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    FromDate = c.DateTime(nullable: false),
    //                    OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
    //                    CountryId = c.Int(),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    ProductId = c.Int(nullable: false),
    //                    Countries_Id = c.Int(),
    //                    Products_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Countries", t => t.Countries_Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .Index(t => t.Countries_Id)
    //            .Index(t => t.Products_Id);
            
    //        CreateTable(
    //            "dbo.ProductPromotions",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    FromDate = c.DateTime(nullable: false),
    //                    EndDate = c.DateTime(),
    //                    PromoPrice = c.Decimal(precision: 18, scale: 2),
    //                    CountryId = c.Int(),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    ProductId = c.Int(nullable: false),
    //                })
    //            .PrimaryKey(t => new { t.Id, t.FromDate })
    //            .ForeignKey("dbo.Countries", t => t.Id, cascadeDelete: true)
    //            .ForeignKey("dbo.Products", t => t.Id, cascadeDelete: true)
    //            .Index(t => t.Id);
            
    //        CreateTable(
    //            "dbo.SupplierStores",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    SupplierId = c.Int(nullable: false),
    //                    StoreName = c.String(nullable: false, maxLength: 100),
    //                    CountryId = c.Int(nullable: false),
    //                    CityId = c.Int(nullable: false),
    //                    AreaId = c.Int(),
    //                    SsLang = c.String(maxLength: 100),
    //                    SsLong = c.String(maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Cities_Id = c.Int(),
    //                    Countries_Id = c.Int(),
    //                    Suppliers_Id = c.Int(),
    //                    Zones_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Cities", t => t.Cities_Id)
    //            .ForeignKey("dbo.Countries", t => t.Countries_Id)
    //            .ForeignKey("dbo.Suppliers", t => t.Suppliers_Id)
    //            .ForeignKey("dbo.Zones", t => t.Zones_Id)
    //            .Index(t => t.Cities_Id)
    //            .Index(t => t.Countries_Id)
    //            .Index(t => t.Suppliers_Id)
    //            .Index(t => t.Zones_Id);
            
    //        CreateTable(
    //            "dbo.Suppliers",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    SupplierName = c.String(nullable: false, maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.SupplyOrderPayments",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    SupplierId = c.Int(nullable: false),
    //                    SupplyOrderId = c.Int(nullable: false),
    //                    Paid = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    SupplyOrder_Id = c.Int(),
    //                    Suppliers_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.SupplyOrders", t => t.SupplyOrder_Id)
    //            .ForeignKey("dbo.Suppliers", t => t.Suppliers_Id)
    //            .Index(t => t.SupplyOrder_Id)
    //            .Index(t => t.Suppliers_Id);
            
    //        CreateTable(
    //            "dbo.SupplyOrders",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    SupplierId = c.Int(nullable: false),
    //                    OrderDate = c.DateTime(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Suppliers_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Suppliers", t => t.Suppliers_Id)
    //            .Index(t => t.Suppliers_Id);
            
    //        CreateTable(
    //            "dbo.SupplyOrderDetails",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    SupplyOrderId = c.Int(nullable: false),
    //                    ProductId = c.Int(nullable: false),
    //                    Quantity = c.Int(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Products_Id = c.Int(),
    //                    SupplyOrder_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .ForeignKey("dbo.SupplyOrders", t => t.SupplyOrder_Id)
    //            .Index(t => t.Products_Id)
    //            .Index(t => t.SupplyOrder_Id);
            
    //        CreateTable(
    //            "dbo.Zones",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    ACityId = c.Int(),
    //                    AAreaName = c.String(nullable: false, maxLength: 100),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Cities_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Cities", t => t.Cities_Id)
    //            .Index(t => t.Cities_Id);
            
    //        CreateTable(
    //            "dbo.OrderPayments",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    CustomerId = c.String(),
    //                    OrderId = c.Int(),
    //                    Paid = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Customers_CCustNo = c.Int(),
    //                    Order_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Customers", t => t.Customers_CCustNo)
    //            .ForeignKey("dbo.Order", t => t.Order_Id)
    //            .Index(t => t.Customers_CCustNo)
    //            .Index(t => t.Order_Id);
            
    //        CreateTable(
    //            "dbo.KitProducts",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    KitName = c.String(),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.ProductBarcodes",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    BarcodeNo = c.Int(nullable: false),
    //                    Barcode = c.String(nullable: false, maxLength: 100),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    ProductId = c.Int(nullable: false),
    //                    Products_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .Index(t => t.Products_Id);
            
    //        CreateTable(
    //            "dbo.ProductImages",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    ImageName = c.String(),
    //                    ImageExist = c.Boolean(nullable: false),
    //                    ImageLink = c.String(),
    //                    ActiveFlag = c.Boolean(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    ProductId = c.Int(nullable: false),
    //                    Products_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .Index(t => t.Products_Id);
            
    //        CreateTable(
    //            "dbo.ProductVotes",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    VoteValue = c.Int(),
    //                    UserId = c.String(maxLength: 128),
    //                    ProductId = c.Int(nullable: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                    Products_Id = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Products", t => t.Products_Id)
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId)
    //            .Index(t => t.UserId)
    //            .Index(t => t.Products_Id);
            
    //        CreateTable(
    //            "dbo.DataLogs",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    DlUserNo = c.Int(),
    //                    DlTransDate = c.DateTime(),
    //                    DlTableName = c.String(maxLength: 100, unicode: false),
    //                    DlColumnName = c.String(maxLength: 100, unicode: false),
    //                    DlNewValue = c.String(maxLength: 4000, unicode: false),
    //                    DlOldValue = c.String(maxLength: 4000, unicode: false),
    //                    DlPageNo = c.String(maxLength: 10, fixedLength: true, unicode: false),
    //                    DlTransType = c.String(maxLength: 10, fixedLength: true, unicode: false),
    //                    Users_UserId = c.Int(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Users", t => t.Users_UserId)
    //            .Index(t => t.Users_UserId);
            
    //        CreateTable(
    //            "dbo.LangTexts",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    LtLangId = c.Int(nullable: false),
    //                    LtText = c.String(maxLength: 500, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => new { t.Id, t.LtLangId })
    //            .ForeignKey("dbo.Languages", t => t.LtLangId)
    //            .Index(t => t.LtLangId);
            
    //        CreateTable(
    //            "dbo.Languages",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    LLanguage = c.String(maxLength: 100, unicode: false),
    //                    CreationDate = c.DateTime(),
    //                    LastUpdateDate = c.DateTime(),
    //                    CreatedBy = c.String(),
    //                    LastUpdatedBy = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.AspNetRoles",
    //            c => new
    //                {
    //                    Id = c.String(nullable: false, maxLength: 128),
    //                    Name = c.String(nullable: false, maxLength: 256),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
    //    }
        
    //    public override void Down()
    //    {
    //        DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
    //        DropForeignKey("dbo.LangTexts", "LtLangId", "dbo.Languages");
    //        DropForeignKey("dbo.DataLogs", "Users_UserId", "dbo.Users");
    //        DropForeignKey("dbo.ProductVotes", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.ProductVotes", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.ProductImages", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.ProductBarcodes", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.ProductAttributes", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.Products", "KitProductsId", "dbo.KitProducts");
    //        DropForeignKey("dbo.InventoryTrans", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Order");
    //        DropForeignKey("dbo.Customers", "Id", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.Order", "Customers_CCustNo", "dbo.Customers");
    //        DropForeignKey("dbo.OrderPayments", "Order_Id", "dbo.Order");
    //        DropForeignKey("dbo.OrderPayments", "Customers_CCustNo", "dbo.Customers");
    //        DropForeignKey("dbo.Customers", "Cities_Id", "dbo.Cities");
    //        DropForeignKey("dbo.SupplierStores", "Zones_Id", "dbo.Zones");
    //        DropForeignKey("dbo.Customers", "Zones_Id", "dbo.Zones");
    //        DropForeignKey("dbo.Zones", "Cities_Id", "dbo.Cities");
    //        DropForeignKey("dbo.SupplierStores", "Suppliers_Id", "dbo.Suppliers");
    //        DropForeignKey("dbo.SupplyOrderPayments", "Suppliers_Id", "dbo.Suppliers");
    //        DropForeignKey("dbo.SupplyOrders", "Suppliers_Id", "dbo.Suppliers");
    //        DropForeignKey("dbo.SupplyOrderPayments", "SupplyOrder_Id", "dbo.SupplyOrders");
    //        DropForeignKey("dbo.SupplyOrderDetails", "SupplyOrder_Id", "dbo.SupplyOrders");
    //        DropForeignKey("dbo.SupplyOrderDetails", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.InventoryTrans", "SupplyOrderDetails_Id", "dbo.SupplyOrderDetails");
    //        DropForeignKey("dbo.InventoryTrans", "Suppliers_Id", "dbo.Suppliers");
    //        DropForeignKey("dbo.InventoryTrans", "SupplierStores_Id", "dbo.SupplierStores");
    //        DropForeignKey("dbo.SupplierStores", "Countries_Id", "dbo.Countries");
    //        DropForeignKey("dbo.SupplierStores", "Cities_Id", "dbo.Cities");
    //        DropForeignKey("dbo.ProductPromotions", "Id", "dbo.Products");
    //        DropForeignKey("dbo.ProductPromotions", "Id", "dbo.Countries");
    //        DropForeignKey("dbo.ProductOriginalPrices", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.ProductOriginalPrices", "Countries_Id", "dbo.Countries");
    //        DropForeignKey("dbo.Users", "Id", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.ExceptionAccesses", "Users_UserId", "dbo.Users");
    //        DropForeignKey("dbo.PositionAccesses", "PaPageNo", "dbo.ModulePages");
    //        DropForeignKey("dbo.Users", "UPositionNo", "dbo.Positions");
    //        DropForeignKey("dbo.PositionAccesses", "Id", "dbo.Positions");
    //        DropForeignKey("dbo.PageCloumns", "PcPageNo", "dbo.ModulePages");
    //        DropForeignKey("dbo.ModulePages", "MpModuleNo", "dbo.Modules");
    //        DropForeignKey("dbo.ExceptionAccesses", "EaPageNo", "dbo.ModulePages");
    //        DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
    //        DropForeignKey("dbo.DataAccesses", "Users_UserId", "dbo.Users");
    //        DropForeignKey("dbo.DataAccesses", "Id", "dbo.Countries");
    //        DropForeignKey("dbo.Customers", "Countries_Id", "dbo.Countries");
    //        DropForeignKey("dbo.Cities", "Countries_Id", "dbo.Countries");
    //        DropForeignKey("dbo.OrderDetails", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.OrderLineTrans", "OrderDetails_Id", "dbo.OrderDetails");
    //        DropForeignKey("dbo.InventoryTrans", "OrderDetails_Id", "dbo.OrderDetails");
    //        DropForeignKey("dbo.DiscountRules", "Products_Id", "dbo.Products");
    //        DropForeignKey("dbo.Products", "CategoriesId", "dbo.Categories");
    //        DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
    //        DropForeignKey("dbo.Products", "Brands_Id", "dbo.Brands");
    //        DropForeignKey("dbo.ProductAttributes", "AttributeHeaders_Id", "dbo.AttributeHeaders");
    //        DropIndex("dbo.AspNetRoles", "RoleNameIndex");
    //        DropIndex("dbo.LangTexts", new[] { "LtLangId" });
    //        DropIndex("dbo.DataLogs", new[] { "Users_UserId" });
    //        DropIndex("dbo.ProductVotes", new[] { "Products_Id" });
    //        DropIndex("dbo.ProductVotes", new[] { "UserId" });
    //        DropIndex("dbo.ProductImages", new[] { "Products_Id" });
    //        DropIndex("dbo.ProductBarcodes", new[] { "Products_Id" });
    //        DropIndex("dbo.OrderPayments", new[] { "Order_Id" });
    //        DropIndex("dbo.OrderPayments", new[] { "Customers_CCustNo" });
    //        DropIndex("dbo.Zones", new[] { "Cities_Id" });
    //        DropIndex("dbo.SupplyOrderDetails", new[] { "SupplyOrder_Id" });
    //        DropIndex("dbo.SupplyOrderDetails", new[] { "Products_Id" });
    //        DropIndex("dbo.SupplyOrders", new[] { "Suppliers_Id" });
    //        DropIndex("dbo.SupplyOrderPayments", new[] { "Suppliers_Id" });
    //        DropIndex("dbo.SupplyOrderPayments", new[] { "SupplyOrder_Id" });
    //        DropIndex("dbo.SupplierStores", new[] { "Zones_Id" });
    //        DropIndex("dbo.SupplierStores", new[] { "Suppliers_Id" });
    //        DropIndex("dbo.SupplierStores", new[] { "Countries_Id" });
    //        DropIndex("dbo.SupplierStores", new[] { "Cities_Id" });
    //        DropIndex("dbo.ProductPromotions", new[] { "Id" });
    //        DropIndex("dbo.ProductOriginalPrices", new[] { "Products_Id" });
    //        DropIndex("dbo.ProductOriginalPrices", new[] { "Countries_Id" });
    //        DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
    //        DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
    //        DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
    //        DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
    //        DropIndex("dbo.AspNetUsers", "UserNameIndex");
    //        DropIndex("dbo.PositionAccesses", new[] { "PaPageNo" });
    //        DropIndex("dbo.PositionAccesses", new[] { "Id" });
    //        DropIndex("dbo.PageCloumns", new[] { "PcPageNo" });
    //        DropIndex("dbo.ModulePages", new[] { "MpModuleNo" });
    //        DropIndex("dbo.ExceptionAccesses", new[] { "Users_UserId" });
    //        DropIndex("dbo.ExceptionAccesses", new[] { "EaPageNo" });
    //        DropIndex("dbo.Users", new[] { "DepartmentId" });
    //        DropIndex("dbo.Users", new[] { "UPositionNo" });
    //        DropIndex("dbo.Users", new[] { "Id" });
    //        DropIndex("dbo.DataAccesses", new[] { "Users_UserId" });
    //        DropIndex("dbo.DataAccesses", new[] { "Id" });
    //        DropIndex("dbo.Cities", new[] { "Countries_Id" });
    //        DropIndex("dbo.Customers", new[] { "Cities_Id" });
    //        DropIndex("dbo.Customers", new[] { "Zones_Id" });
    //        DropIndex("dbo.Customers", new[] { "Countries_Id" });
    //        DropIndex("dbo.Customers", new[] { "Id" });
    //        DropIndex("dbo.Order", new[] { "Customers_CCustNo" });
    //        DropIndex("dbo.OrderLineTrans", new[] { "OrderDetails_Id" });
    //        DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
    //        DropIndex("dbo.OrderDetails", new[] { "Products_Id" });
    //        DropIndex("dbo.InventoryTrans", new[] { "Products_Id" });
    //        DropIndex("dbo.InventoryTrans", new[] { "SupplyOrderDetails_Id" });
    //        DropIndex("dbo.InventoryTrans", new[] { "Suppliers_Id" });
    //        DropIndex("dbo.InventoryTrans", new[] { "SupplierStores_Id" });
    //        DropIndex("dbo.InventoryTrans", new[] { "OrderDetails_Id" });
    //        DropIndex("dbo.DiscountRules", new[] { "Products_Id" });
    //        DropIndex("dbo.Categories", new[] { "Parent_Id" });
    //        DropIndex("dbo.Products", new[] { "Brands_Id" });
    //        DropIndex("dbo.Products", new[] { "CategoriesId" });
    //        DropIndex("dbo.Products", new[] { "KitProductsId" });
    //        DropIndex("dbo.ProductAttributes", new[] { "Products_Id" });
    //        DropIndex("dbo.ProductAttributes", new[] { "AttributeHeaders_Id" });
    //        DropTable("dbo.AspNetRoles");
    //        DropTable("dbo.Languages");
    //        DropTable("dbo.LangTexts");
    //        DropTable("dbo.DataLogs");
    //        DropTable("dbo.ProductVotes");
    //        DropTable("dbo.ProductImages");
    //        DropTable("dbo.ProductBarcodes");
    //        DropTable("dbo.KitProducts");
    //        DropTable("dbo.OrderPayments");
    //        DropTable("dbo.Zones");
    //        DropTable("dbo.SupplyOrderDetails");
    //        DropTable("dbo.SupplyOrders");
    //        DropTable("dbo.SupplyOrderPayments");
    //        DropTable("dbo.Suppliers");
    //        DropTable("dbo.SupplierStores");
    //        DropTable("dbo.ProductPromotions");
    //        DropTable("dbo.ProductOriginalPrices");
    //        DropTable("dbo.AspNetUserRoles");
    //        DropTable("dbo.AspNetUserLogins");
    //        DropTable("dbo.AspNetUserClaims");
    //        DropTable("dbo.AspNetUsers");
    //        DropTable("dbo.Positions");
    //        DropTable("dbo.PositionAccesses");
    //        DropTable("dbo.PageCloumns");
    //        DropTable("dbo.Modules");
    //        DropTable("dbo.ModulePages");
    //        DropTable("dbo.ExceptionAccesses");
    //        DropTable("dbo.Departments");
    //        DropTable("dbo.Users");
    //        DropTable("dbo.DataAccesses");
    //        DropTable("dbo.Countries");
    //        DropTable("dbo.Cities");
    //        DropTable("dbo.Customers");
    //        DropTable("dbo.Order");
    //        DropTable("dbo.OrderLineTrans");
    //        DropTable("dbo.OrderDetails");
    //        DropTable("dbo.InventoryTrans");
    //        DropTable("dbo.DiscountRules");
    //        DropTable("dbo.Categories");
    //        DropTable("dbo.Brands");
    //        DropTable("dbo.Products");
    //        DropTable("dbo.ProductAttributes");
    //        DropTable("dbo.AttributeHeaders");
    //    }
    //}
}
