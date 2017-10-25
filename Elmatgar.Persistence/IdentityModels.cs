//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;

//using Elmatgar.Core.Models;
//using Elmatger.persistence.Configurations;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Elmatgar.Migrations;

//namespace Elmatgar.Persistence
//{
//    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
//    public class ApplicationUser : IdentityUser, IAuditInfo
//    {
//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }
//        //add properties and columns to the users table
//        public string FirstName { get; set; }
//        public string LasttName { get; set; }
//        public Boolean IsCustomer { get; set; }
//        public Boolean IsUser { get; set; }


//        public DateTime? CreationDate { get; set; }

//        public DateTime? LastUpdateDate { get; set; }

//        public string CreatedBy { get; set; }

//        public string LastUpdatedBy { get; set; }

//        public virtual Customers Customers { get; set; }
//        public virtual ProductVote ProductVote { get; set; }
//        public virtual Users Users { get; set; }
//        public IEnumerable<SelectListItem> RolesList { get; set; }
//    }
//    /// <summary>
//    /// DB context inherent from IdentityDbContext
//    /// </summary>
//    public class StoreDbContext : IdentityDbContext<ApplicationUser>
//    {
//        /// <summary>
//        /// set the connection and dbset for all classes in the core 
//        /// </summary>
//        public StoreDbContext()
//            : base("DefaultConnection", throwIfV1Schema: false)
//        {
//            System.Data.Entity.Database.SetInitializer<StoreDbContext>(new CreateDatabaseIfNotExists<StoreDbContext>());

//        }
//        /// <summary>
//        /// DbSet for all classes in the core
//        /// </summary>
//        public DbSet<Zones> Zones { get; set; }
//        public DbSet<AttributeHeaders> AttributeHeaders { get; set; }
//        public DbSet<Brands> Brands { get; set; }
//        public DbSet<Cities> Cities { get; set; }
//        public DbSet<Countries> Countries { get; set; }
//        public DbSet<Customers> Customers { get; set; }
//        public DbSet<DataAccess> DataAccess { get; set; }
//        public DbSet<DataLog> DataLog { get; set; }
//        public DbSet<Departments> Departments { get; set; }
//        public DbSet<DiscountRules> DiscountRules { get; set; }
//        public DbSet<ExceptionAccess> ExceptionAccess { get; set; }
//        public DbSet<InventoryTrans> InventoryTrans { get; set; }
//        public DbSet<KitProducts> KitProducts { get; set; }
       
//        public DbSet<ModulePages> ModulePages { get; set; }
//        public DbSet<Modules> Modules { get; set; }
//        public DbSet<OrderLineTrans> OrderLineTrans { get; set; }
//        public DbSet<OrderDetails> OrderDetails { get; set; }
//        public DbSet<PageCloumns> PageCloumns { get; set; }
//        public DbSet<PositionAccess> PositionAccess { get; set; }
//        public DbSet<Positions> Positions { get; set; }
//        public DbSet<ProductAttributes> ProductAttributes { get; set; }
//        public DbSet<ProductBarcodes> ProductBarcodes { get; set; }
//        public DbSet<ProductImages> ProductImages { get; set; }
//        public DbSet<ProductOriginalPrices> ProductOriginalPrices { get; set; }
//        public DbSet<ProductPromotions> ProductPromotions { get; set; }
//        public DbSet<Products> Products { get; set; }
//        public DbSet<SupplyOrderDetails> SupplyOrderDetails { get; set; }
//        public DbSet<SupplyOrderPayments> SupplyOrderPayments { get; set; }
//        public IQueryable<SupplyOrders> SupplyOrder { get; set; }
//        public DbSet<OrderPayments> SalesOrderPayments { get; set; }
//        public DbSet<Order> Order { get; set; }
//        public DbSet<Categories> Categories { get; set; }
//        public DbSet<SupplierStores> SupplierStores { get; set; }
//        public DbSet<Suppliers> Suppliers { get; set; }
//        public DbSet<Users> Users { get; set; }
//        /// <summary>
//        /// override the OnModelCreating and add the configuration classes
//        /// when scaffolding we comment the  modelBuilder.Configurations and uncomment the  modelBuilder.Entities
//        /// then reverce by uncomment  modelBuilder.Configurations 
//        /// </summary>
//        /// <param name="modelBuilder"></param>
//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {

