﻿using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories
{
    /// <summary>
    /// Dtata Repository for add update delete for any class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataRepository<T> : IDataRepository<T> where T : class, IActiveFlag
    {

        //DbSet and DbContext properties is neded
        protected DbSet<T> DBset { get; set; }
        protected DbContext Context { get; set; }


        /// <summary>
        ///  conestruction take DbContext and chick it
        /// </summary>
        /// <param name="dBcontext"></param>
        public DataRepository(DbContext dBcontext)
        {
            if (dBcontext == null)
            {
                throw new ArgumentException("an instance of DBContext is " + "Required to use this repository . " +
                                            "dBcontext");
            }
            this.Context = dBcontext;
            this.DBset = this.Context.Set<T>();
        }





        /// <summary>
        /// chick if the entity is not atached then the entry stete well set to added
        /// else the entity well be add
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            this.DBset.Add(entity);
        }

        /// <summary>
        /// this for delete a record
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int? id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        /// <summary>
        /// this for delete all 
        /// check if the entity state is not deleted then delete it
        /// if the entity state is  deleted then set its state as deleted 
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DBset.Attach(entity);
                this.DBset.Remove(entity);
            }
        }

        /// <summary>
        /// set the state to Deatached
        /// </summary>
        /// <param name="entity"></param>
        public void Deatach(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            entry.State = EntityState.Detached;
        }





        /// <summary>
        /// to select all
        /// </summary>
        /// <returns>context of class</returns>
        public IQueryable<T> GetAll()
        {
            return this.DBset;
        }


        public T Get(Expression<Func<T, bool>> where)
        {
            return DBset.Where(where).FirstOrDefault<T>();
        }




        /// <summary>
        /// to select all to list
        /// </summary>
        /// <returns>context of class</returns>
        public List<T> GetToList()
        {
            if (this.Context.Database.Connection.State == System.Data.ConnectionState.Open)
            {
                throw new ArgumentException("conniction is open so you can not  use tolist func");
            }
                return this.DBset.ToList();
             
          
        }

        /// <summary>
        /// get item from class by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>context of item from class</returns>
        public T GetById(int? id)
        {

            return this.DBset.Find(id);
        }

        /// <summary>
        /// get item from class by id synchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns>context of item from class</returns>
        public async Task<T> GetByIdAsync(int? id)
        {

            return await this.DBset.FindAsync(id);
        }


        /// <summary>
        /// chick if entry state is deatached then atach it
        /// if entry state is not detached then set entry state as modified
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DBset.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }

        public string AddWhenNew(Expression<Func<T, bool>> identifierExpression, T item)
        {
            var error = string.Empty;

            var countNo = this.DBset.Count(identifierExpression);
            if (countNo > 0)
            {
                error = $"{item.GetType().Name} '{"name you entered"}' already exists";
            }
            return error;
        }
        public IQueryable<T> GetWhereFlag(bool activeFlag)
        {
            IQueryable<T> query = this.DBset.Where(e => e.ActiveFlag == true);

            return query;
        }

    }
}