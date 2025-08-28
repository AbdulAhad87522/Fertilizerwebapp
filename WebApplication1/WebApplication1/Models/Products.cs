using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Products
    {
        public int product_id {  get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public int sale_price { get; set; }
        public int quantity { get; set; }
    }
}
