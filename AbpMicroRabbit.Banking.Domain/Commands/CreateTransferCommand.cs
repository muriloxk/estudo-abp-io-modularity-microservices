using System;
namespace AbpMicroRabbit.Banking.Domain.Commands
{
    public class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(string from, string to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
    }
}
