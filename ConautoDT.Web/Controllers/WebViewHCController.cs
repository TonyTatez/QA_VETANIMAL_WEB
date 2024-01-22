using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VET_ANIMAL.WEB.Controllers
{
    public class WebViewHCController : Controller
    {
        // GET: WebViewHCController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WebViewHCController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WebViewHCController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebViewHCController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WebViewHCController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WebViewHCController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WebViewHCController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebViewHCController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
