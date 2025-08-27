using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        public readonly IuserDAL _userdal;

        public AccountController(IuserDAL userdal)
        {
            _userdal = userdal;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(users user)
        {
            users u = _userdal.getdata(user.Email, user.Password);
            if (u != null)
            {
                HttpContext.Session.SetString("Username", u.Username);
                HttpContext.Session.SetInt32("Role_ID", u.Role_Id);
                ViewBag.Message = $"welcome {u.Username}";
                return RedirectToAction("Signup" , "Account");  
            }
            else
            {
                ViewBag.Message = "Invalid email or password";
                return View();
            }

            //return View();
        }


        [HttpGet]
        public IActionResult Signup()
        {
            return View(new users());
        }

        [HttpPost]
        public IActionResult Signup(users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newuser = new users
                    {
                        Username = user.Username,
                        Email = user.Email,
                        Password = user.Password,
                        Role_Id = user.Role_Id
                    };


                    if (_userdal.registeruser(newuser))
                    {
                        ViewBag.Message = "Signup successful!";
                    }
                    else
                    {
                        ViewBag.Message = "error while sign up";
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid data";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    ViewBag.Message = error.ErrorMessage; // check in output window
                }
            }

            return View(user);
        }
    }
}
