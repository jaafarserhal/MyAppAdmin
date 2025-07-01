using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Core.Models;

namespace MyApp.Core.Repository.Users
{
    public interface IUsersCodeRepository
    {
        Task<UsersCode> CreateUsersCodeAsync(UsersCode usersCode);
        Task<UsersCode> GetValidResetCodeAsync(int userId, string resetCode, int expiryMinutes);
        Task<UsersCode> UpdateUserCodesAsync(UsersCode userscode);

        Task<bool> IsUserCodeValid(int userId, string resetCode);
    }
}