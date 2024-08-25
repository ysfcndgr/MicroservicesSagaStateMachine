using System;
using Shared.Messages;

namespace Shared.StockEvents
{
	public class StockRollbackMessage
	{
		public List<OrderItemMessage> OrderItems { get; set; }
	}
}

