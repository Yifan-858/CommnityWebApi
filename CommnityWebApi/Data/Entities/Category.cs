using System.ComponentModel.DataAnnotations;

namespace CommnityWebApi.Data.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}
