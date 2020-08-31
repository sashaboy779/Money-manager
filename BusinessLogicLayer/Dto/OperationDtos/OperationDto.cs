using BusinessLogicLayer.Dto.CategoryDtos;
using BusinessLogicLayer.Dto.WalletDtos;
using System;

namespace BusinessLogicLayer.Dto.OperationDtos
{
    public class OperationDto
    {
        public int OperationId { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public decimal CurrentBalance { get; set; }

        public int WalletId { get; set; }
        public WalletDto Wallet { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}