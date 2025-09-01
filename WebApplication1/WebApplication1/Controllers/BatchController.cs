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

        //[HttpPost]
        //public IActionResult Create(Batch batch)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (_batchDAL.AddBatchWithDetails(batch, batch.BatchDetails))
        //            {
        //                return RedirectToAction("Login", "Account");
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Data not saved";
        //                return View(batch);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = $"Database Error: {ex.Message}";



        //            return View(batch);
        //        }

        //    }
        //    else
        //    {
        //        ViewBag.Message = "Invalid input";

        //    }
        //    // ✅ Rehydrate SupplierName
        //    var supplier = _supplierDAL.GetsupplierById(batch.supplier_id);
        //    batch.SupplierName = supplier?.name;

        //    // ✅ Rehydrate each ProductName
        //    if (batch.BatchDetails != null)
        //    {
        //        foreach (var d in batch.BatchDetails)
        //        {
        //            var product = _productDAL.getproductbyid(d.product_id);
        //            d.ProductName = product?.name;
        //        }
        //    }
        //        return View(batch);
        //}

        [HttpPost]
        public IActionResult Create(Batch batch)
        {
            // Remove validation errors for display-only properties
            ModelState.Remove("SupplierName");
            if (batch.BatchDetails != null)
            {
                for (int i = 0; i < batch.BatchDetails.Count; i++)
                {
                    ModelState.Remove($"BatchDetails[{i}].ProductName");
                }
            }
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

                        // ✅ Rehydrate before returning view
                        RehydrateBatch(batch);

                        return View(batch);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Database Error: {ex.Message}";

                    // ✅ Rehydrate before returning view
                    RehydrateBatch(batch);

                    return View(batch);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage)
                                  .ToList();

                ViewBag.Message = "Invalid input: " + string.Join(" | ", errors);

                // ✅ Rehydrate before returning view
                RehydrateBatch(batch);

                return View(batch);
            }
        }

        /// <summary>
        /// Helper method to repopulate SupplierName and ProductNames
        /// </summary>
        private void RehydrateBatch(Batch batch)
        {
            // Supplier
            var supplier = _supplierDAL.GetsupplierById(batch.supplier_id);
            batch.SupplierName = supplier?.name;

            // Products
            if (batch.BatchDetails != null)
            {
                foreach (var d in batch.BatchDetails)
                {
                    var product = _productDAL.getproductbyid(d.product_id);
                    d.ProductName = product?.name;
                }
            }
        }


        [HttpGet]
        public IActionResult CheckBatchName(string batchName)
        {
            var exists = _batchDAL.BatchExists(batchName);
            return Json(new { exists });
        }



    }
}
