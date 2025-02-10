using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CompletePaymentAsync(CreateOrderingDto createOrderDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:7073/api/Orderings/CompletePayment", createOrderDto);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Hatası: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }
    }
}
