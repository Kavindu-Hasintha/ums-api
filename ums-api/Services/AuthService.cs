﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public async Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var isExistsUser = await _userManager.FindByNameAsync(registerDto.Username);
            if (isExistsUser is not null)
            {
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = false,
                    StatusCode = 409,
                    Message = "Username already exists",
                };
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Address = registerDto.Address,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!createUserResult.Succeeded)
            {
                var errorString = "User creation failed because: ";
                foreach(var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new GeneralServiceResponseDto()
                {
                    IsSucceed = false,
                    StatusCode = 400,
                    Message = errorString,
                };
            }

            // Add a default USER role to all users
            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);
            await _logService.SaveNewLog(newUser.UserName, "Registered to website");

            return new GeneralServiceResponseDto()
            {
                IsSucceed = true,
                StatusCode = 201,
                Message = "User created successfully",
            };
        }

        public async Task<LoginServiceResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // Find user with username
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user is null)
            {
                return null;
            }

            // Check password of user
            var isPasswordCorrect = await _userManager.ChangePasswordAsync(user, loginDto.Password);
            if (!isPasswordCorrect)
            {
                return null;
            }

            //Return token and userInfo to front-end
            var newToken = GenerateJWTTokenAsync(user);
            var userInfo = ;
            
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



        public Task<LoginServiceResponseDto> MeAsync(MeDto meDto)
        {
            throw new NotImplementedException();
        }





        public Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal User, UpdateRoleDto updateRoleDto)
        {
            throw new NotImplementedException();
        }

        // GenerateJWTTokenAsync
        private async Task<string> GenerateJWTTokenAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: signingCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }
    }
}