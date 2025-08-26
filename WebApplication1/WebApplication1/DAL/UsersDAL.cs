using MySql.Data.MySqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Interfaces;

namespace WebApplication1.DAL
{
    public class UsersDAL : IuserDAL
    {
        public bool registeruser(users us)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "insert into users (username , password_hash , full_name)" +
                    "value (@username ,@password , @email)";
                using(MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@username", us.Username);
                    cmd.Parameters.AddWithValue("@username", us.Password);
                    cmd.Parameters.AddWithValue("@username", us.Email);
                    cmd.Parameters.AddWithValue("@username", us.Role);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            return true;
        }
    }
}
