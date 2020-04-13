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

namespace AbpMicroRabbit.Banking.Application.Services
{
    [Authorize(AccountPermissions.GroupName)]
    public class AccountAppService : ApplicationService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferLogApplicationService _bus;

        public AccountAppService(IAccountRepository accountRepository, ITransferLogApplicationService bus)
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }

        public IEnumerable<Account> GetList()
        {
            return _accountRepository.GetAccounts();
        }

        [Authorize(AccountPermissions.Accounts.Transfer)]
        public async Task Transfer(AccountTransferDto accountTransfer)
        {
            var createTransferCommand = ObjectMapper.Map<AccountTransferDto, CreateTransferCommand>(accountTransfer);
            await _bus.SendCommand(createTransferCommand);
        }
    }
}
