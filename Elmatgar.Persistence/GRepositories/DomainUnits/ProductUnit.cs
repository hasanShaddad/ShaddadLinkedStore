using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public class ProductUnit : IDisposable, IProductUnit
    {
        private IDataRepository<Core.Models.ProductPromotions> _productPromotions = null;
        private IDataRepository<Core.Models.Products> Product = null;
        private IDataRepository<Core.Models.Categories> _category = null;
        private IDataRepository<Core.Models.KitProducts> _kitproduct = null;
        private IDataRepository<Core.Models.Brands> _brands = null;
        private IDataRepository<Core.Models.ProductAttributes> _ProductAttributes = null;

        private IDataRepository<Core.Models.DiscountRules> _discountRules = null;
        private IDataRepository<Core.Models.ProductOriginalPrices> _productOriginalPrice = null;

        private IDataRepository<Core.Models.ProductBarcodes> _productBarCode = null;

        private readonly StoreDbContext _context = new StoreDbContext();

         

        public IDataRepository<Core.Models.Products> Products
        {
            get
            {
                if (this.Product == null)
                {
                    this.Product = new DataRepository<Core.Models.Products>(_context);
                }
                return this.Product;
            }
        }

        public IDataRepository<Core.Models.Categories> CategoryDataRepository
        {


            get
            {
                if (this._category == null)
                {
                    this._category = new DataRepository<Core.Models.Categories>(_context);
                }
                return this._category;
            }
        }

        public IDataRepository<Core.Models.Brands> BrandDataRepository
        {

            get
            {
                if (this._brands == null)
                {
                    this._brands = new DataRepository<Core.Models.Brands>(_context);
                }
                return this._brands;
            }
        }
        public IDataRepository<Core.Models.ProductAttributes> ProductAttributesRepository
        {

            get
            {
                if (this._ProductAttributes == null)
                {
                    this._ProductAttributes = new DataRepository<Core.Models.ProductAttributes>(_context);
                }
                return this._ProductAttributes;
            }
        }

        public IDataRepository<KitProducts> KitDataRepository
        {
            get
            {
                if (this._kitproduct == null)
                {
                    this._kitproduct = new DataRepository<Core.Models.KitProducts>(_context);
                }
                return this._kitproduct;
            }
        }

        public IDataRepository<Core.Models.ProductOriginalPrices> ProductOriginalPricesDataRepository
        {
            get
            {
                if (this._productOriginalPrice == null)
                {
                    this._productOriginalPrice = new DataRepository<Core.Models.ProductOriginalPrices>(_context);
                }
                return this._productOriginalPrice;
            }
        }

        public IDataRepository<Core.Models.DiscountRules> discountRulessDataRepository
        {
            get
            {
                if (this._discountRules == null)
                {
                    this._discountRules = new DataRepository<Core.Models.DiscountRules>(_context);
                }
                return this._discountRules;
            }
        }

        public IDataRepository<Core.Models.ProductPromotions> ProductPromotions
        {
            get
            {
                if (this._productPromotions == null)
                {
                    this._productPromotions = new DataRepository<Core.Models.ProductPromotions>(_context);
                }
                return this._productPromotions;
            }
        }

        public IDataRepository<ProductBarcodes> productBarCodeDataRepository
        {
            get
            {
                if (this._productBarCode  == null)
                {
                    this._productBarCode   = new DataRepository<Core.Models.ProductBarcodes >(_context);
                }
                return this._productBarCode ;
            }
        }

        public void AddProducts(Core.Models.Products entity)
        {
            Products.Add(entity);
        }

        public void UpdateProducts(Core.Models.Products entity)
        {
            Products.Update(entity);
        }

        public void DeleteProducts(int id)
        {
           Products.Delete(id);
        }

        public Task SaveChangesAsync()
        {
          return  _context.SaveChangesAsync();
        }

        public Task<Core.Models.Products> FindAsync(int? id)
        {
            return Products.GetByIdAsync(id);
        }

        /// <summary>
        /// save all changes
        /// </summary>

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<Core.Models.Products> GetAllProducts()
        {
            return Products.GetAll();
        }

        public IQueryable<Core.Models.ProductPromotions> GetAllProductpromotions()
        {
            return ProductPromotions.GetAll();
        }


        public SupplyOrderDetails GetSupplyOrderDetails(int id)
        {
            return _context.SupplyOrderDetails.FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<KitProducts> GetAllKitProducts()
        {
            return KitDataRepository.GetAll();
        }

        public IQueryable<Core.Models.Brands> GetAllBrands()
        {
            return BrandDataRepository.GetAll();
        }
        /// <summary>
        /// get all peoductAttributes with the same db context for products -hassan shaddad
        /// </summary>
        /// <returns>IQueryable for all ProductAttributes</returns>
        public IQueryable<Core.Models.ProductAttributes> GetAllProductAttributes()
        {
            return ProductAttributesRepository.GetAll();
        }

        /// <summary>
        /// dispose all
        /// </summary>
        public void Dispose()
        {
            this._context?.Dispose();
        }

        public IQueryable<Core.Models.Categories> GetAllCategories()
        {
            return  CategoryDataRepository.GetAll();
        }

        public IQueryable<Elmatgar.Core.Models.ProductImages> GetImage(int id)
        {
           return  _context.ProductImages.Where(p => p.ProductId == id);
           
        }

      

        public void DeleteRelatedProduct(int id)
        {
            var price = ProductOriginalPricesDataRepository.Get(e => e.ProductId == id);

            if (price != null)
            {
                ProductOriginalPricesDataRepository.Delete(price);
            }

            //delete discount role for this product
            var discount = discountRulessDataRepository.Get(d=>d.ProductId== id);
            //await _db.DiscountRules.FirstOrDefaultAsync(e => e.ProductId == id);
            if (discount != null)
            {
                discountRulessDataRepository.Delete(discount);
            }



            var barCode = productBarCodeDataRepository.Get(e=> e.ProductId==id );
            // await _db.ProductBarcodes.FirstOrDefaultAsync(e => e.ProductId == id);
            if (barCode != null)
            {
                productBarCodeDataRepository.Delete(barCode);
            }




        }
    }

}

