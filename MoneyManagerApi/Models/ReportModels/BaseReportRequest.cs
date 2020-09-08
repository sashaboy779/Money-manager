using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.ReportModels
{
    public abstract class BaseReportRequest
    {
        [Required(ErrorMessage = ModelsResources.WalletIdRequired)]
        public int? WalletId { get; set; }
        public TimeRange? TimeRange { get; set; } = null;
    }
}
