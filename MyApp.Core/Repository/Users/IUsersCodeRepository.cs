using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Core.Models;

namespace MyApp.Core.Repository.Users
{
    public interface IUsersCodeRepository
    {
        Task<Userscode> CreateUsersCodeAsync(Userscode usersCode);
        Task<Userscode> GetValidResetCodeAsync(int userId, string resetCode, int expiryMinutes);
        Task<Userscode> UpdateUserCodesAsync(Userscode userscode);

        Task<bool> IsUserCodeValid(int userId, string resetCode);
    }
}