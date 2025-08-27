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
         public customer retreivecustomers()
        {
            return null;
        }
        public bool deletecustomer(string username)
        {
            return false;
        }
    }
}
