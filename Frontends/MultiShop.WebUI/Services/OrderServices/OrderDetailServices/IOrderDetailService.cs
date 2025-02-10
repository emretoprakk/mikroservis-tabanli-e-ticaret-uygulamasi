﻿using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderDetailServices
{
    public interface IOrderDetailService
    {
        Task<List<ResultOrderDetailDto>> GetOrderDetailsByOrderId(int orderId);
        Task CreateOrderDetailAsync(CreateOrderDetailDto createOrderDetailDto);
        Task DeleteOrderDetailAsync(int orderDetailId);
    }
}
