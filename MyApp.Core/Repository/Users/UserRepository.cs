using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My.Core.Repository.Base;
using MyApp.Core.Data;
using MyApp.Core.Models;
using MyApp.Core.Utilities;

namespace MyApp.Core.Repository.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {

            return await _context.Users.FirstOrDefaultAsync(u => u.Email == username && u.IsActive && u.RoleId == RoleType.Customer.AsInt());
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int page, int limit)
        {
            return await _context.Users
                .Include(u => u.Role)
                .OrderBy(u => u.UserId)
                .Skip((page - 1) * limit)
                .Take(limit)
                .Where(u => u.IsActive)
                .ToListAsync();
        }

    }
}