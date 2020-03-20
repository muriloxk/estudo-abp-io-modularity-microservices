using AbpMicroRabbit.Banking.Application.Contracts.Dto;
using AbpMicroRabbit.Banking.Domain.Commands;
using AbpMicroRabbit.Banking.Domain.Events;
using AutoMapper;

namespace AbpMicroRabbit.Banking.Application
{
    public class BookStoreApplicationAutoMapperProfile : Profile
    {
        public BookStoreApplicationAutoMapperProfile()
        {
            CreateMap<AccountTransferDto, CreateTransferCommand>()
                            .ForMember(d => d.Amount, opt => opt.MapFrom(src => src.TransferAccount))
                            .ForMember(d => d.To, opt => opt.MapFrom(src => src.ToAccount))
                            .ForMember(d => d.From, opt => opt.MapFrom(src => src.FromAccount));

            CreateMap<CreateTransferCommand, TransferCreatedEvent>();
        }
    }
}
