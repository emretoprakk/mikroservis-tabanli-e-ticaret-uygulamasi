using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            var values = await GetBasket();

            if (values == null || values.BasketItems == null)
            {
                values = new BasketTotalDto
                {
                    BasketItems = new List<BasketItemDto>()
                };
            }

            var existingItem = values.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);
            if (existingItem == null)
            {
                values.BasketItems.Add(basketItemDto);
            }
            else
            {
                existingItem.Quantity += basketItemDto.Quantity;
            }

            await SaveBasket(values);
        }

        public async Task DeleteBasket(string userId)
        {
            await _httpClient.DeleteAsync($"baskets/{userId}");
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("baskets");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new BasketTotalDto
                {
                    BasketItems = new List<BasketItemDto>()
                };
            }

            return await responseMessage.Content.ReadFromJsonAsync<BasketTotalDto>();
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            var result = values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _httpClient.PostAsJsonAsync("baskets", basketTotalDto);
        }

    }
}


