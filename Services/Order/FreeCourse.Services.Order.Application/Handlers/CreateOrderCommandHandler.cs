using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared_.Dtos;
using MediatR;

namespace FreeCourse.Services.Order.Application.Handlers
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
	{
		private readonly OrderDbContext _context;

		public CreateOrderCommandHandler(OrderDbContext context)
		{
			_context = context;
		}

		public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var address = ObjectMapper.Mapper.Map<Address>(request.Address);

			var order = new Domain.OrderAggregate.Order(request.BuyerId, address);

			foreach (var orderItem in request.OrderItems)
			{
				order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.Price, orderItem.PictureUrl);
			}

			await _context.Orders.AddAsync(order, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = order.Id}, 200);
		}
	}
}