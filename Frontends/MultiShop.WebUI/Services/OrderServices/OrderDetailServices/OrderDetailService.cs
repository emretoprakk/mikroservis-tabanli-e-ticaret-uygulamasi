using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.OrderServices.OrderDetailServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly HttpClient _httpClient;

        public OrderDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultOrderDetailDto>> GetOrderDetailsByOrderId(int orderId)
        {
            var responseMessage = await _httpClient.GetAsync($"orderdetails/{orderId}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultOrderDetailDto>>(jsonData);
            return values;
        }

        public async Task CreateOrderDetailAsync(CreateOrderDetailDto createOrderDetailDto)
        {
            await _httpClient.PostAsJsonAsync("orderdetails", createOrderDetailDto);
        }

        public async Task DeleteOrderDetailAsync(int orderDetailId)
        {
            await _httpClient.DeleteAsync($"orderdetails/{orderDetailId}");
        }
    }
}
