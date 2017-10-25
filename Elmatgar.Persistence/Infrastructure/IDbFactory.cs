using System;

namespace Elmatgar.persistence.Infrastructure
{
 public   interface IDbFactory: IDisposable
 {
    
        StoreDbContext Init();
    }
}
