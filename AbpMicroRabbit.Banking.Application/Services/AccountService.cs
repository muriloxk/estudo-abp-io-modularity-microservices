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
using Volo.Abp.Security.Claims;

namespace AbpMicroRabbit.Banking.Application.Services
{
    [Authorize(BankingPermissions.Accounts.Default)]
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
                                             //AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60)
                                             AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(1)
                                         });
        }

        [Authorize(BankingPermissions.Accounts.Transfer)]
        public async Task Transfer(AccountTransferDto accountTransfer)
        {
            var result1 = await AuthorizationService.AuthorizeAsync(BankingPermissions.Accounts.Default);
            var result2 = await AuthorizationService.AuthorizeAsync(BankingPermissions.Accounts.Create);
            var result3 = await AuthorizationService.AuthorizeAsync(BankingPermissions.Accounts.Update);
            var result4 = await AuthorizationService.AuthorizeAsync(BankingPermissions.Accounts.Delete);
            var result5 = await AuthorizationService.AuthorizeAsync(BankingPermissions.Accounts.Transfer);

            var user = CurrentUser;
            var userRoles = CurrentUser.FindClaims(AbpClaimTypes.Role);

        
            var createTransferCommand = ObjectMapper.Map<AccountTransferDto, CreateTransferCommand>(accountTransfer);
            await _bus.SendCommand(createTransferCommand);
        }
    }
}
