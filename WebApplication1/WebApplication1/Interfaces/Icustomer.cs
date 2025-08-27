using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface Icustomer
    {
        bool addcustomer(customer cust);
        customer retreivecustomers();
        bool deletecustomer(string username);
    }
}
