using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Elmatgar.Database
{
    public interface IVoteDatabase
    {
        Task<bool> RegisterProductVote(string userId, int productid, int vote);
    }
}