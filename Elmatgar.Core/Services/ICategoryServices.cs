using System.Web.Mvc;

namespace Elmatgar.Core.Services
{
    public interface ICategoryServices
    {
        string CreateCategories();
        string CreateBackAdminCategories();
        SelectList poulateParentCategorySelectList(int? id);
    }
}