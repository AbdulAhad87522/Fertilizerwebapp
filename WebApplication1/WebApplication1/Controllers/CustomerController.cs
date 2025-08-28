using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

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

        [HttpGet]
        public IActionResult Add()
        {
            ModelState.Clear();
            return View();
        }

            [HttpPost]
        public IActionResult Add(customer custome)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = new customer
                    {
                        first_name = custome.first_name,
                        last_name = custome.last_name,
                        phone = custome.phone,
                        address = custome.address
                    };
                    if (_customerdal.addcustomer(customer))
                    {
                        ViewBag.Message = "Customer added successfully";
                        ModelState.Clear();
                        return View(new customer());
                    }
                    else
                    {
                        ViewBag.Message = "Customer donot added ";

                    }
                }
                else
                {
                    ViewBag.Message = "Wrong input ";

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View(custome);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
                var customer = _customerdal.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, customer updatedcustomer)
        {
            if(id != updatedcustomer.customer_id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _customerdal.editcustomer(updatedcustomer);
                return RedirectToAction("Show" , "Customer");
            }
            
                return View();
        }

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var customer = _customerdal.GetCustomerById(id);
        //    if(customer == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(customer);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult Delete(int id)
        //{
        //       bool deleted = _customerdal.deletecustomer(id);
        //    if (deleted)
        //    {
        //        return RedirectToAction("Show", "Customer");
        //    }
        //    else 

        //        return NotFound();
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool result = _customerdal.deletecustomer(id);
            if (result)
            {
                return RedirectToAction("Show"); // back to list
            }
            return BadRequest("Failed to delete customer");
        }


        public IActionResult Show()
        {
            try
            {
                List<customer> customers = _customerdal.retreivecustomers();
                return View(customers);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            return View();
            }
        }
    }
}
