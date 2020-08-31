using BusinessLogicLayer.Dto.OperationDtos;
using BusinessLogicLayer.Dto.UserDtos;
using System.Collections.Generic;

namespace BusinessLogicLayer.Dto.WalletDtos
{
    public class WalletDto
    {
        public int WalletId { get; set; }
        public string Name { get; set; }
        public CurrencyDto Currency { get; set; }
        public decimal Balance { get; set; }

        public IEnumerable<OperationDto> Operations { get; set; }

        public int UserId { get; set; }
        public UserDto Owner { get; set; }
    }
}
