using ums_api.Dtos.Auth;
using ums_api.Dtos.General;
using System.Security.Claims;

namespace ums_api.Interfaces
{
    public interface IAuthService
    {
        Task<GeneralServiceResponseDto> SeedRolesAsync();
        Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<LoginServiceResponseDto> LoginAsync(LoginDto loginDto);
        Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal User, UpdateRoleDto updateRoleDto);
        Task<LoginServiceResponseDto> MeAsync(MeDto meDto);
        Task<IEnumerable<UserInfoResult>> GetUsersListAsync();
        Task<UserInfoResult> GetUserDetailsByUsername(string username);
        Task<IEnumerable<string>> GetUsernamesListAsync();
    }
}
