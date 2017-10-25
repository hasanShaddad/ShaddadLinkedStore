using Elmatgar.Core.Models;
using Elmatgar.persistence.Configurations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Elmatgar.persistence
{
    public class StoreDbContext : IdentityDbContext<ApplicationUser>
    {

        #region  Constructor



        public StoreDbContext() : base("StoreDbContext")
        {

            System.Data.Entity.Database.SetInitializer<StoreDbContext>(new CreateDatabaseIfNotExists<StoreDbContext>());

        }
        #endregion



        #region ModelCreating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Configurations.Add(new CustomerPaymentMethodConfiguration());
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new BrandsConfiguration());
            modelBuilder.Configurations.Add(new CategoriesConfiguration());
            modelBuilder.Configurations.Add(new CitiesConfiguration());
            modelBuilder.Configurations.Add(new CountriesConfiguration());
            modelBuilder.Configurations.Add(new CustomersConfiguration());
            modelBuilder.Configurations.Add(new DepartmentsConfiguration());
            modelBuilder.Configurations.Add(new DiscountRulesConfiguration());
            modelBuilder.Configurations.Add(new InventoryTransConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new ProductBarcodesConfiguration());
            modelBuilder.Configurations.Add(new ProductsConfiguration());
            modelBuilder.Configurations.Add(new KitProductsConfiguration());
            modelBuilder.Configurations.Add(new SupplyOrderConfiguration());
            modelBuilder.Configurations.Add(new SupplyOrderDetailsConfiguration());
            modelBuilder.Configurations.Add(new SuppliersConfiguration());
            modelBuilder.Configurations.Add(new SupplierStoresConfiguration());
            modelBuilder.Configurations.Add(new UsersConfiguration());
            modelBuilder.Configurations.Add(new ZonesConfiguration());
            modelBuilder.Configurations.Add(new ReturnsConfiguration());
            modelBuilder.Configurations.Add(new AttributeConfiguration());

            //////
            modelBuilder.Entity<DataAccess>()
                .Property(e => e.DaQuery)
                .IsUnicode(false);

            modelBuilder.Entity<DataAccess>()
                .Property(e => e.DaExecute)
                .IsUnicode(false);

            modelBuilder.Entity<DataAccess>()
                .Property(e => e.DaDelete)
                .IsUnicode(false);

            modelBuilder.Entity<DataAccess>()
                .Property(e => e.DaPrint)
                .IsUnicode(false);
            ///////////////////////////////////////////////////////////////////////////////////

            modelBuilder.Entity<DataLog>()
                .Property(e => e.DlTableName)
                .IsUnicode(false);

            modelBuilder.Entity<DataLog>()
                .Property(e => e.DlColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<DataLog>()
                .Property(e => e.DlNewValue)
                .IsUnicode(false);

            modelBuilder.Entity<DataLog>()
                .Property(e => e.DlOldValue)
                .IsUnicode(false);

            modelBuilder.Entity<DataLog>()
                .Property(e => e.DlPageNo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DataLog>()
                .Property(e => e.DlTransType)
                .IsFixedLength()
                .IsUnicode(false);
            ///////////////////////////////////////////////////////////////////////




            /////////////////////////////////////////////////////////////////////
            modelBuilder.Entity<ExceptionAccess>()
                .Property(e => e.EaQuery)
                .IsUnicode(false);

            modelBuilder.Entity<ExceptionAccess>()
                .Property(e => e.EaExecute)
                .IsUnicode(false);

            modelBuilder.Entity<ExceptionAccess>()
                .Property(e => e.EaDelete)
                .IsUnicode(false);

            modelBuilder.Entity<ExceptionAccess>()
                .Property(e => e.EaPrint)
                .IsFixedLength()
                .IsUnicode(false);
            ///////////////////////////////////////////////////////////////////////



            /////////////////////////////////////////////////////////////////////

            //modelBuilder.Entity<LangText>()
            //    .Property(e => e.LtText)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Languages>()
            //    .Property(e => e.LLanguage)
            //    .IsUnicode(false);

            //modelBuilder.Entity<Languages>()
            //    .HasMany(e => e.LangText)
            //    .WithRequired(e => e.Languages)
            //    .HasForeignKey(e => e.LtLangId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModulePages>()
                .Property(e => e.MpPageName)
                .IsUnicode(false);

            modelBuilder.Entity<ModulePages>()
                .Property(e => e.MpPageUrl)
                .IsUnicode(false);

            modelBuilder.Entity<ModulePages>()
                .HasMany(e => e.ExceptionAccess)
                .WithRequired(e => e.ModulePages)
                .HasForeignKey(e => e.EaPageNo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ModulePages>()
                .HasMany(e => e.PageCloumns)
                .WithOptional(e => e.ModulePages)
                .HasForeignKey(e => e.PcPageNo);

            modelBuilder.Entity<ModulePages>()
                .HasMany(e => e.PositionAccess)
                .WithRequired(e => e.ModulePages)
                .HasForeignKey(e => e.PaPageNo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Modules>()
                .Property(e => e.MModuleName)
                .IsUnicode(false);

            modelBuilder.Entity<Modules>()
                .Property(e => e.MModuleUrl)
                .IsUnicode(false);

            modelBuilder.Entity<Modules>()
                .HasMany(e => e.ModulePages)
                .WithOptional(e => e.Modules)
                .HasForeignKey(e => e.MpModuleNo);



            modelBuilder.Entity<PageCloumns>()
                .Property(e => e.PcColumnLable)
                .IsUnicode(false);

            modelBuilder.Entity<PageCloumns>()
                .Property(e => e.PcTableName)
                .IsUnicode(false);

            modelBuilder.Entity<PageCloumns>()
                .Property(e => e.PcColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<PositionAccess>()
                .Property(e => e.PaQuery)
                .IsUnicode(false);

            modelBuilder.Entity<PositionAccess>()
                .Property(e => e.PaExecute)
                .IsUnicode(false);

            modelBuilder.Entity<PositionAccess>()
                .Property(e => e.PaDelete)
                .IsUnicode(false);

            modelBuilder.Entity<PositionAccess>()
                .Property(e => e.PaPrint)
                .IsUnicode(false);

            modelBuilder.Entity<Positions>()
                .Property(e => e.PPositionName)
                .IsUnicode(false);

            modelBuilder.Entity<Positions>()
                .HasMany(e => e.PositionAccess)
                .WithRequired(e => e.Positions)
                .HasForeignKey(e => e.Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Positions>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Positions)
                .HasForeignKey(e => e.UPositionNo);



        }

        #endregion



        #region Propperties

        public DbSet<Zones> Zones { get; set; }
        public DbSet<AttributeHeaders> AttributeHeaders { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<DataAccess> DataAccess { get; set; }
        public DbSet<DataLog> DataLog { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<DiscountRules> DiscountRules { get; set; }
        public DbSet<ExceptionAccess> ExceptionAccess { get; set; }
        public DbSet<InventoryTrans> InventoryTrans { get; set; }
        public DbSet<KitProducts> KitProducts { get; set; }
        //public DbSet<LangText> LangText { get; set; }
        // public DbSet<Languages> Languages { get; set; }
        public DbSet<ModulePages> ModulePages { get; set; }
        public DbSet<Modules> Modules { get; set; }

        public DbSet<OrderLineTrans> OrderLineTrans { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<PageCloumns> PageCloumns { get; set; }
        public DbSet<PositionAccess> PositionAccess { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<ProductAttributes> ProductAttributes { get; set; }
        public DbSet<ProductBarcodes> ProductBarcodes { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductOriginalPrices> ProductOriginalPrices { get; set; }
        public DbSet<ProductPromotions> ProductPromotions { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<SupplyOrderDetails> SupplyOrderDetails { get; set; }
        public DbSet<SupplyOrderPayments> SupplyOrderPayments { get; set; }
        public DbSet<SupplyOrders> SupplyOrder { get; set; }
        public DbSet<OrderPayments> OrderPayments { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SupplierStores> SupplierStores { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        //public DbSet<CartItems> CartItems { get; set; }



        public new DbSet<Users> Users { get; set; }
        public DbSet<ProductVote> ProductVotes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<CustomerPaymentMethod> CustomerPaymentMethods { get; set; }
        public DbSet<Returns> Return { get; set; }

        #endregion




        #region SaveChanges
        public virtual void Commit()
        {
            ApplayRole();
            Applaycreatupdat();

            base.SaveChanges();
        }

        public override int SaveChanges()
        {
            //method for set the creationdate and update date
            this.ApplayRole();
            this.Applaycreatupdat();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            //method for set the creationdate and update date
            this.ApplayRole();
            this.Applaycreatupdat();
            return base.SaveChangesAsync();
        }

        private void ApplayRole()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditInfo &&
                (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                IAuditInfo e = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    e.CreationDate = DateTime.Now;
                }
                else
                {
                    e.LastUpdateDate = DateTime.Now;
                }
            }
        }



        private void Applaycreatupdat()
        {
            foreach (var entry in this.ChangeTracker.Entries()
              .Where(e => e.Entity is IAuditInfo &&
                (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                IAuditInfo e = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    if (e.CreatedBy == null && HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated)
                    {
                        e.CreatedBy = HttpContext.Current.User.Identity.GetUserName();
                    }
                    else
                    {
                        e.CreatedBy = "system";
                    }

                }
                bool val1 = HttpContext.Current != null && HttpContext.Current.Request.IsAuthenticated;

                if (val1 == false && e.LastUpdatedBy == null)
                {
                    e.LastUpdatedBy = "not regesterd";
                }
                else if (e.LastUpdatedBy == null)
                {

                    e.LastUpdatedBy = HttpContext.Current.User.Identity.GetUserName();
                }


            }
        }


        #endregion


        public static StoreDbContext Create()
        {
            return new StoreDbContext();
        }
        // audited by eman herawy
    }
}
