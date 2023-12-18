using Microsoft.AspNetCore.Mvc;

namespace VET_ANIMAL.WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TempData["menu"] = "";

            return View();
        }
    }
}