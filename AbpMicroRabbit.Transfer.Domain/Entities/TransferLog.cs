using System;
using Volo.Abp.Domain.Entities;

namespace AbpMicroRabbit.Banking.Domain.Entities
{
    public class TransferLog : AggregateRoot<Guid>
    {
        public TransferLog(string fromAccount, string toAccount, decimal transferAmount) : base(Guid.NewGuid())
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            TransferAmount = transferAmount;
        }

        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
