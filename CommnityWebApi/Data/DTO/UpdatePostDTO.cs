using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.DTO
{
    public class UpdatePostDTO
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; } 
        public List<Category>? Category{ get; set; }
    }
}
