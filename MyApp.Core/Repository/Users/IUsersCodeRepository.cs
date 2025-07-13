using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My.Core.Repository.Base;
using MyApp.Core.Models;

namespace MyApp.Core.Repository.Users
{
    public interface IUsersCodeRepository : IRepository<UsersCode>
    {
        Task<UsersCode> GetValidResetCodeAsync(int userId, string resetCode, int expiryMinutes);

        Task<bool> IsUserCodeValid(int userId, string resetCode);
    }
}