//            //config class for each table
//            //areas Configuration
//            modelBuilder.Configurations.Add(new ZonesConfiguration());
//            //Attribute Configuration
//            modelBuilder.Configurations.Add(new AttributeConfiguration());
//            //Brands Configuration
//            modelBuilder.Configurations.Add(new BrandsConfiguration());
//            //Categories configurations
//            modelBuilder.Configurations.Add(new CategoriesConfiguration());
//            //applicationuser configurations
//            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
//            //cities configurations
//            modelBuilder.Configurations.Add(new CitiesConfiguration());
//            //countries configurations
//            modelBuilder.Configurations.Add(new CountriesConfiguration());
//            //coustomers configurations
//            modelBuilder.Configurations.Add(new CustomersConfiguration());
//            //dataAcsess configurations
//            modelBuilder.Configurations.Add(new DataAccessConfiguration());
//            //datalog configurations
//            modelBuilder.Configurations.Add(new DataLogConfiguration());
//            //departmentsconfigurations
//            modelBuilder.Configurations.Add(new DepartmentsConfiguration());
//            //exception configurations
//            modelBuilder.Configurations.Add(new ExceptionAccessConfiguration());
//            // inventory configurations
//            modelBuilder.Configurations.Add(new InventoryTransConfiguration());
//            //OrderDetailstrans configurations
//            modelBuilder.Configurations.Add(new OrderLineTransConfiguration());
//            // OrderDetails configurations
//            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
//            //product attreb  configurations
//            modelBuilder.Configurations.Add(new ProductAttributesConfiguration());
//            // product barcode configurations
//            modelBuilder.Configurations.Add(new ProductBarcodesConfiguration());

//            // product configurations
//            modelBuilder.Configurations.Add(new ProductsConfiguration());
//            //SupplyOrderDetails configurations
//            modelBuilder.Configurations.Add(new SupplyOrderDetailsConfiguration());
//            // prchase orders configurations
//            modelBuilder.Configurations.Add(new SupplyOrderConfiguration());
//            //sales order  configurations
//            modelBuilder.Configurations.Add(new OrderConfiguration());
//            // suppliers configurations
//            modelBuilder.Configurations.Add(new SuppliersConfiguration());
//            //suppliers stores  configurations
//            modelBuilder.Configurations.Add(new SupplierStoresConfiguration());
//            //users  configurations
//            modelBuilder.Configurations.Add(new UsersConfiguration());
//            //  KitProducts configurations
//            modelBuilder.Configurations.Add(new KitProductsConfiguration());
//            // Discount Rules Configuration configurations
//            modelBuilder.Configurations.Add(new DiscountRulesConfiguration());




//            //   modelBuilder.Entity<Zones>()
//            //    .Property(e => e.AAreaName)
//            //  .IsUnicode(false);

//            //   modelBuilder.Entity<Zones>()
//            //   .HasMany(e => e.Customers)
//            //     .WithOptional(e => e.Zones)
//            //     .HasForeignKey(e => e.CAreaId);
//            //   modelBuilder.Entity<Zones>()
//            //   .HasMany(e => e.SupplierStores)
//            //        .WithOptional(e => e.Zones)
//            //        .HasForeignKey(e => e.AreaId);
//            //   //////////////////////////////////////////////////////////////////////////////////
//            //   // AttributeHeaders
//            //   modelBuilder.Entity<AttributeHeaders>()
//            //    .Property(e => e.AhAttributeName)
//            //    .IsUnicode(false);
//            //   modelBuilder.Entity<AttributeHeaders>()
//            //   .HasMany(e => e.ProductAttributes)
//            //   .WithOptional(e => e.AttributeHeaders)
//            //   .HasForeignKey(e => e.PaAttHeaderId);
//            //   //////////////////////////////////////////////////////////////////////////////////
//            //   // brands
//            //   modelBuilder.Entity<Brands>()
//            // .Property(e => e.BrandName)
//            //.IsUnicode(false);
//            //   modelBuilder.Entity<Brands>()
//            //  .HasMany(e => e.Products)
//            //    .WithOptional(e => e.Brands)
//            //    .HasForeignKey(e => e.BrandId);

