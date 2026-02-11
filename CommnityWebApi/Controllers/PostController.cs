using AutoMapper;
using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Core.Services;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, ICommentService commentService, IUserService userService,IMapper mapper)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
            _mapper = mapper;
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
        [HttpGet("all/with_comments")]
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

        [HttpGet("search_title")]
        public async Task<IActionResult> GetPostsByTitle([FromQuery] string title)
        {
            try
            {
                var posts = await _postService.GetPostsByTitle(title);
                return Ok(posts);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }            
            
        }

        [HttpGet("search_category")]
        public async Task<IActionResult> GetPostsByCategory([FromQuery] int categoryId)
        {
            try
            {
                var posts = await _postService.GetPostsByCategory(categoryId);
                return Ok(posts);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [Authorize]
        [HttpPatch("update")]
        public async Task<IActionResult> UpdatePost([FromBody]UpdatePostDTO updatePostDTO)
        {
            var userIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!int.TryParse(userIdentifier, out var userId))
            {
                return BadRequest("Invalid access"); 
            }

            var currentPost = await _postService.GetPostById(updatePostDTO.PostId);

            if (currentPost == null)
            {
                return NotFound("Post not found"); 
            }

            if (userId != currentPost.UserId) 
            {
                return BadRequest("Unauthorized User");
            }

            var updatedPost = await _postService.UpdatePost
                (updatePostDTO.PostId, updatePostDTO.Title, updatePostDTO.Text, updatePostDTO.Category);

            var postDto = _mapper.Map<PostDTO>(updatedPost);
            return Ok(postDto);
        }

        [Authorize]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var userIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!int.TryParse(userIdentifier, out var userId))
            {
                return BadRequest("Invalid access"); 
            }

            try
            {
                await _postService.DeletePost(postId,userId);
                return Ok($"Post with postId: {postId} is deleted");
            }
            catch (KeyNotFoundException ex) 
            { 
                return NotFound(ex.Message); 
            }
            catch(UnauthorizedAccessException ex)
            { 
                return BadRequest(ex.Message); 
            } 
        }
       
    }
}   
