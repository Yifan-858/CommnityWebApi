namespace CommnityWebApi.Data.DTO
{
    public class PublishDTO
    {
        public string? Title { get; set; }
        public string? Text { get; set; } 
        public List<string>? Category { get; set; } = new List<string>();
    }
}
