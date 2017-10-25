using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
    public interface IDepartmentsUnit
    {
        /// <summary>
        /// Departments unit
        /// </summary>
        IDataRepository<Departments> Department { get; }

        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();
    }
}