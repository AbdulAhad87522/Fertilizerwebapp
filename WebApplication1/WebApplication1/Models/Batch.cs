namespace WebApplication1.Models
{
    public class Batch
    {
        public int batch_id { get; set; }
        public string batch_name { get; set; }
        public int supplier_id { get; set; }
        public DateTime recieved_date { get; set; }

        public Suppliers Supplier { get; set; } // Navigation
        public List<BatchDetail> BatchDetails { get; set; }
    }

    public class BatchDetail
    {
        public int batch_detail_id { get; set; }
        public int batch_id { get; set; }
        public int product_id { get; set; }
        public decimal cost_price { get; set; }
        public int quantity_recived { get; set; }

        public Batch Batch { get; set; }
        public Products Product { get; set; }
    }

}
