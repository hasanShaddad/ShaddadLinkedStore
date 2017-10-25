using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
    public interface IUsersUnit
    {
        /// <summary>
        /// to register the new user as new customer
        /// </summary>
        IDataRepository<Customers> Customer { get; }

        /// <summary>
        /// regester the new user as an employee
        /// </summary>
        IDataRepository<Users> Users { get; }

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