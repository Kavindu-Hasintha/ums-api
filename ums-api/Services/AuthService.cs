using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ums_api.Constants;
using ums_api.Dtos.Auth;
using ums_api.Dtos.General;
using ums_api.Entities;
using ums_api.Interfaces;

namespace ums_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogService logService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logService = logService;
            _configuration = configuration;
        }

        public async Task<GeneralServiceResponseDto> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isManagerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.MANAGER);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isManagerRoleExists && isUserRoleExists)
            {
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = true,
                    StatusCode = 200,
                    Message = "Roles seeding is already done"
                };
            }

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.MANAGER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));

            return new GeneralServiceResponseDto()
            {
                IsSucceed = true,
                StatusCode = 201,
                Message = "Roles seeding done successfully"
            };
        }

        public Task<UserInfoResult> GetUserDetailsByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetUsernamesListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserInfoResult>> GetUsersListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoginServiceResponseDto> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<LoginServiceResponseDto> MeAsync(MeDto meDto)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }



        public Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal User, UpdateRoleDto updateRoleDto)
        {
            throw new NotImplementedException();
        }
    }
}
