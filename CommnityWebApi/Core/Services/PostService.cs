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

        public async Task<Post> CreatePost(string? title, string? text, List<int>? categoryIds, int userId)
        {
            var post= await _postRepo.CreatePost(title, text,categoryIds, userId);

            if (post == null)
            {
                throw new Exception("Failed to create post.");
            }

            return post;
        }

        public async Task<List<PostDTO>> GetAllPosts()
        {
            var posts = await _postRepo.GetAllPosts();
            var postDTOs = posts.Select(p => new PostDTO
            {
                PostId = p.PostId,
                Title = p.Title,
                Text = p.Text,
                Category = p.Category,
                UserId = p.UserId,
                UserName = p.User.UserName,
            }).ToList();

            return postDTOs;
        }

        public async Task<List<Post>> GetPostsByUser(int userId)
        {
            return await _postRepo.GetPostsByUser(userId);
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _postRepo.GetPostById(postId);

            if( post == null)
            {
                throw new KeyNotFoundException("Post not found");
            }

            return post;
        }
    }
}
