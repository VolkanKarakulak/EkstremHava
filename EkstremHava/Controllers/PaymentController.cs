using EkstremHava.Data;
using EkstremHava.Models;
using Microsoft.AspNetCore.Mvc;
using EkstremHava.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EkstremHava.Controllers
{
    //[Authorize]
    public class PaymentController : Controller
    {
        const string PromoCode = "FREE";

        ApplicationDbContext db;

        public PaymentController(ApplicationDbContext context)
        {
            db = context;
        } // Bu kısmı ekledim

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(Order order)
        {


            order.Username = User.Identity.Name;
            order.OrderDate = DateTime.Now;

            //Save Order
            db.Orders.Add(order);
            db.SaveChanges();

            //Process the order
            var basket = Basket.GetBasket(db, this.HttpContext);
            decimal total = basket.CalculateBasketTotal();
            basket.AddOrderDetails(order);
            order.TotalPrice = total;
            db.SaveChanges();

            return RedirectToAction("Complete", new { id = order.OrderId });
        }
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(o => o.OrderId == id && o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }

        }
    }
}

//using EkstremHavaOlayları.Data;
//using EkstremHavaOlayları.Models;
//using Microsoft.AspNetCore.Mvc;
//using EkstremHavaOlayları.Helper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;

//namespace EkstremHavaOlayları.Controllers
//{
//    //[Authorize]
//    public class PaymentController : Controller
//    {
//        const string PromoCode = "FREE";

//        ApplicationDbContext db;

//        public PaymentController(ApplicationDbContext context)
//        {
//            db = context;
//        } // Bu kısmı ekledim

//        public ActionResult AddressAndPayment()
//        {
//            return View();
//        }

//        // POST: /Checkout/AddressAndPayment
//        [HttpPost]
//        public ActionResult AddressAndPayment(Order order)
//        {


//            order.Username = User.Identity.Name;
//            order.OrderDate = DateTime.Now;

//            //Save Order
//            db.Orders.Add(order);
//            db.SaveChanges();

//            //Process the order
//            var basket = Basket.GetBasket(db, this.HttpContext);
//            decimal total = basket.CalculateBasketTotal();
//            basket.AddOrderDetails(order);
//            order.TotalPrice = total;
//            db.SaveChanges();

//            return RedirectToAction("Payment", new { id = order.OrderId });
//        }


//        public ActionResult Payment()
//        {
//            return View();
//        }


//        [HttpPost]
//        public ActionResult Payment(Order order, Payment payment)
//        {


//            return RedirectToAction("Complete", new { id = order.OrderId });


//        }


//        public ActionResult Complete(int id)
//        {
//            // Validate customer owns this order
//            bool isValid = db.Orders.Any(o => o.OrderId == id && o.Username == User.Identity.Name);

//            if (isValid)
//            {
//                return View(id);
//            }
//            else
//            {
//                return View("Error");
//            }

//        }
//    }
//}

