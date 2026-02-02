using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Core.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePost(string? title, string? text, List<string>? category, int userId);
    }
}
