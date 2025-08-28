using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IProductsDAL
    {
        bool addproducts(Products product);
        bool deleteproduct(int id);
        List<Products> getproducts();
        Products getproductbyid(int id);
        bool updateproduct(Products products);
    }
}
