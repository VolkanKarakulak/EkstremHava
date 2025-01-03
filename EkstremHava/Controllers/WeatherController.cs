using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace EkstremHava.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "2bfaeec51c715d13686cc1f2d0d6e5f4";

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("by-coordinates")]
        public async Task<IActionResult> GetWeatherByCoordinates([FromQuery] double lat, [FromQuery] double lon)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}&units=metric";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Hava durumu alınamadı.");
                }

                var weatherData = await response.Content.ReadAsStringAsync();
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata oluştu: {ex.Message}");
            }
        }
    }
}
