using System;
using System.Collections.Generic;
using BusinessLogicLayer.Dto.OperationDtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayerTest.Fixtures
{
    public class OperationFixture
    {
        public static IEnumerable<object[]> GetOperationsForException()
        {
            yield return new object[]
            {
                new OperationDto
                {
                    WalletId = 1,
                    Amount = 50,
                    CategoryId = 1
                }
            };

            yield return new object[]
            {
                new OperationDto
                {
                    Amount = 50,
                    CategoryId = 1
                }
            };
        }
        
        public User User { get; set; } = new User
        {
            UserId = 1,
            Wallets = new[] {new Wallet {WalletId = 1, Balance = 200, UserId = 1}},
            Categories = new []{new MainCategory {CategoryId = 1}}
        };

        public OperationDto ExpanseOperationDto { get; set; } = new OperationDto
        {
            WalletId = 1,
            Amount = -50,
            OperationDate = new DateTime(2020, 1, 2),
            CategoryId = 1,
            Note = "Operation note"
        };
        
        public Operation ExpanseOperation { get; set; } = new Operation
        {
            WalletId = 1,
            Amount = -50,
            OperationDate = new DateTime(2020, 1, 2),
            CategoryId = 1,
            Note = "Operation note"
        };

        public OperationDto IncomeOperationDto { get; set; } = new OperationDto
        {
            WalletId = 1,
            Amount = 50,
            CategoryId = 1,
            Note = "Operation note"
        };
        
        public Operation IncomeOperation { get; set; } = new Operation
        {
            WalletId = 1,
            Amount = 50,
            CategoryId = 1,
            Note = "Operation note"
        };
        
        public OperationDto OperationNoDateDto { get; set; } = new OperationDto
        {
            WalletId = 1,
            Amount = -50,
            CategoryId = 1,
            Note = "Operation note"
        };
        
        public Operation OperationNoDate { get; set; } = new Operation
        {
            WalletId = 1,
            Amount = -50,
            CategoryId = 1,
            Note = "Operation note"
        };

        public User UserWithoutWallets { get; set; } = new User
        {
            UserId = 2,
            Wallets = new List<Wallet>()
        };

        public User UserWithManyWallets { get; set; } = new User
        {
            UserId = 3,
            Wallets = new List<Wallet>
            {
                new Wallet
                {
                    WalletId = 1, Balance = 200, UserId = 1
                },
                new Wallet
                {
                    WalletId = 2, Balance = 200, UserId = 1
                }
            }
        };
        
        public OperationDto OperationNoWalletIdDto { get; set; } = new OperationDto
        {
            Amount = -50,
            CategoryId = 1
        };
        
        public Operation OperationNoWalletId { get; set; } = new Operation
        {
            Amount = -50,
            CategoryId = 1
        };

        public Operation OperationInvalidWalletId { get; set; } = new Operation
        {
            WalletId = 500,
            Amount = -50,
            CategoryId = 1
        };

        public OperationDto OperationInvalidWalletIdDto { get; set; } = new OperationDto
        {
            WalletId = 500,
            Amount = -50,
            CategoryId = 1
        };
    }
}