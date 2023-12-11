using EkstremHava.Data;
using EkstremHava.Helper;
using EkstremHava.Models;
using EkstremHava.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EkstremHava.Controllers
{
    public class FavoriteController : Controller
    {
        ApplicationDbContext db;

        public FavoriteController(ApplicationDbContext dbcontext)
        {
            db = dbcontext;
        }

        //public IActionResult AddToFavorites (int productId)
        //{
        //    string username = Http.User.Identity. // Kullanıcı kimliğini al

        //    // Eğer ürün daha önce favorilere eklenmemişse
        //    if (!db.FavoriteProducts.Any(fp => fp.Username == username && fp.ProductId == productId))
        //    {
        //        var favoriteProduct = new FavoriteProduct
        //        {
        //            Username = username,
        //            FavoriteProductId = productId
        //        };

        //        db.FavoriteProducts.Add(favoriteProduct);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("Index", "Home"); // İstediğiniz sayfaya yönlendirme????
        //}


        public IActionResult Index()
        {
            Favorite myFavorite = Favorite.GetFavorite(db, HttpContext);

            FavoriteViewModel viewModel = new FavoriteViewModel();

            viewModel.FavoriteItems = myFavorite.GetProductsFromFavorite();
            
            return View(viewModel);
        }
            

        public IActionResult AddProductToFavorite(int id)
        {
            Product? addToBeProduct = db.Products.SingleOrDefault(k => k.ProductId == id); //db.Products.Find(id);??

            Favorite myFavorite = Favorite.GetFavorite(db, HttpContext);

            myFavorite.AddToFavorite(addToBeProduct);

            return RedirectToAction("Index");  //??????
        }

        //[HttpPost]
        public IActionResult RemoveProductFromFavorite(int id)
        {
            Favorite myFavorite = Favorite.GetFavorite(db, HttpContext);

            string productName = db.FavoriteProducts.Include(n => n.Product).SingleOrDefault(n => n.ProductId == id && n.Username == myFavorite.FavoriteName).Product.Name;

            myFavorite.RemoveFromFavorite(id);

            return RedirectToAction("Index");
        }

    }
}
