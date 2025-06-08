using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApp.Core.Builder;
using MyApp.Core.Builder.Model;
using MyApp.Core.Utilities;

namespace MyApp.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersControllers : ControllerBase
    {
        private readonly ILogger<UsersControllers> _logger;
        private readonly IUserBuilder _userService;

        public UsersControllers(ILogger<UsersControllers> logger, IUserBuilder userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger;
        }

    [HttpGet]
   public ActionResult<ServiceResult<List<UserDto>>> GetUsers()
    {
        var data = _userService.GetAllUsersAsync();
        
        if (data.Result.IsSuccess)
            return Ok(data);

        return BadRequest(data);
    }
    }
}