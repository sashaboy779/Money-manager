using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Wallet> WalletRepository { get; }
        IGenericRepository<Operation> OperationRepository { get; }
        IGenericRepository<MainCategory> CategoryRepository { get; }
        Task CommitAsync();
    }
}
