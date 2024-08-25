using System;
using MassTransit;
using Shared.PaymentEvents;
using Shared.Settings;

namespace Payment.API.Consumers
{
    public class PaymentStartedEventConsumer : IConsumer<PaymentStartedEvent>
    {
        private ISendEndpointProvider _sendPoint;

        public PaymentStartedEventConsumer(ISendEndpointProvider sendPoint)
        {
            _sendPoint = sendPoint;
        }

        public async Task Consume(ConsumeContext<PaymentStartedEvent> context)
        {
            var sendEndPoint = await _sendPoint.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.StateMachineQueue}"));
            if (true)
            {
                PaymentCompletedEvent paymentCompletedEvent = new(context.Message.CorrelationId)
                {

                };
                await sendEndPoint.Send(paymentCompletedEvent);
            }
            else
            {
                PaymentFailedEvent paymentFailedEvent = new(context.Message.CorrelationId)
                {
                    Message = "Ödeme alınamadı",
                    OrderItems = context.Message.OrderItems
                };
                await sendEndPoint.Send(paymentFailedEvent);
            }
        }
    }
}