using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Data.Interfaces
{
    public interface ICommentRepo
    {
        Task<Comment> AddComment(string content, int userId, int postId, DateTime createAt);
        Task<List<Comment>> GetAllComments();
        Task<List<Comment>> GetCommentsByUser(int userId);
        Task<List<Comment>> GetCommentsByPost(int postId);
        Task<bool> DeleteComment(int commentId);
       
    }
}
