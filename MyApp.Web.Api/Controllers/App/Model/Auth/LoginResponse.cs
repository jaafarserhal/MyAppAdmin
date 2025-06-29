using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Web.Api.Controllers.App.Model.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
    }
}