using EkstremHava.Data;
using EkstremHava.Models;
using EkstremHava.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EkstremHava.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;


        public ProductController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _db = context;
        }
        public IActionResult Index(ProductFilterViewModel filter)
        {
            List<Product> allProducts = _db.Products.OrderByDescending(p => p.ProductId).ToList();

            // Ürün türlerini al ve SelectList'e dönüştür
            List<ProductType> productTypes = _db.ProductTypes.ToList(); // Burada doğru ProductType listesini almalısınız

            if (filter.SelectedTypeId != 0)
            {
                allProducts = allProducts.Where(p => p.Type.ProductTypeId == filter.SelectedTypeId).ToList();
            }


            // ViewModel oluştur
            ProductFilterViewModel viewModel = new ProductFilterViewModel
            {
                Products = allProducts,
                SelectedTypeId = filter.SelectedTypeId,
            };

            viewModel.LoadTypes(productTypes);

            return View(viewModel);
        }

        public IActionResult Detail(int id)
        {
            Product? prod = _db.Products.Where(p => p.ProductId == id).Include(p => p.Type).FirstOrDefault();
            if (prod != null)
            {
                return View(prod);
            }
            else
            {
                return NotFound();
            }


        }

    }
}
