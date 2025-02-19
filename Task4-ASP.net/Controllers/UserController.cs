using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace Task4_ASP.net.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
  

public IActionResult HandelRegister(string firstName, string lastName, string email, string password, string repeatPassword)
    {
        // التحقق من صحة الاسم (يجب أن يحتوي فقط على أحرف)
        if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
        {
            ViewBag.ErrorMessage = "First name must contain only letters.";
            return View("Register");
        }

        if (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
        {
            ViewBag.ErrorMessage = "Last name must contain only letters.";
            return View("Register");
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            ViewBag.ErrorMessage = "Invalid email format.";
            return View("Register");
        }

       

        if (password != repeatPassword)
        {
            ViewBag.ErrorMessage = "Passwords do not match.";
            return View("Register");
        }

    
        HttpContext.Session.SetString("Email", email);
        HttpContext.Session.SetString("Password", password);
            HttpContext.Session.SetString("FirstName", firstName);
            HttpContext.Session.SetString("LasttName", lastName);



            return RedirectToAction("Login");
    }

    public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult HandelLogin(string email, string password )
        {
            string storedEmail = HttpContext.Session.GetString("Email");
            string storedPassword = HttpContext.Session.GetString("Password");
            string firstName = HttpContext.Session.GetString("FirstName");
            string lastName = HttpContext.Session.GetString("LastName");




            if (email == storedEmail && password == storedPassword)
            {
                TempData["GreetingMessage"] = $"Hello {firstName} {lastName}";


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password. Please try again.";
                return View("Login");
            }
        }

    }

}
