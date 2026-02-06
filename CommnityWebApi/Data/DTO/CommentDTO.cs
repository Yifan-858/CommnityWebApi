using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public int? PostId { get; set; }
        public string? PostTitle { get; set; }

    }
}
