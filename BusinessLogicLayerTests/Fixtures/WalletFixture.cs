using System.Collections.Generic;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.WalletDtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayerTest.Fixtures
{
    public class WalletServiceFixture
    {
        public User DefaultUser { get; set; } = new User
        {
            UserId = 1,
            Wallets = new List<Wallet>()
        };

        public WalletDto DefaultWalletDto { get; set; } = new WalletDto()
        {
            WalletId = 1,
            Name = "DefaultWallet",
            UserId = 1
        };
        
        public Wallet DefaultWallet { get; set; } = new Wallet
        {
            WalletId = 1,
            Name = "DefaultWallet",
            UserId = 1
        };

        public User UserWithDuplicateWallet { get; set; } = new User
        {
            UserId = 2,
            Wallets = new List<Wallet> {
                new Wallet
                {
                    WalletId = 2,
                    Name = "DefaultWallet"
                }
            }
        };

        public User UserWithWallets { get; set; } = new User
        {
            UserId = 3,
            Wallets = new List<Wallet> 
            {
                new Wallet
                {
                    WalletId = 1,
                    Name = "WalletName 1",
                    UserId = 3
                },
                new Wallet
                {
                    WalletId = 2,
                    Name = "WalletName 2",
                    UserId = 3
                },
                new Wallet
                {
                    WalletId = 3,
                    Name = "WalletName 3",
                    UserId = 3
                }
            }
        };
        
        public IEnumerable<WalletDto> UserWalletsDtos { get; set; } = new List<WalletDto>
        {
            new WalletDto
            {
                WalletId = 1,
                Name = "WalletName 1",
                UserId = 3
            },
            new WalletDto
            {
                WalletId = 2,
                Name = "WalletName 2",
                UserId = 3
            },
            new WalletDto
            {
                WalletId = 3,
                Name = "WalletName 3",
                UserId = 3
            }
        };

        public UpdateWalletDto UpdateWalletDto { get; set; } = new UpdateWalletDto
        {
            Name = "UpdateName",
            Currency = CurrencyDto.USD,
            Balance = 69
        };

        public Wallet NullWallet { get; set; } = null;
        public int IncorrectWalletId { get; set; } = 500;

        public PaginationFilter PaginationFilter { get; set; } = new PaginationFilter(1, 100);
    }
}