//            //   ////////////////////////////////////////////////////////////////////////////////////

//            //   modelBuilder.Entity<Cities>()
//            //       .Property(e => e.CtCityName)
//            //       .IsUnicode(false);



//            //   modelBuilder.Entity<Cities>()
//            //       .HasMany(e => e.Zones)
//            //       .WithOptional(e => e.Cities)
//            //       .HasForeignKey(e => e.ACityId);

//            //   modelBuilder.Entity<Cities>()
//            //       .HasMany(e => e.Customers)
//            //       .WithOptional(e => e.Cities)
//            //       .HasForeignKey(e => e.CCityId);

//            //   modelBuilder.Entity<Cities>()
//            //       .HasMany(e => e.SupplierStores)
//            //       .WithRequired(e => e.Cities)
//            //       .HasForeignKey(e => e.CityId)
//            //       .WillCascadeOnDelete(false);

//            //   ////////////////////////////////////////////////////////////////////////////////////

//            //   modelBuilder.Entity<Countries>()
//            //       .Property(e => e.CnCountryName)
//            //       .IsUnicode(false);

//            //   modelBuilder.Entity<Countries>()
//            //       .HasMany(e => e.Cities)
//            //       .WithOptional(e => e.Countries)
//            //       .HasForeignKey(e => e.CtCountryId);

//            //   modelBuilder.Entity<Countries>()
//            //       .HasMany(e => e.Customers)
//            //       .WithOptional(e => e.Countries)
//            //       .HasForeignKey(e => e.CCountryId);

//            //   modelBuilder.Entity<Countries>()
//            //       .HasMany(e => e.DataAccess)
//            //       .WithRequired(e => e.Countries)
//            //       .HasForeignKey(e => e.DaCountryId)
//            //       .WillCascadeOnDelete(false);

//            //   modelBuilder.Entity<Countries>()
//            //       .HasMany(e => e.ProductOriginalPrices)
//            //       .WithOptional(e => e.Countries)
//            //       .HasForeignKey(e => e.CountryId);

//            //   modelBuilder.Entity<Countries>()
//            //       .HasMany(e => e.ProductPromotions)
//            //       .WithOptional(e => e.Countries)
//            //       .HasForeignKey(e => e.CountryId);

//            //   modelBuilder.Entity<Countries>()
//            //       .HasMany(e => e.SupplierStores)
//            //       .WithRequired(e => e.Countries)
//            //       .HasForeignKey(e => e.CountryId)
//            //       .WillCascadeOnDelete(false);

//            ///////////////////////////////////////////////////////////////////////////////////


//            ////////////////////////////////////////////////////////////////////////////
//            modelBuilder.Entity<DataAccess>()
//                .Property(e => e.DaQuery)
//                .IsUnicode(false);

//            modelBuilder.Entity<DataAccess>()
//                .Property(e => e.DaExecute)
//                .IsUnicode(false);

//            modelBuilder.Entity<DataAccess>()
//                .Property(e => e.DaDelete)
//                .IsUnicode(false);

//            modelBuilder.Entity<DataAccess>()
//                .Property(e => e.DaPrint)
//                .IsUnicode(false);
//            ///////////////////////////////////////////////////////////////////////////////////

//            modelBuilder.Entity<DataLog>()
//                .Property(e => e.DlTableName)
//                .IsUnicode(false);

//            modelBuilder.Entity<DataLog>()
//                .Property(e => e.DlColumnName)
//                .IsUnicode(false);

//            modelBuilder.Entity<DataLog>()
//                .Property(e => e.DlNewValue)
//                .IsUnicode(false);

//            modelBuilder.Entity<DataLog>()
//                .Property(e => e.DlOldValue)
//                .IsUnicode(false);

