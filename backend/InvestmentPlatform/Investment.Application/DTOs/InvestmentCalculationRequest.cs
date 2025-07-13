
namespace Investment.Application.DTOs
{
    public class InvestmentCalculationRequest
    {
        public decimal TotalAmount { get; set; }
        public List<InvestmentItem> Investments { get; set; } = new();
    }

    public class InvestmentItem
    {
        public string? Type { get; set; }
        public decimal Percentage { get; set; }
    }
}
