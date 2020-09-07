using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.ReportModels
{
    public class ReportRequest : BaseReportRequest
    {
        [Required(ErrorMessage = ModelsResources.DateInRangeRequired)]
        public DateTime? DateInRange { get; set; }
    }
}
