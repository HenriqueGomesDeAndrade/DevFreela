﻿using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
    }
}
