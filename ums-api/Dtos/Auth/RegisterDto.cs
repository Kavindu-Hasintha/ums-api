using System.ComponentModel.DataAnnotations;

namespace ums_api.Dtos.Auth
{
    public class RegisterDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Address { get; set; }
    }
}
