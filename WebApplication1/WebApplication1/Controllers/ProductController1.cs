using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController1 : Controller
    {
        public readonly IProductsDAL _productdal;

        public ProductController1(IProductsDAL productdal)
        {
            _productdal = productdal;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            ModelState.Clear();
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Products product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var products = new Products
                    {
                        name = product.name,
                        description = product.description,
                        sale_price = product.sale_price,
                        quantity = product.quantity,
                    };
                    if (_productdal.addproducts(products))
                    {
                        ViewBag.Message = "product added succesfully.";
                        return View(new Products());
                    }
                    else
                    {
                        ViewBag.Message = "product not added";
                    }
                }
                else
                {
                    ViewBag.Message = "error in the input.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"error while adding {ex}";
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var producst = _productdal.getproductbyid(id);
            if (producst == null)
            {
                return NotFound();
            }
            return View(producst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct( int id,Products updatedproduct)
        {
            if(id != updatedproduct.product_id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                if (_productdal.updateproduct(updatedproduct))
                {
                    ViewBag.Message = $"{updatedproduct.name} Updated";
                    return RedirectToAction("Show", "ProductController1");
                }
                else 
                { 
                    ViewBag.Message = "error in updating the product";
                }
            }
            else
            {
                ViewBag.Message = "input error";
            }
                return View();
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            bool deleted = _productdal.deleteproduct(id);
            if (deleted)
            {
                return RedirectToAction("Show");
            }
            else
            {
                return BadRequest("Product not deleted");
            }
        }

        [HttpGet]
        public IActionResult show()
        {
            try
                {
                    var products = _productdal.getproducts();
                    return View(products);
                }
            catch (Exception ex) 
                {
                    ViewBag.Message = $"{ex.Message }";
                return View();
                }
        }

       

    }
}