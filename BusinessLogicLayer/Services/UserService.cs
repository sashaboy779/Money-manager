using AutoMapper;
using BusinessLogicLayer.Dto.UserDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : Service, IUserService
    {
        private readonly IPasswordService passwordService;

        public UserService(IUnitOfWork unitOfWork, IPasswordService passwordService, IMapper mapper)
            : base(unitOfWork, mapper)
        {
            this.passwordService = passwordService;
        }

        public async Task<UserDto> AuthenticateAsync(string username, string password)
        {
            var user = await UnitOfWork.UserRepository
                .SingleOrDefaultAsync(x => x.Username == username);

            if (user == null || !passwordService
                .VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return Mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await UnitOfWork.UserRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await UnitOfWork.UserRepository.GetAsync(id);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserDto user, string password)
        {
            if (await UnitOfWork.UserRepository
                .SingleOrDefaultAsync(x => x.Username == user.Username) != null)
            {
                throw new UserException(String.Format(ServiceMessages.UsernameTaken, user.Username));
            }

            SetPasswordHashAndSalt(user, password);
            var userToCreate = Mapper.Map<User>(user);
            UnitOfWork.UserRepository.Create(userToCreate);
            await UnitOfWork.CommitAsync();

            return Mapper.Map<UserDto>(userToCreate);
        }

        public async Task UpdateAsync(UserDto userToUpdate, string password)
        {
            var userWithSameUsername = await UnitOfWork.UserRepository.SingleOrDefaultAsync(x => 
                x.UserId != userToUpdate.UserId && x.Username == userToUpdate.Username);

            if (userWithSameUsername != null)
            {
                throw new UserException(String
                    .Format(ServiceMessages.UsernameTaken, userToUpdate.Username));
            }
            
            if (!string.IsNullOrWhiteSpace(password))
            {
                SetPasswordHashAndSalt(userToUpdate, password);
            }

            UnitOfWork.UserRepository.Update(Mapper.Map<User>(userToUpdate));
            await UnitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await UnitOfWork.UserRepository.GetAsync(id);

            if (user != null)
            {
                UnitOfWork.UserRepository.Remove(user);
                await UnitOfWork.CommitAsync();
            }
        }

        private void SetPasswordHashAndSalt(UserDto user, string password)
        {
            var passwordModel = passwordService.CreatePasswordHash(password);

            user.PasswordHash = passwordModel.PasswordHash;
            user.PasswordSalt = passwordModel.PasswordSalt;
        }
    }
}
