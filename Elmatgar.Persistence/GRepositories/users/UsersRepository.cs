﻿using System.Data.Entity;
using Elmatgar.Core.Models;

namespace Elmatgar.persistence.GRepositories.users 
{
    public class UsersRepository:DataRepository<Users>
    {
        public UsersRepository(DbContext dbContext):base(dbContext)
        {
            
        }
    }
}