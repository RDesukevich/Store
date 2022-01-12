using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
