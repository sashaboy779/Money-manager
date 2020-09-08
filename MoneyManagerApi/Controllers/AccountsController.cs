using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using MoneyManagerApi.Models.UserModels;
using BusinessLogicLayer.Dto.UserDtos;
using MoneyManagerApi.Resources;
using MoneyManagerApi.Infrastructure.Constants;
using MoneyManagerApi.Models;

namespace MoneyManagerApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.Controller)]
    public class AccountsController : BaseController
    {
        private readonly IUserService userService;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IPasswordValidator passwordValidator;

        public AccountsController(IUserService userService, IMapper mapper, IJwtTokenService jwtTokenService,
            IPasswordValidator passwordValidator, IUriService uriService) : base (mapper, uriService)
        {
            this.userService = userService;
            this.jwtTokenService = jwtTokenService;
            this.passwordValidator = passwordValidator;
        }

        [HttpGet]
        public async Task<IActionResult> ShowAsync()
        {
            var userId = GetUserId();
            var user = await userService.GetByIdAsync(userId);
            var userModel = Mapper.Map<User>(user);

            return Ok(userModel);
        }

        [AllowAnonymous]
        [HttpPost(Routes.SignIn)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateModel model)
        {
            var user = await userService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new Errors(ControllerMessage.AuthenticateError));
            }

            var tokenString = jwtTokenService.GenerateToken(user.UserId.ToString());
            return Ok(new { Token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost(Routes.Register)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserModel model)
        {
            var user = Mapper.Map<UserDto>(model);

            try
            {
                var errors = Mapper.Map<Errors>(passwordValidator.Validate(model.Password));

                if (!errors.Any())
                {
                    var createdUser = await userService.CreateAsync(user, model.Password);
                    var userModel = Mapper.Map<User>(createdUser);
                    return Created(String.Empty, userModel);
                }

                return BadRequest(errors);

            }
            catch (UserException ex)
            {
                return BadRequest(new Errors(ex.Message));
            }
        }
    }
}
