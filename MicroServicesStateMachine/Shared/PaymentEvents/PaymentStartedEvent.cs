using System;
using MassTransit;
using Shared.Messages;

namespace Shared.PaymentEvents
{
    public class PaymentStartedEvent : CorrelatedBy<Guid>
    {
        public PaymentStartedEvent(Guid CorrelationId)
        {
            this.CorrelationId = CorrelationId;
        }

        public Guid CorrelationId { get; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}

