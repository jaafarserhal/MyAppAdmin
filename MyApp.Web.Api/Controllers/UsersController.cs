using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Core.Services;
using MyApp.Core.Services.Model;
using MyApp.Core.Utilities;

namespace MyApp.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResult<IEnumerable<UserDto>>>> GetUsers([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var data = await _userService.GetUsersAsync(page, limit);

            if (data.IsSuccess)
                return Ok(data);

            return BadRequest(data);
        }
    }
}