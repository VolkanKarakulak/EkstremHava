using EkstremHava.Data;
using Microsoft.AspNetCore.Mvc;


namespace EkstremHava.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;
        public GalleryController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            var galleryPhotos = _db.GalleryPhotos.ToList();

            return View(galleryPhotos);
            //return View();
        }
    }
}
