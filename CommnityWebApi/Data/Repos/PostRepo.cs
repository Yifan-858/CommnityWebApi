using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;

namespace CommnityWebApi.Data.Repos
{
    public class PostRepo:IPostRepo
    {
        private readonly UserContext _context;
        public PostRepo(UserContext context)
        {
            _context = context;
        }

        //public async Task<Post>
        //{

        //}
    }
}
