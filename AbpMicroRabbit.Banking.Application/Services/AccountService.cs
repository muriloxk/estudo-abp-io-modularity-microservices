using MicroRabbit.Banking.Application.Interfaces;
using Volo.Abp.Application.Services;
using AbpMicroRabbit.Banking.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Application.Contracts.Dto;
using AbpMicroRabbit.Banking.Domain.Commands;
using AbpMicroRabbit.Shared.Infra.Bus;
using System.Collections.Generic;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Shared.Domain;

namespace AbpMicroRabbit.Banking.Application.Services
{
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

        public async Task Transfer(AccountTransferDto accountTransfer)
        {
            var createTransferCommand = ObjectMapper.Map<AccountTransferDto, CreateTransferCommand>(accountTransfer);
            await _bus.SendCommand(createTransferCommand);
        }
    }
}
