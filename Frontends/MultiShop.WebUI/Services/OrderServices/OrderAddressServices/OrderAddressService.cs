using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public class OrderAddressService : IOrderAddressService
    {
        private readonly HttpClient _httpClient;

        public OrderAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto)
        {
            await _httpClient.PostAsJsonAsync("addresses", createOrderAddressDto);
        }

        public async Task DeleteOrderAddressAsync(int addressId)
        {
            await _httpClient.DeleteAsync($"addresses/{addressId}");
        }
    }
}
