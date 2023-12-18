using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VET_ANIMAL.WEB.Controllers
{
    public class Loader : Controller
    {
        // GET: Loader
        public ActionResult Index()
        {
           
            return View();
        }

    }
}
