using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
