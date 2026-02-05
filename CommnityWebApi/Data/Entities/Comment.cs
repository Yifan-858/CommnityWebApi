using System.ComponentModel.DataAnnotations;

namespace CommnityWebApi.Data.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Comment(string content, int userId, int postId, DateTime createAt)
        {
            Content = content;
            UserId = userId;
            PostId = postId;
            CreateAt = createAt;
        }
    }
}
