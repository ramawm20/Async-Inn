using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AsyncInn.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState);

        public Task<UserDto> Authenticate(string username, string password);
    }
}
