using System.ComponentModel.DataAnnotations;  // ✅ correct namespace

namespace WebApplication1.Models
{
    public class users
    {
        public int Id { get; set; }
        public int Role_Id { get; set; }
        
        public string Username { get; set; }
        public string Email { get; set; }
      
        public string Password { get; set; }
    }
}
