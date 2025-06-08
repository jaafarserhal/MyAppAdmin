
using Microsoft.Extensions.Logging;

using MyApp.Core.Models;
using MyApp.Core.Repository.Users;
using MyApp.Core.Services.Model;
using MyApp.Core.Utilities;

namespace MyApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
   
        public async Task<ServiceResult<IEnumerable<UserDto>>> GetUsersAsync(int page = 1, int limit = 10)
        {
            try
            {
                if (page <= 0 || limit <= 0)
                {
                    return ServiceResult<IEnumerable<UserDto>>.Failure("Page and limit must be greater than 0");
                }

                var users = await _userRepository.GetUsersAsync(page, limit);
                var userDtos = users.Select(MapToUserDto).ToList();

                return ServiceResult<IEnumerable<UserDto>>.Success(userDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paginated users");
                return ServiceResult<IEnumerable<UserDto>>.Failure("An error occurred while retrieving users");
            }
        }

        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                RoleName = user.Role?.Name,
                IsActive = user.IsActive
            };
        }

    }
}