using Autofac;
using Autofac.Integration.Mvc;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.Mappings;
using Elmatgar.persistence.GRepositories.DomainUnits;
using Elmatgar.persistence.Infrastructure;
using Elmatgar.Service.Services;
using System.Reflection;
using System.Web.Mvc;


namespace Elmatgar
{
    public class Bootstrapper
    {

        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }


        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();


            builder.RegisterControllers(Assembly.GetExecutingAssembly());
              builder.RegisterType<CategoriesUnit>().As<ICategoriesUnit>().InstancePerRequest();

            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<AdressUnit>().As<IAdressUnit>().InstancePerRequest();
            builder.RegisterType<DepartmentsUnit>().As<IDepartmentsUnit>().InstancePerRequest();
            builder.RegisterType<CategoryService>().As<ICategoryServices>().InstancePerRequest();
            //Register ICategoryHomeService to CategoryHomeService to bring categories list to use in layout menus   
            //- add by hassan shaddad
            builder.RegisterType<CategoryHomeService>().As<ICategoryHomeService>().InstancePerRequest();
            builder.RegisterType<BrandsUnit>().As<IBrandsUnit>().InstancePerRequest();
            builder.RegisterType<SupplierUnit>().As<ISupplierUnit>().InstancePerRequest();
            builder.RegisterType<SupplierService>().As<ISupplierService>().InstancePerRequest();
            builder.RegisterType<ProductUnit>().As<IProductUnit>().InstancePerRequest();
            builder.RegisterType<ProductDetailsService>().As<IProductDetailsService>().InstancePerRequest();
            builder.RegisterType<OrdersUnit>().As<IOrdersUnit>().InstancePerRequest();
            builder.RegisterType<OrdersServices>().As<IOrdersServices>().InstancePerRequest();
            builder.RegisterType<InventoryServices>().As<IInventoryServices>().InstancePerRequest();
            builder.RegisterType<InventoryUnit>().As<IInventoryUnit>().InstancePerRequest();
            builder.RegisterType<UsersUnit>().As<IUsersUnit>().InstancePerRequest();
            builder.RegisterType<AttributeHeaderUnit>().As<IAttributeHeadersUnit>().InstancePerRequest();
            builder.RegisterType<ProductAttributeUnit>().As<IProductAttributeUnit>().InstancePerRequest();
            builder.RegisterType<DiscountRulesUnit>().As<IDiscountRulesUnit>().InstancePerRequest();
            builder.RegisterType<KitProductUnit>().As<IKitProductsUnit>().InstancePerRequest();
            builder.RegisterType<ProductBarCodeUnit>().As<IProductBarCodeUnit>().InstancePerRequest();
            builder.RegisterType<ProductImageUnit>().As<IProductImagesUnit>().InstancePerRequest();
            builder.RegisterType<ProductOriginalPricesUnit>().As<IProductOriginalPricesUnit>().InstancePerRequest();
            builder.RegisterType<ProductPromotionsUnit>().As<IProductPromotionsUnit>().InstancePerRequest();
            //Register IProductVotesUnit to ProductVotesUnit to add vote rating to product to use in products view 
            //and productDetails view - add by hassan shaddad
            builder.RegisterType<ProductVotesUnit>().As<IProductVoteUnit>().InstancePerRequest();
            //Register IProductsForHomeService to ProductsForHomeService to bring products list to use in sale pages 
            //like home/index page - add by hassan shaddad
            builder.RegisterType<ProductsForHomeService>().As<IProductsForHomeService>().InstancePerRequest();
            //// Repositories
            //builder.RegisterAssemblyTypes(typeof(CategoriesUnit).Assembly)
            //    .Where(t => t.Name.EndsWith("Unit"))
            //    .AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterAssemblyTypes(typeof(AdressUnit).Assembly)
            //   .Where(t => t.Name.EndsWith("Unit"))
            //   .AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterAssemblyTypes(typeof(DepartmentsUnit).Assembly)
            //   .Where(t => t.Name.EndsWith("Unit"))
            //   .AsImplementedInterfaces().InstancePerRequest();
            //commented by Hassan Shaddad
            // Services



            builder.RegisterAssemblyTypes(typeof(CategoryService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces().InstancePerRequest();



            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));




        }
    }
}
