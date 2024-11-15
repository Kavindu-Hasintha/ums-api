using System.ComponentModel.DataAnnotations;

namespace ums_api.Dtos.Auth
{
    public class UpdateRoleDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        public RoleType NewRole { get; set; }
    }

    public enum RoleType
    {
        ADMIN,
        MANAGER,
        USER
    }
}
