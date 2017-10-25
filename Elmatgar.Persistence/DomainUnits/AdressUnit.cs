using System;
using System.Data.Entity;
using System.Linq;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Units;
using Elmatger.persistence.Repositories;

namespace Elmatger.persistence.DomainUnits
{
    /// <summary>
    /// the main class to handel the adresses like cities areas countres
    /// </summary>
    /// 
   public class AdressUnit:IDisposable,  IAdressUnit
    {
        private StoreDbContext _context = new StoreDbContext();
        private IDataRepository<Countries> _countries = null;
        private IDataRepository<Cities> _cities = null;
        private IDataRepository<Zones> _areas = null;
      


        /// <summary>
        /// handel the Countries with singleToon patern
        /// </summary>
        public IDataRepository<Countries> Country
        {
            get
            {
                if (this._countries==null)
                {
                   this._countries = new DataRepository<Countries>(this._context);
                    
                }
                return this._countries;
            }
        }
        /// <summary>
        /// handel the cities  with singleToon patern
        /// </summary>
        public IDataRepository<Cities> City
        {
            get
            {
                if (this._cities == null)
                {
                    this._cities = new DataRepository<Cities>(this._context);

                }
                return this._cities;
            }
        }
        /// <summary>
        /// handel the areas  with singleToon patern
        /// </summary>
        public IDataRepository<Zones> Area 
        {
            get
            {
                if (this._areas == null)
                {
                    this._areas = new DataRepository<Zones>(this._context);

                }
                return this._areas;
            }
        }


        public IQueryable<Cities> CitiesesJoincountries
        {
            get
            {
                if (this._cities == null)
                {
                    this._cities = new DataRepository<Cities>(this._context);

                }
                return this._context.Cities.Include(c => c.Countries);
            }
        }

        public IQueryable<Zones> ZonesJoinCitieses
        {
            get
            {
                if (this._areas == null)
                {
                    this._areas = new DataRepository<Zones>(this._context);

                }
                return this._context.Zones.Include(c => c.Cities);
            }
        }
        /// <summary>
        /// save all changes at once
        /// </summary>
        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            this._context?.Dispose();
        }
    }
}
