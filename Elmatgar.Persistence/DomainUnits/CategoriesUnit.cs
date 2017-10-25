using System;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using Elmatger.persistence.Repositories;

namespace Elmatger.persistence.DomainUnits
{
    public class CategoriesUnit:IDisposable, ICategoriesUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Categories> _categories = null;

        /// <summary>
        /// Departments unit
        /// </summary>
        public IDataRepository<Categories> Category
        {
            get
            {
                if (this._categories == null)

                {
                    this._categories = new DataRepository<Categories>(this._context);
                }
                return this._categories;
            }

        }
 
        /// <summary>
        /// save all changes
        /// </summary>
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
        /// <summary>
        /// dispose all
        /// </summary>
        public void Dispose()
        {
            this._context?.Dispose();
        }
    }
}