using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommnityWebApi.Core.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUser([FromBody] SignUpDTO signUpDTO);
        string GenerateToken(User user);
        Task<User> Login([FromBody] LoginDTO login);
        Task<User> GetUserById(int userId);
        Task<User> UpdateUserProfile(int userId, string? userName, string? email, string? passwordHash);
        Task DeleteUser(int userId);
    }
}
