using System;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using Elmatger.persistence.Repositories;

namespace Elmatger.persistence.DomainUnits
{
    public class DepartmentsUnit:IDisposable, IDepartmentsUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Departments> _departments = null;

        /// <summary>
        /// Departments unit
        /// </summary>
        public IDataRepository<Departments> Department
        {
            get
            {
                if (this._departments == null)

                {
                    this._departments = new DataRepository<Departments>(this._context);
                }
                return this._departments;
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