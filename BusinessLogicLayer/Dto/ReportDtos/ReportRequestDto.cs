using System;

namespace BusinessLogicLayer.Dto.ReportDtos
{
    public class ReportRequestDto
    {
        public int WalletId { get; set; }
        public TimeRangeDto TimeRange { get; set; }
        public DateTime DateInRange { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}
