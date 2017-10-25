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
  


    public class CategoryService : ICategoryServices
    {
        private readonly StoreDbContext _applicationDbContext = new StoreDbContext();
        
         public SelectList poulateParentCategorySelectList(int? id)
        {

            SelectList selectList;
            if (id == null)
                selectList = new SelectList(
                     _applicationDbContext
                    .Categories
                    .Where(c => c.Parent.Parent ==null&&c.Productses.Count==0), "Id", "CategoryName"
                    );
            else   /*(_StoreDbContext.Categories.Count(c=>c.ParentId==id)==0)*/
                selectList = new SelectList(
                    _applicationDbContext
                   .Categories
                   .Where(c => c.Id != id), "Id", "CategoryName"
                   );
            //else
            //    selectList = new SelectList(
            //      _StoreDbContext
            //      .Categories
            //      .Where(c => false), "Id", "CategoryName"
            //      );
            return selectList;
        }

        //check if category name is exest
       



        //get list of all nodes from categories table
        //todo:take this to outer modeol 
        /// <summary>
        /// get list of all nodes from categories
        /// </summary>
        /// <returns></returns>
        private List<Categories> GetListOfNode()
        {
          var sorceCategorieses = _applicationDbContext.Categories.ToList();
            List<Categories> categorieses = new List<Categories>();
            foreach (Categories sorceCategoey in sorceCategorieses)
            {
                Categories c = new Categories();
                c.Id = sorceCategoey.Id;
                c.CategoryName = sorceCategoey.CategoryName;
                if (sorceCategoey.ParentId!= null)
                {
                    c.Parent = new Categories();
                    c.Parent.Id = (int)sorceCategoey.ParentId;
                }
                categorieses.Add(c);

            }
            return categorieses;
        }
        //nterface for forist of nodes
        private string EnumerateBackAdminNodes(Categories parent)
        {
            //init an empty string
            string content = String.Empty;
            content += "<li class=\"treenode\">";
            content += parent.CategoryName;
            content += string.Format("<a href=\"/Admin/Categories/Edit/{0}\"class=\"btn btn-primary btn-xs treenodeeditbutton\">Edit</a>", parent.Id);
            content += string.Format("<a href=\"/Admin/Categories/Delete/{0}\"class=\"btn btn-danger btn-xs treenodedeletebutton\">Delete</a>", parent.Id);

            //if there is no children then end <li>
            if (parent.Children.Count == 0)
                content += "</li>";
            else //f there are children add <ul>
                content += "<ul>";
            //loop one past the number of children
            int numberOfChildren = parent.Children.Count;
            for (int i = 0; i <= numberOfChildren; i++)
            {
                //if there is cheldren call the function again
                if (numberOfChildren > 0 && i < numberOfChildren)
                {
                    Categories child = parent.Children[i];
                    content += EnumerateBackAdminNodes(child);
                }
                //if there is no children end the <ul>
                if (numberOfChildren > 0 && i == numberOfChildren)
                {
                    content += "</ul>";
                }

            }
            return content;
        }
        //todo: get this out of here
        //nterface for forist of nodes
        private string EnumerateNodes(Categories parent)
        {
            //init an empty string
            string content = String.Empty;
            if (parent.Parent == null && parent.Children.Count > 0)
            {
                content += "<li class=\"color2\">";
                content += "<a class=\"dropdown-toggle\" data-toggle=\"dropdown\" href=\"#\" role=\"button\" aria-haspopup=\"true\" aria-expanded=\"false\">" + parent.CategoryName + "<span class=\"caret\"></span>" + "</a>";

            }
            else if (parent.Parent == null && parent.Children.Count == 0)
            {

                content += "<li class=\"color2\">";
                content += string.Format("<a href=\"/Home/products/{0}\">" + parent.CategoryName + "</a>", parent.Id);
            }
            else if (parent.Parent != null && parent.Children.Count > 0)
            {
                content += "<li class=\"color2\">";
                content += "<a class=\"dropdown-toggle\" data-toggle=\"dropdown\" href=\"#\" role=\"button\" aria-haspopup=\"true\" aria-expanded=\"false\">" + parent.CategoryName + "<span class=\"caret\"></span>" + "</a>";

                //content += "<li>";
                //content += parent.CategoryName;

            }

            else if (parent.Parent != null && parent.Children.Count == 0)
            {
                if (parent.Parent.Parent != null )
                {

                    content += "<li class=\"productLi\">";
                    content += string.Format("<a href=\"/Home/products/{0}\">" + parent.CategoryName + "</a>", parent.Id);
                    content += "</br>";
                }
                else { 

                content += "<li class=\"productLi\">";
                content += string.Format("<a href=\"/Home/products/{0}\">" + parent.CategoryName + "</a>", parent.Id);
                    }
            }


            //if there is no children then end <li>
             if (parent.Children.Count == 0)
            {
               
                content += "</li>";
            }

          
            else {//f there are children add <ul>
                if (parent.Parent?.Parent == null && parent.Children.Count > 0)
                {//f there are children add <ul>
                    content += " <ul class=\"dropdown-menu\" role=\"menu\"><div class=\"col-12\">";
                }
                else { 
                content += " <ul class=\"dropdown-menu\" role=\"menu\"><div class=\"h_nav\">";
                }

                //loop one past the number of children
                int numberOfChildren = parent.Children.Count;
            for (int i = 0; i <= numberOfChildren; i++)
            {
                //if there is cheldren call the function again
                if (numberOfChildren > 0 && i < numberOfChildren)
                {
                    Categories child = parent.Children[i];
                    content += EnumerateNodes(child);
                }
                //if there is no children end the <ul>
                if (numberOfChildren > 0 && i == numberOfChildren)
                {
                    content += "</div></ul>";
                    content += "</li>";
                }
                }
            }
            return content;
        }



        public string CreateCategories()
        {
            string fullString = "<ul>";
            fullString += "<li class=\"color2\">";
            fullString += "<a class=\"dropdown-toggle\" data-toggle=\"dropdown\" href=\"#\" role=\"button\" aria-haspopup=\"true\" aria-expanded=\"false\">" + "our products" + "<span class=\"caret\"></span>" + "</a>";

            
            fullString += " <ul class=\"dropdown-menu\" role=\"menu\"><div class=\"h_nav\">";
            IList<Categories> lestOfNoods = GetListOfNode();
            IList<Categories> topLevelCategorieses = TreeHelper.ConvertToForest(lestOfNoods);
            foreach (var category in topLevelCategorieses)
            {
                fullString += EnumerateNodes(category);
            }


            fullString += "</div></ul></li></ul>";
            return fullString;

        }

        public string CreateBackAdminCategories()
        {
            string fullString = "<ul>";

            IList<Categories> lestOfNoods = GetListOfNode();
            IList<Categories> topLevelCategorieses = TreeHelper.ConvertToForest(lestOfNoods);
            foreach (var category in topLevelCategorieses)
            {
                fullString += EnumerateBackAdminNodes(category);
            }


            fullString += "</ul>";
            return fullString;

        }

       
    }
}
