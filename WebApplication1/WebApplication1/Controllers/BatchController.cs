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

        //[HttpPost]
        //public IActionResult Create(Batch batch, List<BatchDetail> details)
        //{
        //    //if (ModelState.IsValid)
        //    {
        //        if (_batchDAL.AddBatchWithDetails(batch, details))
        //        {
        //            return RedirectToAction("Login", "Account"); // later show all batches
        //        }
        //        else
        //        {
        //            ViewBag.Message = "data not saved";
        //        }
        //        //}
        //        //else
        //        //{
        //        //    ViewBag.Message = "invalid input";
        //        //}
        //        return View(batch);
        //    }
        //}

        [HttpPost]
        public IActionResult Create(Batch batch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_batchDAL.AddBatchWithDetails(batch, batch.BatchDetails))
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewBag.Message = "Data not saved";
                        return View(batch);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Database Error: {ex.Message}";

                    // Populate helper names
                    //var supplier = _supplierDAL.GetsupplierById(batch.supplier_id);
                    //batch.SupplierName = supplier?.name;

                    //foreach (var d in batch.BatchDetails)
                    //{
                    //    var product = _productDAL.getproductbyid(d.product_id);
                    //    d.ProductName = product?.name;
                    //}

                    return View(batch);
                }

            }
            else
            {
                ViewBag.Message = "Invalid input";
               
            }
            // ✅ Rehydrate SupplierName
            var supplier = _supplierDAL.GetsupplierById(batch.supplier_id);
            batch.SupplierName = supplier?.name;

            // ✅ Rehydrate each ProductName
            if (batch.BatchDetails != null)
            {
                foreach (var d in batch.BatchDetails)
                {
                    var product = _productDAL.getproductbyid(d.product_id);
                    d.ProductName = product?.name;
                }
            }
            return View(batch);
        }
        [HttpGet]
        public IActionResult CheckBatchName(string batchName)
        {
            var exists = _batchDAL.BatchExists(batchName);
            return Json(new { exists });
        }



    }
}
