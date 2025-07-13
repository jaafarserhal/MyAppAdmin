using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My.Core.Repository.Base;
using MyApp.Core.Models;

namespace MyApp.Core.Repository.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);

        Task<IEnumerable<User>> GetUsersAsync(int page, int limit);
    }
}