using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Core.Models;
using MyApp.Core.Services.Model;
using MyApp.Core.Common.Models;

namespace MyApp.Core.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Retrieves a paginated list of users.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<UserDto>>> GetUsersAsync(int skip, int take);

        Task<User> AuthenticateAsync(string username, string password);

        Task<AppApiResponse<User>> RegisterAsync(User user);

        Task<AppApiResponse<User>> LoginAsync(string email, string password);
        Task<AppApiResponse<string>> SendPasswordVerificationCode(string email);

        Task<AppApiResponse<string>> VerifyResetCodeAsync(string email, string resetCode);

        Task<AppApiResponse<string>> ResetPasswordAsync(string email, string resetCode, string newPassword);
    }
}