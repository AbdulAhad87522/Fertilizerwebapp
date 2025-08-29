using System.Data;
using MySql.Data.MySqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class SuplierDAL : Isupplierdal
    {
        public bool addsupplier(Suppliers supplier)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "insert into suppliers (name  , phone , address) values (@name , @phone ,@address)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", supplier.name);
                        cmd.Parameters.AddWithValue("@phone", supplier.phone);
                        cmd.Parameters.AddWithValue("@address", supplier.address);
                        return cmd.ExecuteNonQuery() > 0;

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
                return false;
        }

        public bool deletesupplier(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "delete from suppliers where supplier_id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool editsupplier(Suppliers supplier)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = "UPDATE suppliers " +
                                   "SET name = @name, phone = @phone, address = @address " +
                                   "WHERE supplier_id = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", supplier.name);
                        cmd.Parameters.AddWithValue("@phone", supplier.phone);
                        cmd.Parameters.AddWithValue("@address", supplier.address);
                        cmd.Parameters.AddWithValue("@id", supplier.supplier_id);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // returns true if update succeeded
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Suppliers GetsupplierById(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM suppliers WHERE supplier_id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Suppliers
                            {
                                supplier_id = reader.GetInt32("supplier_id"),
                                name = reader.GetString("name"),
                                phone = reader.GetString("phone"),
                                address = reader.GetString("address")
                            };
                        }
                    }
                }
            }
            return null; // not found
        }

        public List<Suppliers> retrievesuppliers()
        {
            try
            {
                List<Suppliers> suppliers = new List<Suppliers>();
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "select * from suppliers";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var supplier = new Suppliers
                                {
                                    supplier_id = reader.IsDBNull("supplier_id") ? 0 : reader.GetInt32("supplier_id"),
                                    name = reader.IsDBNull("name") ? string.Empty : reader.GetString("name"),
                                    phone = reader.IsDBNull("phone") ? string.Empty : reader.GetString("phone"),
                                    address = reader.IsDBNull("address") ? string.Empty : reader.GetString("address")
                                };
                                suppliers.Add(supplier);
                            }
                        }
                    }

                }
                return suppliers;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public List<Suppliers> SearchSuppliers(string term)
        {
            var suppliers = new List<Suppliers>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT supplier_id, name FROM suppliers WHERE name LIKE @term";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@term", "%" + term + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Suppliers
                            {
                                supplier_id = reader.GetInt32("supplier_id"),
                                name = reader.GetString("name"),
                                address = "",
                                phone = ""
                            });
                        }
                    }
                }
            }
            return suppliers;
        }


    }
}
