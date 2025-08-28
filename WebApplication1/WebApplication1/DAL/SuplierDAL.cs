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
            throw new NotImplementedException();
        }

        public bool editsupplier(Suppliers supplier)
        {
            throw new NotImplementedException();
        }

        public Suppliers GetsupplierById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Suppliers> retrievesuppliers()
        {
            throw new NotImplementedException();
        }
    }
}
