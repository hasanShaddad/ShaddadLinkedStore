using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.Core.Services;
using Elmatgar.Core.Units;
using Elmatgar.persistence;
using Elmatgar.Service.Services;
using Elmatgar.ViewModels;
using Categories = Elmatgar.Core.Models.Categories;
using TreeHelper = Elmatgar.Core.Repositories.TreeHelper;

namespace Elmatgar.Areas.Admin.Controllers.ProductsTable
{
    public class CategoriesController : Controller
    {
      //  private StoreDbContext _StoreDbContext = new StoreDbContext();
        private ICategoryServices _categoryService;
        private readonly ICategoriesUnit _db;
        public CategoriesController(ICategoryServices categoryService, ICategoriesUnit db)
        {
   
            _categoryService = categoryService;
            _db = db;

        }
     
        
        //todo:take this from here
        //filtering the parent list
        //check if category name is exest
        public ActionResult IsCategAvailble(string categoryName)
        {
            //using (_StoreDbContext)
            //{
                try
                {
                    var tag = _db.Category.Get(c => c.CategoryName == categoryName);
                        //_StoreDbContext.Categories.Single(m => m.CategoryName == categoryName);
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            //}
        }
       
        //todo: get this out of here
        //nterface for forist of nodes
       

        // GET: Admin/Categories
        public ActionResult Index()
        {
            //start outermost list
            string fullString = _categoryService.CreateBackAdminCategories();
            return View((object)fullString);
        }


        // GET: Admin/Categories/Create

        public ActionResult Create()
        {
            ViewBag.ParentId = _categoryService.poulateParentCategorySelectList(null);
            return View();
        }

        // POST: Admin/Categories/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind(Include = "Id,ParentId,CategoryName,SLCategoryName,CategoryTitle,CategoryDescription")] CategoryViewModel categoriesViewModel)
        {
            if (ModelState.IsValid)
            {
                Categories categories = new Categories();
                categories.Id = categoriesViewModel.Id;
                categories.ParentId = categoriesViewModel.ParentId;
                categories.CategoryName = categoriesViewModel.CategoryName;
                categories.CategoryTitle = categoriesViewModel.CategoryTitle;
                categories.CategoryDescription = categoriesViewModel.CategoryDescription;
                categories.SLCategoryName = categoriesViewModel.SLCategoryName;

                try
                {
                  //  _StoreDbContext.Categories.Add(categories);


                    _db.AddCategory(categories);
                     _db.SaveChanges();
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                    ViewBag.ParentId = _categoryService.poulateParentCategorySelectList(null);
                    return View("Create", categoriesViewModel);
                }

                return RedirectToAction("Index");
            }

            ViewBag.ParentId = _categoryService.poulateParentCategorySelectList(null);
            return View(categoriesViewModel);
        }
        //todo:handel if creationdate or lastupdatedate is empty
        // GET: Admin/Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await _db.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            //set the category view model
            CategoryEditViewModel categoryEditViewModel = new CategoryEditViewModel();
            categoryEditViewModel.Id = categories.Id;
            categoryEditViewModel.ParentId = categories.ParentId;
            categoryEditViewModel.CategoryName = categories.CategoryName;
            categoryEditViewModel.SLCategoryName = categories.SLCategoryName;

            ViewBag.ParentId = _categoryService.poulateParentCategorySelectList(categoryEditViewModel.Id);
            return View(categoryEditViewModel);
        }

        // POST: Admin/Categories/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Edit([Bind(Include = "Id,ParentId,CategoryName,SLCategoryName,CategoryTitle,CategoryDescription")] CategoryEditViewModel categoryViewModel)
        {
            Categories editedCategories = new Categories();
            if (ModelState.IsValid)
            {
                //retrive the data from viewmodel

                try
                {
                    editedCategories.Id = categoryViewModel.Id;
                    editedCategories.ParentId = categoryViewModel.ParentId;
                    editedCategories.CategoryName = categoryViewModel.CategoryName;
                    editedCategories.CategoryTitle = categoryViewModel.CategoryTitle;
                    editedCategories.CategoryDescription = categoryViewModel.CategoryDescription;
                    editedCategories.SLCategoryName = categoryViewModel.SLCategoryName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.ParentId = _categoryService.poulateParentCategorySelectList(null);
                    return View("Edit", categoryViewModel);
                    throw;
                }



              //  _StoreDbContext.Entry(editedCategories).State = EntityState.Modified;


                _db.UpdateCategory(editedCategories);
                 _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = _categoryService.poulateParentCategorySelectList(categoryViewModel.Id);
            return View(categoryViewModel);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = await _db.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categories categories = await _db.FindAsync(id);
            try
            {
               // _StoreDbContext.Categories.Remove(categories);


                _db.DeleteCategory(id);
                 _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "You attempted to delete a category which has children categories");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View("Delete", categories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