//            modelBuilder.Entity<DataLog>()
//                .Property(e => e.DlPageNo)
//                .IsFixedLength()
//                .IsUnicode(false);

//            modelBuilder.Entity<DataLog>()
//                .Property(e => e.DlTransType)
//                .IsFixedLength()
//                .IsUnicode(false);
//            ///////////////////////////////////////////////////////////////////////




//            /////////////////////////////////////////////////////////////////////
//            modelBuilder.Entity<ExceptionAccess>()
//                .Property(e => e.EaQuery)
//                .IsUnicode(false);

//            modelBuilder.Entity<ExceptionAccess>()
//                .Property(e => e.EaExecute)
//                .IsUnicode(false);

//            modelBuilder.Entity<ExceptionAccess>()
//                .Property(e => e.EaDelete)
//                .IsUnicode(false);

//            modelBuilder.Entity<ExceptionAccess>()
//                .Property(e => e.EaPrint)
//                .IsFixedLength()
//                .IsUnicode(false);
//            ///////////////////////////////////////////////////////////////////////



//            /////////////////////////////////////////////////////////////////////

            

//            modelBuilder.Entity<ModulePages>()
//                .Property(e => e.MpPageName)
//                .IsUnicode(false);

//            modelBuilder.Entity<ModulePages>()
//                .Property(e => e.MpPageUrl)
//                .IsUnicode(false);

//            modelBuilder.Entity<ModulePages>()
//                .HasMany(e => e.ExceptionAccess)
//                .WithRequired(e => e.ModulePages)
//                .HasForeignKey(e => e.EaPageNo)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<ModulePages>()
//                .HasMany(e => e.PageCloumns)
//                .WithOptional(e => e.ModulePages)
//                .HasForeignKey(e => e.PcPageNo);

//            modelBuilder.Entity<ModulePages>()
//                .HasMany(e => e.PositionAccess)
//                .WithRequired(e => e.ModulePages)
//                .HasForeignKey(e => e.PaPageNo)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Modules>()
//                .Property(e => e.MModuleName)
//                .IsUnicode(false);

//            modelBuilder.Entity<Modules>()
//                .Property(e => e.MModuleUrl)
//                .IsUnicode(false);

//            modelBuilder.Entity<Modules>()
//                .HasMany(e => e.ModulePages)
//                .WithOptional(e => e.Modules)
//                .HasForeignKey(e => e.MpModuleNo);



//            modelBuilder.Entity<PageCloumns>()
//            .Property(e => e.PcColumnLable)
//            .IsUnicode(false);

//            modelBuilder.Entity<PageCloumns>()
//                .Property(e => e.PcTableName)
//                .IsUnicode(false);

//            modelBuilder.Entity<PageCloumns>()
//                .Property(e => e.PcColumnName)
//                .IsUnicode(false);

//            modelBuilder.Entity<PositionAccess>()
//         .Property(e => e.PaQuery)
//         .IsUnicode(false);

//            modelBuilder.Entity<PositionAccess>()
//                .Property(e => e.PaExecute)
//                .IsUnicode(false);

//            modelBuilder.Entity<PositionAccess>()
//                .Property(e => e.PaDelete)
//                .IsUnicode(false);

//            modelBuilder.Entity<PositionAccess>()
//                .Property(e => e.PaPrint)
//                .IsUnicode(false);

//            modelBuilder.Entity<Positions>()
//                .Property(e => e.PPositionName)
//                .IsUnicode(false);

//            modelBuilder.Entity<Positions>()
//                .HasMany(e => e.PositionAccess)
//                .WithRequired(e => e.Positions)
//                .HasForeignKey(e => e.Id)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Positions>()
//                .HasMany(e => e.Users)
//                .WithOptional(e => e.Positions)
//                .HasForeignKey(e => e.UPositionNo);
//            //////////////////////////////////////////////////////////////////////
//            //  modelBuilder.Entity<Departments>()
//            //   .Property(e => e.DDepartmentName)
//            //   .IsUnicode(false);

