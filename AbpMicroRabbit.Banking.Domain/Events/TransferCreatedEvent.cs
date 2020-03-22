using AbpMicroRabbit.Shared.Domain;

namespace AbpMicroRabbit.Banking.Domain.Events
{
    public class TransferCreatedEvent : Event
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public decimal Amount { get; private set; }

        public TransferCreatedEvent(string from, string to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
