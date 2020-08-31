using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Resources;

namespace BusinessLogicLayer.Services.Interfaces
{
    public abstract class Service
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        protected Service(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        protected async Task<User> GetUser(int userId)
        {
            var user = await UnitOfWork.UserRepository.GetAsync(userId);

            if (user == null)
            {
                throw new UserException(ServiceMessages.InvalidToken);
            }

            return user;
        }

        protected IEnumerable<Wallet> GetUserWallets(int userId)
        {
            return GetUser(userId).Result.Wallets;
        }
        
        protected IEnumerable<MainCategory> GetUserCategories(int userId)
        {
            return GetUser(userId).Result.Categories;
        }
        
        protected IEnumerable<TEntity> ConvertToPaged<TEntity>(IEnumerable<TEntity> data, PaginationFilter filter)
            where TEntity : class
        {
            return data
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize + 1)
                .ToList();
        }
        
        protected bool IsPagingSpecified(PaginationFilter paginationQuery)
        {
            if (paginationQuery != null)
            {
                return paginationQuery.PageNumber != 0 || paginationQuery.PageSize != 0;
            }

            return false;
        }
    }
}
