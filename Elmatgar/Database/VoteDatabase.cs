using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elmatgar.Core.Models;
using Elmatgar.persistence;

namespace Elmatgar.Database
{
    public class VoteDatabase : IVoteDatabase
    {
        private StoreDbContext _StoreDbContext = new StoreDbContext();

        /// <summary>
        /// Register a product vote
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productid"></param>
        /// <param name="vote"></param>
        public async Task<bool> RegisterProductVote(string userId, int productid, int vote)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            IQueryable<ProductVote> productVote =
              _StoreDbContext.ProductVotes.Where(e => e.ProductId == productid && e.UserId == userId);
            if (productVote.Any())
            { 
                var firstOrDefault = await productVote.FirstOrDefaultAsync();
                if (firstOrDefault != null) firstOrDefault.VoteValue = vote;

                _StoreDbContext.Entry(firstOrDefault).State = EntityState.Modified;
                await _StoreDbContext.SaveChangesAsync();
            }
            else
            {
                ProductVote newProductVote = new ProductVote();
                newProductVote.UserId = userId;
                newProductVote.VoteValue = vote;
                newProductVote.ProductId = productid;
                _StoreDbContext.ProductVotes.Add(newProductVote);
                await _StoreDbContext.SaveChangesAsync();
            }


            var product =await _StoreDbContext.Products.FirstOrDefaultAsync(e => e.Id == productid);
            if (product != null)
            {
                int? prodTotalVote =
                    product.TotalVote;
                IQueryable<ProductVote> productVoteAfter =  
                    _StoreDbContext.ProductVotes.Where(e => e.ProductId == productid);
 
                int? voteSum = productVoteAfter
                    .Select(uf => uf.VoteValue)
                    .DefaultIfEmpty()
                    .Sum();
                int voteCount = productVoteAfter.Count();

                var rate = voteSum / voteCount;
                product.TotalVote = rate;
                _StoreDbContext.Entry(product).State = EntityState.Modified;
                await _StoreDbContext.SaveChangesAsync();
            }
           

            // access your database here and update/insert the vote
            return true;


        }
    }
}
