using BusinessLogicLayer.Dto.WalletDtos;

namespace BusinessLogicLayer.Dto.ReportDtos
{
    public class ReportDto
    {
        public string Name { get; set; }
        public CurrencyDto Currency { get; set; }
        public decimal? Income { get; set; }
        public decimal? Expense { get; set; }
        public decimal? OpeningBalance { get; set; }
        public decimal? EndingBalance { get; set; }
    }
}
