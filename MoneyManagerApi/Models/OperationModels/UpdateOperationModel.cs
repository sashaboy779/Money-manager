using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.OperationModels
{
    public class UpdateOperationModel
    {
        public int OperationId { get; set; }

        [Required(ErrorMessage = ModelsResources.OperationDateRequired)]
        public DateTime? OperationDate { get; set; }

        [Required(ErrorMessage = ModelsResources.CategoryIdRequired)]
        public int? CategoryId { get; set; }
        public string Note { get; set; }
    }
}
