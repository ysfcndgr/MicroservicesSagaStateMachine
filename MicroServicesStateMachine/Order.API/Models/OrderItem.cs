using System;
namespace Order.API.Models
{
	public sealed class OrderItem
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int Count { get; set; }
		public decimal Price { get; set; }
	}
}

