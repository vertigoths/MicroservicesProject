using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
	public class Order : Entity, IAggregateRoot
	{
		public DateTime CreateDate { get; private set; }

		public Address Address { get; private set; } 

		public string BuyerId { get; private set; }

		private readonly List<OrderItem> _orderItems;

		public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

		public Order(string buyerId, Address address)
		{
			_orderItems = new List<OrderItem>();

			CreateDate = DateTime.Now;

			Address = address;

			BuyerId = buyerId;
		}

		public Order() {}

		public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
		{
			var existsProduct = _orderItems.Any(x => x.ProductId == productId);

			if (!existsProduct)
			{
				var orderItem = new OrderItem(productId, productName, pictureUrl, price);

				_orderItems.Add(orderItem);
			}
		}

		public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
	}
}