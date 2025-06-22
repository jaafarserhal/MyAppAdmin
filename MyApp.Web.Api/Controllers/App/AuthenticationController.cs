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

namespace MyApp.Web.Api.Controllers.App
{
    [ApiController]
    [Route("api/app/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
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

                var result = await _userService.SignupAsync(user);

                return Ok(AppApiResponse<string>.Success(result.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during signup.");
                return StatusCode(500, AppApiResponse<string>.Failure("An unexpected error occurred."));
            }
        }


    }
}