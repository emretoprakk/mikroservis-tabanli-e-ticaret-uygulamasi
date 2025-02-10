using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "Mağazamız";
            ViewBag.directory2 = "Ana Sayfa";
            ViewBag.directory3 = "Tüm Ürünler";
            return View();
        }
    }
}
