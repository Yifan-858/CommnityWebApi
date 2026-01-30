using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;

namespace CommnityWebApi.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext _context;

        public UserRepo(UserContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
