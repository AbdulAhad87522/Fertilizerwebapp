using MySql.Data.MySqlClient;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class BatchDAL :IBatchDAL
    {
        public bool AddBatchWithDetails(Batch batch, List<BatchDetail> details)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert batch
                        string batchQuery = "INSERT INTO batches (batch_name, supplier_id, recieved_date) VALUES (@name, @supplier, @date); SELECT LAST_INSERT_ID();";
                        using (var cmd = new MySqlCommand(batchQuery, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@name", batch.batch_name);
                            cmd.Parameters.AddWithValue("@supplier", batch.supplier_id);
                            cmd.Parameters.AddWithValue("@date", batch.recieved_date);
                            batch.batch_id = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert details
                        foreach (var d in details)
                        {
                            string detailQuery = "INSERT INTO batch_details (batch_id, product_id, cost_price, quantity_recived) VALUES (@batch, @product, @price, @qty)";
                            using (var cmd = new MySqlCommand(detailQuery, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@batch", batch.batch_id);
                                cmd.Parameters.AddWithValue("@product", d.product_id);
                                cmd.Parameters.AddWithValue("@price", d.cost_price);
                                cmd.Parameters.AddWithValue("@qty", d.quantity_recived);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
