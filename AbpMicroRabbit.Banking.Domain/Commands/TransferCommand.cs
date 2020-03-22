using System;
using AbpMicroRabbit.Shared.Domain;

namespace AbpMicroRabbit.Banking.Domain.Commands
{
    public class TransferCommand : Command
    {
        public string From { get; protected set; }
        public string To { get; protected set; }
        public decimal Amount { get; protected set; }
    }
}