//            //  modelBuilder.Entity<Departments>()
//            //      .HasMany(e => e.Users)
//            //      .WithOptional(e => e.Departments)
//            //      .HasForeignKey(e => e.DepartmentId);


//            //  modelBuilder.Entity<OrderLineTrans>()
//            //      .Property(e => e.OltStatus)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<OrderDetails>()
//            //      .Property(e => e.OrderStatus)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<OrderDetails>()
//            //      .HasMany(e => e.InventoryTrans)
//            //      .WithRequired(e => e.OrderDetails)
//            //      .HasForeignKey(e => e.ItOrderLineNo)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<OrderDetails>()
//            //      .HasMany(e => e.OrderLineTrans)
//            //      .WithRequired(e => e.OrderDetails)
//            //      .HasForeignKey(e => e.OltOrderLineNo)
//            //      .WillCascadeOnDelete(false);



//            //  modelBuilder.Entity<InventoryTrans>()
//            // .Property(e => e.ItTransType)
//            // .IsUnicode(false);



//            //  modelBuilder.Entity<ProductAttributes>()
//            //      .Property(e => e.PaAttValue)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<ProductBarcodes>()
//            //      .Property(e => e.Barcode)
//            //      .IsUnicode(false);




//            //  modelBuilder.Entity<Products>()
//            //      .Property(e => e.Name)
//            //      .IsRequired()
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Products>()
//            //      .HasMany(e => e.InventoryTrans)
//            //      .WithOptional(e => e.Products)
//            //      .HasForeignKey(e => e.ItProdId);

//            //  modelBuilder.Entity<KitProducts>()
//            //     .HasMany(e => e.Productses)
//            //.WithOptional(e => e.KitProducts)
//            //.HasForeignKey(e => e.KitProductsId)
//            //.WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //      .HasMany(e => e.OrderDetails)
//            //      .WithOptional(e => e.Products)
//            //      .HasForeignKey(e => e.ProductId);

//            //  modelBuilder.Entity<Products>()
//            //      .HasMany(e => e.ProductAttributes)
//            //      .WithRequired(e => e.Products)
//            //      .HasForeignKey(e => e.ProductId);

//            //  modelBuilder.Entity<Products>()
//            //      .HasMany(e => e.ProductBarcodes)
//            //      .WithRequired(e => e.Products)
//            //      .HasForeignKey(e => e.ProductId)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //     .HasMany(e => e.ProductVote)
//            //     .WithRequired(e => e.Products)
//            //     .HasForeignKey(e => e.ProductId)
//            //     .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //              .HasMany(e => e.DiscountRules)
//            //              .WithRequired(e => e.Products)
//            //              .HasForeignKey(e => e.ProductId)
//            //              .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //      .HasMany(e => e.ProductOriginalPrices)
//            //      .WithRequired(e => e.Products)
//            //      .HasForeignKey(e => e.ProductId)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //    .HasMany(e => e.SupplyOrderDetailses)
//            //    .WithRequired(e => e.Products)
//            //    .HasForeignKey(e => e.ProductId)
//            //    .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //    .HasMany(e => e.ProductImages)
//            //    .WithRequired(e => e.Products)
//            //    .HasForeignKey(e => e.ProductId)
//            //    .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //      .HasMany(e => e.ProductPromotions)
//            //      .WithRequired(e => e.Products)
//            //      .HasForeignKey(e => e.ProductId)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Products>()
//            //      .HasRequired(e => e.Categories)
//            //      .WithMany(prod => prod.Productses)
//            //      .WillCascadeOnDelete(false);



//            //  modelBuilder.Entity<SupplyOrderDetails>()
//            //      .HasMany(e => e.InventoryTrans)
//            //      .WithRequired(e => e.SupplyOrderDetails)
//            //      .HasForeignKey(e => e.ItPoLineNo)
//            //   .WillCascadeOnDelete(false);



