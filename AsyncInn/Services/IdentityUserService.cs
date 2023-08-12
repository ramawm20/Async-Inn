using AsyncInn.Interfaces;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace AsyncInn.Services
{
    public class IdentityUserService : IUserService


    {
        private UserManager<ApplicationUser> userManager;

        private JwtTokenService jwtTokenService;
        public IdentityUserService(UserManager<ApplicationUser> manager, JwtTokenService jwtTokenService)
        {
            userManager = manager;
            this.jwtTokenService = jwtTokenService;
        }


        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            
            if (await userManager.CheckPasswordAsync(user, password))
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await jwtTokenService.GetToken(user,System.TimeSpan.FromMinutes(10)),
                    
                };
            }

            return null;
         
        }

        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            var userDTO = new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(10)),
            };

            return userDTO;
        }

        public async Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result= await userManager.CreateAsync(user,data.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(user, data.Roles);                                                            
                var userDTO = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(10)),
                };

                return userDTO;
            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                  error.Code.Contains("Password") ? nameof(data.Password) :
                  error.Code.Contains("Email") ? nameof(data.Email) :
                  error.Code.Contains("UserName") ? nameof(data.Username) :
                  "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;

        }
    }
}
