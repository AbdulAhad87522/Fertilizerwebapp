using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Linq;
using WebApplication1.Models;

public class SalesController : Controller
{
    private readonly YourDbContext _context;

    public SalesController(YourDbContext context)
    {
        _context = context;
    }

    // Search product
    [HttpGet]
    public IActionResult SearchProduct(string query)
    {
        var products = _context.Products
            .Where(p => p.Name.Contains(query))
            .Select(p => new {
                p.ProductId,
                p.Name,
                p.Description,
                p.SalePrice,
                p.Quantity
            }).ToList();

        return Json(products);
    }

    // Search customer
    [HttpGet]
    public IActionResult SearchCustomer(string query)
    {
        var customers = _context.Customers
            .Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query))
            .Select(c => new {
                c.CustomerId,
                Name = c.FirstName + " " + c.LastName,
                c.Phone,
                c.Address
            }).ToList();

        return Json(customers);
    }

    // Save Sale
    [HttpPost]
    public IActionResult Create([FromBody] CustomerBill bill)
    {
        bill.SaleDate = DateTime.Now;
        bill.PaymentStatus = (bill.TotalPrice == bill.PaidAmount) ? "Paid" : "Due";

        _context.CustomerBills.Add(bill);
        _context.SaveChanges();

        return Json(new { success = true, billId = bill.BillID });
    }
}
