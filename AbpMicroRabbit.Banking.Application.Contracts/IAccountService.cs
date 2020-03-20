using System.Collections.Generic;
using System.Threading.Tasks;
using AbpMicroRabbit.Banking.Application.Contracts.Dto;
using AbpMicroRabbit.Banking.Domain.Entities;
using Volo.Abp.Application.Services;

namespace MicroRabbit.Banking.Application.Interfaces
{
    public interface IAccountService : IApplicationService
    {
        Task<IEnumerable<Account>> GetListAsync();
        Task Transfer(AccountTransferDto accountTransfer);
    }
}
