using AutoMapper;
using MoneyManagerUi.Data.User;
using MoneyManagerUi.Data.Category;
using MoneyManagerUi.Data.Opration;
using MoneyManagerUi.Data.Wallet;

namespace MoneyManagerUi.Infrastructure.Profiles
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Wallet, Wallet>();
            CreateMap<WalletOperation, Operation>();
            CreateMap<WalletOperation, UpdateOperation>()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(c => c.Category.CategoryId));
                
            CreateMap<Category, Category>();

            CreateMap<RegisterUserModel, AuthenticateModel>();
        }
    }
}
