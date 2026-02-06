using AutoMapper;
using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using CommnityWebApi.Data.Repos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CommnityWebApi.Core.Services
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IUserRepo _userRepo;
        private readonly IPostRepo _postRepo;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepo commentRepo, IUserRepo userRepo,IPostRepo postRepo,IMapper mapper)
        {
            _commentRepo = commentRepo;
            _userRepo = userRepo;
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<CommentDTO> AddComment(string content, int userId, int postId, DateTime createAt)
        {
            var comment = await _commentRepo.AddComment(content, userId, postId, createAt);
            if(comment == null)
            {
                throw new Exception($"Failed to add comment");
            }
            
            var commentDto = _mapper.Map<CommentDTO>(comment);
            return commentDto;
        }

        public async Task<List<CommentDTO>> GetAllComments()
        {
            var comments = await _commentRepo.GetAllComments();
            var commentDtos = _mapper.Map<List<CommentDTO>>(comments);
            return commentDtos;
        }

        public async Task<List<CommentDTO>> GetCommentByUser(int userId)
        {
            User user = await _userRepo.GetUserById(userId);
            if(user == null)
            {
                throw new Exception($"User with id: {userId} is not found");
            }

            var comments = await _commentRepo.GetCommentsByUser(userId);
            var commentDtos = _mapper.Map<List<CommentDTO>>(comments);

            return commentDtos;
        }

        public async Task<List<CommentDTO>> GetCommentByPost(int postId)
        {
            Post post = await _postRepo.GetPostById(postId);

            if(post == null)
            {
                throw new Exception($"post with id: {postId} is not found");
            }

            var comments = await _commentRepo.GetCommentsByPost(postId);
            var commentDtos = _mapper.Map<List<CommentDTO>>(comments);

            return commentDtos;
        }

        public async Task<bool> DeteleComment(int commentId)
        {
            Comment comment = await _commentRepo.GetSingleComment(commentId);
            if(comment == null)
            {
                throw new Exception($"Comment with id: {commentId} is not found");
            }

            return await _commentRepo.DeleteComment(commentId);
        }

    }
}
