namespace AbpMicroRabbit.Banking.Application.Contracts.Dto
{
    public class AccountTransferDto
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAccount { get; set; }
    }
}
