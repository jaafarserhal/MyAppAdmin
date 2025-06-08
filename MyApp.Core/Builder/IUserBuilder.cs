using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Core.Builder.Model;
using MyApp.Core.Utilities;

namespace MyApp.Core.Builder
{
    public interface IUserBuilder
    {
        Task<ServiceResult<IEnumerable<UserDto>>> GetAllUsersAsync();
    }
}