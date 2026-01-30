using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.Interfaces
{
    public interface IUserRepo
    {
        public List<User> GetAllUsers();
    }
}
