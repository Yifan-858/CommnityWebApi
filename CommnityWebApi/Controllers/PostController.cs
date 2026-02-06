using AutoMapper;
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
        private readonly ICommentService _commentService;

        public PostController(IPostService postService,ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost("publish")]
        public async Task<IActionResult> PublishPost([FromBody] PublishDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            try
            {
               var postDTO = await _postService.CreatePost(dto.Title, dto.Text, dto.Category, userId);
               return Ok(postDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPosts()
        {
            List<PostDTO> postDTOs = await _postService.GetAllPosts();

            return Ok(postDTOs);
        }

        [Authorize]
        [HttpGet("all/comments")]
        public async Task<IActionResult> GetAllPostsWithComments()
        {
            List<PostDTO> postDTOs = await _postService.GetAllPosts();

            var postsWithComments = new List<PostsWithCommentsDTO>();

            foreach (var post in postDTOs) 
            { 
                var comments = await _commentService.GetCommentByPost(post.PostId);
                postsWithComments.Add(new PostsWithCommentsDTO
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Text = post.Text,
                    Category = post.Category,
                    UserId = post.UserId,
                    UserName = post.UserName,
                    Comments = comments
                });
            }
           
            return Ok(postsWithComments);
        }


        [HttpGet("{userId}/posts")]
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
