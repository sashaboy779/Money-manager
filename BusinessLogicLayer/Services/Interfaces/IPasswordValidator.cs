using BusinessLogicLayer.Dto;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IPasswordValidator
    {
        ErrorsDto Validate(string password);
    }
}