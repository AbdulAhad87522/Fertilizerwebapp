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
                string query = "insert into users (username , password_hash , Email, Role_Id)" +
                    "value (@username ,@password , @email, @roleid)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", us.Username);
                    cmd.Parameters.AddWithValue("@password", us.Password);
                    cmd.Parameters.AddWithValue("@email", us.Email);
                    cmd.Parameters.AddWithValue("@roleid", us.Role_Id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public users getdata(string Email, string password)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "select user_id, username , Role_Id from users where Email = @email and password_hash = @password";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", Email);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new users
                            {
                                Id = reader.GetInt32("user_id"),
                                Username = reader.GetString("username"),
                                //Password = reader.GetString("password_hash"),
                                Role_Id = reader.GetInt32("Role_Id")
                                //Email = reader.GetString("Email")
                            };
                        }

                    }
                }
            }
            return null;
        }
    }
}
