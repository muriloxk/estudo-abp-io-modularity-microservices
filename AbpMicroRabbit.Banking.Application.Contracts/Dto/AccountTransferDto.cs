namespace AbpMicroRabbit.Banking.Application.Contracts.Dto
{
    public class AccountTransferDto 
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal TransferAccount { get; set; }
    }
}
