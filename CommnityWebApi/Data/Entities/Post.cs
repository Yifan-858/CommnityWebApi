using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommnityWebApi.Data.Entities
{ 
    public enum Category
    {   Art,
        Entertainment,
        Food, 
        Lifestyle, 
        Technology   
    }
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Category> Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
