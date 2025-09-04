namespace WebApplication1.Models
{
    public class CustomerBillDetail
    {
       
            public int BillDetailId { get; set; }
            public int BillId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public int Discount { get; set; }
            public string Status { get; set; }
        
    }
}
