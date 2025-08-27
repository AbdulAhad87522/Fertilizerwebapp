using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        public readonly Icustomer _customerdal;

        public CustomerController(Icustomer customerdal)
        {
            _customerdal = customerdal;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            if(ModelState.IsValid)
            {

            }
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Show()
        {
            return View();
        }
    }
}
