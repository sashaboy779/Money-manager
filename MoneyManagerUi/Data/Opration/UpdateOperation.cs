using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Data.Opration
{
    public class UpdateOperation
    {
        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.OperationDateRequired))]
        public DateTime? OperationDate { get; set; }

        public int CategoryId { get; set; }
        public int OperationId { get; set; }
        public string Note { get; set; }
    }
}
