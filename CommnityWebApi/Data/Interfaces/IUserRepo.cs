using CommnityWebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Data.Interfaces
{
    public interface IUserRepo
    {
        Task<User> CreateUser(string userName, string email, string passwordHash);

        Task<List<User>> GetAllUser();

        Task<User?> GetUserById(int id);

        Task<User> UpdateUser(int id, string? userName, string? email, string? passwordHash);

        Task<bool> DeleteUser(int id);
        
    }
}
