using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class DefaultController : Controller
    {
        public async Task< IActionResult> WeatherDetail()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://cities-temperature.p.rapidapi.com/weather/v1/current?location=konya"),
                Headers =
    {
        { "x-rapidapi-key", "918ee52d1amsh548bb7d35e18cb1p1be77ajsn3aa138070f41" },
        { "x-rapidapi-host", "cities-temperature.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<WeatherViewModel>(body);
                ViewBag.cityTemp = values.temperature;
                return View();
            }
        }

        public async Task<IActionResult> Exchange()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=USD&to_symbol=TRY&language=en"),
                Headers =
    {
        { "x-rapidapi-key", "918ee52d1amsh548bb7d35e18cb1p1be77ajsn3aa138070f41" },
        { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ExchangeViewModel.Rootobject>(body);
                ViewBag.exchangeRateUsd = values.data.exchange_rate;
                ViewBag.previous_closeUsd = values.data.previous_close;
            }


            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=EUR&to_symbol=TRY&language=en"),
                Headers =
    {
        { "x-rapidapi-key", "918ee52d1amsh548bb7d35e18cb1p1be77ajsn3aa138070f41" },
        { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
    },
            };
            using (var response = await client2.SendAsync(request2))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ExchangeViewModel.Rootobject>(body);
                ViewBag.exchangeRateEur = values.data.exchange_rate;
                ViewBag.previous_closeEur = values.data.previous_close;
                return View();
            }


        }
    }
}
