using System;
using MassTransit;
using Order.API.Contexts;
using Shared.OrderEvents;

namespace Order.API.Consumers
{
    public class OrderCompletedEventConsumer: IConsumer<OrderCompletedEvent>
    {
        private OrderDbContext _orderDbContext;

        public OrderCompletedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
           Order.API.Models.Order order = await _orderDbContext.Orders.FindAsync(context.Message.OrderId);
            if (order is not null)
            {
                order.OrderStatus = Enums.OrderStatus.Completed;
                await _orderDbContext.SaveChangesAsync();
            }
        }
    }
}

