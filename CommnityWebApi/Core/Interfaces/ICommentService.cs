using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Core.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDTO> AddComment(string content, int userId, int postId, DateTime createAt);
        

        Task<List<CommentDTO>> GetAllComments();
       
        Task<List<CommentDTO>> GetCommentByUser(int userId);
        

        Task<List<CommentDTO>> GetCommentByPost(int postId);
        

        Task<bool> DeteleComment(int commentId);
        
    }
}
