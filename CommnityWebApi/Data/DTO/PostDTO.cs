using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.DTO
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string Text { get; set; } 
        public List<Category> Category { get; set; }
        public int UserId { get; set; }
    }
}
