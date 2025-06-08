using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Core.Models;

namespace MyApp.Core.Repository.Users
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<IEnumerable<User>> GetUsersAsync(int page, int limit);
    }
}