using System;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;

namespace Elmatgar.persistence.GRepositories.DomainUnits
{
    public class CategoriesUnit:IDisposable, ICategoriesUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Elmatgar.Core.Models.Categories> _categories = null;

        /// <summary>
        /// Departments unit
        /// </summary>
        public IDataRepository<Elmatgar.Core.Models.Categories> Category
        {
            get
            {
                if (this._categories == null)

                {
                    this._categories = new DataRepository<Elmatgar.Core.Models.Categories>(this._context);
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

        public Task<Core.Models.Categories> FindAsync(int? id)
        {
            return Category.GetByIdAsync(id);
        }

        public void AddCategory(Core.Models.Categories entity)
        {
            Category.Add(entity);
        }

        public void UpdateCategory(Core.Models.Categories entity)
        {
           Category.Update(entity);
        }

        public void DeleteCategory(int id)
        {
            
            Category.Delete(id);
        }
    }
}