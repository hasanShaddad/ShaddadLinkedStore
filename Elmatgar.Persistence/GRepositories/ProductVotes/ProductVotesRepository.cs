using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmatgar.persistence.GRepositories.ProductVotes
{
  public  class ProductVotesRepository :DataRepository<Elmatgar.Core.Models.ProductVote>
    {
        public ProductVotesRepository(DbContext dBcontext) : base(dBcontext)
        {
        }
    }
}
