using MicroRabbit.Banking.Application.Interfaces;
using Volo.Abp.Application.Services;
using AbpMicroRabbit.Banking.Domain.Repositories;
using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Application.Contracts.Dto;
using AbpMicroRabbit.Banking.Domain.Commands;
using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using AbpMicroRabbit.Banking.Application.Contracts.Permissions;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace AbpMicroRabbit.Banking.Application.Services
{
    [Authorize(BankingPermissions.GroupName)]
    public class AccountAppService : ApplicationService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferLogApplicationService _bus;

        private readonly IDistributedCache<IEnumerable<Account>> _cache;

        public AccountAppService(IAccountRepository accountRepository,
                                 ITransferLogApplicationService bus,
                                 IDistributedCache<IEnumerable<Account>> cache)
        {
            _accountRepository = accountRepository;
            _bus = bus;
            _cache = cache;
        }

        public IEnumerable<Account> GetList()
        {
            return  _cache.GetOrAdd(CurrentTenant.Id.ToString() + "AllAccounts",
                                         () => _accountRepository.GetAccounts(),
                                         () => new DistributedCacheEntryOptions
                                            {
                                             AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                                         });
        }

        [Authorize(BankingPermissions.Accounts.Transfer)]
        public async Task Transfer(AccountTransferDto accountTransfer)
        {
            var createTransferCommand = ObjectMapper.Map<AccountTransferDto, CreateTransferCommand>(accountTransfer);
            await _bus.SendCommand(createTransferCommand);
        }
    }
}
