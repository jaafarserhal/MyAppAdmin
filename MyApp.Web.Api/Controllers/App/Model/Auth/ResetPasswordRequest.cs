using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Api.Controllers.App.Model.Auth
{
    public class InitiatePasswordResetRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class VerifyResetCodeRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string ResetCode { get; set; }
    }

    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string ResetCode { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}