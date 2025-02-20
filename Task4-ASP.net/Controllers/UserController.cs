using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace Task4_ASP.net.Controllers
{
    public class UserController : Controller
    {
        string AdminEmail = "rudainaalyasein@gmail.com";
        string password = "12345";
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
            string id = Guid.NewGuid().ToString();
            string status = "true";
            string phone="";
            HttpContext.Session.SetString("Status", status);
            HttpContext.Session.SetString("ID", id);

            HttpContext.Session.SetString("Email", email);
        HttpContext.Session.SetString("Password", password);
            HttpContext.Session.SetString("FirstName", firstName);
            HttpContext.Session.SetString("LastName", lastName);
            HttpContext.Session.SetString("Phone", phone);




            return RedirectToAction("Login");
    }

        public IActionResult Login()
        {
           
            string storedEmail = Request.Cookies["UserEmail"];
            string storedPassword = Request.Cookies["UserPassword"];

            if (storedEmail != null && storedPassword != null)
            {
                string firstName = HttpContext.Session.GetString("FirstName");
                string lastName = HttpContext.Session.GetString("LastName");
                HttpContext.Session.SetString("Status", "true");

                TempData["GreetingMessage"] = $"Welcome back, {firstName} {lastName}";
                if (storedEmail==AdminEmail && storedPassword == password)
                {
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult HandelLogin(string email, string password, string rem) 
        {
            string storedEmail = Request.Cookies["UserEmail"];
            string storedPassword = Request.Cookies["UserPassword"];

            if (storedEmail != null )
            {
                string firstName = HttpContext.Session.GetString("FirstName");
                string lastName = HttpContext.Session.GetString("LastName");
                if (storedEmail == AdminEmail && storedPassword == password) {
                    TempData["GreetingMessage"] = $"Welcome back, {firstName} {lastName}";

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }


            }
            else
            {
                string sessionEmail = HttpContext.Session.GetString("Email");
                string sessionPassword = HttpContext.Session.GetString("Password");
                string firstName = HttpContext.Session.GetString("FirstName");
                string lastName = HttpContext.Session.GetString("LastName");

                if (email == sessionEmail && password == sessionPassword)
                {
                    TempData["GreetingMessage"] = $"Hello {firstName} {lastName}";

                    if (rem == "yes" && AdminEmail!= sessionEmail && password!= sessionPassword)
                    {
                        CookieOptions options = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(7)
                        };
                        Response.Cookies.Append("UserEmail", email, options);
                        Response.Cookies.Append("UserPassword", password, options); 

                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                  
                    ViewBag.ErrorMessage = "Invalid email or password. Please try again.";
                    return View("Login");
                }
            }
        }

        public IActionResult Profile()
        {
            string storedEmail = HttpContext.Session.GetString("Email");
            string storedPassword = HttpContext.Session.GetString("Password");
            string firstName = HttpContext.Session.GetString("FirstName");
            string lastName = HttpContext.Session.GetString("LastName");
            string Phone = HttpContext.Session.GetString("Phone");


            string[] userData = { storedEmail, storedPassword, firstName, lastName,Phone };

            TempData["UserData"] = userData;

            return View();
        }
        [HttpPost]
        public IActionResult HandleEditProfile(string firstName, string lastName, string email, string password, string phone)
        {
            HttpContext.Session.SetString("FirstName", firstName);
            HttpContext.Session.SetString("LastName", lastName);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Password", password);
            HttpContext.Session.SetString("Phone", phone); 
                                                          
            TempData["UserData"] = new string[] { email, password, firstName, lastName, phone };

            return RedirectToAction("Profile", "User");
        }



    }


}
