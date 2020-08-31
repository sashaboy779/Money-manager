using System;

namespace BusinessLogicLayer.Dto.OperationDtos
{
    public class UpdateOperationDto
    {
        public int OperationId { get; set; }
        public DateTime OperationDate { get; set; }
        public string Note { get; set; }
        public int CategoryId { get; set; }
    }
}
