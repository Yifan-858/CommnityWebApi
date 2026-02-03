using CommnityWebApi.Data.Entities;

namespace CommnityWebApi.Data.DTO
{
    public class PublishDTO
    {
        public string? Title { get; set; }
        public string? Text { get; set; } 
        public List<int>? Category { get; set; } = new List<int>();
    }
}
