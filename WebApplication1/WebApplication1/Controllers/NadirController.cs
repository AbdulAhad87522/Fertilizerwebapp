using ItecwebApp.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.Sign_in;

namespace WebApplication1.Controllers
{
    public class NadirController : Controller
    {
        public IUSERDALinter idl;
        public EmailService em;
        public NadirController(IUSERDALinter idl, EmailService em)
        {
            this.idl = idl;
            this.em = em;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {

            if (!ModelState.IsValid)
            { return View(model); }
            string roletoassign = "user";
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin") && !string.IsNullOrEmpty(model.role_name))
            {
                roletoassign = model.role_name;
            }
            var user = new UserModel
            {
                Username = model.Username,
                Email = model.Email,
                Password = PasswordHelper.HashPassword(model.Password),
                role_name = roletoassign,
                Name = model.Name,

            };
            if(!User.Identity.IsAuthenticated && roletoassign == "user")
            {
                string otp = new Random().Next(100000, 999999).ToString();
                HttpContext.Session.SetString("otp",otp);
                HttpContext.Session.SetString("email",model.Email);
                HttpContext.Session.SetString("username",model.Username);
                HttpContext.Session.SetString("password",PasswordHelper.HashPassword( model.Password));
                HttpContext.Session.SetString("name",model.Name);
                HttpContext.Session.SetString("role", model.role_name);
                em.SendEmail(model.Email, "Verify Your Email", $"Your OTP is: <b>{otp}</b>");

            }
            if (idl.Adduser(user))
            {
                TempData["SuccessMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Registration failed. Username or Email might already exist.");
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel u)
        {
            try
            {
                var username = u.Username;
                var password = u.Password; // <-- plain password

                var user = idl.GetUserByUsername(username);

                if (user != null && PasswordHelper.VerifyPassword(password, user.Password))
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.role_name) // actual role
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddHours(2)
                        });

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult verifyotp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult verifyotp(string enterotp)
        {
            string savedotp = HttpContext.Session.GetString("otp");
            if (!string.IsNullOrEmpty(savedotp) && savedotp == enterotp)
            {
                var user = new UserModel {

                    Email= HttpContext.Session.GetString("email"),
                    Username= HttpContext.Session.GetString("username"),
                    Name = HttpContext.Session.GetString("name"),
                    Password = HttpContext.Session.GetString("password"),
                    role_name = HttpContext.Session.GetString("role"),

                };
                if (idl.Adduser(user))
                {
                    HttpContext.Session.Clear();

                    TempData["SuccessMessage"] = "Registration successful! Please log in.";
                    return RedirectToAction("Login");
                }
               

            }
            ViewBag.error = "Invalid OTP. Please try again.";
            return View();
        }
        [HttpGet]
        public IActionResult ExternalLogin(string provider, string returnUrl = "/")
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Nadir", new { returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, provider); // provider must be "Google" or "GitHub"
        }
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null)
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");

            // Extract claims
            var email = User.FindFirstValue(ClaimTypes.Email);
            var name = User.FindFirstValue(ClaimTypes.Name) ?? email;

            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "External login failed: no email returned.";
                return RedirectToAction("Login");
            }

            var existingUser = idl.GetUserByEmail(email);

            if (existingUser == null)
            {
                var newUser = new UserModel
                {
                    Name = name,
                    Username = email,
                    Email = email,
                    role_name = "Student",
                    Password = null
                };

                idl.Adduser(newUser);
                existingUser = idl.GetUserByEmail(email);
            }

            // Build claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, existingUser.Username),
        new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
        new Claim(ClaimTypes.Email, existingUser.Email),
        new Claim(ClaimTypes.Role, existingUser.role_name)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            // Always go to Home/Index after success
            return RedirectToAction("Index", "Home");
        }


    }
}
