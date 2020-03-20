using System;
using Volo.Abp.Domain.Entities;

namespace AbpMicroRabbit.Banking.Domain.Entities
{
    public class Account : AggregateRoot<Guid>
    {
        public Account(string accountType, decimal accountBalance)
        {
            AccountType = accountType;
            AccountBalance = accountBalance;
        }

        protected Account() { }

        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
