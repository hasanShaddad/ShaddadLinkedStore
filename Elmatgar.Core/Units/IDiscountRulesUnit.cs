using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;

namespace Elmatgar.Core.Units
{
   public interface IDiscountRulesUnit
    {

        IDataRepository<DiscountRules> DiscountRepository { get; }
        IDataRepository<Products> ProductRepository { get; }

        void AddDiscount(DiscountRules entity);

        void UpdateDiscount(DiscountRules entity);

        void DeleteDiscount(int id);

        Task SaveChangesAsync();
        Task<Core.Models.DiscountRules> FindAsync(int? id);
        /// <summary>
        /// save all changes
        /// </summary>
        void SaveChanges();
        IQueryable<DiscountRules> GetAllDiscount();
        IQueryable<Products> GetAllProducts();


        /// <summary>
        /// dispose all
        /// </summary>
        void Dispose();
    }
}
