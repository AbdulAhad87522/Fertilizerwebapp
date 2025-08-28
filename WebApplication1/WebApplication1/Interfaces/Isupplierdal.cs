using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface Isupplierdal
    {
        bool addsupplier(Suppliers supplier);
        List<Suppliers> retrievesuppliers();
        bool deletesupplier(int id);
        bool editsupplier(Suppliers supplier);
        Suppliers GetsupplierById(int id);
    }
}
