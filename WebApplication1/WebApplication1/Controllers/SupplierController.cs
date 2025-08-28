using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SupplierController : Controller
    {
        public readonly Isupplierdal _supplierdal;

        public SupplierController(Isupplierdal supplierdal)
        {
            _supplierdal = supplierdal;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Suppliers supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var suppliers = new Suppliers
                    {
                        name = supplier.name,
                        address = supplier.address,
                        phone = supplier.phone
                    };
                    if (_supplierdal.addsupplier(supplier))
                    {
                        ViewBag.Message = "Supplier added suuccessfuly";
                        return View(new Suppliers());
                    }
                    else
                    {
                        ViewBag.Message = "not added";
                    }
                }
                else
                {
                    ViewBag.Message = "invalid data enterd";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
                return View();
            }

        [HttpPost]
        public IActionResult Show()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();

        }

    }

}
