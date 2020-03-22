namespace AbpMicroRabbit.Transfer.Application.Contracts.Dto
{
    public class TransferLogDto
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal TransferAmount { get; set; }
    }
}
