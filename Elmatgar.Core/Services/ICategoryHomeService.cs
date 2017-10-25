using System.Collections.Generic;
using System.Web.Mvc;
using Elmatgar.Core.Models;

namespace Elmatgar.Core.Services
{
    public interface ICategoryHomeService
    {
        
        IList<Categories> GetCategoriese(int? categId);
        IList<Categories> GetListOfNodeForMenu(int? categId);
        IList<Categories> GetTopCategOnly();
        IList<Categories> GetLastCategOnly();
    }
}