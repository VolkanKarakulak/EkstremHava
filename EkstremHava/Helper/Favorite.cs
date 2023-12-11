using EkstremHava.Data;
using EkstremHava.Models;
using Microsoft.EntityFrameworkCore;

namespace EkstremHava.Helper
{
    public class Favorite
    {
        ApplicationDbContext _db;

        public string FavoriteName { get; set; }

        private const string FavoriteSessionKey = "FavoriteName";

        private Favorite(ApplicationDbContext db, string favoriteName)
        {

            _db = db;
            FavoriteName = favoriteName;

        }

        public static Favorite GetFavorite(ApplicationDbContext db, HttpContext http)
        {
            string favorite_name = Favorite.GetFavoriteName(http);
            Favorite favorite = new Favorite(db, favorite_name);
            return favorite;
        }
        private static string GetFavoriteName(HttpContext http)
        {
            string? favoriteName = http.Session.GetString(FavoriteSessionKey);


            if (favoriteName == null)
            {
                string userName = http.User.Identity.Name; ////**********

                if ((String.IsNullOrWhiteSpace(userName)))
                {
                    Guid tempCartId = Guid.NewGuid();
                    http.Session.SetString(FavoriteSessionKey, tempCartId.ToString());
                }
                else
                {
                    http.Session.SetString(FavoriteSessionKey, userName);
                }

            }
            return http.Session.GetString(FavoriteSessionKey);



        }
        public void AddToFavorite(Product product)
        {
            FavoriteProduct? favoriteItem = _db.FavoriteProducts.SingleOrDefault(c => c.Username == this.FavoriteName && c.ProductId == product.ProductId);

            if (favoriteItem == null)
            {

                favoriteItem = new FavoriteProduct()
                {
                    ProductId = product.ProductId,
                    Username = this.FavoriteName
                };

                _db.FavoriteProducts.Add(favoriteItem);
            }
            else
            {
               
               
            }

            _db.SaveChanges();
        }

        public void EmptyFavorite()
        {
            IQueryable<FavoriteProduct> favoriteItems = _db.FavoriteProducts.Where(i => i.Username == this.FavoriteName);

            foreach (FavoriteProduct favoriteItem in favoriteItems)
            {
                _db.FavoriteProducts.Remove(favoriteItem);
            }

        }

        public string RemoveFromFavorite(Product product)
        {
            return RemoveFromFavorite(product.ProductId);
        }

        public string RemoveFromFavorite(int id)
        {
            FavoriteProduct? productToRemoved = _db.FavoriteProducts.SingleOrDefault(b => b.Username == this.FavoriteName && b.ProductId == id);

            if (productToRemoved != null)
            {
                _db.FavoriteProducts.Remove(productToRemoved);
                _db.SaveChanges();
            }
            return ("Ürün Favorilerden Kaldırıldı");
        }

        public List<FavoriteProduct> GetProductsFromFavorite()
        {
            return _db.FavoriteProducts.Where(p => p.Username == this.FavoriteName).Include(p => p.Product).ToList();
        }

            
        public int AddOrderDetails(Order order)
        {
            var products = GetProductsFromFavorite();

            foreach (FavoriteProduct product in products)
            {
                OrderDetail od = new OrderDetail()
                {
                    ProductId = product.ProductId,
                    TotalPrice = product.Product.Price,
                    OrderId = order.OrderId,
                   
                };

                _db.OrderDetails.Add(od);

            }
            _db.SaveChanges();

            return order.OrderId;
        }


    }
}
