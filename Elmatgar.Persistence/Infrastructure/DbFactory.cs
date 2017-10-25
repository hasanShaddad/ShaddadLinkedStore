

using System;
using Elmatgar.Core.Units;

namespace Elmatgar.persistence.Infrastructure
{
  public  class DbFactory:Disposable, IDbFactory
    {
        StoreDbContext _dbContext;

       
        public  StoreDbContext Init()
        {
            return _dbContext ?? (_dbContext = new StoreDbContext());
        }


        protected override void DisposeCore()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();

            }
        }

        
    }
}
