using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext _context;

        public UserRepo(UserContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(string userName, string email, string passwordHash)
        {
            var user = new User(userName,email,passwordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> UpdateUser(int id, string? userName, string? email, string? passwordHash)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);

            if(user == null)
            {
                throw new KeyNotFoundException($"User with id{id} not found");
            }

            if (!string.IsNullOrEmpty(userName))
            {
                user.UserName = userName;
            }

            if (!string.IsNullOrEmpty(email))
            {
                 user.Email = email;
            }

            if (!string.IsNullOrEmpty(passwordHash))
            {
                user.PasswordHash = passwordHash;
            }
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if(user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
