using System;
using MassTransit;

namespace Shared.StockEvents
{
    public class StockNotReservedEvent : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; }

        public StockNotReservedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public string Message { get; set; }
    }
}

