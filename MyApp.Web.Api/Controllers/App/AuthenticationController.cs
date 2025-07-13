using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Common.Models;
using MyApp.Core.Models;
using MyApp.Core.Services;
using MyApp.Core.Utilities;
using MyApp.Web.Api.Controllers.App.Model.Auth;
using MyApp.Web.Api.Controllers.App.Utils;

namespace MyApp.Web.Api.Controllers.App
{
    [ApiController]
    [Route("api/app/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;

        private readonly IJwtService _jwtService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService, IJwtService jwtService)
        {
            _logger = logger;
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
            {
                return BadRequest(AppApiResponse<string>.Failure("Request cannot be null."));
            }
            try
            {

                // Ensure passwords match
                if (request.Password != request.ConfirmPassword)
                {
                    return BadRequest(AppApiResponse<string>.Failure("Passwords do not match.", HttpStatusCodeEnum.Conflict));
                }

                var user = new User
                {
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    HashPassword = request.Password
                };

                var result = await _userService.RegisterAsync(user);

                if (result.StatusCode == HttpStatusCodeEnum.OK.AsInt())
                {
                    // Generate JWT token
                    var token = _jwtService.GenerateToken(result.Data);

                    // Create signup response with token
                    var signupResponse = new LoginResponse
                    {
                        Token = token,
                        UserId = result.Data.UserId,
                        Email = user.Email,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        Message = "Registration successful!"
                    };

                    return Ok(AppApiResponse<LoginResponse>.Success(signupResponse));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during signup.");
                return StatusCode(500, AppApiResponse<string>.Failure("An unexpected error occurred."));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest(AppApiResponse<string>.Failure("Request cannot be null."));
            }

            try
            {
                // Validate user credentials
                var result = await _userService.LoginAsync(request.Email, request.Password);

                if (result.StatusCode == HttpStatusCodeEnum.OK.AsInt())
                {
                    // Generate JWT token
                    var token = _jwtService.GenerateToken(result.Data);

                    // Create login response with token
                    var loginResponse = new LoginResponse
                    {
                        Token = token,
                        UserId = result.Data.UserId,
                        Email = result.Data.Email,
                        FullName = result.Data.FullName,
                        PhoneNumber = result.Data.PhoneNumber,
                        Message = "Login successful!"
                    };

                    return Ok(AppApiResponse<LoginResponse>.Success(loginResponse));
                }

                return Ok(AppApiResponse<string>.Failure("Invalid email or password.", HttpStatusCodeEnum.Unauthorized));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login.");
                return StatusCode(500, AppApiResponse<string>.Failure("An unexpected error occurred."));
            }
        }

        /// <summary>
        /// Initiates password reset process by sending a reset code to the user's email
        /// </summary>
        /// <param name="request">Email address to send reset code to</param>
        /// <returns>Success response regardless of whether email exists (for security)</returns>
        [HttpPost("send-reset-code")]
        [ProducesResponseType(typeof(AppApiResponse<string>), 200)]
        [ProducesResponseType(typeof(AppApiResponse<string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendPasswordVerificationCode([FromBody] InitiatePasswordResetRequest request)
        {
            if (request == null)
            {
                return BadRequest(AppApiResponse<string>.Failure("Request cannot be null."));
            }
            try
            {
                var result = await _userService.SendPasswordVerificationCode(request.Email);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in InitiatePasswordReset API");
                return StatusCode(500, AppApiResponse<string>.Failure("An unexpected error occurred"));
            }
        }

        /// <summary>
        /// Verifies if a reset code is valid
        /// </summary>
        /// <param name="request">Email and reset code to verify</param>
        /// <returns>Verification result</returns>
        [HttpPost("verify-code")]
        [ProducesResponseType(typeof(AppApiResponse<string>), 200)]
        [ProducesResponseType(typeof(AppApiResponse<string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> VerifyResetCode([FromBody] VerifyResetCodeRequest request)
        {
            if (request == null)
            {
                return BadRequest(AppApiResponse<string>.Failure("Request cannot be null."));
            }

            try
            {
                var result = await _userService.VerifyResetCodeAsync(request.Email, request.ResetCode);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in VerifyResetCode API");
                return StatusCode(500, AppApiResponse<string>.Failure("An unexpected error occurred"));
            }
        }

        /// <summary>
        /// Resets user password using the reset code
        /// </summary>
        /// <param name="request">Email, reset code, and new password</param>
        /// <returns>Password reset result</returns>
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(AppApiResponse<string>), 200)]
        [ProducesResponseType(typeof(AppApiResponse<string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (request == null)
            {
                return BadRequest(AppApiResponse<string>.Failure("Request cannot be null."));
            }

            try
            {
                if (request.NewPassword != request.ConfirmPassword)
                {
                    return BadRequest(AppApiResponse<string>.Failure("Passwords do not match.", HttpStatusCodeEnum.Conflict));
                }

                var result = await _userService.ResetPasswordAsync(
                    request.Email,
                    request.ResetCode,
                    request.NewPassword);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ResetPassword API");
                return StatusCode(500, AppApiResponse<string>.Failure("An unexpected error occurred"));
            }
        }
    }
}