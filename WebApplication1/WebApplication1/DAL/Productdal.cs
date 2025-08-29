using System.Data;
using MySql.Data.MySqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class Productdal : IProductsDAL
    {
        public bool addproducts(Products product)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "insert into products (name , description , sale_price , quantity) values (@name , @description , @sale_price ,@quantity)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", product.name);
                    cmd.Parameters.AddWithValue("@description", product.description);
                    cmd.Parameters.AddWithValue("@sale_price", product.sale_price);
                    cmd.Parameters.AddWithValue("@quantity", product.quantity);
                    return cmd.ExecuteNonQuery() > 0;

                }
            }
        }

        public bool deleteproduct(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "delete from products where product_id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Products getproductbyid(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM products WHERE product_id = @id";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Products
                            {
                                product_id = reader.GetInt32("product_id"),
                                name = reader.GetString("name"),
                                description = reader.GetString("description"),
                                sale_price = reader.GetInt32("sale_price"),
                                quantity = reader.GetInt32("quantity")
                            };
                        }
                    }
                }
            }
            return null; // not found
        }


        public List<Products> getproducts()
        {
            try
            {
                var product = new List<Products>();
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "select * from products";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var products = new Products
                                {
                                    product_id = reader.IsDBNull("product_id") ? 0 : reader.GetInt32("product_id"),
                                    name = reader.IsDBNull("name") ? string.Empty : reader.GetString("name"),
                                    description = reader.IsDBNull("description") ? string.Empty : reader.GetString("description"),
                                    sale_price = reader.IsDBNull("sale_price") ? 0 : reader.GetInt32("sale_price"),
                                    quantity = reader.IsDBNull("quantity") ? 0 : reader.GetInt32("quantity")
                                };
                                product.Add(products);
                            }
                        }
                    }
                }
                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public bool updateproduct(Products products)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = "UPDATE products " +
                                   "SET name = @name, description = @description, sale_price = @sale_price  , quantity = @quantity " +
                                   "WHERE product_id = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", products.name);
                        cmd.Parameters.AddWithValue("@description", products.description);
                        cmd.Parameters.AddWithValue("@sale_price", products.sale_price);
                        cmd.Parameters.AddWithValue("@quantity", products.quantity);
                        cmd.Parameters.AddWithValue("@id", products.product_id);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // returns true if update succeeded
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Products> SearchProducts(string term)
        {
            var products = new List<Products>();

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                string query = "SELECT product_id, name FROM products WHERE name LIKE @term";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@term", "%" + term + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Products
                            {
                                product_id = reader.GetInt32("product_id"),
                                name = reader.GetString("name"),
                                description = "", // placeholder
                                sale_price = 0,   // placeholder
                                quantity = 0      // placeholder
                            });
                        }
                    }
                }
            }

            return products;
        }


    }
}
