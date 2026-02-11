using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Core.Interfaces
{
    public interface IPostService
    {
        Task<PostDTO> CreatePost(string? title, string? text, List<int>? categoryIds, int userId);
        Task<List<PostDTO>> GetAllPosts();
        Task<List<Post>> GetPostsByUser(int userId);
        Task<PostDTO> GetPostById(int postId);

        Task<List<PostDTO>> GetPostsByTitle(string title);
        Task<List<PostDTO>> GetPostsByCategory(int categoryId);
        Task<Post> UpdatePost(int postId, string? title, string? text, List<Category>? category);
        Task DeletePost(int postId, int userId);
    }
}
