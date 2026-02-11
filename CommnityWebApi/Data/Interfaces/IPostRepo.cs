using CommnityWebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Data.Interfaces
{
    public interface IPostRepo
    {
       Task<Post> CreatePost(string? title, string? text, List<int>? categoryIds,int userId);
       Task<List<Post>> GetAllPosts();
       Task<List<Post>> GetPostsByUser(int userId);
       Task<Post?> GetPostById(int postId);
       Task<List<Post>> GetPostsByTitle(string title);
       Task<List<Post>> GetPostsByCategoryId(int categoryId);
       Task<Post> UpdatePost(int postId, string? title, string? text, List<Category>? category);
       Task<bool> DeletePost(int postId);
        
    }
}
