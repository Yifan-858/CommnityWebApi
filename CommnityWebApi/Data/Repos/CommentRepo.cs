using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommnityWebApi.Data.Repos
{
    public class CommentRepo : ICommentRepo
    {
        private readonly UserContext _context;
        public CommentRepo(UserContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddComment(string content, int userId, int postId, DateTime createAt)
        {
            var comment = new Comment(content, userId, postId, createAt);
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _context.Comment
                            .Include(c => c.User)
                            .Include(c => c.Post)
                            .ToListAsync();

            return comments;
        }

        public async Task<List<Comment>> GetCommentsByUser(int userId)
        {
            var comments = await _context.Comment.Where(c => c.UserId == userId).ToListAsync();
            return comments;
        }

        public async Task<List<Comment>> GetCommentsByPost(int postId)
        { 
            var comments = await _context.Comment.Where(c=> c.PostId == postId).ToListAsync();
            return comments;
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await _context.Comment.FindAsync(commentId);
            if(comment == null)
            {
                return false; 
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
