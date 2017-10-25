using Elmatgar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elmatgar.Core.Repositories
{
    public interface IDataRepository<T> where T : class, IActiveFlag
    {
        //select all as list
        List<T> GetToList();

        //select all
        IQueryable<T> GetAll();

        //select by id
        T GetById(int? id);
        /// <summary>
        /// get item from class by id synchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns>context of item from class</returns>
        Task<T> GetByIdAsync(int? id);
        IQueryable<T> GetWhereFlag(bool activeFlag);


        T Get(Expression<Func<T, bool>> where);

        //add to database
        void Add(T entity);
        //update 
        void Update(T entity);
        //Delete all
        void Delete(T entity);


        //Delete by id
        void Delete(int? id);
        //attach entity if it is deattached
        void Deatach(T entity);

        string AddWhenNew(Expression<Func<T, bool>> identifierExpression, T item);
    }
}