//            //  modelBuilder.Entity<SupplyOrders>()
//            //      .HasMany(e => e.SupplyOrderDetails)
//            //      .WithRequired(e => e.SupplyOrders)
//            //      .HasForeignKey(e => e.SupplyOrderId)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<SupplyOrders>()
//            //      .HasMany(e => e.SupplyOrderPayments)
//            //      .WithRequired(e => e.SupplyOrders)
//            //      .HasForeignKey(e => e.SupplyOrderId)
//            //        .WillCascadeOnDelete(false);







//            //  modelBuilder.Entity<Order>()
//            //      .Property(e => e.OrderStatus)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Order>()
//            //       .HasMany(e => e.OrderDetails)
//            //      .WithRequired(e => e.Order)
//            //      .HasForeignKey(e => e.OrderId)
//            //   .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Order>()
//            //      .HasMany(e => e.OrderPayments)
//            //      .WithRequired(e => e.Order)
//            //      .HasForeignKey(e => e.OrderId)
//            //    .WillCascadeOnDelete(false);



//            //  modelBuilder.Entity<Categories>()
//            //      .Property(e => e.Id).HasColumnName("CategoryId");


//            //  modelBuilder.Entity<Categories>()
//            //      .Property(e => e.CategoryName)
//            //      .IsRequired()
//            //      .HasMaxLength(100)
//            //      .HasColumnAnnotation("Index",
//            //          new IndexAnnotation(new IndexAttribute("AK_Category_CategoryName") { IsUnique = true }));

//            //  modelBuilder.Entity<Categories>()
//            //      .HasOptional(c => c.Parent)
//            //      .WithMany(c => c.Children)
//            //      .HasForeignKey(c => c.ParentCategoryId)
//            //      .WillCascadeOnDelete(false);




//            //  modelBuilder.Entity<SupplierStores>()
//            //      .Property(e => e.StoreName)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<SupplierStores>()
//            //      .Property(e => e.SsLang)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<SupplierStores>()
//            //      .Property(e => e.SsLong)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<SupplierStores>()
//            //      .HasMany(e => e.InventoryTrans)
//            //      .WithOptional(e => e.SupplierStores)
//            //      .HasForeignKey(e => e.ItStoreNo);

//            //  modelBuilder.Entity<Suppliers>()
//            //      .Property(e => e.SupplierName)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Suppliers>()
//            //      .HasMany(e => e.SupplyOrderPayments)
//            //      .WithRequired(e => e.Suppliers)
//            //      .HasForeignKey(e => e.SupplierId)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Suppliers>()
//            //      .HasMany(e => e.SupplyOrders)
//            //      .WithRequired(e => e.Suppliers)
//            //      .HasForeignKey(e => e.SupplierId)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Suppliers>()
//            //      .HasMany(e => e.SupplierStores)
//            //      .WithRequired(e => e.Suppliers)
//            //      .HasForeignKey(e => e.SupplierId)
//            //      .WillCascadeOnDelete(false);

//            //  modelBuilder.Entity<Suppliers>()
//            //   .HasMany(e => e.InventoryTranses)
//            //   .WithRequired(e => e.Suppliers)
//            //   .HasForeignKey(e => e.ItSupplierNo)
//            //   .WillCascadeOnDelete(false);


//            //  //////////////////////////////////////////////////////////////////////////////////////////////////
//            //  ////  scaffolding scaffolding   scaffolding scaffolding   scaffolding scaffolding   scaffolding
//            //  ////  scaffolding   scaffolding scaffolding  scaffolding scaffolding   scaffolding scaffolding


//            //  modelBuilder.Entity<Users>()
//            //      .Property(e => e.UUserName)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Users>()
//            //      .Property(e => e.UActive)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Users>()
//            //      .Property(e => e.UEmail)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Users>()
//            //      .Property(e => e.UMobile)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Users>()
//            //      .Property(e => e.UId)
//            //      .IsUnicode(false);



//            //  modelBuilder.Entity<Users>()
//            //      .Property(e => e.UPassword)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            // .HasKey(e => e.CCustNo);
//            //  modelBuilder.Entity<Customers>()
//            // .Property(c => c.CCustNo)
//            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            //  //for make the id forgin key

