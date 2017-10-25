using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
   public  interface IBrandsUnit
    {

        IDataRepository<Brands> BrandsRepository { get; }

        void AddBrand(Brands entity);

        void UpdateBrand(Brands entity);

        void DeleteBrand(int id);

        Task SaveChangesAsync();
        Task<Core.Models.Brands> FindAsync(int? id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
      IQueryable<Brands>  GetAllBrands();
        

        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();
    }
}
