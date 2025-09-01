using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication1.Models
{
    public class Batch
    {
        public int batch_id { get; set; }

        [Required(ErrorMessage = "Batch name is required")]
        public string batch_name { get; set; }

        public int supplier_id { get; set; }

        [Required(ErrorMessage = "Received date is required")]
        [DataType(DataType.Date)]
        public DateTime? recieved_date { get; set; }

        public List<BatchDetail> BatchDetails { get; set; }

        [Required(ErrorMessage = "Total price is required")]
        public decimal total_price { get; set; }

        [Required(ErrorMessage = "Paid amount is required")]
        public decimal paid_amount { get; set; }

        // Remove [Required] - this is for display only
        public string SupplierName { get; set; }
    }

    public class BatchDetail
    {
        public int batch_detail_id { get; set; }
        public int batch_id { get; set; }
        public int product_id { get; set; }

        [Required(ErrorMessage = "Cost price is required")]
        public decimal cost_price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int quantity_recived { get; set; }

        // Remove [Required] - this is for display only
        public string ProductName { get; set; }
    }
}
