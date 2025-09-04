namespace WebApplication1.Models
{
    public class CustomerBill
    {
     
            public int BillID { get; set; }
            public int CustomerID { get; set; }
            public DateTime SaleDate { get; set; }
            public int TotalPrice { get; set; }
            public int PaidAmount { get; set; }
            public string PaymentStatus { get; set; }
            public List<CustomerBillDetail> BillDetails { get; set; }
        
    }
}
