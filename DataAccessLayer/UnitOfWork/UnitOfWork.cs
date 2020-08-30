using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Wallet> WalletRepository { get; }
        public IGenericRepository<Operation> OperationRepository { get; }
        public IGenericRepository<MainCategory> CategoryRepository { get; }

        private readonly DataContext context;

        public UnitOfWork(DataContext context,
            IGenericRepository<User> userRepository,
            IGenericRepository<Wallet> walletRepository,
            IGenericRepository<Operation> operationRepository,
            IGenericRepository<MainCategory> categoryRepository)
        {
            this.context = context;

            UserRepository = userRepository;
            WalletRepository = walletRepository;
            OperationRepository = operationRepository;
            CategoryRepository = categoryRepository;
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
