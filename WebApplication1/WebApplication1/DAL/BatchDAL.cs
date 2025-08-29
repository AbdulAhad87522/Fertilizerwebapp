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
                        // 1️⃣ Insert batch
                        string batchQuery = @"INSERT INTO batches (batch_name, supplier_id, recieved_date) 
                                      VALUES (@name, @supplier, @date); 
                                      SELECT LAST_INSERT_ID();";
                        using (var cmd = new MySqlCommand(batchQuery, conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@name", batch.batch_name);
                            cmd.Parameters.AddWithValue("@supplier", batch.supplier_id);
                            cmd.Parameters.AddWithValue("@date", batch.recieved_date);
                            batch.batch_id = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 2️⃣ Insert batch details + update product qty
                        foreach (var d in details)
                        {
                            string detailQuery = @"INSERT INTO batch_details 
                                           (batch_id, product_id, cost_price, quantity_recived) 
                                           VALUES (@batch, @product, @price, @qty)";
                            using (var cmd = new MySqlCommand(detailQuery, conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@batch", batch.batch_id);
                                cmd.Parameters.AddWithValue("@product", d.product_id);
                                cmd.Parameters.AddWithValue("@price", d.cost_price);
                                cmd.Parameters.AddWithValue("@qty", d.quantity_recived);
                                cmd.ExecuteNonQuery();
                            }

                            string updateQuery = @"UPDATE products 
                                           SET quantity = quantity + @qty 
                                           WHERE product_id = @id";
                            using (var cmdUpdate = new MySqlCommand(updateQuery, conn, tran))
                            {
                                cmdUpdate.Parameters.AddWithValue("@qty", d.quantity_recived);
                                cmdUpdate.Parameters.AddWithValue("@id", d.product_id);
                                cmdUpdate.ExecuteNonQuery();
                            }
                        }

                        // 3️⃣ Insert supplier bill
                        int supplierBillId;
                        string billQuery = @"INSERT INTO supplierbills 
                                     (supplier_id, Date, total_price, batch_id, paid_amount, payment_status) 
                                     VALUES (@supplier, @date, @total, @batch, @paid, 
                                     CASE WHEN @paid >= @total THEN 'Paid' ELSE 'Due' END);
                                     SELECT LAST_INSERT_ID();";
                        using (var cmdBill = new MySqlCommand(billQuery, conn, tran))
                        {
                            cmdBill.Parameters.AddWithValue("@supplier", batch.supplier_id);
                            cmdBill.Parameters.AddWithValue("@date", batch.recieved_date);
                            cmdBill.Parameters.AddWithValue("@total", batch.total_price);
                            cmdBill.Parameters.AddWithValue("@batch", batch.batch_id);
                            cmdBill.Parameters.AddWithValue("@paid", batch.paid_amount);
                            supplierBillId = Convert.ToInt32(cmdBill.ExecuteScalar());
                        }

                        // 4️⃣ Insert supplier bill details (per product in batch)
                        foreach (var d in details)
                        {
                            string billDetailQuery = @"INSERT INTO supplier_bill_details 
                                               (supplier_Bill_ID, product_id, quantity) 
                                               VALUES (@bill, @product, @qty)";
                            using (var cmdBillDetail = new MySqlCommand(billDetailQuery, conn, tran))
                            {
                                cmdBillDetail.Parameters.AddWithValue("@bill", supplierBillId);
                                cmdBillDetail.Parameters.AddWithValue("@product", d.product_id);
                                cmdBillDetail.Parameters.AddWithValue("@qty", d.quantity_recived);
                                cmdBillDetail.ExecuteNonQuery();
                            }
                        }

                        tran.Commit();
                        return true;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool BatchExists(string batchName)
        {
            using (var conn = DatabaseHelper.GetConnection()) // from your DatabaseHelper
            {
                string query = "SELECT COUNT(*) FROM batches WHERE batch_name = @batchName";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@batchName", batchName);
                    conn.Open();
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }


    }
}
