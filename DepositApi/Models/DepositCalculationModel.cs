namespace DepositApi.Models
{
    public class DepositCalculationModel
    {
        public int Id { get; set; }

        public int Month { get; set; }

        public decimal PercentAdded { get; set; }

        public decimal TotalAmount { get; set; }

        public int DepositId { get; set; }
    }
}
