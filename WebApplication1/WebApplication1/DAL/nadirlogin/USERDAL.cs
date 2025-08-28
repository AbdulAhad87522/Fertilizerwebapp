using MySql.Data.MySqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.Sign_in;

namespace WebApplication1.DAL.nadirlogin
{
    public class USERDAL : IUSERDALinter
    {
        // Helper method to safely read strings (handles NULL)
        private string? SafeGetString(MySqlDataReader reader, string column)
        {
            int colIndex = reader.GetOrdinal(column);
            return reader.IsDBNull(colIndex) ? null : reader.GetString(colIndex);
        }

        public bool Adduser(UserModel user)
        {
            int role_id = DatabaseHelper.getroleid(user.role_name);
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO users 
                                 (username, password_hash, email, role_id, full_name) 
                                 VALUES (@username, @password, @email, @roleId, @name)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password); // Already hashed in controller
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@roleId", role_id);
                    cmd.Parameters.AddWithValue("@name", user.Name);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public UserModel? GetUserByUsername(string username)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT u.user_id, u.username, u.password_hash, u.email, 
                                        u.role_id, u.full_name, r.role_name 
                                 FROM users u 
                                 JOIN roles r ON r.role_id = u.role_id 
                                 WHERE u.username = @username";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                Id = reader.GetInt32("user_id"),
                                Username = reader.GetString("username"),
                                Password = SafeGetString(reader, "password_hash"),
                                Email = reader.GetString("email"),
                                RoleId = reader.GetInt32("role_id"),
                                Name = reader.GetString("full_name"),
                                role_name = reader.GetString("role_name")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public UserModel? GetUserByEmail(string email)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"SELECT u.user_id, u.username, u.password_hash, u.email, 
                                        u.role_id, u.full_name, r.role_name 
                                 FROM users u 
                                 JOIN roles r ON r.role_id = u.role_id 
                                 WHERE u.email = @Email";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserModel
                            {
                                Id = reader.GetInt32("user_id"),
                                Username = reader.GetString("username"),
                                Password = SafeGetString(reader, "password_hash"),
                                Email = reader.GetString("email"),
                                RoleId = reader.GetInt32("role_id"),
                                Name = reader.GetString("full_name"),
                                role_name = reader.GetString("role_name")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
