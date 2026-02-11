using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Core.Services;
using CommnityWebApi.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommnityWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService,IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> PostComment([FromBody] AddCommentDTO addCommentDTO)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var postId = addCommentDTO.postId;

            try 
            { 
                var currentPost = await _postService.GetPostById(postId);
                var createdAt = DateTime.UtcNow;
                var comment = await _commentService.AddComment(addCommentDTO.Content,userId,postId,createdAt);
                return Ok(comment);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }
        }

        [Authorize]
        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> ShowSinglePostWithComments(int postId)
        {
            var comments = await _commentService.GetCommentByPost(postId);
            return Ok(comments);
        }
    }
}
