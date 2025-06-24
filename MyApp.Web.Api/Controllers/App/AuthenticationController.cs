using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApp.Core.Common.Models;
using MyApp.Core.Models;
using MyApp.Core.Services;
using MyApp.Core.Utilities;
using MyApp.Web.Api.Controllers.App.Model;
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
                    FirstName = request.FirstName,
                    LastName = request.LastName,
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
                        FirstName = user.FirstName,
                        LastName = user.LastName,
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
                        FirstName = result.Data.FirstName,
                        LastName = result.Data.LastName,
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
    }
}