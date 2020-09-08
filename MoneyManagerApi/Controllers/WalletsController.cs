using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.WalletDtos;
using BusinessLogicLayer.Services.Interfaces;
using MoneyManagerApi.Infrastructure.Constants;
using MoneyManagerApi.Models.Paging;
using MoneyManagerApi.Models.WalletModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoneyManagerApi.Controllers
{
    [Route(Routes.Controller)]
    [ApiController]
    [Authorize]
    public class WalletsController : BaseController
    {
        private readonly IWalletSevice walletService;

        public WalletsController(IWalletSevice walletService, IMapper mapper, IUriService uriService) 
            : base(mapper, uriService)
        {
            this.walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalletAsync(CreateWalletModel wallet)
        {
            var userId = GetUserId();
            var walletDto = Mapper.Map<WalletDto>(wallet);
            var createdWalletEntity = await walletService.CreateWalletAsync(userId, walletDto);
            var createdWallet = Mapper.Map<Wallet>(createdWalletEntity);

            return CreatedAtRoute( 
                Routes.ShowWallet,
                new { id = createdWallet.WalletId },
                createdWallet);
        }

        [HttpGet(Routes.Id, Name = Routes.ShowWallet)]
        public async Task<IActionResult> ShowWalletAsync(int id)
        {
            var walletDto = await walletService.GetWalletAsync(GetUserId(), id);

            if (walletDto == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Wallet>(walletDto));
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllWalletsAsync([FromQuery] PaginationQuery pagination)
        {
            var userId = GetUserId();
            var filter = Mapper.Map<PaginationFilter>(pagination);
            var walletsDto = await walletService.GetAllWalletsAsync(userId, filter);

            if (!walletsDto.Any())    
            {
                return NotFound();
            }

            var wallets = Mapper.Map<IEnumerable<Wallet>>(walletsDto); 
            return IsPagingSpecified(pagination) ? Ok(CreatePagedResponse(pagination, wallets)) : Ok(wallets);
        }

        [HttpPut(Routes.Id)]
        public async Task<IActionResult> UpdateWalletsAsync(int id, [FromBody] UpdateWalletModel wallet)
        {
            var userId = GetUserId();
            var updateWalletDto = Mapper.Map<UpdateWalletDto>(wallet);
            await walletService.UpdateWalletAsync(userId, id, updateWalletDto);

            return NoContent();
        }

        [HttpDelete(Routes.Id)]
        public async Task<IActionResult> DeleteWalletsAsync(int id)
        {
            await walletService.DeleteWalletAsync(GetUserId(), id);
            
            return NoContent();
        }
    }
}
