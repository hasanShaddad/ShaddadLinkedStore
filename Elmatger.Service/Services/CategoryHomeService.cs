using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Repositories;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.persistence;
using TreeHelper = Elmatgar.Core.Models.TreeHelper;

namespace Elmatgar.Service.Services
{



    public class CategoryHomeService : ICategoryHomeService
    {
        private readonly StoreDbContext _StoreDbContext = new StoreDbContext();

        private List<Categories> GetListOfNode(int? categId)
        {
            var sorceCategorieses = _StoreDbContext.Categories.ToList();
            if (categId > 0)
            {
                sorceCategorieses = _StoreDbContext.Categories.Where(m => m.ParentId == categId).ToList();
            }
            var AllCategorieses = _StoreDbContext.Categories;
            List<Categories> categorieses = new List<Categories>();
            foreach (Categories sorceCategoey in sorceCategorieses)
            {
                Categories c = new Categories();
                c.Children = new List<Categories>();
                c.Id = sorceCategoey.Id;
                c.CategoryName = sorceCategoey.CategoryName;
                c.SLCategoryName = sorceCategoey.SLCategoryName;
                c.CategoryTitle = sorceCategoey.CategoryTitle;
                c.CategoryDescription = sorceCategoey.CategoryDescription;

                if (sorceCategoey.ParentId != null)
                {

                    c.Parent = new Categories();
                    c.Parent.Id = (int) sorceCategoey.ParentId;
                    try
                    {

                       
                            c.Children.Add(sorceCategoey);
                       
                    
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                categorieses.Add(c);

            }
            return categorieses.ToList();
        }


        private List<Categories> GetLastNode()
        {
            List<Categories> finalCategorieses = new List<Categories>();
            List<Categories> categorieses = GetListOfNode(null);
            IList<Categories> topLevelCategorieses = TreeHelper.ConvertToForest(categorieses);
            foreach (Categories sorceCategoey in topLevelCategorieses)
            {
                if (sorceCategoey.Children.Count == 0)
                {
                    finalCategorieses.Add(sorceCategoey);
                }
                else
                {
                    GetValue(sorceCategoey, finalCategorieses);
                }
                

            }



            return finalCategorieses;
        }
        /// <summary>
        /// Get List Of Node For home Menu
        /// </summary>
        /// <param name="categId"></param>
        /// <returns></returns>
        public IList<Categories> GetListOfNodeForMenu(int? categId)
        {


            var sorceCategorieses = _StoreDbContext.Categories.ToList();
            if (categId > 0)
            {
                sorceCategorieses = _StoreDbContext.Categories.Where(m => m.ParentId == categId).ToList();
            }
            var allCategorieses = _StoreDbContext.Categories.ToList();
            List<Categories> categorieses = new List<Categories>();
            foreach (var sorceCategoey in sorceCategorieses)
            {
                var c = new Categories();
                c.Children = new List<Categories>();
                c.Id = sorceCategoey.Id;
                c.CategoryName = sorceCategoey.CategoryName;
                if (sorceCategoey.ParentId != null)
                {
                    c.Parent = new Categories();
                    c.Parent.Id = (int)sorceCategoey.ParentId;


                    foreach (var n in allCategorieses)
                    {
                        if (n.ParentId == c.Id)
                        {
                            c.Children.Add(n);
                        }
                    }

                }
                categorieses.Add(c);

            }
            return categorieses.ToList();

        }
        private static void GetValue(Categories sorceCategoey, List<Categories> finalCategorieses)
        {
            foreach (Categories childCategoey in sorceCategoey.Children.ToList())
            {
                if (childCategoey.Children.Count == 0)
                {
                    finalCategorieses.Add(childCategoey);
                }
                else
                {
                    GetValue(childCategoey, finalCategorieses);
                }
            }
        }

        // GET: CategMenue

        public IList<Categories> GetCategoriese(int? categId)
        {
            if (categId > 0)
            {
                IList<Categories> lestOfNoods = GetListOfNode(categId);

                return lestOfNoods.ToList();
            }
            else
            {
               IList<Categories> lestOfNoods =  GetListOfNode(null);
               IList<Categories> topLevelCategorieses = TreeHelper.ConvertToForest(lestOfNoods);
                return  topLevelCategorieses.ToList();
            }

        }


    

        public IList<Categories> GetTopCategOnly()
        {
            List<Categories> lestOfNoods = GetListOfNode(null);
            IList<Categories> topLevelCategorieses = TreeHelper.ConvertToForest(lestOfNoods);
            return  topLevelCategorieses.ToList();
        }
        public IList<Categories> GetLastCategOnly()
        {
            IList<Categories> lestOfNoods = GetLastNode();
          
            return lestOfNoods.ToList();
           
        }

        
    }
}
