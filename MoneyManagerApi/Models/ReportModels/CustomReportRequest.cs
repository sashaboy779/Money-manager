using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.ReportModels
{
    public class CustomReportRequest : BaseReportRequest
    {
        [Required(ErrorMessage = ModelsResources.StartingDateRequired)]
        public DateTime? StartingDate { get; set; }

        [Required(ErrorMessage = ModelsResources.EndingDateRequired)]
        public DateTime? EndingDate { get; set; }
    }
}
