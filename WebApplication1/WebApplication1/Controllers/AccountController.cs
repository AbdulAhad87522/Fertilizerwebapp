using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        public static IuserDAL _userdal;

        public AccountController(IuserDAL _userdal)
        {
            _userdal = _userdal;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(users user)
        {
            if(ModelState.IsValid)
            {
                var newuser = new users
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password
                };
                 
                ViewBag.Message = "Signup successful!";
            }
            return View();
        }
    }
}
