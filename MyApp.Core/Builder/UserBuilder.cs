
using Microsoft.Extensions.Logging;
using MyApp.Core.Builder.Model;
using MyApp.Core.Models;
using MyApp.Core.Repository.Users;
using MyApp.Core.Utilities;

namespace MyApp.Core.Builder
{
    public class UserBuilder : IUserBuilder
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserBuilder> _logger;

        public UserBuilder(IUserRepository userRepository, ILogger<UserBuilder> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ServiceResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                var userDtos = users.Select(MapToUserDto).ToList();

                return ServiceResult<IEnumerable<UserDto>>.Success(userDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
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