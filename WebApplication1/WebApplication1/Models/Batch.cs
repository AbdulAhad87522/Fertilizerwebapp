using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Batch
    {
        public int batch_id { get; set; }
        [Required(ErrorMessage ="Batch name is required")]
        public string batch_name { get; set; }
        [Required]
        public int supplier_id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime recieved_date { get; set; }
        public List<BatchDetail> BatchDetails { get; set; }
    }

    public class BatchDetail
    {
        public int batch_detail_id { get; set; }
        public int batch_id { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public decimal cost_price { get; set; }

        [Required]
        public int quantity_recived { get; set; }
    }
    

}
