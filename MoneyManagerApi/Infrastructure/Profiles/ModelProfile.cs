using AutoMapper;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.CategoryDtos;
using BusinessLogicLayer.Dto.OperationDtos;
using BusinessLogicLayer.Dto.UserDtos;
using BusinessLogicLayer.Dto.WalletDtos;
using MoneyManagerApi.Models;
using MoneyManagerApi.Models.CategoryModels;
using MoneyManagerApi.Models.OperationModels;
using MoneyManagerApi.Models.Paging;
using MoneyManagerApi.Models.UserModels;
using MoneyManagerApi.Models.WalletModels;

namespace MoneyManagerApi.Infrastructure.Profiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<ErrorsDto, Errors>();

            CreateUserMap();
            CreateWalletMap();
            CreateCategoryMap();
            CreateOperationMap();
        }

        private void CreateUserMap()
        {
            CreateMap<UserDto, User>();
            CreateMap<RegisterUserModel, UserDto>();
            CreateMap<UpdateUserModel, UserDto>().ReverseMap();
        }

        private void CreateWalletMap()
        {
            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, CreateWalletModel>().ReverseMap();
            CreateMap<WalletDto, CreateWalletModel>().ReverseMap();
            CreateMap<Currency, CurrencyDto>().ReverseMap();

            CreateMap<CreateWalletModel, WalletDto>().ReverseMap();
            CreateMap<UpdateWalletModel, UpdateWalletDto>().ReverseMap();
        }

        private void CreateCategoryMap()
        {
            CreateMap<CreateSubcategoryModel, SubcategoryDto>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ReverseMap()
                .IncludeAllDerived();

            CreateMap<MainCategory, MainCategoryDto>().ReverseMap();
            CreateMap<Subcategory, SubcategoryDto>().ReverseMap();
            CreateMap<CreateCategoryModel, MainCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryModel, UpdateCategoryDto>().ReverseMap();
        }

        private void CreateOperationMap()
        {
            CreateMap<Operation, OperationDto>().ReverseMap();
            CreateMap<OperationDto, WalletOperationModel>();
            CreateMap<CreateOperationModel, OperationDto>().ReverseMap();
            CreateMap<UpdateOperationModel, UpdateOperationDto>().ReverseMap();
        }
    }
}
