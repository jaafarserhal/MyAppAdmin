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
    public class UsersCodeRepository : Repository<Userscode>, IUsersCodeRepository
    {

        public UsersCodeRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Userscode> CreateUsersCodeAsync(Userscode usersCode)
        {
            usersCode.CreatedAt = DateTime.UtcNow;
            _context.Userscodes.Add(usersCode);
            await _context.SaveChangesAsync();
            return usersCode;
        }

        public async Task<Userscode> GetValidResetCodeAsync(int userId, string resetCode, int expiryMinutes)
        {
            // Find the reset code in the database for the given user and code
            var validResetCode = await _context.Userscodes
                .Where(rc => rc.UserId == userId && rc.Code == resetCode
                            && rc.IsActive
                            && rc.StatusLookupId == UserCodeStatusLookup.Pending.AsInt())
                .FirstOrDefaultAsync();

            if (validResetCode == null)
            {
                return null; // No reset code found
            }

            // Check if the reset code has expired (more than 15 minutes old)
            if (validResetCode.ExpirationTime < DateTime.UtcNow.AddMinutes(-expiryMinutes))
            {
                return null; // Reset code is expired
            }

            return validResetCode; // Valid reset code
        }

        public async Task<bool> IsUserCodeValid(int userId, string resetCode)
        {
            var lastCode = await _context.Userscodes
                .Where(rc => rc.UserId == userId && rc.Code == resetCode && rc.IsActive)
                .OrderByDescending(rc => rc.CreatedAt)
                .FirstOrDefaultAsync();

            return lastCode != null
                && lastCode.StatusLookupId == UserCodeStatusLookup.Processed.AsInt();
        }


        public async Task<Userscode> UpdateUserCodesAsync(Userscode userscode)
        {
            _context.Userscodes.Update(userscode);
            await _context.SaveChangesAsync();
            return userscode;
        }

    }
}