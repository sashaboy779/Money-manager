using AutoMapper;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.UserDtos;
using BusinessLogicLayer.Dto.WalletDtos;
using MoneyManagerApi.Models;
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

    }
}
