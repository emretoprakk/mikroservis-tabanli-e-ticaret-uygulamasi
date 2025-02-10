using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;

namespace MultiShop.WebUI.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<bool> CompletePaymentAsync(CreateOrderingDto createOrderDto);
    }
}
