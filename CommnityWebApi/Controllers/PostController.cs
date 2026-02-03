using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace CommnityWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost("publish")]
        public async Task<IActionResult> PublishPost([FromBody] PublishDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Post post;
           
            try
            {
               post = await _postService.CreatePost(dto.Title, dto.Text, dto.Category, userId);
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            var postDTO = new PostDTO
            {
                PostId = post.PostId,
                Title = post.Title,
                Text = post.Text,
                Category = post.Category,
                UserId = post.UserId,
                UserName = post.User.UserName
            };

            return Ok(postDTO);
 
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPosts()
        {
            List<PostDTO> postDTOs = await _postService.GetAllPosts();

            return Ok(postDTOs);
        }

        [HttpGet("user/{userId}/posts")]
        public async Task<IActionResult> GetAllPostsFromUser(int userId)
        {
            List<Post> posts = await _postService.GetPostsByUser(userId);

            //do not use posts==null, repo doesnot return null
            if(!posts.Any())
            {
                return NotFound();
            }

            return Ok(posts);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetSinglePostFromUser(int postId)
        {
            var post = await _postService.GetPostById(postId);

            if(post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        //searchByTitle+Category GET
        //update guarded
        //delete guarded
    }
}   
