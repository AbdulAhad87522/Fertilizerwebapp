namespace WebApplication1.Models.Sign_in
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string Password { get; set; }
        public required string Username { get; set; }
        public int RoleId { get; set; }
        public string? role_name { get; set; }
    }
    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
    public class RegisterModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public  string? Password { get; set; }
        public int RoleId { get; set; }
        public string? role_name { get; set; }
    }
}
