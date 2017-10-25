using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
   public interface ICategoriesUnit
    {
        IDataRepository<Categories> Category { get; }

        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        Task<Core.Models.Categories> FindAsync(int? id);

        void AddCategory(Categories entity);
        void UpdateCategory(Categories entity);
        void DeleteCategory(int id);


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();
    }
}
