using EkstremHava.Data;
using EkstremHava.Helper;
using EkstremHava.Models;
using EkstremHava.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace EkstremHava.Controllers
{
    public class BasketController : Controller
    {
        ApplicationDbContext db;

        public BasketController(ApplicationDbContext dbcontext)
        {
            db = dbcontext;
        }
        public IActionResult Index()
        {
            Basket myBasket = Basket.GetBasket(db, HttpContext);

            BasketViewModel viewModel = new BasketViewModel();

            viewModel.BasketItems = myBasket.GetProductsFromBasket();
            viewModel.BasketTotal = myBasket.CalculateBasketTotal();
            viewModel.ItemsInBasketCount = myBasket.GetProductCountFromBasket();

            int basketCount = myBasket.GetProductCountFromBasket();

            //ViewData["BasketCount"] = viewModel.ItemsInBasketCount;
            ViewBag.basketCount = basketCount;

            return View(viewModel);
        }

        public IActionResult AddProductToBasket(int id)
        {

            Product? addToBeProduct = db.Products.SingleOrDefault(k => k.ProductId == id);


            Basket myBasket = Basket.GetBasket(db, HttpContext);


            myBasket.AddToBasket(addToBeProduct);

            return RedirectToAction("Index");
        }

        //[HttpPost]
        public IActionResult DeleteProductFromBasket(int id)
        {
            Basket myBasket = Basket.GetBasket(db, HttpContext);

            string ProductName = db.BasketInfos.Include(n => n.Product).SingleOrDefault(n => n.ProductId == id && n.Username == myBasket.BasketName).Product.Name;

            int remainingQuantityOfRelatedProduct = myBasket.RemoveFromBasket(id);

            DeleteFromBasketViewModel results = new DeleteFromBasketViewModel()
            {

                Message = ProductName + " isimli ürün sepetten çıkarıldı",
                BasketTotal = myBasket.CalculateBasketTotal(),
                QuantityOfProductBasket = myBasket.GetProductCountFromBasket(),
                QuantityOfRelatedProduct = remainingQuantityOfRelatedProduct,
                DeletedOfProductId = id
            };



            return RedirectToAction("Index");
        }


   //     public PartialViewResult TotalCount()
   //     {

   //         Basket myBasket = Basket.GetBasket(db, HttpContext);

			//int basketCount = myBasket.GetProductCountFromBasket();

			////ViewData["BasketCount"] = viewModel.ItemsInBasketCount;
			//ViewBag.basketCount = basketCount;



			//return PartialView();
   //     }

        //public IActionResult TotalCount(int count)
        //{

        //    ViewData["SepetAdet"] = count;

        //    return View();
        //}




    }
}
