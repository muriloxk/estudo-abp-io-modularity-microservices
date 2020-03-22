using AbpMicroRabbit.Banking.Application.Contracts.Dto;
using AbpMicroRabbit.Banking.Domain.Commands;
using AbpMicroRabbit.Banking.Domain.Events;
using AutoMapper;

namespace AbpMicroRabbit.Banking.Application
{
    public class BankingApplicationAutoMapperProfile : Profile
    {
        public BankingApplicationAutoMapperProfile()
        {
            CreateMap<AccountTransferDto, CreateTransferCommand>()
                  .ConstructUsing(x => new CreateTransferCommand(x.FromAccount, x.ToAccount, x.TransferAccount));

            CreateMap<CreateTransferCommand, TransferCreatedEvent>()
                 .ConstructUsing(x => new TransferCreatedEvent(x.From, x.To, x.Amount));
        }
    }
}
