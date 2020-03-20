using System.Collections.Generic;
using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Application.Contracts.Dto;
using AbpMicroRabbit.Banking.Domain.Commands;
using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Banking.Domain.Repositories;
using MicroRabbit.Banking.Application.Interfaces;
using Volo.Abp.EventBus.Distributed;
using AbpMicroRabbit.Shared.Infra.Bus;
using Volo.Abp.Application.Services;

namespace AbpMicroRabbit.Banking.Application.Services
{
    public class AccountService : ApplicationService, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDistributedEventBus _bus;

        public AccountService(IAccountRepository accountRepository, IDistributedEventBus bus)
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }

        public async Task<IEnumerable<Account>> GetListAsync()
        {
            //TODO: Criar sistema de cache
            return await _accountRepository.GetListAsync();
        }

        //TODO implementar Transfer AccountService
        public async Task Transfer(AccountTransferDto accountTransfer)
        {
          var createTransferCommand =  ObjectMapper.Map<AccountTransferDto, CreateTransferCommand>(accountTransfer);
          await _bus.SendCommand(createTransferCommand);
        }
    }
}
