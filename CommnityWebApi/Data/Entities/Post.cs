using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommnityWebApi.Data.Entities
{
    //public enum Category
    //{
    //    Art = 0,
    //    Entertainment = 1,
    //    Food = 2,
    //    Lifestyle = 3,
    //    Technology = 4
    //}
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; } 
        //Many to many
        public List<Category> Category { get; set; }

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
