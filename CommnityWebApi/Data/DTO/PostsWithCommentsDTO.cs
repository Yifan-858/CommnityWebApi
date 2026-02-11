using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.DTO
{
    public class PostsWithCommentsDTO
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; } 
        public List<string>? Category{ get; set; } = new List<string>();
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<CommentDTO>? Comments{ get; set; } = new List<CommentDTO>();

    }
}
