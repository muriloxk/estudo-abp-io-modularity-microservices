using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace AbpMicroRabbit.Banking.Domain.Entities
{
    public class Account : AggregateRoot<Guid>, IMultiTenant
    {
        public Account(string accountType,
                       decimal accountBalance) : base(Guid.NewGuid())
        {
            AccountType = accountType;
            AccountBalance = accountBalance;
        }

        protected Account() { }

        public string AccountType { get; set; }
        public decimal AccountBalance { get; set; }
        public Guid? TenantId { get; set; }
    }
}
