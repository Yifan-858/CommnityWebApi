using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommnityWebApi.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUser(string userName, string email, string password);
        string GenerateToken(User user);
        Task<User> Login([FromBody] LoginDTO login);
    }
}
