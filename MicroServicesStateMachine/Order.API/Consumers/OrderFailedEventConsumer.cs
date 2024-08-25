using System;
using MassTransit;
using Order.API.Contexts;
using Shared.OrderEvents;

namespace Order.API.Consumers
{
    public class OrderFailedEventConsumer : IConsumer<OrderFailedEvent>
    {
        private OrderDbContext _orderDbContext;

        public OrderFailedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<OrderFailedEvent> context)
        {
            Order.API.Models.Order order = await _orderDbContext.Orders.FindAsync(context.Message.OrderId);
            if (order is not null)
            {
                order.OrderStatus = Enums.OrderStatus.Fail;
                await _orderDbContext.SaveChangesAsync();
            }
        }
    }
}

