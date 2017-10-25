using System.Web.Mvc;
using Elmatgar.Core.ViewModels;
 

namespace Elmatgar.Core.Services
{
    public interface IProductDetailsService
    {
    ProductsViewModel GetProductsDetails(int? id);
        decimal? FinalPrice(int? id);


    }
}
