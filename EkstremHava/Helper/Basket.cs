using EkstremHava.Data;
using EkstremHava.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EkstremHava.Helper
{

    public class Basket   
    {
        ApplicationDbContext _db;

        public string BasketName { get; set; }

        private const string BasketSessionKey = "BasketName";

        private Basket(ApplicationDbContext db, string basketName)
        {

            this._db = db;
            this.BasketName = basketName;

        }

        public static Basket GetBasket(ApplicationDbContext db, HttpContext http)
        {
            string basket_name = Basket.GetBasketName(http);
            Basket basket = new Basket(db, basket_name);
            return basket;
        }
        private static string GetBasketName(HttpContext http) 
        {
            string? basketName = http.Session.GetString(BasketSessionKey);


            if(basketName == null)
            {
                string userName = http.User.Identity.Name;

                if ((String.IsNullOrWhiteSpace(userName)))
                {
                    Guid tempCartId = Guid.NewGuid();
                    http.Session.SetString(BasketSessionKey, tempCartId.ToString());
                }
                else
                {
                    http.Session.SetString(BasketSessionKey, userName);
                }

            }
            return http.Session.GetString(BasketSessionKey);


            //if (basketName != null)
            //{
            //    return basketName;
            //}

            //if (http.User.Identity != null)
            //{
            //    basketName = http.User.Identity.Name;
            //}
            //else
            //{
            //    basketName = Guid.NewGuid().ToString();
            //}

            //http.Session.SetString(BasketSessionKey, basketName);

            //return basketName;

        }
        public void AddToBasket(Product product)
        {
            BasketInfo? basketItem =  _db.BasketInfos.SingleOrDefault(c => c.Username == this.BasketName && c.ProductId == product.ProductId);

            if (basketItem  == null) 
            {

                basketItem = new BasketInfo()
                {
                    ProductId = product.ProductId,
                    Username = this.BasketName,
                    Quantity = 1,
                    DateOfAddition = DateTime.Now
                };

                _db.BasketInfos.Add(basketItem);
            }
            else 
            {
                basketItem.Quantity++;
                basketItem.DateOfAddition = DateTime.Now;
            
            }

            _db.SaveChanges();
        }

        public void EmptyBasket()
        {
            IQueryable<BasketInfo> basketItems =_db.BasketInfos.Where(i => i.Username == this.BasketName);

            foreach(BasketInfo basketItem in basketItems)
            {
                _db.BasketInfos.Remove(basketItem);
            }

        }

        public int RemoveFromBasket(Product product)
        {
            return RemoveFromBasket(product.ProductId);
        }

        public int RemoveFromBasket(int id)
        {
            BasketInfo? productToRemoved = _db.BasketInfos.SingleOrDefault(b => b.Username == this.BasketName && b.ProductId == id);

            int remainingQuantity = 0;

            if ( productToRemoved != null)
            {
                if (productToRemoved.Quantity > 1)
                {
                    productToRemoved.Quantity--;
                    remainingQuantity = productToRemoved.Quantity;
                }
                else
                {
                    _db.BasketInfos.Remove(productToRemoved);
                }
            }

            _db.SaveChanges();

            return remainingQuantity;
        }

        public List<BasketInfo> GetProductsFromBasket()
        {
            return _db.BasketInfos.Where(p => p.Username == this.BasketName).Include(p => p.Product).ToList();
        }

        public int GetProductCountFromBasket()
        {
            var products = _db.BasketInfos.Where(p => p.Username == this.BasketName);

            if (products.Any())
            {
                return products.Sum(p => p.Quantity);
            }
            else
            {
                return 0;
            }

            //if (products == null) // Where sorguları her zaman null dönmeyebilir; en azından boş bir koleksiyon dönecektir. Eğer products null dönerse ve bu durumu kontrol etmezsek products.Sum(p => p.Quantity) ifadesi çalıştırılmadan önce bir NullReferenceException hatası alabiliriz.
            //{
            //    return 0;
            //}

            //return products.Sum(p => p.Quantity);
        }

        public decimal CalculateBasketTotal()
        {
            var products =  GetProductsFromBasket();
            return products.Sum(p => p.Quantity * p.Product.Price);
        }


        //public int totalCount()
        //{
        //    int count = _db.BasketInfos.Where(item => item.Username == this.BasketName).Sum(item => item.Quantity);

        //    return count;
        //}


        public void ClaimBasket(string userName)
        {
            var items = _db.BasketInfos.Where(i => i.Username == this.BasketName);

            foreach (BasketInfo item in items)
            {
                item.Username = userName;
            }

            _db.SaveChanges();
        }
        public int AddOrderDetails(Order order) 
        {
            var products = GetProductsFromBasket();

            foreach (BasketInfo product in products)
            {
                OrderDetail od = new OrderDetail()
                {
                    ProductId = product.ProductId,
                    TotalPrice = product.Product.Price,
                    OrderId = order.OrderId,
                    Quantity = product.Quantity
                };

            _db.OrderDetails.Add(od);

            }
            _db.SaveChanges();

            EmptyBasket();

            return order.OrderId;
        }


       
    }
}
