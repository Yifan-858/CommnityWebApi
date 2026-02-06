using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommnityWebApi.Data.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        //Many to many
        public List<Category> Categories { get; set; } = new();

        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public Post(string? title, string? text, int userId)
        {
            Title = title?? "Untitled";
            Text = text ?? string.Empty;
            UserId = userId;
        }
    }



}
