using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmatgar.Core.Models;

namespace Elmatgar.Core.Services
{
    public interface IProductsForHomeService
    {
        List<Products> ProductsPromoted();
    }
}
