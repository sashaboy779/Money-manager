using AutoMapper;
using BusinessLogicLayer.Dto.CategoryDtos;
using BusinessLogicLayer.Dto.OperationDtos;
using BusinessLogicLayer.Dto.UserDtos;
using BusinessLogicLayer.Dto.WalletDtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Enums;

namespace BusinessLogicLayer.Profiles
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateUserMap();
            CreateWalletMap();
            CreateCategoryMap();
            CreateOperationMap();
        }

        private void CreateUserMap()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }

        private void CreateWalletMap()
        {
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, UpdateWalletDto>().ReverseMap();
        }

        private void CreateCategoryMap()
        {
            CreateMap<MainCategory, MainCategoryDto>().ReverseMap();
            CreateMap<Subcategory, SubcategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, MainCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

            CreateMap<Category, CategoryDto>().IncludeAllDerived();
            CreateMap<CategoryDto, Category>().IncludeAllDerived();
        }

        private void CreateOperationMap()
        {
            CreateMap<OperationDto, Operation>()
                .ForMember(x => x.OperationDate, transform => 
                    transform.AddTransform(action => action.ToUniversalTime()));
            CreateMap<Operation, OperationDto>()
                .ForMember(x => x.OperationDate, transform => 
                    transform.AddTransform(action => action.ToLocalTime()));
            CreateMap<Operation, UpdateOperationDto>().ReverseMap();
            CreateMap<Currency, CurrencyDto>().ReverseMap();
        }
    }
}
