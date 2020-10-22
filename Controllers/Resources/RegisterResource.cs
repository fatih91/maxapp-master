using System.ComponentModel.DataAnnotations;

namespace maxapp.Controllers.Resources
{
    public class RegisterResource
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
            
        public string Role { get; set; }
    }
}