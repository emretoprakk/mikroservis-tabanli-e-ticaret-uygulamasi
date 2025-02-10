﻿using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderOrderingServices
{
    public interface IOrderOrderingService
    {
        Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id);
        Task CreateOrderAsync(CreateOrderingDto createOrderDto);
        Task DeleteOrderAsync(int orderingId);
    }
}
