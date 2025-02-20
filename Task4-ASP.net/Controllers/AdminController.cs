using Microsoft.AspNetCore.Mvc;

namespace Task4_ASP.net.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
