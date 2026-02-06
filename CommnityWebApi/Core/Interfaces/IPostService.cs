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
    }
}