//            //  modelBuilder.Entity<Customers>()
//            // .HasKey(e => e.Id);

//            //  //Configure userId as FK for customers

//            //  modelBuilder.Entity<ApplicationUser>()
//            //   .HasRequired(s => s.Customers)
//            //   .WithRequiredPrincipal(ad => ad.users).WillCascadeOnDelete(false);

//            //  //  modelBuilder.Entity<Users>()
//            //  //.HasKey(e => e.Id);

//            //  // Configure userId as FK for customers
//            //  modelBuilder.Entity<ApplicationUser>()
//            //                  .HasRequired(s => s.Users)
//            //                  .WithRequiredPrincipal(ad => ad.users).WillCascadeOnDelete(false);

//            //  // Configure userId as FK for customers
//            //  modelBuilder.Entity<ApplicationUser>()
//            //      .Ignore(s => s.RolesList);


//            //  // Configure userId as FK for ProductVote
//            //  modelBuilder.Entity<ApplicationUser>()
//            //   .HasOptional(s => s.ProductVote)
//            //   .WithOptionalPrincipal(ad => ad.Users).WillCascadeOnDelete(false);
//            //  ///////////////////////////////////////////////

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CFirstName)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CLastName)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CEmail)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CMobile)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CHomePhone)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CWorkPhone)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CAddress)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CLang)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CLong)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CCustStatus)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CCustClass)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .Property(e => e.CPassword)
//            //      .IsUnicode(false);

//            //  modelBuilder.Entity<Customers>()
//            //      .HasMany(e => e.OrderPayments)
//            //      .WithOptional(e => e.Customers)
//            //      .HasForeignKey(e => e.CustomerId);

//            //  modelBuilder.Entity<Customers>()
//            //      .HasMany(e => e.Order)
//            //      .WithOptional(e => e.Customers)
//            //      .HasForeignKey(e => e.CustomerId);


//            base.OnModelCreating(modelBuilder);

//        }


//        /// <summary>
//        /// void to set creationdate and updatedate as datetime.now
//        /// </summary>
//        private void ApplayRole()
//        {
//            foreach (var entry in this.ChangeTracker.Entries()
//                .Where(e => e.Entity is IAuditInfo &&
//                (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
//            {
//                IAuditInfo e = (IAuditInfo)entry.Entity;
//                if (entry.State == EntityState.Added)
//                {
//                    e.CreationDate = DateTime.Now;
//                }
//                e.LastUpdateDate = DateTime.Now;
//            }
//        }
//        private void Applaycreatupdat()
//        {
//            foreach (var entry in this.ChangeTracker.Entries()
//              .Where(e => e.Entity is IAuditInfo &&
//                (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
//            {
//                IAuditInfo e = (IAuditInfo)entry.Entity;
//                if (entry.State == EntityState.Added)
//                {
//                    if (e.CreatedBy == null && HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated)
//                    {
//                        e.CreatedBy = HttpContext.Current.User.Identity.GetUserName();
//                    }
//                    else
//                    {
//                        e.CreatedBy = "system";
//                    }

//                }
//                bool val1 = HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated;

//                if (val1 == false && e.LastUpdatedBy == null)
//                {
//                    e.LastUpdatedBy = "not regesterd";
//                }
//                else if (e.LastUpdatedBy == null)
//                {

//                    e.LastUpdatedBy = HttpContext.Current.User.Identity.GetUserName();
//                }


//            }
//        }
//        public override int SaveChanges()
//        {
//            //method for set the creationdate and update date
//            this.ApplayRole();
//            this.Applaycreatupdat();
//            return base.SaveChanges();
//        }

//        public override Task<int> SaveChangesAsync()
//        {
//            //method for set the creationdate and update date
//            this.ApplayRole();
//            this.Applaycreatupdat();
//            return base.SaveChangesAsync();
//        }

//        public static StoreDbContext Create()
//        {
//            return new StoreDbContext();
//        }

//        public System.Data.Entity.DbSet<ProductVote> ProductVotes { get; set; }
//    }
//}

namespace Elmatger.persistence
{
}