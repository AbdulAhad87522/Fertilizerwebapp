using MySql.Data.MySqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class Customerdal : Icustomer
    {
        public   bool addcustomer(customer cust)
        { 
            using(var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "insert into customers (first_name , last_name , phone , address) values (@f_name , @l_name , @phone ,@address)";
                using(var cmd = new MySqlCommand(query , conn))
                {
                    cmd.Parameters.AddWithValue("@f_name", cust.first_name);
                    cmd.Parameters.AddWithValue("@l_name", cust.last_name);
                    cmd.Parameters.AddWithValue("@phone", cust.phone);
                    cmd.Parameters.AddWithValue("@address", cust.address);


                    return cmd.ExecuteNonQuery() > 0;

                }
            }
        }
         public List<customer> retreivecustomers()
        {
            try
            {
                var customers = new List<customer>();
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "select * from customers";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var customer = new customer
                                {
                                    customer_id = reader.GetInt32("customer_id"),
                                    first_name = reader.GetString("first_name"),
                                    last_name = reader.GetString("last_name"),
                                    phone = reader.GetString("phone"),
                                    address = reader.GetString("address")
                                };
                                customers.Add(customer);
                            }
                        }
                    }
                }
                            return customers;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        public bool deletecustomer(int id)
        {
            using(var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "delete from customers where customer_id = @id";
                using(var cmd = new MySqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@id" , id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool editcustomer(customer customer)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = "UPDATE customers " +
                               "SET first_name = @firstname, last_name = @lastname, phone = @phone, address = @address " +
                               "WHERE customer_id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@firstname", customer.first_name);
                    cmd.Parameters.AddWithValue("@lastname", customer.last_name);
                    cmd.Parameters.AddWithValue("@phone", customer.phone);
                    cmd.Parameters.AddWithValue("@address", customer.address);
                    cmd.Parameters.AddWithValue("@id", customer.customer_id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // returns true if update succeeded
                }
            }
        }

        public customer GetCustomerById(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM customers WHERE customer_id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new customer
                            {
                                customer_id = reader.GetInt32("customer_id"),
                                first_name = reader.GetString("first_name"),
                                last_name = reader.GetString("last_name"),
                                phone = reader.GetString("phone"),
                                address = reader.GetString("address")
                            };
                        }
                    }
                }
            }
            return null; // not found
        }


    }
}
