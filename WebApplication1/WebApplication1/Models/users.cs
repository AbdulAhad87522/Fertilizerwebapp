using Microsoft.Build.Framework;

namespace WebApplication1.Models
{
    public class users
    {
        public int Role_Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
