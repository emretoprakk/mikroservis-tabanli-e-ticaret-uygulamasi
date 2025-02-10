using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Dtos;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using System.Transactions;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> OrderingList()
        {
            var values = await _mediator.Send(new GetOrderingQuery());
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderingById(int id)
        {
            var values = await _mediator.Send(new GetOrderingByIdQuery(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrdering(CreateOrderingCommand command)
        {
            await _mediator.Send(command);
            return Ok("Siparis basariyla eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveOrdering(int id)
        {
            await _mediator.Send(new RemoveOrderingCommand(id));
            return Ok("Siparis basariyla silindi.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrdering(UpdateOrderingCommand command)
        {
            await _mediator.Send(command);
            return Ok("Siparis basariyla guncellendi.");
        }

        [HttpGet("GetOrderingByUserId/{id}")]
        public async Task<IActionResult> GetOrderingByUserId(string id)
        {
            var values = await _mediator.Send(new GetOrderingByUserIdQuery(id));
            return Ok(values);
        }

        [HttpPost("CompletePayment")]
        public async Task<IActionResult> CompletePayment([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null || !createOrderDto.OrderDetails.Any())
            {
                return BadRequest("Geçersiz sipariş bilgileri.");
            }

            try
            {            
                // Sipariş oluştur
                var orderCommand = new CreateOrderingCommand
                {
                    UserId = createOrderDto.UserId,
                    TotalPrice = createOrderDto.TotalPrice,
                    OrderDate = createOrderDto.OrderDate,
                };

                var orderId = await _mediator.Send(orderCommand); // Sipariş ID'si döner

                // Sipariş detaylarını ekle
                foreach (var detail in createOrderDto.OrderDetails)
                {
                    var orderDetailCommand = new CreateOrderDetailCommand
                    {
                        ProductId = detail.ProductId,
                        ProductName = detail.ProductName,
                        ProductPrice = detail.ProductPrice,
                        ProductAmount = detail.ProductAmount,
                        ProductTotalPrice = detail.ProductTotalPrice,
                        OrderingId = orderId
                    };

                    await _mediator.Send(orderDetailCommand); // Detayları ekle
                }

                return Ok("Sipariş başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
            }
        }

    }
}

