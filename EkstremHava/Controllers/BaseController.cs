//using EkstremHavaOlayları.Data;
//using EkstremHavaOlayları.Helper;
//using EkstremHavaOlayları.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace EkstremHavaOlayları.Controllers
//{
//	public class BaseController : Controller
//	{
//		protected ApplicationDbContext _db; // Veritabanı bağlantısı

//		public BaseController(ApplicationDbContext dbContext)
//		{
//			_db = dbContext;
//			ShareCommonData(); // Ortak verileri paylaşan metodu çağır
//		}

//		private void ShareCommonData()
//		{
//			// Ortak verileri burada paylaşabilirsiniz
//			// Örneğin, sepetteki ürün sayısını alabilir ve ViewData'ya ekleyebilirsiniz
//			Basket myBasket = Basket.GetBasket(_db, HttpContext);
//			int itemsInBasketCount = myBasket.GetProductCountFromBasket();
//			ViewData["ItemsInBasketCount"] = itemsInBasketCount;
//		}
//		public class HomeController : BaseController
//		{
//			private readonly ILogger<HomeController> _logger;

//			public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
//				: base(context)
//			{
//				_logger = logger;
//			}

//			public IActionResult Index()
//			{
//				List<Product> newProducts = _db.Products.OrderByDescending(p => p.ProductId).Take(3).ToList();
//				int basketCount = (int)ViewData["ItemsInBasketCount"];

//				return View(newProducts);
//			}
//		}
//	}
//}
