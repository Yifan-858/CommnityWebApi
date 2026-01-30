using System.ComponentModel.DataAnnotations;

namespace CommnityWebApi.Data.DTO
{
    public class UserDTO
    {
       public int UserId { get; set; }
       public string UserName { get; set; }
       public string Email { get; set; }

    }
}
