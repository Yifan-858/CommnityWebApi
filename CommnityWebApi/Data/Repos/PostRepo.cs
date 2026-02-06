using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Data.Repos
{
    public class PostRepo:IPostRepo
    {
        private readonly UserContext _context;
        public PostRepo(UserContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePost(string? title, string? text, List<int>? categoryIds, int userId)
        {
            var post = new Post(title, text, userId);

            if(categoryIds != null && categoryIds.Any())
            {
                var categories = await _context.Category
                    .Where(c => categoryIds.Contains(c.CategoryId)).ToListAsync();

                post.Categories = categories;
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            //load User
            await _context.Entry(post)
                  .Reference(p => p.User)
                  .LoadAsync();

            return post;
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Categories)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByUser(int userId)
        {
            return await _context.Posts.Where(p=> p.UserId == userId).ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts.SingleOrDefaultAsync(p=> p.PostId == postId);
        }

        public async Task<Post> UpdatePost(int postId, string? title, string? text, List<Category>? category)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p=> p.PostId==postId);

            if(post == null)
            {
                throw new KeyNotFoundException($"Post id{postId} not found");
            }

            if(!string.IsNullOrEmpty(title))
            {
                post.Title = title;
            }

            if (!string.IsNullOrEmpty(text))
            {
                post.Text = text;
            }

            if (category != null)
            {
                post.Categories = category;
            }

            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePost(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);//search for primary key
            if(post == null)
            {
                return false;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
