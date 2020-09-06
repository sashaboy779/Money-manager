using AutoMapper;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.CategoryDtos;
using BusinessLogicLayer.Dto.OperationDtos;
using BusinessLogicLayer.Dto.ReportDtos;
using BusinessLogicLayer.Dto.UserDtos;
using BusinessLogicLayer.Dto.WalletDtos;
using MoneyManagerApi.Models;
using MoneyManagerApi.Models.Paging;
using MoneyManagerApi.Models.UserModels;

namespace MoneyManagerApi.Infrastructure.Profiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<ErrorsDto, Errors>();

            CreateUserMap();
        }

        private void CreateUserMap()
        {
            CreateMap<UserDto, User>();
            CreateMap<RegisterUserModel, UserDto>();
            CreateMap<UpdateUserModel, UserDto>().ReverseMap();
        }  
    }
}
