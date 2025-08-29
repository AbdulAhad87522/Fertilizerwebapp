using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BatchController : Controller
    {
        private readonly IBatchDAL _batchDAL;
        private readonly Isupplierdal _supplierDAL;
        private readonly IProductsDAL _productDAL;

        public BatchController(IBatchDAL batchDAL, Isupplierdal supplierDAL, IProductsDAL productDAL)
        {
            _batchDAL = batchDAL;
            _supplierDAL = supplierDAL;
            _productDAL = productDAL;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Suppliers = _supplierDAL.retrievesuppliers();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Batch batch, List<BatchDetail> details)
        {
            if (ModelState.IsValid)
            {
                _batchDAL.AddBatchWithDetails(batch, details);
                return RedirectToAction("Index"); // later show all batches
            }
            return View(batch);
        }
    }

}
