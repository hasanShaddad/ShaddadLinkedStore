using System;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using Elmatger.persistence.Repositories;

namespace Elmatger.persistence.DomainUnits
{
    public class UsersUnit:IDisposable, IUsersUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Customers> _customers = null;
        private IDataRepository<Users> _users = null;
        /// <summary>
        /// to register the new user as new customer
        /// </summary>
        public IDataRepository<Customers> Customer 
        {
            get
            {
                if (this._customers == null)

                {
                    this._customers = new DataRepository<Customers>(this._context);
                }
                return this._customers;
            }

        }

        /// <summary>
        /// regester the new user as an employee
        /// </summary>
        public IDataRepository<Users> Users
        {
            get
            {
                if (this._users==null)
                {
                    this._users=new DataRepository<Users>(this._context);
                }
                return this._users;
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