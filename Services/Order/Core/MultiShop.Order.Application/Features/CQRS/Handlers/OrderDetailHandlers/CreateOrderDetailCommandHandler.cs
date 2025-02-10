using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, Unit>
    {
        private readonly IRepository<OrderDetail> _repository;

        public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            var orderDetail = new OrderDetail
            {
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                ProductPrice = request.ProductPrice,
                ProductAmount = request.ProductAmount,
                ProductTotalPrice = request.ProductTotalPrice,
                OrderingId = request.OrderingId
            };

            await _repository.CreateAsync(orderDetail);

            return Unit.Value;
        }
    }
}