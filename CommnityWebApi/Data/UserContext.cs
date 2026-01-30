using CommnityWebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

    }
}
