using WebApplication1.Models.Sign_in;

namespace WebApplication1.Interfaces
{
    public interface IUSERDALinter
    {
        bool Adduser(UserModel user);
        UserModel? GetUserByEmail(string email);
        UserModel? GetUserByUsername(string username);
    }
}