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

            [HttpGet]
            public IActionResult Show()
            {
                try
                {

                    var suppliers = _supplierdal.retrievesuppliers();

                    return View(suppliers);
                }
                catch(Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                return View();
            }

            [HttpPost]
            public IActionResult Delete(int id)
            {
                if(_supplierdal.deletesupplier(id))
                {
                    return RedirectToAction("Show");
                }
                else
                {
                    return BadRequest("supplier not deleted");
                }

            }
            [HttpGet]
            public IActionResult Edit(int id)
            {
                var supplier = _supplierdal.GetsupplierById(id);
                if (supplier == null) return NotFound();

                return PartialView("_EditSupplierPartial", supplier); // ✅ Must match filename
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, Suppliers supplier)
            {
                if (ModelState.IsValid)
                {
                    if (_supplierdal.editsupplier(supplier))
                    {
                        return Json(new { success = true });
                        ViewBag.Message = "supplier edited";
                    }
                    else
                    {
                        ViewBag.Message = "supplier not edited ";
                    }
                }

                return PartialView("_EditSupplierPartial", supplier);
            }

        [HttpGet]
        public JsonResult Search(string term)
        {
            var suppliers = _supplierdal.SearchSuppliers(term); // implement in DAL like you did for products
            return Json(suppliers.Select(s => new { id = s.supplier_id, label = s.name, value = s.name }));
        }


    }

}
