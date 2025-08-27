using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IuserDAL
    {
        bool registeruser(users us);
        users getdata(string email , string password);

    }

}
