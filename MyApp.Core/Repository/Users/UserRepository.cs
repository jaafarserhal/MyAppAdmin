using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My.Core.Repository.Base;
using MyApp.Core.Data;
using MyApp.Core.Models;

namespace MyApp.Core.Repository.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                                 .Include(u => u.Stores) // Include related stores if necessary
                                 .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                                 .Include(u => u.Stores) // Include related stores if necessary
                                 .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                                 .Include(u => u.Stores) // Include related stores if necessary
                                 .ToListAsync();
        }
    }
}