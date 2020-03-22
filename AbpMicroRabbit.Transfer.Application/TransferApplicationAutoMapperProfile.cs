using AbpMicroRabbit.Banking.Domain.Entities;
using AbpMicroRabbit.Transfer.Application.Contracts.Dto;
using AutoMapper;

namespace AbpMicroRabbit.Transfer.Application
{
    public class TransferApplicationAutoMapperProfile : Profile
    {
        public TransferApplicationAutoMapperProfile()
        {
            CreateMap<TransferLog, TransferLogDto>();
        }
    }
}
