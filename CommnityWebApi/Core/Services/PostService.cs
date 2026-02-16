using AutoMapper;
using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using CommnityWebApi.Data.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Core.Services
{
    public class PostService: IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;
        private readonly UserContext _context;

        public PostService(IPostRepo postRepo, IMapper mapper, UserContext context)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<PostDTO> CreatePost(string? title, string? text, List<int>? categoryIds, int userId)
        {
            var post= await _postRepo.CreatePost(title, text,categoryIds, userId);

            if (post == null)
            {
                throw new Exception("Failed to create post.");
            }
            
            var postDTO = _mapper.Map<PostDTO>(post);

            return postDTO;
        }

        public async Task<List<PostDTO>> GetAllPosts()
        {
            var posts = await _postRepo.GetAllPosts();
            var postDTOs = _mapper.Map<List<PostDTO>>(posts);

            return postDTOs;
        }

        public async Task<List<Post>> GetPostsByUser(int userId)
        {
            return await _postRepo.GetPostsByUser(userId);
        }

        public async Task<PostDTO> GetPostById(int postId)
        {
            var post = await _postRepo.GetPostById(postId);

            var postDTO = _mapper.Map<PostDTO>(post);
            return postDTO;
        }

        public async Task<List<PostDTO>> GetPostsByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("Must provide valid title."); 
            }

            var posts = await _postRepo.GetPostsByTitle(title);

            var postDtos = _mapper.Map<List<PostDTO>>(posts);
            return postDtos;
        }

        public async Task<List<PostDTO>> GetPostsByCategory(int categoryId)
        {
            if(categoryId <= 0)
            {
                throw new ArgumentException("Invalid category id");
            }

            var posts = await _postRepo.GetPostsByCategoryId(categoryId);
           
            var postDtos = _mapper.Map<List<PostDTO>>(posts);
            return postDtos;
        }

        public async Task<Post> UpdatePost(int postId, string? title, string? text, List<Category>? category)
        {
            var post = await _postRepo.UpdatePost(postId, title, text, category);

            return post;
        }

        public async Task DeletePost(int postId, int userId)
        {
            var currentPost = await _postRepo.GetPostById(postId);

            if (currentPost == null)
            {
               throw new KeyNotFoundException("Post not found"); 
            }

            if (userId != currentPost.UserId) 
            {
               throw new UnauthorizedAccessException("Unauthorized User: cannot delete other's post.");
            }

            await _postRepo.DeletePost(postId);
        }
    }
}
