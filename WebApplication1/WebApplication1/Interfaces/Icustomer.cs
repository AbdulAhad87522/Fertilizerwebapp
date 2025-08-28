using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface Icustomer
    {
        bool addcustomer(customer cust);
        List<customer> retreivecustomers();
        bool deletecustomer(int id);
        bool editcustomer(customer customer);
        customer GetCustomerById(int id);
    }
}
