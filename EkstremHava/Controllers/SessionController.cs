using Microsoft.AspNetCore.Mvc;

namespace EkstremHava.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("bilgi", "abcdef");

            return View();
        }
    }
}
