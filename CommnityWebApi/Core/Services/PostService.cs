using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommnityWebApi.Core.Services
{
    public class PostService: IPostService
    {
        private readonly IPostRepo _postRepo;

        public PostService(IPostRepo postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<Post> CreatePost(string? title, string? text, List<string>? category, int userId)
        {
            var post= await _postRepo.CreatePost(title, text, category, userId);

            if(post == null)
            {
                throw new Exception("Failed to create post.");
            }

            return post;
        }
    }
}
