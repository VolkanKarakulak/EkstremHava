using EkstremHava.Data;
using EkstremHava.Models;
using EkstremHava.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Diagnostics;

namespace EkstremHava.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "2bfaeec51c715d13686cc1f2d0d6e5f4";


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, HttpClient httpClient)
        {
            _logger = logger;
            _db = context;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            List<Product>  newProducts = _db.Products.OrderByDescending(p => p.ProductId).Take(3).ToList();

            // Hava durumu bilgisini al
            var weatherData = await GetWeatherDataAsync(41.00, 28.97); // İstanbul'un koordinatları

            var viewModel = new HomeViewModel
            {
                NewProducts = newProducts,
                Weather = weatherData
            };

            return View(viewModel);
        }

        // Hava durumu verisini çeken yardımcı metot
        private async Task<WeatherViewModel> GetWeatherDataAsync(double lat, double lon)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

                return new WeatherViewModel
                {
                    CityName = data.name,
                    Description = data.weather[0].description,
                    Temperature = data.main.temp,
                    Humidity = data.main.humidity,
                    WindSpeed = data.wind.speed
                };
            }

            return null;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}