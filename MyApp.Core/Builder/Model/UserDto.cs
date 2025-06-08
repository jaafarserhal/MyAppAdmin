using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Core.Builder.Model
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}